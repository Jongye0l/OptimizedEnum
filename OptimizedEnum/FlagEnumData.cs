using System;
using System.Collections.Generic;
using System.Reflection;
#if NETSTANDARD1_0
using System.Linq;
#endif
using System.Text;
using OptimizedEnum.Tool;

namespace OptimizedEnum;

class FlagEnumData<T> : EnumData<T> where T : struct, Enum {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public static SortedIndexedDictionary<T>? RemoveFlagDictionary;
    public static string[] FlagEnums;
    public static string ZeroString;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    static FlagEnumData() {
        FieldInfo[] fields = Fields;
        string[] flagEnums;
        T allFlags = AllFlags;
        if(allFlags.AsLong() == 0) flagEnums = [];
        else if(allFlags.AsLong() == 1) flagEnums = new string[1];
        else flagEnums = new string[(int)
#if NETCOREAPP2_0 || NETCOREAPP2_1
                                    Utils.Log2(allFlags.AsFloatUnsigned())
#elif NETCOREAPP3_0 || NET5_0
                                    MathF.Log2(allFlags.AsFloatUnsigned())
#else
                                    Utils.Log2(allFlags.AsDoubleUnsigned())
#endif
                                    + 1];
        FlagEnums = flagEnums;
        int dictCount = fields.Length - allFlags.BitCount() - (HasZero ? 1 : 0);
        List<T>? duplicates = null;
        if(dictCount != 0) {
            RemoveFlagDictionary = new SortedIndexedDictionary<T>((uint) dictCount);
            duplicates = [];
        }
        foreach(FieldInfo field in fields) {
#pragma warning disable CS8605 // Unboxing a possibly null value.
            T value = (T) field.GetValue(null);
#pragma warning restore CS8605 // Unboxing a possibly null value.
            string name = field.Name;
            if(value.AsLong() == 0) ZeroString = name;
            else if(value.AsLong() == 1) flagEnums[0] = name;
            else {
                if(value.BitCount() == 1) flagEnums[(int) 
#if NETCOREAPP2_0 || NETCOREAPP2_1
                    Utils.Log2(value.AsFloatUnsigned())
#elif NETCOREAPP3_0 || NET5_0
                    MathF.Log2(value.AsFloatUnsigned())
#else
                    Utils.Log2(value.AsDoubleUnsigned())
#endif
                ] = name;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                else RemoveFlagDictionary.Add(value, name, duplicates);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }
        }
        ZeroString ??= "0";
    }

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    public static string GetStringDict(T eEnum) {
        int bitCount = eEnum.BitCount();
        if(bitCount <= 1) return GetStringNormal(eEnum);
        string str = RemoveFlagDictionary[eEnum];
        if(str != null) return str;
        StringBuilder sb = new();
        SortedFloatDictionary sortedList = new(bitCount);
        for(int i = RemoveFlagDictionary.Count - 1; i >= 0; i--) {
            T key = RemoveFlagDictionary.Keys[i];
            if(!eEnum.HasAllFlags(key)) continue;
            sortedList.Add(
#if NETCOREAPP2_0 || NETCOREAPP2_1
                Utils.Log2(key.AsFloatUnsigned())
#elif NETCOREAPP3_0 || NET5_0
                MathF.Log2(key.AsFloatUnsigned())
#else
                Utils.Log2(key.AsDoubleUnsigned())
#endif
                , RemoveFlagDictionary.Values[i]);
            eEnum = eEnum.RemoveFlags(key);
        }
        if(!AllFlags.HasAllFlags(eEnum)) return eEnum.GetNumberStringFast();
        int index = 0;
        if(eEnum.AsLong() != 0)
            foreach(int i in eEnum.GetBitLocations()) {
                while(sortedList.count > index && sortedList.Array[index].Key < i) sb.Append(sortedList.Array[index++].Value).Append(", ");
                sb.Append(FlagEnums[i]).Append(", ");
            }
        while(sortedList.count > index) sb.Append(sortedList.Array[index++].Value).Append(", ");
        sb.Length -= 2;
        return sb.ToString();
    }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

    public static string GetStringNormal(T eEnum) {
        if(eEnum.AsLong() == 0) return ZeroString;
        if(!AllFlags.HasAllFlags(eEnum)) return eEnum.GetNumberStringFast();
        if(eEnum.AsLong() == 1) return FlagEnums[0];
        if(eEnum.BitCount() == 1) return FlagEnums[(int) 
#if NETCOREAPP2_0 || NETCOREAPP2_1
                Utils.Log2(eEnum.AsFloatUnsigned())
#elif NETCOREAPP3_0 || NET5_0
                MathF.Log2(eEnum.AsFloatUnsigned())
#else
                Utils.Log2(eEnum.AsDoubleUnsigned())
#endif
        ];
        StringBuilder sb = new();
        foreach(int i in eEnum.GetBitLocations()) sb.Append(FlagEnums[i]).Append(", ");
        sb.Length -= 2;
        return sb.ToString();
    }

    public static bool IsDefined(T eEnum) {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        return eEnum.AsLong() == 0 ? HasZero : eEnum.BitCount() == 1 ? FlagEnums[(int)
#if NETCOREAPP2_0 || NETCOREAPP2_1
                                                                           Utils.Log2(eEnum.AsFloatUnsigned())
#elif NETCOREAPP3_0 || NET5_0
                                                                           MathF.Log2(eEnum.AsFloatUnsigned())
#else
                                                                           Utils.Log2(eEnum.AsDoubleUnsigned())
#endif
                                                                       ] != null : RemoveFlagDictionary?[eEnum] != null;
    }

    public static void SetEnumName(T eEnum, string name) {
        long lValue = eEnum.AsLong();
        if(lValue == 0) {
            ZeroString = name;
            return;
        }
        if(lValue == 1) {
            if(FlagEnums.Length < 1) FlagEnums = new string[1];
            FlagEnums[0] = name;
            return;
        }
        int i = eEnum.BitCount();
        if(i == 1) {
            int v = (int)
#if NETCOREAPP2_0 || NETCOREAPP2_1
                Utils.Log2(eEnum.AsFloatUnsigned());
#elif NETCOREAPP3_0 || NET5_0
                MathF.Log2(eEnum.AsFloatUnsigned());
#else
                Utils.Log2(eEnum.AsDoubleUnsigned());
#endif
            if(FlagEnums.Length <= v) {
                string[] flagEnums = new string[v + 1];
                Array.Copy(FlagEnums, flagEnums, FlagEnums.Length);
                FlagEnums = flagEnums;
            }
            FlagEnums[v] = name;
            return;
        }
        if(RemoveFlagDictionary == null) RemoveFlagDictionary = new SortedIndexedDictionary<T>([eEnum], [name], 1);
        else RemoveFlagDictionary.AddOrSet(eEnum, name, false);
    }

    public override string GetString(object eEnum) {
        return RemoveFlagDictionary == null ? GetStringNormal((T) eEnum) : GetStringDict((T) eEnum);
    }
    
    public override string GetString(object eEnum, string? format) {
        if(format == null || format.Length == 0) goto GetString;
        if(format.Length == 1) {
            switch(format[0]) {
                case 'G':
                case 'g':
                case 'F':
                case 'f':
                    goto GetString;
                case 'D':
                case 'd':
                    return ((T) eEnum).GetNumberStringFast();
                case 'X':
                case 'x':
                    return ((T) eEnum).GetHexString();
            }
        }
        throw new FormatException();
GetString:
        return RemoveFlagDictionary == null ? GetStringNormal((T) eEnum) : GetStringDict((T) eEnum);
    }

    public override string? GetName(object eEnum) {
        T value = (T) eEnum;
        return value.AsLong() == 0                                   ? ZeroString :
               !AllFlags.HasAllFlags(value) || value.BitCount() != 1 ? RemoveFlagDictionary?[value] :
               value.AsLong() == 1                                   ? FlagEnums[0] : FlagEnums[(int) 
#if NETCOREAPP2_0 || NETCOREAPP2_1
                                                                           Utils.Log2(value.AsFloatUnsigned())
#elif NETCOREAPP3_0 || NET5_0
                                                                           MathF.Log2(value.AsFloatUnsigned())
#else
                                                                           Utils.Log2(value.AsDoubleUnsigned())
#endif
                                                                       ];
    }

    public override bool IsDefined(object eEnum) => IsDefined((T) eEnum);
    public override void SetEnumName(object eEnum, string name) => SetEnumName((T) eEnum, name);
}