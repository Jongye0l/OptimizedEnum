using System;
using System.Linq;
using System.Reflection;

namespace OptimizedEnum;

abstract class EnumData<T> where T : struct, Enum {
    public static readonly EnumData<T> Instance = CreateInstance();
    public static readonly DataType dataType = GetDataType();
    public static readonly EnumType enumType;
    public static SortedNameDictionary<T> NameDictionary;
    public readonly T AllFlags;
    public readonly T[] Values;
    public readonly bool HasZero;

    static EnumData() {
        
    }

    public EnumData(FieldInfo[] fields) {
        T allFlags = 0.As<int, T>();
        int count = fields.Length;
        Values = new T[count];
        NameDictionary = new SortedNameDictionary<T>(count);
        for(int i = 0; i < count; i++) {
            T value = Values[i] = (T) fields[i].GetValue(null);
            string name = fields[i].Name;
            NameDictionary.Add(name, value);
            if(value.AsLong() == 0) HasZero = true;
            else allFlags = allFlags.CombineFlags(value);
        }
        AllFlags = allFlags;
    }

    private static DataType GetDataType() => Type.GetTypeCode(typeof(T)) switch {
        TypeCode.Char => DataType.Char,
        TypeCode.SByte or TypeCode.Int16 or TypeCode.Int32 => DataType.Int,
        TypeCode.Int64 => DataType.Long,
        TypeCode.Byte or TypeCode.UInt16 or TypeCode.UInt32 => DataType.Unsigned,
        TypeCode.UInt64 => DataType.UnsignedLong,
        _ => throw new NotSupportedException($"Enum type {typeof(T)} is not supported.")
    };

    private static EnumData<T> CreateInstance() {
        FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static);
        if(typeof(T).GetCustomAttribute(typeof(FlagsAttribute)) != null) return new FlagEnumData<T>(fields);
        bool[] checkField = new bool[fields.Length];
        bool outOfRange = false;
        bool isSorted = true;
        int length = fields.Length;
        if(ILUtils.GetSize<T>() == 8) {
            ulong lengthLong = (ulong) length;
            for(uint i = 0; i < length; i++) {
                ulong v = ((T) fields[i].GetValue(null)).As<T, ulong>();
                if(lengthLong <= v) {
                    outOfRange = true;
                    break;
                }
                checkField[v] = true;
                if(i != v) isSorted = false;
            }
        } else {
            for(uint i = 0; i < length; i++) {
                uint v = ((T) fields[i].GetValue(null)).As<T, uint>();
                if(length <= v) {
                    outOfRange = true;
                    break;
                }
                checkField[v] = true;
                if(i != v) isSorted = false;
            }
        }
        return !outOfRange && checkField.All(t => t) ? new SortedEnumData<T>(fields, isSorted) : new UnsortedEnumData<T>(fields);
    }

    public abstract string GetString(T eEnum);

    public abstract string GetName(T eEnum);
    
    public abstract bool IsDefined(T eEnum);

    public T Parse(string str) => NameDictionary.GetValue(str);

    public T Parse(string str, bool ignoreCase) => NameDictionary.GetValue(str, ignoreCase);

    public bool TryParse(string str, out T eEnum) => NameDictionary.TryGetValue(str, out eEnum);
    
    public bool TryParse(string str, bool ignoreCase, out T eEnum) => NameDictionary.TryGetValue(str, out eEnum, ignoreCase);
}