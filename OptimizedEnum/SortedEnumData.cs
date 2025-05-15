using System;
using System.Reflection;

namespace OptimizedEnum;

static class SortedEnumData<T> where T : struct, Enum {
    public static string[] Names;
    public static int Length;

    public static void Setup(FieldInfo[] fields, bool isSorted) {
        int length = Length = fields.Length;
        string[] names = Names = new string[length];
        if(isSorted) for(int i = 0; i < length; i++) names[i] = fields[i].Name;
        else foreach(FieldInfo field in fields) names[((T) field.GetValue(null)).AsInteger()] = field.Name;
    }
    
    public static bool IsDefined(T eEnum) => (uint) eEnum.AsInteger() < Length;
}