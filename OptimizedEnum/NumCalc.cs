using System;

namespace OptimizedEnum;

abstract class NumCalc<T> where T : struct, Enum {
    public static readonly NumCalc<T> Instance = ILUtils.GetNumCalc<T>();
    public abstract string GetString(T eEnum);
    public abstract bool GetBool(T eEnum);
    public abstract bool Equal(T eEnum, int value);
    public abstract bool LessThan(T eEnum, T value);
    public abstract bool LessThan(T eEnum, int value);
    public abstract int BitCount(T eEnum);
    public abstract int[] GetBitLocations(T eEnum);
    public abstract T2 GetValue<T2>(T2[] array, T eEnum);
    public abstract void SetValue<T2>(T2[] array, T eEnum, T2 value);
}