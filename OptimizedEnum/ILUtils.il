.class public abstract sealed auto ansi beforefieldinit OptimizedEnum.ILUtils extends [mscorlib]System.Object {
    .custom instance void [mscorlib]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )

    .method public hidebysig static bool HasAnyFlags<valuetype .ctor (class [mscorlib]System.Enum, class [mscorlib]System.ValueType) T>(!!T 'flags') cil managed {
        .custom instance void [mscorlib]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 2
        ldarg.0
        ldc.i4.0
        conv.i8
        cgt.un
        ret
    }

    .method public hidebysig static bool HasAnyFlags<valuetype .ctor (class [mscorlib]System.Enum, class [mscorlib]System.ValueType) T>(!!T 'flags', !!T otherFlags) cil managed {
        .custom instance void [mscorlib]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 2
        ldarg.0
        ldarg.1
        and
        ldc.i4.0
        conv.i8
        cgt.un
        ret
    }

    .method public hidebysig static bool HasAllFlags<valuetype .ctor (class [mscorlib]System.Enum, class [mscorlib]System.ValueType) T>(!!T 'flags', !!T otherFlags) cil managed {
        .custom instance void [mscorlib]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 2
        ldarg.0
        ldarg.1
        and
        ldarg.1
        ceq
        ret
    }

    .method public hidebysig static !!T ToggleFlags<valuetype .ctor (class [mscorlib]System.Enum, class [mscorlib]System.ValueType) T>(!!T 'flags', !!T otherFlags) cil managed {
        .custom instance void [mscorlib]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 2
        ldarg.0
        ldarg.1
        xor
        ret
    }

    .method public hidebysig static !!T CommonFlags<valuetype .ctor (class [mscorlib]System.Enum, class [mscorlib]System.ValueType) T>(!!T 'flags', !!T otherFlags) cil managed {
        .custom instance void [mscorlib]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 2
        ldarg.0
        ldarg.1
        and
        ret
    }

    .method public hidebysig static !!T CombineFlags<valuetype .ctor (class [mscorlib]System.Enum, class [mscorlib]System.ValueType) T>(!!T 'flags', !!T otherFlags) cil managed {
        .custom instance void [mscorlib]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 2
        ldarg.0
        ldarg.1
        or
        ret
    }

    .method public hidebysig static !!T RemoveFlags<valuetype .ctor (class [mscorlib]System.Enum, class [mscorlib]System.ValueType) T>(!!T 'flags', !!T otherFlags) cil managed {
        .custom instance void [mscorlib]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 2
        ldarg.0
        ldarg.1
        not
        and
        ret
    }

    .method public hidebysig static int32 AsInteger<valuetype .ctor (class [mscorlib]System.Enum, class [mscorlib]System.ValueType) T>(!!T 'flags') cil managed {
        .custom instance void [mscorlib]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 1
        ldarg.0
        conv.i4
        ret
    }


    .method public hidebysig static unsigned int32 AsUnsignedInteger<valuetype .ctor (class [mscorlib]System.Enum, class [mscorlib]System.ValueType) T>(!!T 'flags') cil managed {
        .custom instance void [mscorlib]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 1
        ldarg.0
        conv.u4
        ret
    }

    .method public hidebysig static int64 AsLong<valuetype .ctor (class [mscorlib]System.Enum, class [mscorlib]System.ValueType) T>(!!T 'flags') cil managed {
        .custom instance void [mscorlib]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 1
        ldarg.0
        conv.i8
        ret
    }

    .method public hidebysig static unsigned int64 AsUnsignedLong<valuetype .ctor (class [mscorlib]System.Enum, class [mscorlib]System.ValueType) T>(!!T 'flags') cil managed {
        .custom instance void [mscorlib]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 1
        ldarg.0
        conv.u8
        ret
    }

    .method public hidebysig static char AsChar<valuetype .ctor (class [mscorlib]System.Enum, class [mscorlib]System.ValueType) T>(!!T 'flags') cil managed {
        .custom instance void [mscorlib]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 1
        ldarg.0
        conv.u2
        ret
    }

    .method public hidebysig static float32 AsFloat<valuetype .ctor (class [mscorlib]System.Enum, class [mscorlib]System.ValueType) T>(!!T 'flags') cil managed {
        .custom instance void [mscorlib]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 1
        ldarg.0
        conv.r4
        ret
    }

    .method public hidebysig static float64 AsDouble<valuetype .ctor (class [mscorlib]System.Enum, class [mscorlib]System.ValueType) T>(!!T 'flags') cil managed {
        .custom instance void [mscorlib]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 1
        ldarg.0
        conv.r8
        ret
    }

    .method public hidebysig static int32 GetSize<valuetype .ctor (class [mscorlib]System.Enum, class [mscorlib]System.ValueType) T>() cil managed {
        .maxstack 1
        sizeof !!T
        ret
    }

    .method public hidebysig static !!T[] GetSystemEnumValues<valuetype .ctor (class [mscorlib]System.Enum, class [mscorlib]System.ValueType) T>() cil managed {
        .maxstack 1
        ldtoken !!T
        call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle)
        call class [mscorlib]System.Array [mscorlib]System.Enum::GetValues(class [mscorlib]System.Type)
        ret
    }

    .method public hidebysig static class [OptimizedEnum]OptimizedEnum.NumCalc`1<!!T> GetNumCalc<valuetype .ctor (class [mscorlib]System.Enum, class [mscorlib]System.ValueType) T>() cil managed {
        .maxstack 2
        ldtoken !!T
        call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle)
        call valuetype [mscorlib]System.TypeCode [mscorlib]System.Type::GetTypeCode(class [mscorlib]System.Type)
        ldc.i4.4
        sub
        switch (IsChar, IsInt, IsUInt, IsInt, IsUInt, IsInt, IsUInt, IsLong, IsULong)
        ldstr "Unsupported enum type: "
        ldtoken !!T
        call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle)
        callvirt instance string [mscorlib]System.Type::get_FullName()
        call string [mscorlib]System.String::Concat(string, string)
        newobj instance void [mscorlib]System.NotSupportedException::.ctor(string)
        throw
IsInt:
        newobj instance void class OptimizedEnum.NumCalcI4`1<!!T>::.ctor()
        ret
IsUInt:
        newobj instance void class OptimizedEnum.NumCalcU4`1<!!T>::.ctor()
        ret
IsLong:
        newobj instance void class OptimizedEnum.NumCalcI8`1<!!T>::.ctor()
        ret
IsULong:
        newobj instance void class OptimizedEnum.NumCalcU8`1<!!T>::.ctor()
        ret
IsChar:
        newobj instance void class OptimizedEnum.NumCalcChar`1<!!T>::.ctor()
        ret
    }
}