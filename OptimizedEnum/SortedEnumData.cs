using System;
using System.Reflection;
using OptimizedEnum.Tool;

namespace OptimizedEnum;

class SortedEnumData<T> : EnumData<T> where T : struct, Enum {
    public static string[] Names;
    public static int Length;

    public static void Setup(FieldInfo[] fields, bool isSorted, int count) {
        Length = count;
        string[] names = Names = new string[count];
        if(isSorted)
            for(int i = 0; i < count; i++)
                names[i] = fields[i].Name;
        else
            foreach(FieldInfo field in fields) 
                names[((T) field.GetValue(null)).AsInteger()] ??= field.Name;
    }

    public override string GetString(object eEnum) {
        return ILUtils.GetOrDefault(Names, (T) eEnum, Length);
    }
    
    public override string GetName(object eEnum) {
        return ILUtils.GetOrNull(Names, (T) eEnum, Length);
    }
    
    public override bool IsDefined(object eEnum) {
        return (uint) ((T) eEnum).AsInteger() < Length;
    }
}