using System;
using System.Reflection;

namespace OptimizedEnum;

static class UnsortedEnumData<T> where T : struct, Enum {
    public static SortedIndexedDictionary<T> dictionary;

    public static void Setup(FieldInfo[] fields) {
        dictionary = new SortedIndexedDictionary<T>(fields.Length);
        foreach(FieldInfo field in fields) dictionary.Add((T) field.GetValue(null), field.Name);
    }
}