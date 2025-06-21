using System;
using System.Collections.Generic;
using OptimizedEnum.Tool;

namespace OptimizedEnum;

class SortedIndexedDictionary<T>(T[] keys, string[] values, int count) where T : struct, Enum {
    public readonly T[] Keys = keys;
    public readonly string[] Values = values;
    public int Count = count;

    public SortedIndexedDictionary(uint count) : this(new T[count], new string[count], 0) {
    }

    public string? this[T key] {
        get {
            int left = 0;
            int right = Count - 1;
            while(left <= right) {
                int mid = (left + right) / 2;
                if(Keys[mid].Equal(key)) return Values[mid];
                if(Keys[mid].LessThan(key)) left = mid + 1;
                else right = mid - 1;
            }
            return null;
        }
    }
    public void Add(T key, string value, List<T>? duplicates = null) {
        int left = 0;
        int right = Count - 1;
        while(left <= right) {
            int mid = (left + right) / 2;
            if(Keys[mid].GreaterThan(key)) right = mid - 1;
            else if(duplicates != null && Keys[mid].Equal(key)) {
                if(!duplicates.Contains(key)) {
                    duplicates.Add(key);
                    Values[mid] = EnumData<T>.Dictionary[key]!;
                }
                return;
            } else left = mid + 1;
        }
        for(int i = Count; i > left; i--) {
            Keys[i] = Keys[i - 1];
            Values[i] = Values[i - 1];
        }
        Keys[left] = key;
        Values[left] = value;
        Count++;
    }
}