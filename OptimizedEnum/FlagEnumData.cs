using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OptimizedEnum;

static class FlagEnumData<T> where T : struct, Enum {
    public static SortedIndexedDictionary<T> dictionary;
    public static string[] FlagEnums;
    public static string zeroString;

    public static void Setup(FieldInfo[] fields) {
        string[] flagEnums;
        T allFlags = EnumData<T>.AllFlags;
        if(allFlags.AsLong() == 0) flagEnums = [];
        else if(allFlags.AsLong() == 1) flagEnums = new string[1];
        else flagEnums = new string[(int) Utils.Log2(allFlags.AsDouble()) + 1];
        FlagEnums = flagEnums;
        int dictCount = fields.Length - ILUtils.BitCount(allFlags) - (EnumData<T>.HasZero ? 1 : 0);
        if(dictCount != 0) dictionary = new SortedIndexedDictionary<T>(dictCount);
        foreach(FieldInfo field in fields) {
            T value = (T) field.GetValue(null);
            string name = field.Name;
            if(value.AsLong() == 0) zeroString ??= name;
            else if(value.AsLong() == 1) flagEnums[0] ??= name;
            else {
                double logValue = Utils.Log2(value.AsDouble());
                if(logValue % 1 == 0) flagEnums[(int) logValue] ??= name;
                else dictionary.Add(value, name);
            }
        }
        zeroString ??= "0";
    }

    public static string GetStringDict(T eEnum) {
        int bitCount = ILUtils.BitCount(eEnum);
        if(bitCount <= 1) return GetStringNormal(eEnum);
        string str = dictionary[eEnum];
        if(str != null) return str;
        StringBuilder sb = new();
        SortedDoubleDictionary sortedList = new(bitCount);
        for(int i = dictionary.array.Length - 1; i >= 0; i--) {
            KeyValuePair<T, string> valuePair = dictionary.array[i];
            if(!eEnum.HasAllFlags(valuePair.Key)) continue;
            sortedList.Add(Utils.Log2(valuePair.Key.AsDouble()), valuePair.Value);
            eEnum = eEnum.RemoveFlags(valuePair.Key);
        }
        if(!EnumData<T>.AllFlags.HasAllFlags(eEnum)) return ILUtils.GetString(eEnum);
        int index = 0;
        if(eEnum.AsLong() != 0)
            foreach(int i in ILUtils.GetBitLocations(eEnum)) {
                while(sortedList.count > index && sortedList.array[index].Key < i) sb.Append(sortedList.array[index++].Value).Append(", ");
                sb.Append(FlagEnums[i]).Append(", ");
            }
        while(sortedList.count > index) sb.Append(sortedList.array[index++].Value).Append(", ");
        sb.Length -= 2;
        return sb.ToString();
    }

    public static string GetStringNormal(T eEnum) {
        if(eEnum.AsLong() == 0) return zeroString;
        if(!EnumData<T>.AllFlags.HasAllFlags(eEnum)) return ILUtils.GetString(eEnum);
        if(eEnum.AsLong() == 1) return FlagEnums[0];
        double logValue = Utils.Log2(eEnum.AsDouble());
        if(logValue % 1 == 0) return FlagEnums[(int) logValue];
        StringBuilder sb = new();
        foreach(int i in ILUtils.GetBitLocations(eEnum)) sb.Append(FlagEnums[i]).Append(", ");
        sb.Length -= 2;
        return sb.ToString();
    }

    public static bool IsDefined(T eEnum) {
        double logValue = Utils.Log2(eEnum.AsDouble());
        return logValue % 1 == 0 ? FlagEnums[(int) logValue] != null : dictionary?[eEnum] != null;
    }
}