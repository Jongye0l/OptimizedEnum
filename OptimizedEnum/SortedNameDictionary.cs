using System;
using System.Collections.Generic;
using OptimizedEnum.Tool;

namespace OptimizedEnum;

struct SortedNameDictionary<T>(int count) where T : struct, Enum {
    public readonly KeyValuePair<string, T>[] array = new KeyValuePair<string, T>[count];
    public int count;

    public T GetValue(string key) {
        int left = 0;
        int right = count - 1;
        while(left <= right) {
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
        return key.ParseAsNumber<T>();
    }

    public T GetValueIgnoreCase(string key) {
        int left2, right2;
        int left = left2 = 0;
        int right = right2 = count - 1;
        while(true) {
            bool run = false;
            int mid;
            int result;
            if(left <= right) {
                mid = (left + right) / 2;
                result = Compare(array[mid].Key, key, false);
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
                run = true;
            }
            if(left2 <= right2) {
                mid = (left2 + right2) / 2;
                result = Compare(array[mid].Key, key, true);
                switch(result) {
                    case 0:
                        return array[mid].Value;
                    case < 0:
                        left2 = mid + 1;
                        break;
                    default:
                        right2 = mid - 1;
                        break;
                }
                run = true;
            }
            if(!run) break;
        }
        return key.ParseAsNumber<T>();
    }

    public bool TryGetValue(string key, out T value) {
        int left = 0;
        int right = count - 1;
        while(left <= right) {
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
        return key.TryParseAsNumber(out value);
    }

    public bool TryGetValueIgnoreCase(string key, out T value) {
        int left2, right2;
        int left = left2 = 0;
        int right = right2 = count - 1;
        while(true) {
            bool run = false;
            int mid;
            int result;
            if(left <= right) {
                mid = (left + right) / 2;
                result = Compare(array[mid].Key, key, false);
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
                run = true;
            }
            if(left2 <= right2) {
                mid = (left2 + right2) / 2;
                result = Compare(array[mid].Key, key, true);
                switch(result) {
                    case 0:
                        value = array[mid].Value;
                        return true;
                    case < 0:
                        left2 = mid + 1;
                        break;
                    default:
                        right2 = mid - 1;
                        break;
                }
                run = true;
            }
            if(!run) break;
        }
        return key.TryParseAsNumber(out value);
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

    private static unsafe int Compare(string a, string b, bool lower) {
        if(a.Length != b.Length) return a.Length - b.Length;
        fixed(char* aPtr = a) fixed(char* bPtr = b) {
            for(int i = 0; i < a.Length; i++) {
                char c1 = FixChar(aPtr[i], lower);
                char c2 = FixChar(bPtr[i], lower);
                if(c1 != c2) return c1 - c2;
            }
        }
        return 0;
    }

    private static char FixChar(char c, bool lower) {
        return lower switch {
            true when c is >= 'A' and <= 'Z' => (char) (c + 32),
            false when c is >= 'a' and <= 'z' => (char) (c - 32),
            _ => c
        };
    }
}