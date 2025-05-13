using System;
using System.Reflection;

namespace OptimizedEnum;

class SortedEnumData<T> : EnumData<T> where T : struct, Enum {
    public string[] Names;

    public SortedEnumData(FieldInfo[] fields, bool isSorted) : base(fields) {
        Names = new string[fields.Length];
        if(isSorted) {
            int length = fields.Length;
            for(int i = 0; i < length; i++) Names[i] = fields[i].Name;
        } else
            foreach(FieldInfo field in fields)
                Names[((T) field.GetValue(null)).AsUnsignedInteger()] = field.Name;
    }

    public override string GetString(T eEnum) {
        NumCalc<T> numCalc = NumCalc<T>.Instance;
        return numCalc.IsPositive(eEnum) && numCalc.LessThan(eEnum, Names.Length) ? Names[eEnum.AsInteger()] : numCalc.GetString(eEnum);
    }

    public override string GetName(T eEnum) {
        NumCalc<T> numCalc = NumCalc<T>.Instance;
        return numCalc.IsPositive(eEnum) && numCalc.LessThan(eEnum, Names.Length) ? Names[eEnum.AsInteger()] : null;
    }
}