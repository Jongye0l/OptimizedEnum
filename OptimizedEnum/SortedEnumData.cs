using System;
using System.Reflection;

namespace OptimizedEnum;

class SortedEnumData<T> : EnumData<T> where T : struct, Enum {
    public string[] Names;
    public int Length;

    public SortedEnumData(FieldInfo[] fields, bool isSorted) : base(fields) {
        int length = Length = fields.Length;
        string[] names = Names = new string[length];
        if(isSorted) for(int i = 0; i < length; i++) names[i] = fields[i].Name;
        else {
            NumCalc<T> numCalc = NumCalc;
            foreach(FieldInfo field in fields) numCalc.SetValue(names, (T) field.GetValue(null), field.Name);
        }
    }


    public override bool IsDefined(T eEnum) => (uint) NumCalc.ToInt(eEnum) < Length;
    public override string GetString(T eEnum) => NumCalc.GetOrDefault(Names, eEnum, Length);
    public override string GetName(T eEnum) => NumCalc.GetOrNull(Names, eEnum, Length);
}