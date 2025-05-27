using System;
using System.Collections.Generic;
using System.Reflection;
using OptimizedEnum.Tool;

namespace OptimizedEnum;

class UnsortedEnumData<T> : EnumData<T> where T : struct, Enum {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public static SortedIndexedDictionary<T> NotDupDict;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public static void Setup(FieldInfo[] fields, int count) {
        if(count == fields.Length) NotDupDict = Dictionary;
        else {
            NotDupDict = new SortedIndexedDictionary<T>(count);
            List<T> duplicates = [];
#pragma warning disable CS8605 // Unboxing a possibly null value.
            foreach(FieldInfo field in fields) NotDupDict.Add((T) field.GetValue(null), field.Name, duplicates);
#pragma warning restore CS8605 // Unboxing a possibly null value.
        }
    }

    public override string GetString(object eEnum) {
        T value = (T) eEnum;
        return NotDupDict[value] ?? value.GetNumberStringFast();
    }
    
    public override string? GetName(object eEnum) {
        return NotDupDict[(T) eEnum];
    }
    
    public override bool IsDefined(object eEnum) {
        return NotDupDict[(T) eEnum] != null;
    }
}