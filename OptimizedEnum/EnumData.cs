using System;
using System.Linq;
using System.Reflection;

namespace OptimizedEnum;

abstract class EnumData<T> where T : struct, Enum {
    public static readonly EnumData<T> Instance = CreateInstance();
    public readonly T AllFlags;
    public readonly T[] Values;
    public readonly NumCalc<T> NumCalc;
    public readonly bool HasZero;

    public EnumData(FieldInfo[] fields) {
        Values = ILUtils.GetSystemEnumValues<T>();
        NumCalc<T> numCalc = NumCalc = NumCalc<T>.Instance;
        T allFlags = ILUtils.GetSystemMinValue<T>();
        foreach(T value in Values) 
            if(!numCalc.GetBool(value)) HasZero = true;
            else allFlags = allFlags.CombineFlags(value);
        AllFlags = allFlags;
    }

    private static EnumData<T> CreateInstance() {
        FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static);
        if(ILUtils.IsFlag<T>()) return new FlagEnumData<T>(fields);
        bool[] checkField = new bool[fields.Length];
        bool outOfRange = false;
        bool isSorted = true;
        int length = fields.Length;
        if(ILUtils.GetSize<T>() == 8) {
            for(int i = 0; i < length; i++) {
                T value = (T) fields[i].GetValue(null);
                ulong v = value.AsUnsignedLong();
                if((ulong) length <= v) {
                    outOfRange = true;
                    break;
                }
                checkField[v] = true;
                if((ulong) i != v) isSorted = false;
            }
        } else {
            for(int i = 0; i < length; i++) {
                T value = (T) fields[i].GetValue(null);
                uint v = value.AsUnsignedInteger();
                if(length <= v) {
                    outOfRange = true;
                    break;
                }
                checkField[v] = true;
                if(i != v) isSorted = false;
            }
        }
        return !outOfRange && checkField.All(t => t) ? new SortedEnumData<T>(fields, isSorted) : new UnsortedEnumData<T>(fields);
    }

    public abstract string GetString(T eEnum);

    public abstract string GetName(T eEnum);
}