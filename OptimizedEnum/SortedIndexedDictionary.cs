using System;
using System.Collections.Generic;
using OptimizedEnum.Tool;

namespace OptimizedEnum;

class SortedIndexedDictionary<T>(int count) where T : struct, Enum {
    public readonly KeyValuePair<T, string>[] array = new KeyValuePair<T, string>[count];
    private int count;

    public string this[T key] {
        get {
            int left = 0;
            int right = count - 1;
            while(left <= right) {
                int mid = (left + right) / 2;
                if(array[mid].Key.Equal(key)) return array[mid].Value;
                if(array[mid].Key.LessThan(key)) left = mid + 1;
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
            if(array[mid].Key.Equal(key)) return;
            if(array[mid].Key.LessThan(key)) left = mid + 1;
            else right = mid - 1;
        }
        for(int i = count; i > left; i--) array[i] = array[i - 1];
        array[left] = new KeyValuePair<T, string>(key, value);
        count++;
    }
}