.class private auto ansi beforefieldinit OptimizedEnum.NumCalcI4`1<valuetype .ctor (class [mscorlib]System.Enum) T> extends class [OptimizedEnum]OptimizedEnum.NumCalc`1<!T> {
    .method public hidebysig virtual instance string GetString(!T eEnum) cil managed {
        .maxstack 3
        ldarg.1
        call instance string [mscorlib]System.Int32::ToString()
        ret
    }

    .method public hidebysig virtual instance bool GetBool(!T eEnum) cil managed {
        .maxstack 1
        ldarg.1
        ret
    }

    .method public hidebysig virtual instance bool Equal(!T eEnum, int32 'value') cil managed {
        .maxstack 2
        ldarg.1
        ldarg.2
        ceq
        ret
    }

    .method public hidebysig virtual instance bool Equal(!T eEnum, unsigned int32 'value') cil managed {
        .maxstack 2
        ldarg.1
        ldarg.2
        ceq
        ret
    }

    .method public hidebysig virtual instance bool Equal(!T eEnum, int64 'value') cil managed {
        .maxstack 2
        ldarg.1
        conv.i8
        ldarg.2
        ceq
        ret
    }

    .method public hidebysig virtual instance bool Equal(!T eEnum, unsigned int64 'value') cil managed {
        .maxstack 2
        ldarg.1
        conv.u8
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
    
    .method public hidebysig virtual instance bool LessThan(!T eEnum, int32 'value') cil managed {
        .maxstack 2
        ldarg.1
        ldarg.2
        clt
        ret
    }
    
    .method public hidebysig virtual instance bool GreaterThan(!T eEnum, !T 'value') cil managed {
        .maxstack 2
        ldarg.2
        ldarg.1
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

    .method public hidebysig virtual instance bool IsPositive(!T eEnum) cil managed {
        .maxstack 2
        ldarg.1
        ldc.i4.0
        clt
        ldc.i4.0
        ceq
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
        .maxstack 3
        ldarg.1
        call instance string [mscorlib]System.UInt32::ToString()
        ret
    }

    .method public hidebysig virtual instance bool GetBool(!T eEnum) cil managed {
        .maxstack 1
        ldarg.1
        ret
    }

    .method public hidebysig virtual instance bool Equal(!T eEnum, int32 'value') cil managed {
        .maxstack 2
        ldarg.1
        ldarg.2
        ceq
        ret
    }

    .method public hidebysig virtual instance bool Equal(!T eEnum, unsigned int32 'value') cil managed {
        .maxstack 2
        ldarg.1
        ldarg.2
        ceq
        ret
    }

    .method public hidebysig virtual instance bool Equal(!T eEnum, int64 'value') cil managed {
        .maxstack 2
        ldarg.1
        conv.i8
        ldarg.2
        ceq
        ret
    }

    .method public hidebysig virtual instance bool Equal(!T eEnum, unsigned int64 'value') cil managed {
        .maxstack 2
        ldarg.1
        conv.u8
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
    
    .method public hidebysig virtual instance bool LessThan(!T eEnum, int32 'value') cil managed {
        .maxstack 2
        ldarg.1
        ldarg.2
        clt
        ret
    }
    
    .method public hidebysig virtual instance bool GreaterThan(!T eEnum, !T 'value') cil managed {
        .maxstack 2
        ldarg.2
        ldarg.1
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

    .method public hidebysig virtual instance bool IsPositive(!T eEnum) cil managed {
        .maxstack 1
        ldc.i4.1
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
        .maxstack 3
        ldarg.1
        call instance string [mscorlib]System.Int64::ToString()
        ret
    }

    .method public hidebysig virtual instance bool GetBool(!T eEnum) cil managed {
        .maxstack 1
        ldarg.1
        ldc.i4.0
        conv.i8
        ceq
        not
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

    .method public hidebysig virtual instance bool Equal(!T eEnum, unsigned int32 'value') cil managed {
        .maxstack 2
        ldarg.1
        ldarg.2
        conv.i8
        ceq
        ret
    }

    .method public hidebysig virtual instance bool Equal(!T eEnum, int64 'value') cil managed {
        .maxstack 2
        ldarg.1
        ldarg.2
        ceq
        ret
    }

    .method public hidebysig virtual instance bool Equal(!T eEnum, unsigned int64 'value') cil managed {
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
    
    .method public hidebysig virtual instance bool LessThan(!T eEnum, int32 'value') cil managed {
        .maxstack 2
        ldarg.1
        ldarg.2
        conv.i8
        clt
        ret
    }
    
    .method public hidebysig virtual instance bool GreaterThan(!T eEnum, !T 'value') cil managed {
        .maxstack 2
        ldarg.2
        ldarg.1
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

    .method public hidebysig virtual instance bool IsPositive(!T eEnum) cil managed {
        .maxstack 2
        ldarg.1
        ldc.i4.0
        conv.i8
        clt
        ldc.i4.0
        ceq
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
        .maxstack 3
        ldarg.1
        call instance string [mscorlib]System.UInt64::ToString()
        ret
    }

    .method public hidebysig virtual instance bool GetBool(!T eEnum) cil managed {
        .maxstack 1
        ldarg.1
        ldc.i4.0
        conv.u8
        cgt
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

    .method public hidebysig virtual instance bool Equal(!T eEnum, unsigned int32 'value') cil managed {
        .maxstack 2
        ldarg.1
        ldarg.2
        conv.i8
        ceq
        ret
    }

    .method public hidebysig virtual instance bool Equal(!T eEnum, int64 'value') cil managed {
        .maxstack 2
        ldarg.1
        ldarg.2
        ceq
        ret
    }

    .method public hidebysig virtual instance bool Equal(!T eEnum, unsigned int64 'value') cil managed {
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
    
    .method public hidebysig virtual instance bool LessThan(!T eEnum, int32 'value') cil managed {
        .maxstack 2
        ldarg.1
        ldarg.2
        conv.u8
        clt
        ret
    }
    
    .method public hidebysig virtual instance bool GreaterThan(!T eEnum, !T 'value') cil managed {
        .maxstack 2
        ldarg.2
        ldarg.1
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

    .method public hidebysig virtual instance bool IsPositive(!T eEnum) cil managed {
        .maxstack 1
        ldc.i4.1
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

    .method public hidebysig virtual instance bool GetBool(!T eEnum) cil managed {
        .maxstack 1
        ldarg.1
        ret
    }

    .method public hidebysig virtual instance bool Equal(!T eEnum, int32 'value') cil managed {
        .maxstack 2
        ldarg.1
        ldarg.2
        ceq
        ret
    }

    .method public hidebysig virtual instance bool Equal(!T eEnum, unsigned int32 'value') cil managed {
        .maxstack 2
        ldarg.1
        ldarg.2
        ceq
        ret
    }

    .method public hidebysig virtual instance bool Equal(!T eEnum, int64 'value') cil managed {
        .maxstack 2
        ldarg.1
        conv.i8
        ldarg.2
        ceq
        ret
    }

    .method public hidebysig virtual instance bool Equal(!T eEnum, unsigned int64 'value') cil managed {
        .maxstack 2
        ldarg.1
        conv.u8
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
    
    .method public hidebysig virtual instance bool LessThan(!T eEnum, int32 'value') cil managed {
        .maxstack 2
        ldarg.1
        ldarg.2
        clt
        ret
    }
    
    .method public hidebysig virtual instance bool GreaterThan(!T eEnum, !T 'value') cil managed {
        .maxstack 2
        ldarg.2
        ldarg.1
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

    .method public hidebysig virtual instance bool IsPositive(!T eEnum) cil managed {
        .maxstack 2
        ldarg.1
        ldc.i4.0
        clt
        ldc.i4.0
        ceq
        ret
    }

    .method public hidebysig specialname rtspecialname instance void .ctor() cil managed {
        .maxstack 1
        ldarg.0
        call instance void class [OptimizedEnum]OptimizedEnum.NumCalc`1<!T>::.ctor()
        ret
    }
}