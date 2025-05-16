using System;
using System.Reflection;

namespace OptimizedEnum;

static class UnsortedEnumData<T> where T : struct, Enum {
    public static SortedIndexedDictionary<T> dictionary;

    public static void Setup(FieldInfo[] fields, int count) {
        dictionary = new SortedIndexedDictionary<T>(count);
        foreach(FieldInfo field in fields) dictionary.Add((T) field.GetValue(null), field.Name);
    }
}