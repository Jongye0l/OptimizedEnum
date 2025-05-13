using System;
using System.Reflection;

namespace OptimizedEnum;

class UnsortedEnumData<T> : EnumData<T> where T : struct, Enum {
    public SortedIndexedDictionary<T> dictionary;

    public UnsortedEnumData(FieldInfo[] fields) : base(fields) {
        dictionary = new SortedIndexedDictionary<T>(fields.Length);
        foreach(FieldInfo field in fields) dictionary.Add((T) field.GetValue(null), field.Name);
    }

    public override string GetString(T eEnum) => dictionary[eEnum] ?? NumCalc.GetString(eEnum);

    public override string GetName(T eEnum) => dictionary[eEnum];
}