using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using OptimizedEnum.Tool;

namespace OptimizedEnum;

class FlagEnumData<T> : EnumData<T> where T : struct, Enum {
    public static SortedIndexedDictionary<T> dictionary;
    public static string[] FlagEnums;
    public static string zeroString;

    public static void Setup(FieldInfo[] fields) {
        string[] flagEnums;
        T allFlags = AllFlags;
        if(allFlags.AsLong() == 0) flagEnums = [];
        else if(allFlags.AsLong() == 1) flagEnums = new string[1];
        else flagEnums = new string[(int) Utils.Log2(allFlags.AsDoubleUnsigned()) + 1];
        FlagEnums = flagEnums;
        int dictCount = fields.Length - allFlags.BitCount() - (HasZero ? 1 : 0);
        if(dictCount != 0) dictionary = new SortedIndexedDictionary<T>(dictCount);
        foreach(FieldInfo field in fields) {
            T value = (T) field.GetValue(null);
            string name = field.Name;
            if(value.AsLong() == 0) zeroString ??= name;
            else if(value.AsLong() == 1) flagEnums[0] ??= name;
            else {
                if(value.BitCount() == 1) flagEnums[(int) Utils.Log2(value.AsDoubleUnsigned())] ??= name;
                else dictionary.Add(value, name);
            }
        }
        zeroString ??= "0";
    }

    public static string GetStringDict(T eEnum) {
        int bitCount = eEnum.BitCount();
        if(bitCount <= 1) return GetStringNormal(eEnum);
        string str = dictionary[eEnum];
        if(str != null) return str;
        StringBuilder sb = new();
        SortedDoubleDictionary sortedList = new(bitCount);
        for(int i = dictionary.array.Length - 1; i >= 0; i--) {
            KeyValuePair<T, string> valuePair = dictionary.array[i];
            if(!eEnum.HasAllFlags(valuePair.Key)) continue;
            sortedList.Add(Utils.Log2(valuePair.Key.AsDoubleUnsigned()), valuePair.Value);
            eEnum = eEnum.RemoveFlags(valuePair.Key);
        }
        if(!AllFlags.HasAllFlags(eEnum)) return eEnum.GetNumberStringFast();
        int index = 0;
        if(eEnum.AsLong() != 0)
            foreach(int i in eEnum.GetBitLocations()) {
                while(sortedList.count > index && sortedList.array[index].Key < i) sb.Append(sortedList.array[index++].Value).Append(", ");
                sb.Append(FlagEnums[i]).Append(", ");
            }
        while(sortedList.count > index) sb.Append(sortedList.array[index++].Value).Append(", ");
        sb.Length -= 2;
        return sb.ToString();
    }

    public static string GetStringNormal(T eEnum) {
        if(eEnum.AsLong() == 0) return zeroString;
        if(!AllFlags.HasAllFlags(eEnum)) return eEnum.GetNumberStringFast();
        if(eEnum.AsLong() == 1) return FlagEnums[0];
        if(eEnum.BitCount() == 1) return FlagEnums[(int) Utils.Log2(eEnum.AsDoubleUnsigned())];
        StringBuilder sb = new();
        foreach(int i in eEnum.GetBitLocations()) sb.Append(FlagEnums[i]).Append(", ");
        sb.Length -= 2;
        return sb.ToString();
    }

    public static bool IsDefined(T eEnum) {
        return eEnum.AsLong() == 0 ? HasZero : eEnum.BitCount() == 1 ? FlagEnums[(int) Utils.Log2(eEnum.AsDoubleUnsigned())] != null : dictionary?[eEnum] != null;
    }

    public override string GetString(object eEnum) {
        return dictionary == null ? GetStringNormal((T) eEnum) : GetStringDict((T) eEnum);
    }
    
    public override string GetName(object eEnum) {
        T value = (T) eEnum;
        return dictionary != null && value.BitCount() > 1            ? dictionary[value] ?? value.GetNumberStringFast() :
               value.AsLong() == 0                                   ? zeroString :
               !AllFlags.HasAllFlags(value) || value.BitCount() != 1 ? value.GetNumberStringFast() :
               value.AsLong() == 1                                   ? FlagEnums[0] : FlagEnums[(int) Utils.Log2(value.AsDoubleUnsigned())];

    }

    public override bool IsDefined(object eEnum) => IsDefined((T) eEnum);
}