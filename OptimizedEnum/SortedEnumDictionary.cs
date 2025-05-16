using System;
using System.Collections.Generic;
using System.Reflection;
using OptimizedEnum.Tool;

namespace OptimizedEnum;

struct SortedEnumDictionary {
    private static readonly Type EnumDataType = typeof(EnumData<>);
    public KeyValuePair<int, EnumData>[] array;
    public int count;
    public int capacity;
    
    public SortedEnumDictionary() {
        array = new KeyValuePair<int, EnumData>[capacity = 16];
    }

    public EnumData GetOrCreate(Type key) {
        int left = 0;
        int right = count - 1;
        int hash = key.GetHashCode();
        while(left <= right) {
            int mid = (left + right) / 2;
            if(array[mid].Key == hash) return array[mid].Value;
            if(array[mid].Key < hash) left = mid + 1;
            else right = mid - 1;
        }
        return EnumDataType.MakeGenericType(key).GetField("Instance", BindingFlags.Public | BindingFlags.Static).GetValue(null).As<EnumData>();
    }

    public void Add(Type key, EnumData value) {
        if(capacity == count) {
            KeyValuePair<int, EnumData>[] newArray = new KeyValuePair<int, EnumData>[capacity += 16];
            Array.Copy(array, newArray, count);
            array = newArray;
        }
        int left = 0;
        int right = count - 1;
        int hash = key.GetHashCode();
        while(left <= right) {
            int mid = (left + right) / 2;
            if(array[mid].Key < hash) left = mid + 1;
            else right = mid - 1;
        }
        for(int i = count; i > left; i--) array[i] = array[i - 1];
        array[left] = new KeyValuePair<int, EnumData>(hash, value);
        count++;
    }
}