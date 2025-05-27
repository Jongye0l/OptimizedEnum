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
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public Array ValuesArray;
    public string[] NamesArray;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public abstract string GetString(object eEnum);
    public abstract string? GetName(object eEnum);
    public abstract bool IsDefined(object eEnum);
    public abstract object ParseObj(string str);
    public abstract object ParseObj(string str, bool ignoreCase);
    public abstract bool TryParse(string str, out object? eEnum);
    public abstract bool TryParse(string str, bool ignoreCase, out object? eEnum);
    public abstract bool HasAllFlag(object eEnum1, object eEnum2);
    public abstract bool HasAnyFlag(object eEnum1, object eEnum2);
}

abstract class EnumData<T> : EnumData where T : struct, Enum {
    public static EnumData<T> Instance;
    public static readonly DataType DataType;
    public static readonly EnumType EnumType;
    public static SortedNameDictionary<T> NameDictionary;
    public static readonly T AllFlags;
    public static readonly SortedIndexedDictionary<T> Dictionary;
    public static readonly bool HasZero;
    public static readonly bool LowNotUnsigned;

    static EnumData() {
#if NETSTANDARD1_0
        List<FieldInfo> fieldList = typeof(T).GetRuntimeFields().ToList();
        Type? fieldType = null;
        foreach(FieldInfo fieldInfo in fieldList) {
            if(fieldInfo.Name != "value__") continue;
            fieldType = fieldInfo.FieldType;
            fieldList.Remove(fieldInfo);
            break;
        }
        DataType = fieldType == typeof(sbyte) || fieldType == typeof(short) || fieldType == typeof(int) ? DataType.Int :
            fieldType == typeof(long) ? DataType.Long :
            fieldType == typeof(byte) || fieldType == typeof(ushort) || fieldType == typeof(uint) ? DataType.Unsigned :
            fieldType == typeof(ulong) ? DataType.UnsignedLong :
            fieldType == typeof(char) ? DataType.Char : throw new NotSupportedException($"Enum type {typeof(T)} is not supported.");
        FieldInfo[] fields = fieldList.ToArray();
#else
        DataType = Type.GetTypeCode(typeof(T)) switch {
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
        LowNotUnsigned = DataType == DataType.Unsigned && Utils.GetSize<T>() < 4;
        T allFlags = Utils.GetZero<T>();
        int count = fields.Length;
        SortedIndexedDictionary<T> dictionary = new(count);
        NameDictionary = new SortedNameDictionary<T>(count);
        for(int i = 0; i < count; i++) {
#pragma warning disable CS8605 // Unboxing a possibly null value.
            T value = (T) fields[i].GetValue(null);
#pragma warning restore CS8605 // Unboxing a possibly null value.
            string name = fields[i].Name;
            dictionary.Add(value, name);
            NameDictionary.Add(name, value);
            if(value.AsLong() == 0) HasZero = true;
            else if(value.BitCount() == 1) allFlags = allFlags.CombineFlags(value);
        }
        Dictionary = dictionary;
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
            EnumType = EnumType.Flag;
            return;
        }
        bool[] checkField = new bool[fields.Length];
        bool outOfRange = false;
        bool isSorted = true;
        int length = fields.Length;
        int realCount = 0;
        uint u;
        if(Utils.GetSize<T>() == 8) {
            List<ulong> overflowField8 = [];
            ulong lengthLong = (ulong) length;
            for(u = 0; u < length; u++) {
#pragma warning disable CS8605 // Unboxing a possibly null value.
                ulong longValue = ((T) fields[u].GetValue(null)).As<T, ulong>();
#pragma warning restore CS8605 // Unboxing a possibly null value.
                if(lengthLong <= longValue) {
                    outOfRange = true;
                    if(!overflowField8.Contains(longValue)) {
                        overflowField8.Add(longValue);
                        realCount++;
                    }
                } else {
                    if(!checkField[longValue]) realCount++;
                    checkField[longValue] = true;
                    if(u != longValue) isSorted = false;
                }
            }
        } else {
            List<uint> overflowField4 = [];
            for(u = 0; u < length; u++) {
#pragma warning disable CS8605 // Unboxing a possibly null value.
                uint intValue = ((T) fields[u].GetValue(null)).As<T, uint>();
#pragma warning restore CS8605 // Unboxing a possibly null value.
                if(length <= intValue) {
                    outOfRange = true;
                    if(!overflowField4.Contains(intValue)) {
                        overflowField4.Add(intValue);
                        realCount++;
                    }
                } else {
                    if(!checkField[intValue]) realCount++;
                    checkField[intValue] = true;
                    if(u != intValue) isSorted = false;
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
        EnumType = EnumType.Unsorted;
    }

    protected EnumData() {
        ValuesArray = Dictionary.Keys;
        NamesArray = Dictionary.Values;
        EnumDataDictionary.Add(typeof(T), this);
    }

    public override object ParseObj(string str) {
        return NameDictionary.GetValue(str);
    }
    
    public override object ParseObj(string str, bool ignoreCase) {
        return ignoreCase ? NameDictionary.GetValueIgnoreCase(str) : NameDictionary.GetValue(str);
    }
    
    public override bool TryParse(string str, out object? eEnum) {
        if(NameDictionary.TryGetValue(str, out T value)) {
            eEnum = value;
            return true;
        }
        eEnum = null;
        return false;
    }
    
    public override bool TryParse(string str, bool ignoreCase, out object? eEnum) {
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