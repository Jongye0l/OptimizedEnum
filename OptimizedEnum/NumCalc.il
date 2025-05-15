.class private auto ansi beforefieldinit OptimizedEnum.NumCalcI4`1<valuetype .ctor (class [mscorlib]System.Enum) T> extends class [OptimizedEnum]OptimizedEnum.NumCalc`1<!T> {
    .method public hidebysig virtual instance string GetString(!T eEnum) cil managed {
        .maxstack 1
        ldarg.1
        call string [OptimizedEnum]OptimizedEnum.Utils::Int32ToString(int32)
        ret
    }

    .method public hidebysig virtual instance bool Equal(!T eEnum, !T 'value') cil managed {
        .maxstack 2
        ldarg.1
        ldarg.2
        ceq
        ret
    }

    .method public hidebysig virtual instance bool Equal(!T eEnum, int32 'value') cil managed {
        .maxstack 2
        ldarg.1
        ldarg.2
        ceq
        ret
    }
    
    .method public hidebysig virtual instance bool LessThan(!T eEnum, !T 'value') cil managed {
        .maxstack 2
        ldarg.1
        ldarg.2
        clt
        ret
    }

    .method public hidebysig virtual instance int32 BitCount(!T eEnum) cil managed {
        .maxstack 3
        ldarg.1
        dup
        ldc.i4.1
        shr
        ldc.i4 0x55555555
        and
        sub
        starg.s eEnum

        ldarg.1
        ldc.i4 0x33333333
        and
        ldarg.1
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

    .method public hidebysig virtual instance int32[] GetBitLocations(!T eEnum) cil managed {
        .maxstack 4
        .locals init (int32[] bits, int32 count, int32 subValue)
        ldarg.0
        ldarg.1
        call instance int32 class OptimizedEnum.NumCalcI4`1<!T>::BitCount(!T)
        newarr [mscorlib]System.Int32
        stloc.0
        ldc.i4.0
        dup
        stloc.1
        stloc.2
        ldarg.1
StartRepeat:
        brfalse EndRepeat

        ldarg.1
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
        ldarg.1
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

    .method public hidebysig virtual instance string GetOrDefault(string[] 'array', !T eEnum, int32 length) cil managed {
        .maxstack 2
        ldarg.2
        ldarg.3
        clt.un
        brfalse SkipGetValue
        ldarg.1
        ldarg.2
        ldelem string
        ret
SkipGetValue:
        ldarg.2
        call string [OptimizedEnum]OptimizedEnum.Utils::Int32ToString(int32)
        ret
    }

    .method public hidebysig virtual instance string GetOrNull(string[] 'array', !T eEnum, int32 length) cil managed {
        .maxstack 2
        ldarg.2
        ldarg.3
        clt.un
        brfalse SkipGetValue
        ldarg.1
        ldarg.2
        ldelem string
        ret
SkipGetValue:
        ldnull
        ret
    }

    .method public hidebysig virtual instance void SetValue(string[] 'array', !T eEnum, string 'value') cil managed {
        .maxstack 3
        ldarg.1
        ldarg.2
        ldarg.3
        stelem string
        ret
    }

    .method public hidebysig virtual instance int32 ToInt(!T eEnum) cil managed {
        .maxstack 1
        ldarg.1
        ret
    }

    .method public hidebysig virtual instance !T ParseAsNum(string str) cil managed {
        .maxstack 1
        ldarg.1
        call int32 [OptimizedEnum]OptimizedEnum.Utils::ParseInt32(string)
        ret
    }

    .method public hidebysig virtual instance !T GetSystemMinValue() cil managed {
        .maxstack 1
        ldc.i4.0
        ret
    }

    .method public hidebysig specialname rtspecialname instance void .ctor() cil managed {
        .maxstack 1
        ldarg.0
        call instance void class [OptimizedEnum]OptimizedEnum.NumCalc`1<!T>::.ctor()
        ret
    }
}

.class private auto ansi beforefieldinit OptimizedEnum.NumCalcU4`1<valuetype .ctor (class [mscorlib]System.Enum) T> extends class [OptimizedEnum]OptimizedEnum.NumCalc`1<!T> {
    .method public hidebysig virtual instance string GetString(!T eEnum) cil managed {
        .maxstack 1
        ldarg.1
        call string [OptimizedEnum]OptimizedEnum.Utils::UInt32ToString(uint32)
        ret
    }

    .method public hidebysig virtual instance bool Equal(!T eEnum, !T 'value') cil managed {
        .maxstack 2
        ldarg.1
        ldarg.2
        ceq
        ret
    }

    .method public hidebysig virtual instance bool Equal(!T eEnum, int32 'value') cil managed {
        .maxstack 2
        ldarg.1
        ldarg.2
        ceq
        ret
    }
    
    .method public hidebysig virtual instance bool LessThan(!T eEnum, !T 'value') cil managed {
        .maxstack 2
        ldarg.1
        ldarg.2
        clt.un
        ret
    }

    .method public hidebysig virtual instance int32 BitCount(!T eEnum) cil managed {
        .maxstack 3
        ldarg.1
        dup
        ldc.i4.1
        shr
        ldc.i4 0x55555555
        and
        sub
        starg.s eEnum

        ldarg.1
        ldc.i4 0x33333333
        and
        ldarg.1
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

    .method public hidebysig virtual instance int32[] GetBitLocations(!T eEnum) cil managed {
        .maxstack 4
        .locals init (int32[] bits, int32 count, int32 subValue)
        ldarg.0
        ldarg.1
        call instance int32 class OptimizedEnum.NumCalcI4`1<!T>::BitCount(!T)
        newarr [mscorlib]System.Int32
        stloc.0
        ldc.i4.0
        stloc.1
        ldc.i4.1
        stloc.2
        ldarg.1
StartRepeat:
        brfalse EndRepeat

        ldloc.2
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
        ldarg.1
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

    .method public hidebysig virtual instance string GetOrDefault(string[] 'array', !T eEnum, int32 length) cil managed {
        .maxstack 2
        ldarg.2
        ldarg.3
        clt.un
        brfalse SkipGetValue
        ldarg.1
        ldarg.2
        ldelem string
        ret
SkipGetValue:
        ldarg.2
        call string [OptimizedEnum]OptimizedEnum.Utils::UInt32ToString(uint32)
        ret
    }

    .method public hidebysig virtual instance string GetOrNull(string[] 'array', !T eEnum, int32 length) cil managed {
        .maxstack 2
        ldarg.2
        ldarg.3
        clt.un
        brfalse SkipGetValue
        ldarg.1
        ldarg.2
        ldelem string
        ret
SkipGetValue:
        ldnull
        ret
    }

    .method public hidebysig virtual instance void SetValue(string[] 'array', !T eEnum, string 'value') cil managed {
        .maxstack 3
        ldarg.1
        ldarg.2
        ldarg.3
        stelem string
        ret
    }

    .method public hidebysig virtual instance int32 ToInt(!T eEnum) cil managed {
        .maxstack 1
        ldarg.1
        ret
    }

    .method public hidebysig virtual instance !T ParseAsNum(string str) cil managed {
        .maxstack 1
        ldarg.1
        call uint32 [OptimizedEnum]OptimizedEnum.Utils::ParseUInt32(string)
        ret
    }

    .method public hidebysig virtual instance !T GetSystemMinValue() cil managed {
        .maxstack 1
        ldc.i4.0
        ret
    }

    .method public hidebysig specialname rtspecialname instance void .ctor() cil managed {
        .maxstack 1
        ldarg.0
        call instance void class [OptimizedEnum]OptimizedEnum.NumCalc`1<!T>::.ctor()
        ret
    }
}

.class private auto ansi beforefieldinit OptimizedEnum.NumCalcI8`1<valuetype .ctor (class [mscorlib]System.Enum) T> extends class [OptimizedEnum]OptimizedEnum.NumCalc`1<!T> {
    .method public hidebysig virtual instance string GetString(!T eEnum) cil managed {
        .maxstack 1
        ldarg.1
        call string [OptimizedEnum]OptimizedEnum.Utils::Int64ToString(int64)
        ret
    }

    .method public hidebysig virtual instance bool Equal(!T eEnum, !T 'value') cil managed {
        .maxstack 2
        ldarg.1
        ldarg.2
        ceq
        ret
    }

    .method public hidebysig virtual instance bool Equal(!T eEnum, int32 'value') cil managed {
        .maxstack 2
        ldarg.1
        ldarg.2
        conv.i8
        ceq
        ret
    }
    
    .method public hidebysig virtual instance bool LessThan(!T eEnum, !T 'value') cil managed {
        .maxstack 2
        ldarg.1
        ldarg.2
        clt
        ret
    }

    .method public hidebysig virtual instance int32 BitCount(!T eEnum) cil managed {
        .maxstack 3
        ldarg.1
        dup
        ldc.i4.1
        shr
        ldc.i8 0x5555555555555555
        and
        sub
        starg.s eEnum

        ldarg.1
        ldc.i8 0x3333333333333333
        and
        ldarg.1
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

    .method public hidebysig virtual instance int32[] GetBitLocations(!T eEnum) cil managed {
        .maxstack 4
        .locals init (int32[] bits, int32 count, int32 subValue)
        ldarg.0
        ldarg.1
        call instance int32 class OptimizedEnum.NumCalcI4`1<!T>::BitCount(!T)
        newarr [mscorlib]System.Int32
        stloc.0
        ldc.i4.0
        stloc.1
        ldc.i4.1
        stloc.2
        ldarg.1
StartRepeat:
        brfalse EndRepeat

        ldloc.2
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
        ldarg.1
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

    .method public hidebysig virtual instance string GetOrDefault(string[] 'array', !T eEnum, int32 length) cil managed {
        .maxstack 2
        ldarg.2
        ldarg.3
        conv.i8
        clt.un
        brfalse SkipGetValue
        ldarg.1
        ldarg.2
        conv.i4
        ldelem string
        ret
SkipGetValue:
        ldarg.2
        call string [OptimizedEnum]OptimizedEnum.Utils::Int64ToString(int64)
        ret
    }

    .method public hidebysig virtual instance string GetOrNull(string[] 'array', !T eEnum, int32 length) cil managed {
        .maxstack 2
        ldarg.2
        ldarg.3
        conv.i8
        clt.un
        brfalse SkipGetValue
        ldarg.1
        ldarg.2
        conv.i4
        ldelem string
        ret
SkipGetValue:
        ldnull
        ret
    }

    .method public hidebysig virtual instance void SetValue(string[] 'array', !T eEnum, string 'value') cil managed {
        .maxstack 3
        ldarg.1
        ldarg.2
        conv.i4
        ldarg.3
        stelem string
        ret
    }

    .method public hidebysig virtual instance int32 ToInt(!T eEnum) cil managed {
        .maxstack 1
        ldarg.1
        conv.i4
        ret
    }

    .method public hidebysig virtual instance !T ParseAsNum(string str) cil managed {
        .maxstack 1
        ldarg.1
        call int64 [OptimizedEnum]OptimizedEnum.Utils::ParseInt64(string)
        ret
    }

    .method public hidebysig virtual instance !T GetSystemMinValue() cil managed {
        .maxstack 1
        ldc.i4.0
        conv.i8
        ret
    }

    .method public hidebysig specialname rtspecialname instance void .ctor() cil managed {
        .maxstack 1
        ldarg.0
        call instance void class [OptimizedEnum]OptimizedEnum.NumCalc`1<!T>::.ctor()
        ret
    }
}

.class private auto ansi beforefieldinit OptimizedEnum.NumCalcU8`1<valuetype .ctor (class [mscorlib]System.Enum) T> extends class [OptimizedEnum]OptimizedEnum.NumCalc`1<!T> {
    .method public hidebysig virtual instance string GetString(!T eEnum) cil managed {
        .maxstack 1
        ldarg.1
        call string [OptimizedEnum]OptimizedEnum.Utils::UInt64ToString(uint64)
        ret
    }

    .method public hidebysig virtual instance bool Equal(!T eEnum, !T 'value') cil managed {
        .maxstack 2
        ldarg.1
        ldarg.2
        ceq
        ret
    }

    .method public hidebysig virtual instance bool Equal(!T eEnum, int32 'value') cil managed {
        .maxstack 2
        ldarg.1
        ldarg.2
        conv.i8
        ceq
        ret
    }
    
    .method public hidebysig virtual instance bool LessThan(!T eEnum, !T 'value') cil managed {
        .maxstack 2
        ldarg.1
        ldarg.2
        clt.un
        ret
    }

    .method public hidebysig virtual instance int32 BitCount(!T eEnum) cil managed {
        .maxstack 3
        ldarg.1
        dup
        ldc.i4.1
        shr
        ldc.i8 0x5555555555555555
        and
        sub
        starg.s eEnum

        ldarg.1
        ldc.i8 0x3333333333333333
        and
        ldarg.1
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

    .method public hidebysig virtual instance int32[] GetBitLocations(!T eEnum) cil managed {
        .maxstack 4
        .locals init (int32[] bits, int32 count, int32 subValue)
        ldarg.0
        ldarg.1
        call instance int32 class OptimizedEnum.NumCalcI4`1<!T>::BitCount(!T)
        newarr [mscorlib]System.Int32
        stloc.0
        ldc.i4.0
        stloc.1
        ldc.i4.1
        stloc.2
        ldarg.1
StartRepeat:
        brfalse EndRepeat

        ldloc.2
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
        ldarg.1
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

    .method public hidebysig virtual instance string GetOrDefault(string[] 'array', !T eEnum, int32 length) cil managed {
        .maxstack 2
        ldarg.2
        ldarg.3
        conv.u8
        clt.un
        brfalse SkipGetValue
        ldarg.1
        ldarg.2
        conv.i4
        ldelem string
        ret
SkipGetValue:
        ldarg.2
        call string [OptimizedEnum]OptimizedEnum.Utils::UInt64ToString(uint64)
        ret
    }

    .method public hidebysig virtual instance string GetOrNull(string[] 'array', !T eEnum, int32 length) cil managed {
        .maxstack 2
        ldarg.2
        ldarg.3
        conv.u8
        clt.un
        brfalse SkipGetValue
        ldarg.1
        ldarg.2
        conv.i4
        ldelem string
        ret
SkipGetValue:
        ldnull
        ret
    }

    .method public hidebysig virtual instance void SetValue(string[] 'array', !T eEnum, string 'value') cil managed {
        .maxstack 3
        ldarg.1
        ldarg.2
        conv.i4
        ldarg.3
        stelem string
        ret
    }

    .method public hidebysig virtual instance int32 ToInt(!T eEnum) cil managed {
        .maxstack 1
        ldarg.1
        conv.i4
        ret
    }

    .method public hidebysig virtual instance !T ParseAsNum(string str) cil managed {
        .maxstack 1
        ldarg.1
        call uint64 [OptimizedEnum]OptimizedEnum.Utils::ParseUInt64(string)
        ret
    }

    .method public hidebysig virtual instance !T GetSystemMinValue() cil managed {
        .maxstack 1
        ldc.i4.0
        conv.u8
        ret
    }

    .method public hidebysig specialname rtspecialname instance void .ctor() cil managed {
        .maxstack 1
        ldarg.0
        call instance void class [OptimizedEnum]OptimizedEnum.NumCalc`1<!T>::.ctor()
        ret
    }
}

.class private auto ansi beforefieldinit OptimizedEnum.NumCalcChar`1<valuetype .ctor (class [mscorlib]System.Enum) T> extends class [OptimizedEnum]OptimizedEnum.NumCalc`1<!T> {
    .method public hidebysig virtual instance string GetString(!T eEnum) cil managed {
        .maxstack 2
        ldarg.1
        ldc.i4.1
        newobj instance void System.String::.ctor(char, int32)
        ret
    }

    .method public hidebysig virtual instance bool Equal(!T eEnum, !T 'value') cil managed {
        .maxstack 2
        ldarg.1
        ldarg.2
        ceq
        ret
    }

    .method public hidebysig virtual instance bool Equal(!T eEnum, int32 'value') cil managed {
        .maxstack 2
        ldarg.1
        ldarg.2
        ceq
        ret
    }
    
    .method public hidebysig virtual instance bool LessThan(!T eEnum, !T 'value') cil managed {
        .maxstack 2
        ldarg.1
        ldarg.2
        clt
        ret
    }

    .method public hidebysig virtual instance int32 BitCount(!T eEnum) cil managed {
        .maxstack 3
        ldarg.1
        dup
        ldc.i4.1
        shr
        ldc.i4 0x55555555
        and
        sub
        starg.s eEnum

        ldarg.1
        ldc.i4 0x33333333
        and
        ldarg.1
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

    .method public hidebysig virtual instance int32[] GetBitLocations(!T eEnum) cil managed {
        .maxstack 4
        .locals init (int32[] bits, int32 count, int32 subValue)
        ldarg.0
        ldarg.1
        call instance int32 class OptimizedEnum.NumCalcI4`1<!T>::BitCount(!T)
        newarr [mscorlib]System.Int32
        stloc.0
        ldc.i4.0
        stloc.1
        ldc.i4.1
        stloc.2
        ldarg.1
StartRepeat:
        brfalse EndRepeat

        ldloc.2
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
        ldarg.1
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

    .method public hidebysig virtual instance string GetOrDefault(string[] 'array', !T eEnum, int32 length) cil managed {
        .maxstack 2
        ldarg.2
        ldarg.3
        clt.un
        brfalse SkipGetValue
        ldarg.1
        ldarg.2
        ldelem string
        ret
SkipGetValue:
        ldarg.2
        ldc.i4.1
        newobj instance void System.String::.ctor(char, int32)
        ret
    }

    .method public hidebysig virtual instance string GetOrNull(string[] 'array', !T eEnum, int32 length) cil managed {
        .maxstack 2
        ldarg.2
        ldarg.3
        clt.un
        brfalse SkipGetValue
        ldarg.1
        ldarg.2
        ldelem string
        ret
SkipGetValue:
        ldnull
        ret
    }

    .method public hidebysig virtual instance void SetValue(string[] 'array', !T eEnum, string 'value') cil managed {
        .maxstack 3
        ldarg.1
        ldarg.2
        ldarg.3
        stelem string
        ret
    }

    .method public hidebysig virtual instance int32 ToInt(!T eEnum) cil managed {
        .maxstack 1
        ldarg.1
        ret
    }

    .method public hidebysig virtual instance !T ParseAsNum(string str) cil managed {
        .maxstack 3
        ldarg.1
        callvirt instance int32 [mscorlib]System.String::get_Length()
        ldc.i4.1
        bne.un Throw

        ldarg.1
        ldc.i4.0
        callvirt instance char [mscorlib]System.String::get_Chars(int32)
        ret
Throw:
        ldstr "Invalid character '{0}' in string '{1}'"
        ldarg.1
        ldc.i4.0
        callvirt instance char [mscorlib]System.String::get_Chars(int32)
        box [mscorlib]System.Char
        ldarg.1
        call string [mscorlib]System.String::Format(string, object, object)
        newobj instance void [mscorlib]System.FormatException::.ctor(string)
        throw
    }

    .method public hidebysig virtual instance !T GetSystemMinValue() cil managed {
        .maxstack 1
        ldc.i4.0
        ret
    }

    .method public hidebysig specialname rtspecialname instance void .ctor() cil managed {
        .maxstack 1
        ldarg.0
        call instance void class [OptimizedEnum]OptimizedEnum.NumCalc`1<!T>::.ctor()
        ret
    }
}