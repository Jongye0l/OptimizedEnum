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
        NumCalc<T> numCalc = NumCalc<T>.Instance;
        if(!numCalc.GetBool(AllFlags)) FlagEnums = [];
        else if(numCalc.Equal(AllFlags, 1)) FlagEnums = new string[1];
        else FlagEnums = new string[numCalc.BitCount(AllFlags)];
        SetupDict(fields.Length - FlagEnums.Length);
        foreach(FieldInfo field in fields) {
            T value = (T) field.GetValue(null);
            string name = field.Name;
            if(!numCalc.GetBool(value)) zeroString = name;
            else if(numCalc.Equal(value, 1)) FlagEnums[0] = name;
            else {
                double logValue = Utils.Log2(value.AsDouble());
                if(logValue % 1 == 0) FlagEnums[(int) logValue] = name;
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
        NumCalc<T> numCalc = NumCalc<T>.Instance;
        if(numCalc.BitCount(eEnum) <= 1) return GetStringNormal(eEnum);
        string str = dictionary[eEnum];
        if(str != null) return str;
        StringBuilder sb = new();
        foreach(KeyValuePair<T, string> valuePair in dictionary) {
            if(!eEnum.HasAllFlags(valuePair.Key)) continue;
            sb.Append(valuePair.Value).Append(", ");
            eEnum = eEnum.RemoveFlags(valuePair.Key);
        }
        if(!AllFlags.HasAllFlags(eEnum)) return numCalc.GetString(eEnum);
        if(numCalc.GetBool(eEnum))
            foreach(int i in numCalc.GetBitLocations(eEnum))
                sb.Append(FlagEnums[i]).Append(", ");
        sb.Length -= 2;
        return sb.ToString();
    }

    public string GetStringNormal(T eEnum) {
        NumCalc<T> numCalc = NumCalc<T>.Instance;
        if(!numCalc.GetBool(eEnum)) return zeroString;
        if(!AllFlags.HasAllFlags(eEnum)) return numCalc.GetString(eEnum);
        if(numCalc.Equal(eEnum, 1)) return FlagEnums[0];
        double logValue = Utils.Log2(eEnum.AsDouble());
        if(logValue % 1 == 0) return FlagEnums[(int) logValue];
        StringBuilder sb = new();
        foreach(int i in numCalc.GetBitLocations(eEnum)) sb.Append(FlagEnums[i]).Append(", ");
        sb.Length -= 2;
        return sb.ToString();
    }

    public override string GetName(T eEnum) {
        return dictionary == null ? GetNameNormal(eEnum) : GetNameDict(eEnum);
    }

    public string GetNameDict(T eEnum) {
        return NumCalc<T>.Instance.BitCount(eEnum) > 1 ? dictionary[eEnum] ?? NumCalc<T>.Instance.GetString(eEnum) : GetNameNormal(eEnum);
    }

    public string GetNameNormal(T eEnum) {
        NumCalc<T> numCalc = NumCalc<T>.Instance;
        return !numCalc.GetBool(eEnum)                                      ? zeroString :
               !AllFlags.HasAllFlags(eEnum) || numCalc.BitCount(eEnum) != 1 ? numCalc.GetString(eEnum) :
               numCalc.Equal(eEnum, 1)                                      ? FlagEnums[0] : FlagEnums[(int) Utils.Log2(eEnum.AsDouble())];
    }
}