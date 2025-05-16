using System;
using System.Linq;
using System.Reflection;

namespace OptimizedEnum;

static class EnumData<T> where T : struct, Enum {
    public static readonly DataType dataType;
    public static readonly EnumType enumType;
    public static SortedNameDictionary<T> NameDictionary;
    public static readonly T AllFlags;
    public static readonly T[] Values;
    public static readonly bool HasZero;

    static EnumData() {
        dataType = Type.GetTypeCode(typeof(T)) switch {
            TypeCode.Char => DataType.Char,
            TypeCode.SByte or TypeCode.Int16 or TypeCode.Int32 => DataType.Int,
            TypeCode.Int64 => DataType.Long,
            TypeCode.Byte or TypeCode.UInt16 or TypeCode.UInt32 => DataType.Unsigned,
            TypeCode.UInt64 => DataType.UnsignedLong,
            _ => throw new NotSupportedException($"Enum type {typeof(T)} is not supported.")
        };
        FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static);
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
        if(typeof(T).GetCustomAttribute(typeof(FlagsAttribute)) != null) {
            FlagEnumData<T>.Setup(fields);
            enumType = EnumType.Flag;
            return;
        }
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
        if(!outOfRange && checkField.All(t => t)) SortedEnumData<T>.Setup(fields, isSorted);
        else UnsortedEnumData<T>.Setup(fields);
    }

    public static string GetString(T eEnum) {
        return enumType switch {
            EnumType.Sorted => ILUtils.GetOrDefault(SortedEnumData<T>.Names, eEnum, SortedEnumData<T>.Length),
            EnumType.Unsorted => UnsortedEnumData<T>.dictionary[eEnum] ?? ILUtils.GetString(eEnum),
            EnumType.Flag => FlagEnumData<T>.dictionary == null ? FlagEnumData<T>.GetStringNormal(eEnum) : FlagEnumData<T>.GetStringDict(eEnum),
            _ => throw new NotSupportedException()
        };
    }

    public static string GetName(T eEnum) {
        return enumType switch {
            EnumType.Sorted => ILUtils.GetOrNull(SortedEnumData<T>.Names, eEnum, SortedEnumData<T>.Length),
            EnumType.Unsorted => UnsortedEnumData<T>.dictionary[eEnum],
            EnumType.Flag => FlagEnumData<T>.dictionary != null && ILUtils.BitCount(eEnum) > 1 ? FlagEnumData<T>.dictionary[eEnum] ?? ILUtils.GetString(eEnum) :
                             eEnum.AsLong() == 0                                               ? FlagEnumData<T>.zeroString :
                             !AllFlags.HasAllFlags(eEnum) || ILUtils.BitCount(eEnum) != 1      ? ILUtils.GetString(eEnum) :
                             eEnum.AsLong() == 1                                               ? FlagEnumData<T>.FlagEnums[0] : FlagEnumData<T>.FlagEnums[(int) Utils.Log2(eEnum.AsDouble())],
            _ => throw new NotSupportedException()
        };
    }

    public static bool IsDefined(T eEnum) {
        return enumType switch {
            EnumType.Sorted => (uint) eEnum.AsInteger() < SortedEnumData<T>.Length,
            EnumType.Unsorted => UnsortedEnumData<T>.dictionary[eEnum] != null,
            EnumType.Flag => FlagEnumData<T>.IsDefined(eEnum),
            _ => throw new NotSupportedException()
        };
    }

    public static T Parse(string str) {
        return NameDictionary.GetValue(str);
    }

    public static T Parse(string str, bool ignoreCase) {
        return ignoreCase ? NameDictionary.GetValueIgnoreCase(str) : NameDictionary.GetValue(str);
    }

    public static bool TryParse(string str, out T eEnum) {
        return NameDictionary.TryGetValue(str, out eEnum);
    }

    public static bool TryParse(string str, bool ignoreCase, out T eEnum) {
        return ignoreCase ? NameDictionary.TryGetValueIgnoreCase(str, out eEnum) : NameDictionary.TryGetValue(str, out eEnum);
    }
}