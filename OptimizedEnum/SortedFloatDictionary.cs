using System.Collections.Generic;

namespace OptimizedEnum;

struct SortedFloatDictionary(int count) {
#if NETCOREAPP2_0_OR_GREATER || NET5_0
    public readonly KeyValuePair<float, string>[] Array = new KeyValuePair<float, string>[count];
#else
    public readonly KeyValuePair<double, string>[] Array = new KeyValuePair<double, string>[count];
#endif
    public int count;

#if NETCOREAPP2_0_OR_GREATER || NET5_0
    public void Add(float key, string value) {
#else
    public void Add(double key, string value) {
#endif
        int left = 0;
        int right = count - 1;
        while(left <= right) {
            int mid = (left + right) / 2;
            if(Array[mid].Key < key) left = mid + 1;
            else right = mid - 1;
        }
        for(int i = count; i > left; i--) Array[i] = Array[i - 1];
#if NETCOREAPP2_0_OR_GREATER || NET5_0
        Array[left] = new KeyValuePair<float, string>(key, value);
#else
        Array[left] = new KeyValuePair<double, string>(key, value);
#endif
        count++;
    }
}