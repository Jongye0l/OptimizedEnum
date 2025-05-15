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
        else foreach(FieldInfo field in fields) names[((T) field.GetValue(null)).AsInteger()] = field.Name;
    }


    public override bool IsDefined(T eEnum) => (uint) eEnum.AsInteger() < Length;
    public override string GetString(T eEnum) => ILUtils.GetOrDefault(Names, eEnum, Length, dataType);
    public override string GetName(T eEnum) => ILUtils.GetOrNull(Names, eEnum, Length);
}