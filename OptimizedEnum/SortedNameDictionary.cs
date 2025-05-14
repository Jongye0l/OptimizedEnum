using System;
using System.Collections.Generic;

namespace OptimizedEnum;

struct SortedNameDictionary<T>(int count) where T : struct, Enum {
    public readonly KeyValuePair<string, T>[] array = new KeyValuePair<string, T>[count];
    public int count;

    public T GetValue(string key) {
        int left = 0;
        int right = count - 1;
        while(left < right) {
            int mid = (left + right) / 2;
            int result = Compare(array[mid].Key, key);
            switch(result) {
                case 0:
                    return array[mid].Value;
                case < 0:
                    left = mid + 1;
                    break;
                default:
                    right = mid - 1;
                    break;
            }
        }
        return array[left].Key == key ? array[left].Value : throw new ArgumentException($"Invalid enum value: {key}");
    }
    
    public T GetValue(string key, bool ignoreCase) {
        int left = 0;
        int right = count - 1;
        while(left < right) {
            int mid = (left + right) / 2;
            int result = Compare(array[mid].Key, key, ignoreCase);
            switch(result) {
                case 0:
                    return array[mid].Value;
                case < 0:
                    left = mid + 1;
                    break;
                default:
                    right = mid - 1;
                    break;
            }
        }
        return array[left].Key == key ? array[left].Value : throw new ArgumentException($"Invalid enum value: {key}");
    }
    
    public bool TryGetValue(string key, out T value, bool ignoreCase) {
        int left = 0;
        int right = count - 1;
        while(left < right) {
            int mid = (left + right) / 2;
            int result = Compare(array[mid].Key, key, ignoreCase);
            switch(result) {
                case 0:
                    value = array[mid].Value;
                    return true;
                case < 0:
                    left = mid + 1;
                    break;
                default:
                    right = mid - 1;
                    break;
            }
        }
        if(array[left].Key == key) {
            value = array[left].Value;
            return true;
        }
        value = default;
        return false;
    }
    
    public bool TryGetValue(string key, out T value) {
        int left = 0;
        int right = count - 1;
        while(left < right) {
            int mid = (left + right) / 2;
            int result = Compare(array[mid].Key, key);
            switch(result) {
                case 0:
                    value = array[mid].Value;
                    return true;
                case < 0:
                    left = mid + 1;
                    break;
                default:
                    right = mid - 1;
                    break;
            }
        }
        if(array[left].Key == key) {
            value = array[left].Value;
            return true;
        }
        value = default;
        return false;
    }

    public void Add(string key, T value) {
        int left = 0;
        int right = count - 1;
        while(left <= right) {
            int mid = (left + right) / 2;
            if(Compare(array[mid].Key, key) < 0) left = mid + 1;
            else right = mid - 1;
        }
        for(int i = count; i > left; i--) array[i] = array[i - 1];
        array[left] = new KeyValuePair<string, T>(key, value);
        count++;
    }
    
    private static int Compare(string a, string b) {
        return a.Length == b.Length ? string.CompareOrdinal(a, b) : a.Length - b.Length;
    }
    
    private static unsafe int Compare(string a, string b, bool ignoreCase) {
        if(a.Length != b.Length) return a.Length - b.Length;
        if(!ignoreCase) return string.CompareOrdinal(a, b);
        fixed(char* aPtr = a) fixed(char* bPtr = b) {
            for(int i = 0; i < a.Length; i++) {
                char c1 = ToLower(aPtr[i]);
                char c2 = ToLower(bPtr[i]);
                if(c1 != c2) return c1 - c2;
            }
        }
        return 0;
    }
    
    private static char ToLower(char c) {
        if(c is >= 'A' and <= 'Z') return (char) (c + 32);
        return c;
    }
}