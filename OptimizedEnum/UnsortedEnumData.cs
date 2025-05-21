using System;
using System.Reflection;
using OptimizedEnum.Tool;

namespace OptimizedEnum;

class UnsortedEnumData<T> : EnumData<T> where T : struct, Enum {
    public static SortedIndexedDictionary<T> Dictionary;

    public static void Setup(FieldInfo[] fields, int count) {
        Dictionary = count == fields.Length ? new SortedIndexedDictionary<T>(Values, Names, count) : new SortedIndexedDictionary<T>(count);
        foreach(FieldInfo field in fields) Dictionary.Add((T) field.GetValue(null), field.Name);
    }

    public override string GetString(object eEnum) {
        T value = (T) eEnum;
        return Dictionary[value] ?? value.GetNumberStringFast();
    }
    
    public override string? GetName(object eEnum) {
        return Dictionary[(T) eEnum];
    }
    
    public override bool IsDefined(object eEnum) {
        return Dictionary[(T) eEnum] != null;
    }
}