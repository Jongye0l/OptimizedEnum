using System;
using System.Reflection;
using OptimizedEnum.Tool;

namespace OptimizedEnum;

class UnsortedEnumData<T> : EnumData<T> where T : struct, Enum {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public static SortedIndexedDictionary<T> Dictionary;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public static void Setup(FieldInfo[] fields, int count) {
        if(count == fields.Length) Dictionary = new SortedIndexedDictionary<T>(Values, Names, count);
        else {
            Dictionary = new SortedIndexedDictionary<T>(count, false);
#pragma warning disable CS8605 // Unboxing a possibly null value.
            foreach(FieldInfo field in fields) Dictionary.Add((T) field.GetValue(null), field.Name);
#pragma warning restore CS8605 // Unboxing a possibly null value.
        }
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