.class public abstract sealed auto ansi beforefieldinit OptimizedEnum.Tool.Utils extends [System.Runtime]System.Object {
    .method public hidebysig static bool HasAnyFlags<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(!!T 'flags') cil managed {
        .custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 2
        ldarg.0
        conv.i8
        ldc.i4.0
        conv.i8
        cgt.un
        ret
    }

    .method public hidebysig static bool HasAnyFlags<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(!!T 'flags', !!T otherFlags) cil managed {
        .custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 2
        ldarg.0
        ldarg.1
        and
        conv.i8
        ldc.i4.0
        conv.i8
        cgt.un
        ret
    }

    .method public hidebysig static bool HasAllFlags<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(!!T 'flags', !!T otherFlags) cil managed {
        .custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 2
        ldarg.0
        ldarg.1
        and
        ldarg.1
        ceq
        ret
    }

    .method public hidebysig static !!T ToggleFlags<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(!!T 'flags', !!T otherFlags) cil managed {
        .custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 2
        ldarg.0
        ldarg.1
        xor
        ret
    }

    .method public hidebysig static !!T CommonFlags<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(!!T 'flags', !!T otherFlags) cil managed {
        .custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 2
        ldarg.0
        ldarg.1
        and
        ret
    }

    .method public hidebysig static !!T CombineFlags<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(!!T 'flags', !!T otherFlags) cil managed {
        .custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 2
        ldarg.0
        ldarg.1
        or
        ret
    }

    .method public hidebysig static !!T RemoveFlags<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(!!T 'flags', !!T otherFlags) cil managed {
        .custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 2
        ldarg.0
        ldarg.1
        not
        and
        ret
    }

    .method public hidebysig static int32 AsInteger<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(!!T 'flags') cil managed {
        .custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 1
        ldarg.0
        conv.i4
        ret
    }


    .method public hidebysig static unsigned int32 AsUnsignedInteger<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(!!T 'flags') cil managed {
        .custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 1
        ldarg.0
        conv.u4
        ret
    }

    .method public hidebysig static int64 AsLong<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(!!T 'flags') cil managed {
        .custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 1
        ldarg.0
        conv.i8
        ret
    }

    .method public hidebysig static unsigned int64 AsUnsignedLong<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(!!T 'flags') cil managed {
        .custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 1
        ldarg.0
        conv.u8
        ret
    }

    .method public hidebysig static char AsChar<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(!!T 'flags') cil managed {
        .custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 1
        ldarg.0
        conv.u4
        ret
    }

    .method public hidebysig static float32 AsFloat<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(!!T 'flags') cil managed {
        .custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 1
        ldarg.0
        conv.r4
        ret
    }

    .method public hidebysig static float64 AsDouble<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(!!T 'flags') cil managed {
        .custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 1
        ldarg.0
        conv.r8
        ret
    }

    .method public hidebysig static float32 AsFloatUnsigned<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(!!T 'flags') cil managed {
        .custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 2
        ldarg.0
        ldsfld bool class OptimizedEnum.EnumData`1<!!T>::LowNotUnsigned
        brfalse Skip
        
        call float32 OptimizedEnum.Tool.Utils::AsFloatUnsigned0<!!T>(!!T)
        ret
Skip:
        conv.r.un
        ret
    }

    .method private hidebysig static float32 AsFloatUnsigned0<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(!!T 'flags') cil managed {
        .custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 3
        ldarg.0
        sizeof !!T
        ldc.i4.1
        beq IsByte
        conv.u2
        conv.r4
        ret
IsByte:
        conv.u1
        conv.r4
        ret
    }

    .method public hidebysig static float64 AsDoubleUnsigned<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(!!T 'flags') cil managed {
        .custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 2
        ldarg.0
        ldsfld bool class OptimizedEnum.EnumData`1<!!T>::LowNotUnsigned
        brfalse Skip

        call float64 OptimizedEnum.Tool.Utils::AsDoubleUnsigned0<!!T>(!!T)
        ret
Skip:
        conv.r.un
        conv.r8
        ret
    }

    .method private hidebysig static float64 AsDoubleUnsigned0<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(!!T 'flags') cil managed {
        .custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 3
        ldarg.0
        sizeof !!T
        ldc.i4.1
        beq IsByte
        conv.u2
        conv.r8
        ret
IsByte:
        conv.u1
        conv.r8
        ret
    }

    .method public hidebysig static !!T AsUnsigned<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(!!T 'flags') cil managed {
        .custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 3
        ldarg.0
        ldsfld bool class OptimizedEnum.EnumData`1<!!T>::LowNotUnsigned
        brfalse Skip

        call !!T OptimizedEnum.Tool.Utils::AsUnsigned0<!!T>(!!T)
Skip:
        ret
    }

    .method private hidebysig static !!T AsUnsigned0<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(!!T 'flags') cil managed {
        .custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 3
        ldarg.0
        sizeof !!T
        ldc.i4.1
        beq IsByte
        conv.u2
        ret
IsByte:
        conv.u1
        ret
    }

    .method public hidebysig static !!T2 As<valuetype .ctor (class [System.Runtime]System.ValueType) T, valuetype .ctor (class [System.Runtime]System.ValueType) T2>(!!T 'flags') cil managed {
        .custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 1
        ldarg.0
        ret
    }

    .method public hidebysig static !!T As<T>(object 'value') cil managed {
        .custom instance void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(uint8) = ( 01 00 01 00 00 )
        .custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .param type T
            .custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = ( 01 00 02 00 00 )
        .maxstack 1
        ldarg.0
        ret
    }

    .method public hidebysig static bool Equal<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(!!T 'flags', !!T otherFlags) cil managed {
        .custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 2
        ldarg.0
        ldarg.1
        ceq
        ret
    }

    .method public hidebysig static bool LessThan<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(!!T 'flags', !!T otherFlags) cil managed {
        .custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 2
        ldarg.0
        ldarg.1
        clt
        ret
    }

    .method public hidebysig static bool LessThanUnsigned<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(!!T 'flags', !!T otherFlags) cil managed {
        .custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 2
        ldarg.0
        ldarg.1
        clt.un
        ret
    }

    .method public hidebysig static bool GreaterThan<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(!!T 'flags', !!T otherFlags) cil managed {
        .custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 2
        ldarg.0
        ldarg.1
        cgt
        ret
    }

    .method public hidebysig static bool GreaterThanUnsigned<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(!!T 'flags', !!T otherFlags) cil managed {
        .custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 2
        ldarg.0
        ldarg.1
        cgt.un
        ret
    }

    .method public hidebysig static int32 BitCount<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(!!T eEnum) cil managed {
        .custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 2
        sizeof !!T
        ldc.i4.8
        beq Bit8

        ldarg.0
        call int32 OptimizedEnum.Tool.Utils::BitCount4<!!T>(!!T)
        ret
Bit8:
        ldarg.0
        call int32 OptimizedEnum.Tool.Utils::BitCount8<!!T>(!!T)
        ret
    }

    .method private hidebysig static int32 BitCount4<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(!!T eEnum) cil managed {
        .custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 3
        ldarg.0
        ldsfld bool class OptimizedEnum.EnumData`1<!!T>::LowNotUnsigned
        brfalse Skip
        
        sizeof !!T
        ldc.i4.1
        beq IsByte
        sizeof !!T
        ldc.i4.2
        beq IsShort
IsByte:
        conv.u1
        br Skip
IsShort:
        conv.u2
Skip:
        dup
        ldc.i4.1
        shr
        ldc.i4 0x55555555
        and
        sub
        starg.s eEnum

        ldarg.0
        ldc.i4 0x33333333
        and
        ldarg.0
        ldc.i4.2
        shr
        ldc.i4 0x33333333
        and
        add

        dup
        ldc.i4.4
        shr
        add
        ldc.i4 0x0F0F0F0F
        and
        ldc.i4 0x01010101
        mul
        ldc.i4.s 24
        shr
        ret
    }

    .method private hidebysig static int32 BitCount8<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(!!T eEnum) cil managed {
        .custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .maxstack 3
        ldarg.0
        dup
        ldc.i4.1
        shr
        ldc.i8 0x5555555555555555
        and
        sub
        starg.s eEnum

        ldarg.0
        ldc.i8 0x3333333333333333
        and
        ldarg.0
        ldc.i4.2
        shr
        ldc.i8 0x3333333333333333
        and
        add

        dup
        ldc.i4.4
        shr
        add
        ldc.i8 0xF0F0F0F0F0F0F0F
        and
        ldc.i8 0x101010101010101
        mul
        ldc.i4.s 56
        shr
        conv.i4
        ret
    }

    .method public hidebysig static int32[] GetBitLocations<valuetype .ctor (class [System.Runtime]System.Enum) T>(!!T eEnum) cil managed {
        .custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .param [0]
            .custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = ( 01 00 01 00 00 )
        .maxstack 4
        .locals init (int32[] bits, int32 count, int32 subValue)
        ldarg.0
        call int32 OptimizedEnum.Tool.Utils::BitCount<!!T>(!!T)
        newarr [System.Runtime]System.Int32
        stloc.0
        ldc.i4.0
        dup
        stloc.1
        stloc.2
        ldarg.0
StartRepeat:
        brfalse EndRepeat

        ldarg.0
        conv.i4
        ldc.i4.1
        and
        brfalse SkipSetValue

        ldloc.0
        ldloc.1
        dup
        ldc.i4.1
        add
        stloc.1
        ldloc.2
        stelem.i4
SkipSetValue:
        ldarg.0
        ldc.i4.1
        shr
        dup
        starg.s eEnum

        ldloc.2
        ldc.i4.1
        add
        stloc.2

        br StartRepeat
EndRepeat:
        ldloc.0
        ret
    }

    .method public hidebysig static int32 GetSize<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>() cil managed {
        .maxstack 1
        sizeof !!T
        ret
    }

    .method public hidebysig static string GetOrDefault<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(string[] 'array', !!T eEnum, uint32 length) cil managed {
        .custom instance void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(uint8) = ( 01 00 01 00 00 )
        .param type T
            .custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = ( 01 00 00 00 00 )
        .param [2]
            .custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = ( 01 00 00 00 00 )
        .maxstack 5
        ldarg.0
        ldarg.1
        ldarg.2

        sizeof !!T
        ldc.i4.8
        beq Bit8
        
        call string OptimizedEnum.Tool.Utils::GetOrDefault4<!!T>(string[], !!T, uint32)
        ret
Bit8:
        call string OptimizedEnum.Tool.Utils::GetOrDefault8<!!T>(string[], !!T, uint32)
        ret
    }

    .method private hidebysig static string GetOrDefault4<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(string[] 'array', !!T eEnum, uint32 length) cil managed {
        .maxstack 2
        ldarg.1
        ldarg.2
        clt.un
        brfalse SkipGetValue

        ldarg.0
        ldarg.1
        ldelem string
        ret

SkipGetValue:
        ldarg.1
        call string OptimizedEnum.Tool.Utils::GetNumberString<!!T>(!!T)
        ret
    }

    .method private hidebysig static string GetOrDefault8<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(string[] 'array', !!T eEnum, uint32 length) cil managed {
        .maxstack 2
        ldarg.1
        ldarg.2
        conv.i8
        clt.un
        brfalse SkipGetValue

        ldarg.0
        ldarg.1
        conv.i4
        ldelem string
        ret

SkipGetValue:
        ldarg.1
        call string OptimizedEnum.Tool.Utils::GetNumberString<!!T>(!!T)
        ret
    }

    .method public hidebysig static string GetOrNull<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(string[] 'array', !!T eEnum, uint32 length) cil managed {
        .param [0]
            .custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = ( 01 00 02 00 00 )
        .param [1]
            .custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = ( 01 00 01 00 00 )
        .maxstack 5
        ldarg.0
        ldarg.1
        ldarg.2

        sizeof !!T
        ldc.i4.8
        beq Bit8
        
        call string OptimizedEnum.Tool.Utils::GetOrNull4<!!T>(string[], !!T, uint32)
        ret
Bit8:
        call string OptimizedEnum.Tool.Utils::GetOrNull8<!!T>(string[], !!T, uint32)
        ret
    }

    .method private hidebysig static string GetOrNull4<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(string[] 'array', !!T eEnum, uint32 length) cil managed {
        .maxstack 2
        ldarg.1
        ldarg.2
        clt.un
        brfalse SkipGetValue

        ldarg.0
        ldarg.1
        ldelem string
        ret

SkipGetValue:
        ldnull
        ret
    }

    .method private hidebysig static string GetOrNull8<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(string[] 'array', !!T eEnum, uint32 length) cil managed {
        .maxstack 2
        ldarg.1
        ldarg.2
        conv.i8
        clt.un
        brfalse SkipGetValue

        ldarg.0
        ldarg.1
        conv.i4
        ldelem string
        ret

SkipGetValue:
        ldnull
        ret
    }

    .method public hidebysig static !!T[] GetSystemEnumValues<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>() cil managed {
        .param [0]
            .custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8[]) = ( 01 00 02 00 00 00 01 00 00 00 )
        .maxstack 1
        ldtoken !!T
        call class [System.Runtime]System.Type [System.Runtime]System.Type::GetTypeFromHandle(valuetype [System.Runtime]System.RuntimeTypeHandle)
        call class [System.Runtime]System.Array [System.Runtime]System.Enum::GetValues(class [System.Runtime]System.Type)
        ret
    }

    .method public hidebysig static string GetNumberStringCustom<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(!!T eEnum) cil managed {
        .custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .param [0]
            .custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = ( 01 00 01 00 00 )
        .maxstack 2
        ldarg.0
        ldsfld valuetype OptimizedEnum.DataType class OptimizedEnum.EnumData`1<!!T>::DataType
        switch (IsInt, IsUInt, IsLong, IsULong)
        ldc.i4.1
        newobj instance void System.String::.ctor(char, int32)
        ret
IsInt:
        call string OptimizedEnum.Tool.Utils::Int32ToString(int32)
        ret
IsUInt:
        call string OptimizedEnum.Tool.Utils::UInt32ToString(uint32)
        ret
IsLong:
        call string OptimizedEnum.Tool.Utils::Int64ToString(int64)
        ret
IsULong:
        call string OptimizedEnum.Tool.Utils::UInt64ToString(uint64)
        ret
    }

    .method public hidebysig static string GetNumberString<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(!!T eEnum) cil managed {
        .custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .param [0]
            .custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = ( 01 00 01 00 00 )
        .maxstack 2
        ldarga.s eEnum
        ldsfld valuetype OptimizedEnum.DataType class OptimizedEnum.EnumData`1<!!T>::DataType
        switch (IsInt, IsUInt, IsLong, IsULong)
        pop
        ldarg.0
        ldc.i4.1
        newobj instance void System.String::.ctor(char, int32)
        ret
IsInt:
        call instance string [System.Runtime]System.Int32::ToString()
        ret
IsUInt:
        call instance string [System.Runtime]System.UInt32::ToString()
        ret
IsLong:
        call instance string [System.Runtime]System.Int64::ToString()
        ret
IsULong:
        call instance string [System.Runtime]System.UInt64::ToString()
        ret
    }

    .method public hidebysig static string GetHexString<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(!!T eEnum) cil managed {
        .param [0]
            .custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = ( 01 00 01 00 00 )
        .maxstack 4
        ldarg.0
        sizeof !!T
        ldc.i4.2
        mul
        ldsfld valuetype OptimizedEnum.DataType class OptimizedEnum.EnumData`1<!!T>::DataType
        switch (IsInt, IsUInt)
        call string OptimizedEnum.Tool.Utils::UInt64ToHexString(uint64, int32)
        ret
IsInt:
        dup
        ldc.i4.8
        beq IsUInt
        call string OptimizedEnum.Tool.Utils::Int32ToHexString(int32, int32)
        ret
IsUInt:
        call string OptimizedEnum.Tool.Utils::UInt32ToHexString(uint32, int32)
        ret
    }

    .method public hidebysig static !!T ParseAsNumber<valuetype .ctor (class [System.Runtime]System.Enum, class [System.Runtime]System.ValueType) T>(string 'value') cil managed {
        .param [1]
            .custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = ( 01 00 01 00 00 )
        .maxstack 2
        ldarg.0
        ldsfld valuetype OptimizedEnum.DataType class OptimizedEnum.EnumData`1<!!T>::DataType
        switch (IsInt, IsUInt, IsLong, IsULong)

        call instance int32 [System.Runtime]System.String::get_Length()
        ldc.i4.1
        beq SkipThrow

        ldstr "Invalid enum value: "
        ldarg.0
        call string [System.Runtime]System.String::Concat(string, string)
        newobj instance void [System.Runtime]System.ArgumentException::.ctor(string)
        throw
SkipThrow:
        ldarg.0
        ldc.i4.0
        call instance char [System.Runtime]System.String::get_Chars(int32)
        ret
IsInt:
        call int32 OptimizedEnum.Tool.Utils::ParseInt32(string)
        ret
IsUInt:
        call uint32 OptimizedEnum.Tool.Utils::ParseUInt32(string)
        ret
IsLong:
        call int64 OptimizedEnum.Tool.Utils::ParseInt64(string)
        ret
IsULong:
        call uint64 OptimizedEnum.Tool.Utils::ParseUInt64(string)
        ret
    }

    .method public hidebysig static bool TryParseAsNumber<valuetype .ctor (class [System.Runtime]System.Enum) T>(string 'value', [out] !!T& result) cil managed {
        .custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = ( 01 00 00 00 )
        .param [1]
            .custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = ( 01 00 01 00 00 )
        .maxstack 4
        ldarg.0
        ldarg.1
        ldsfld valuetype OptimizedEnum.DataType class OptimizedEnum.EnumData`1<!!T>::DataType
        switch (IsInt, IsUInt, IsLong, IsULong)

        ldarg.0
        call instance int32 [System.Runtime]System.String::get_Length()
        ldc.i4.1
        beq SkipThrow

        pop
        pop
        ldc.i4.0
        ret

SkipThrow:
        ldarg.0
        ldc.i4.0
        call instance char [System.Runtime]System.String::get_Chars(int32)
        stind.i2
        pop
        ldc.i4.1
        ret
IsInt:
        call bool OptimizedEnum.Tool.Utils::TryParseInt32(string, int32&)
        ret
IsUInt:
        call bool OptimizedEnum.Tool.Utils::TryParseUInt32(string, uint32&)
        ret
IsLong:
        call bool OptimizedEnum.Tool.Utils::TryParseInt64(string, int64&)
        ret
IsULong:
        call bool OptimizedEnum.Tool.Utils::TryParseUInt64(string, uint64&)
        ret
    }

    .method public hidebysig static !!T GetZero<T>() cil managed {
        .custom instance void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(uint8) = ( 01 00 01 00 00 )
        .param type T
            .custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = ( 01 00 02 00 00 )
        .maxstack 2
        sizeof !!T
        ldc.i4.8
        beq IsLong

        call !!T OptimizedEnum.Tool.Utils::GetZero4<!!T>()
        ret
IsLong:
        call !!T OptimizedEnum.Tool.Utils::GetZero8<!!T>()
        ret
    }

    .method private hidebysig static !!T GetZero4<T>() cil managed {
        .maxstack 1
        ldc.i4.0
        ret
    }

    .method private hidebysig static !!T GetZero8<T>() cil managed {
        .maxstack 1
        ldc.i4.0
        conv.i8
        ret
    }
}