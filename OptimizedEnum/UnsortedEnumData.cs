using System;
using System.Reflection;
using OptimizedEnum.Tool;

namespace OptimizedEnum;

class UnsortedEnumData<T> : EnumData<T> where T : struct, Enum {
    public static SortedIndexedDictionary<T> dictionary;

    public static void Setup(FieldInfo[] fields, int count) {
        dictionary = new SortedIndexedDictionary<T>(count);
        foreach(FieldInfo field in fields) dictionary.Add((T) field.GetValue(null), field.Name);
    }

    public override string GetString(object eEnum) {
        T value = (T) eEnum;
        return dictionary[value] ?? value.GetNumberString();
    }
    
    public override string GetName(object eEnum) {
        return dictionary[(T) eEnum];
    }
    
    public override bool IsDefined(object eEnum) {
        return dictionary[(T) eEnum] != null;
    }
}