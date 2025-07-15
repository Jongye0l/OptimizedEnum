using System;
using System.Collections.Generic;
using OptimizedEnum.Tool;

namespace OptimizedEnum;

class SortedIndexedDictionary<T>(T[] keys, string[] values, int count) where T : struct, Enum {
    public T[] Keys = keys;
    public string[] Values = values;
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

    public void AddOrSet(T key, string value, bool allowDuplicates) {
        int left = 0;
        int right = Count - 1;
        int i;
        T[] keys;
        string[] values;
        while(left <= right) {
            int mid = (left + right) / 2;
            if(Keys[mid].GreaterThan(key)) right = mid - 1;
            else if(allowDuplicates && Keys[mid].Equal(key)) {
                while(Keys[mid - 1].Equals(key)) mid--;
                Values[mid] = value;
                i = mid;
                while(Keys[i + 1].Equals(key) && i < Count) i++;
                if(mid != i) {
                    int remove = i - mid;
                    keys = new T[Count -= remove];
                    values = new string[Count];
                    Array.Copy(Keys, keys, mid);
                    keys[mid] = key;
                    values[mid] = value;
                    Array.Copy(Keys, mid + remove, keys, mid + 1, Count - mid - 1);
                    Keys = keys;
                    Values = values;
                }
                return;
            } else left = mid + 1;
        }
        keys = new T[++Count];
        values = new string[Count];
        Array.Copy(Keys, keys, left);
        keys[left] = key;
        values[left] = value;
        Array.Copy(Keys, left, keys, left + 1, Count - left - 1);
        Keys = keys;
        Values = values;
    }
}