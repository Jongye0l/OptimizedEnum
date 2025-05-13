using System;

namespace OptimizedEnum;

abstract class NumString<T> where T : struct, Enum {
    public abstract string GetString(T eEnum);
}

class NumStringI4<T> : NumString<T> where T : struct, Enum {
    public override string GetString(T eEnum) => eEnum.AsInteger().ToString();
}

class NumStringU4<T> : NumString<T> where T : struct, Enum {
    public override string GetString(T eEnum) => eEnum.AsUnsignedInteger().ToString();
}

class NumStringI8<T> : NumString<T> where T : struct, Enum {
    public override string GetString(T eEnum) => eEnum.AsLong().ToString();
}

class NumStringU8<T> : NumString<T> where T : struct, Enum {
    public override string GetString(T eEnum) => eEnum.AsUnsignedLong().ToString();
}

class NumStringChar<T> : NumString<T> where T : struct, Enum {
    public override string GetString(T eEnum) => eEnum.AsChar().ToString();
}