using System;
using OptimizedEnum.Tool;

namespace OptimizedEnum;

struct SortedList<T>(int count) where T : struct, Enum {
    public readonly T[] array = new T[count];
    public int count;
    
    public void Add(T key) {
        int left = 0;
        int right = count - 1;
        while(left <= right) {
            int mid = (left + right) / 2;
            if(array[mid].Equal(key)) {
                left = mid + 1;
                break;
            }
            if(array[mid].LessThan(key)) left = mid + 1;
            else right = mid - 1;
        }
        for(int i = count; i > left; i--) array[i] = array[i - 1];
        array[left] = key;
        count++;
    }
}