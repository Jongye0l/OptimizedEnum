using System;
using System.Collections.Generic;
#if NETSTANDARD1_0 || NETSTANDARD1_5
using System.Linq;
#endif
using System.Reflection;
using OptimizedEnum.Tool;

namespace OptimizedEnum;

abstract class EnumData {
    public static SortedEnumDictionary EnumDataDictionary = new();
    public Array ValuesArray;
    public string[] NamesArray;
    public abstract string GetString(object eEnum);
    public abstract string? GetName(object eEnum);
    public abstract bool IsDefined(object eEnum);
    public abstract object ParseObj(string str);
    public abstract object ParseObj(string str, bool ignoreCase);
    public abstract bool TryParse(string str, out object eEnum);
    public abstract bool TryParse(string str, bool ignoreCase, out object eEnum);
    public abstract bool HasAllFlag(object eEnum1, object eEnum2);
    public abstract bool HasAnyFlag(object eEnum1, object eEnum2);
}

abstract class EnumData<T> : EnumData where T : struct, Enum {
    public static EnumData<T> Instance;
    public static readonly DataType dataType;
    public static readonly EnumType enumType;
    public static SortedNameDictionary<T> NameDictionary;
    public static readonly T AllFlags;
    public static readonly string[] Names;
    public static readonly T[] Values;
    public static readonly bool HasZero;
    public static readonly bool LowNotUnsigned;

    static EnumData() {
#if NETSTANDARD1_0
        List<FieldInfo> fieldList = typeof(T).GetRuntimeFields().ToList();
        Type fieldType = null;
        foreach(FieldInfo fieldInfo in fieldList) {
            if(fieldInfo.Name != "value__") continue;
            fieldType = fieldInfo.FieldType;
            fieldList.Remove(fieldInfo);
            break;
        }
        dataType = fieldType == typeof(sbyte) || fieldType == typeof(short) || fieldType == typeof(int) ? DataType.Int :
            fieldType == typeof(long) ? DataType.Long :
            fieldType == typeof(byte) || fieldType == typeof(ushort) || fieldType == typeof(uint) ? DataType.Unsigned :
            fieldType == typeof(ulong) ? DataType.UnsignedLong :
            fieldType == typeof(char) ? DataType.Char : throw new NotSupportedException($"Enum type {typeof(T)} is not supported.");
        FieldInfo[] fields = fieldList.ToArray();
#else
        dataType = Type.GetTypeCode(typeof(T)) switch {
            TypeCode.Char => DataType.Char,
            TypeCode.SByte or TypeCode.Int16 or TypeCode.Int32 => DataType.Int,
            TypeCode.Int64 => DataType.Long,
            TypeCode.Byte or TypeCode.UInt16 or TypeCode.UInt32 => DataType.Unsigned,
            TypeCode.UInt64 => DataType.UnsignedLong,
            _ => throw new NotSupportedException($"Enum type {typeof(T)} is not supported.")
        };
        FieldInfo[] fields =
#if NETSTANDARD1_5
            typeof(T).GetTypeInfo().GetFields(BindingFlags.Public | BindingFlags.Static);
#else
            typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static);
#endif
#endif
        LowNotUnsigned = dataType == DataType.Unsigned && Utils.GetSize<T>() < 4;
        T allFlags = Utils.GetZero<T>();
        int count = fields.Length;
        SortedIndexedDictionary<T> dictionary = new(count);
        NameDictionary = new SortedNameDictionary<T>(count);
        for(int i = 0; i < count; i++) {
            T value = (T) fields[i].GetValue(null);
            string name = fields[i].Name;
            dictionary.Add(value, name);
            NameDictionary.Add(name, value);
            if(value.AsLong() == 0) HasZero = true;
            else if(value.BitCount() == 1) allFlags = allFlags.CombineFlags(value);
        }
        Values = dictionary.keys;
        Names = dictionary.values;
        AllFlags = allFlags;
        if(
#if NETCOREAPP1_0
            ((ICustomAttributeProvider) typeof(T)).GetCustomAttributes(typeof(FlagsAttribute), true)
#elif NETSTANDARD1_0 || NETSTANDARD1_5
            typeof(T).GetTypeInfo().GetCustomAttributes(typeof(FlagsAttribute)).ToArray()
#else
            typeof(T).GetCustomAttributes(typeof(FlagsAttribute), false)
#endif
               .Length > 0) {
            FlagEnumData<T>.Setup(fields);
            Instance = new FlagEnumData<T>();
            enumType = EnumType.Flag;
            return;
        }
        bool[] checkField = new bool[fields.Length];
        bool outOfRange = false;
        bool isSorted = true;
        int length = fields.Length;
        int realCount = 0;
        if(Utils.GetSize<T>() == 8) {
            List<ulong> overflowField = [];
            ulong lengthLong = (ulong) length;
            for(uint i = 0; i < length; i++) {
                ulong v = ((T) fields[i].GetValue(null)).As<T, ulong>();
                if(lengthLong <= v) {
                    outOfRange = true;
                    if(!overflowField.Contains(v)) {
                        overflowField.Add(v);
                        realCount++;
                    }
                } else {
                    if(!checkField[v]) realCount++;
                    checkField[v] = true;
                    if(i != v) isSorted = false;
                }
            }
        } else {
            List<uint> overflowField = [];
            for(uint i = 0; i < length; i++) {
                uint v = ((T) fields[i].GetValue(null)).As<T, uint>();
                if(length <= v) {
                    outOfRange = true;
                    if(!overflowField.Contains(v)) {
                        overflowField.Add(v);
                        realCount++;
                    }
                } else {
                    if(!checkField[v]) realCount++;
                    checkField[v] = true;
                    if(i != v) isSorted = false;
                }
            }
        }
        uint cur = 0;
        if(!outOfRange) {
            if(!isSorted) foreach(bool b in checkField) {
                switch(cur) {
                    case 0:
                        if(!b) cur++;
                        break;
                    case 1:
                        if(b) goto SortedSkip;
                        break;
                }
            }
            SortedEnumData<T>.Setup(fields, isSorted, realCount);
            Instance = new SortedEnumData<T>();
            return;
        }
SortedSkip:
        UnsortedEnumData<T>.Setup(fields, realCount);
        Instance = new UnsortedEnumData<T>();
        enumType = EnumType.Unsorted;
    }

    protected EnumData() {
        ValuesArray = Values;
        NamesArray = Names;
        EnumDataDictionary.Add(typeof(T), this);
    }

    public override object ParseObj(string str) {
        return NameDictionary.GetValue(str);
    }
    
    public override object ParseObj(string str, bool ignoreCase) {
        return ignoreCase ? NameDictionary.GetValueIgnoreCase(str) : NameDictionary.GetValue(str);
    }
    
    public override bool TryParse(string str, out object eEnum) {
        if(NameDictionary.TryGetValue(str, out T value)) {
            eEnum = value;
            return true;
        }
        eEnum = null;
        return false;
    }
    
    public override bool TryParse(string str, bool ignoreCase, out object eEnum) {
        if(ignoreCase) {
            if(NameDictionary.TryGetValueIgnoreCase(str, out T value)) {
                eEnum = value;
                return true;
            }
        } else if(NameDictionary.TryGetValue(str, out T value)) {
            eEnum = value;
            return true;
        }
        eEnum = null;
        return false;
    }
    
    public override bool HasAllFlag(object eEnum1, object eEnum2) {
        return ((T) eEnum1).HasAllFlags((T) eEnum2);
    }
    
    public override bool HasAnyFlag(object eEnum1, object eEnum2) {
        return ((T) eEnum1).HasAnyFlags((T) eEnum2);
    }
}