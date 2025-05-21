using System;
using System.Collections.Generic;
using OptimizedEnum.Tool;

namespace OptimizedEnum;

class SortedIndexedDictionary<T>(T[] keys, string[] values, int count) where T : struct, Enum {
    public readonly T[] keys = keys;
    public readonly string[] values = values;
    public int count = count;
    
    public SortedIndexedDictionary(int count) : this(new T[count], new string[count], 0) { }

    public string this[T key] {
        get {
            int left = 0;
            int right = count - 1;
            while(left <= right) {
                int mid = (left + right) / 2;
                if(keys[mid].Equal(key)) return values[mid];
                if(keys[mid].LessThan(key)) left = mid + 1;
                else right = mid - 1;
            }
            return null;
        }
    }

    public void Add(T key, string value) {
        int left = 0;
        int right = count - 1;
        while(left <= right) {
            int mid = (left + right) / 2;
            if(keys[mid].Equal(key)) return;
            if(keys[mid].LessThan(key)) left = mid + 1;
            else right = mid - 1;
        }
        for(int i = count; i > left; i--) {
            keys[i] = keys[i - 1];
            values[i] = values[i - 1];
        }
        keys[left] = key;
        values[left] = value;
        count++;
    }
}