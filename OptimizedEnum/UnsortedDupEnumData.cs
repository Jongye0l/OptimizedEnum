using System;
using System.Reflection;

namespace OptimizedEnum;

class UnsortedDupEnumData<T> : UnsortedEnumData<T> where T : struct, Enum {

    public UnsortedDupEnumData(FieldInfo[] fields) : base(fields) {
    }
    public override string GetString(T eEnum) {
        throw new NotImplementedException();
    }
    public override string GetName(T eEnum) {
        throw new NotImplementedException();
    }
}