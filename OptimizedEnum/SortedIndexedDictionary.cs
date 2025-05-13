using System;
using System.Collections;
using System.Collections.Generic;

namespace OptimizedEnum;

class SortedIndexedDictionary<T>(int count) : IEnumerable<KeyValuePair<T, string>> where T : struct, Enum {
    private static NumCalc<T> numCalc = NumCalc<T>.Instance;
    private readonly KeyValuePair<T, string>[] _array = new KeyValuePair<T, string>[count];
    private int count;

    public string this[T key] {
        get {
            int left = 0;
            int right = count - 1;
            while(left < right) {
                int mid = (left + right) / 2;
                if(numCalc.LessThan(key, _array[mid].Key)) left = mid + 1;
                else right = mid - 1;
            }
            return left == right ? _array[(left + right) / 2].Value : null;
        }
    }

    public IEnumerator<KeyValuePair<T, string>> GetEnumerator() {
        return new Enumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    public void Add(T key, string value) {
        int left = 0;
        int right = count - 1;
        while(left <= right) {
            int mid = (left + right) / 2;
            if(numCalc.LessThan(key, _array[mid].Key)) left = mid + 1;
            else right = mid - 1;
        }
        for(int i = count; i > left; i--) _array[i] = _array[i - 1];
        _array[left] = new KeyValuePair<T, string>(key, value);
        count++;
    }

    public KeyValuePair<T, string> GetOfIndex(int index) => _array[index];

    private struct Enumerator(SortedIndexedDictionary<T> dictionary) : IEnumerator<KeyValuePair<T, string>> {
        private int current = dictionary.count - 1;

        public bool MoveNext() {
            return current-- != 0;
        }

        public void Reset() {
            current = dictionary.count - 1;
        }

        public KeyValuePair<T, string> Current => dictionary.GetOfIndex(current);
        object IEnumerator.Current => Current;
        public void Dispose() { }
    }
}