using System.Collections.Generic;

namespace OptimizedEnum;

struct SortedDoubleDictionary(int count) {
    public readonly KeyValuePair<double, string>[] array = new KeyValuePair<double, string>[count];
    public int count;

    public void Add(double key, string value) {
        int left = 0;
        int right = count - 1;
        while(left <= right) {
            int mid = (left + right) / 2;
            if(array[mid].Key < key) left = mid + 1;
            else right = mid - 1;
        }
        for(int i = count; i > left; i--) array[i] = array[i - 1];
        array[left] = new KeyValuePair<double, string>(key, value);
        count++;
    }
}