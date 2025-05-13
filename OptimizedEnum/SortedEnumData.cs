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
        } else {
            NumCalc<T> numCalc = NumCalc<T>.Instance;
            foreach(FieldInfo field in fields)
                numCalc.SetValue(Names, (T) field.GetValue(null), field.Name);
        }
    }

    public override string GetString(T eEnum) {
        NumCalc<T> numCalc = NumCalc<T>.Instance;
        return numCalc.LessThan(eEnum, Names.Length) ? numCalc.GetValue(Names, eEnum) : numCalc.GetString(eEnum);
    }

    public override string GetName(T eEnum) {
        NumCalc<T> numCalc = NumCalc<T>.Instance;
        return numCalc.LessThan(eEnum, Names.Length) ? numCalc.GetValue(Names, eEnum) : null;
    }
}