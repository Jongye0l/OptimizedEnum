using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OptimizedEnum;

class FlagEnumData<T> : EnumData<T> where T : struct, Enum {
    public SortedIndexedDictionary<T> dictionary;
    public string[] FlagEnums;
    public string zeroString;

    public FlagEnumData(FieldInfo[] fields) : base(fields) {
        string[] flagEnums;
        if(AllFlags.AsLong() == 0) flagEnums = [];
        else if(AllFlags.AsLong() == 1) flagEnums = new string[1];
        else flagEnums = new string[(int) Utils.Log2(AllFlags.AsDouble()) + 1];
        FlagEnums = flagEnums;
        SetupDict(fields.Length - ILUtils.BitCount(AllFlags) - (HasZero ? 1 : 0));
        foreach(FieldInfo field in fields) {
            T value = (T) field.GetValue(null);
            string name = field.Name;
            if(value.AsLong() == 0) zeroString = name;
            else if(value.AsLong() == 1) flagEnums[0] = name;
            else {
                double logValue = Utils.Log2(value.AsDouble());
                if(logValue % 1 == 0) flagEnums[(int) logValue] = name;
                else dictionary.Add(value, name);
            }
        }
        zeroString ??= "0";
    }

    protected void SetupDict(int count) {
        if(count != 0) dictionary = new SortedIndexedDictionary<T>(count);
    }

    public override string GetString(T eEnum) {
        return dictionary == null ? GetStringNormal(eEnum) : GetStringDict(eEnum);
    }

    public string GetStringDict(T eEnum) {
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
        if(!AllFlags.HasAllFlags(eEnum)) return ILUtils.GetString(eEnum, dataType);
        int index = 0;
        if(eEnum.AsLong() != 0) {
            foreach(int i in ILUtils.GetBitLocations(eEnum)) {
                while(sortedList.count > index && sortedList.array[index].Key < i) sb.Append(sortedList.array[index++].Value).Append(", ");
                sb.Append(FlagEnums[i]).Append(", ");
            }
        }
        while(sortedList.count > index) sb.Append(sortedList.array[index++].Value).Append(", ");
        sb.Length -= 2;
        return sb.ToString();
    }

    public string GetStringNormal(T eEnum) {
        if(eEnum.AsLong() == 0) return zeroString;
        if(!AllFlags.HasAllFlags(eEnum)) return ILUtils.GetString(eEnum, dataType);
        if(eEnum.AsLong() == 1) return FlagEnums[0];
        double logValue = Utils.Log2(eEnum.AsDouble());
        if(logValue % 1 == 0) return FlagEnums[(int) logValue];
        StringBuilder sb = new();
        foreach(int i in ILUtils.GetBitLocations(eEnum)) sb.Append(FlagEnums[i]).Append(", ");
        sb.Length -= 2;
        return sb.ToString();
    }

    public override string GetName(T eEnum) {
        return dictionary == null ? GetNameNormal(eEnum) : GetNameDict(eEnum);
    }

    public override bool IsDefined(T eEnum) {
        double logValue = Utils.Log2(eEnum.AsDouble());
        return logValue % 1 == 0 ? FlagEnums[(int) logValue] != null : dictionary?[eEnum] != null;
    }

    public string GetNameDict(T eEnum) {
        return ILUtils.BitCount(eEnum) > 1 ? dictionary[eEnum] ?? ILUtils.GetString(eEnum, dataType) : GetNameNormal(eEnum);
    }

    public string GetNameNormal(T eEnum) {
        return eEnum.AsLong() == 0                                          ? zeroString :
               !AllFlags.HasAllFlags(eEnum) || ILUtils.BitCount(eEnum) != 1 ? ILUtils.GetString(eEnum, dataType) :
               eEnum.AsLong() == 1                                          ? FlagEnums[0] : FlagEnums[(int) Utils.Log2(eEnum.AsDouble())];
    }
}