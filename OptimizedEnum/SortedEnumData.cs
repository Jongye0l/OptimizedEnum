using System;
using System.Reflection;

namespace OptimizedEnum;

static class SortedEnumData<T> where T : struct, Enum {
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
}