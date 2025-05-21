using System;
using System.Collections.Generic;
using System.Reflection;
using OptimizedEnum.Tool;

namespace OptimizedEnum;

struct SortedEnumDictionary {
    private static readonly Type EnumDataType = typeof(EnumData<>);
    public KeyValuePair<int, EnumData>[] Array;
    public int Count;
    public int Capacity;
    
    public SortedEnumDictionary() {
        Array = new KeyValuePair<int, EnumData>[Capacity = 16];
    }

    public EnumData GetOrCreate(Type key) {
        int left = 0;
        int right = Count - 1;
        int hash = key.GetHashCode();
        while(left <= right) {
            int mid = (left + right) / 2;
            if(Array[mid].Key == hash) return Array[mid].Value;
            if(Array[mid].Key < hash) left = mid + 1;
            else right = mid - 1;
        }
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
        return EnumDataType.MakeGenericType(key).
#if NETSTANDARD1_0
            GetRuntimeField("Instance")
#elif NETSTANDARD1_5
            GetTypeInfo().GetField("Instance", BindingFlags.Public | BindingFlags.Static)
#else
            GetField("Instance", BindingFlags.Public | BindingFlags.Static)
#endif
            .GetValue(null).As<EnumData>();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8604 // Possible null reference argument.
    }

    public void Add(Type key, EnumData value) {
        if(Capacity == Count) {
            KeyValuePair<int, EnumData>[] newArray = new KeyValuePair<int, EnumData>[Capacity += 16];
            System.Array.Copy(Array, newArray, Count);
            Array = newArray;
        }
        int left = 0;
        int right = Count - 1;
        int hash = key.GetHashCode();
        while(left <= right) {
            int mid = (left + right) / 2;
            if(Array[mid].Key < hash) left = mid + 1;
            else right = mid - 1;
        }
        for(int i = Count; i > left; i--) Array[i] = Array[i - 1];
        Array[left] = new KeyValuePair<int, EnumData>(hash, value);
        Count++;
    }
}