.assembly extern mscorlib {
  .publickeytoken = ( b7 7a 5c 56 19 34 e0 89 )
  .ver 4:0:0:0
}

.assembly extern OptimizedEnum {
  .ver 1:0:0:0
}

.assembly OptimizedEnum_IL {
  .custom instance void [mscorlib]System.Reflection.AssemblyTitleAttribute::.ctor(string)
    = (
      01 00 10 4f 70 74 69 6d 69 7a 65 64 45 6e 75 6d // ...OptimizedEnum
      2d 49 4c 00 00                                  // -IL..
    )
    // string('OptimizedEnum-IL')
  .custom instance void [mscorlib]System.Reflection.AssemblyDescriptionAttribute::.ctor(string)
    = (
      01 00 45 49 74 20 6f 70 74 69 6d 69 7a 65 73 20 // ..EIt optimizes
      74 68 65 20 65 6e 75 6d 20 73 6f 20 74 68 61 74 // the enum so that
      20 69 74 20 63 61 6e 20 62 65 20 75 73 65 64 20 //  it can be used
      77 69 74 68 20 62 65 74 74 65 72 20 70 65 72 66 // with better perf
      6f 72 6d 61 6e 63 65 2e 00 00                   // ormance...
    )
    // string('It optimizes the enum so that it can be used with better performance.')
  .custom instance void [mscorlib]System.Reflection.AssemblyCompanyAttribute::.ctor(string)
    = (01 00 08 4a 6f 6e 67 79 65 6f 6c 00 00 ) // ...Jongyeol..
    // string('Jongyeol')
  .custom instance void [mscorlib]System.Reflection.AssemblyProductAttribute::.ctor(string)
    = (
      01 00 0d 4f 70 74 69 6d 69 7a 65 64 45 6e 75 6d // ...OptimizedEnum
      00 00                                           // ..
    )
    // string('OptimizedEnum')
  .custom instance void [mscorlib]System.Reflection.AssemblyCopyrightAttribute::.ctor(string)
    = (
      01 00 1a 43 6f 70 79 72 69 67 68 74 20 c2 a9 20 // ...Copyright Â©
      4a 6f 6e 67 79 65 6f 6c 20 32 30 32 35 00 00    // Jongyeol 2025..
    )
    // string('Copyright © Jongyeol 2025')
  .custom instance void [mscorlib]System.Runtime.InteropServices.ComVisibleAttribute::.ctor(bool)
    = (01 00 00 00 00 ) // .....
    // string('')
  .custom instance void [mscorlib]System.Reflection.AssemblyFileVersionAttribute::.ctor(string)
    = (01 00 07 31 2e 30 2e 30 2e 30 00 00 ) // ...1.0.0.0..
    // string('1.0.0.0')
  .custom instance void [mscorlib]System.Runtime.Versioning.TargetFrameworkAttribute::.ctor(string)
    = (
      01 00 1c 2e 4e 45 54 46 72 61 6d 65 77 6f 72 6b // ....NETFramework
      2c 56 65 72 73 69 6f 6e 3d 76 34 2e 38 2e 31 01 // ,Version=v4.8.1.
      00 54 0e 14 46 72 61 6d 65 77 6f 72 6b 44 69 73 // .T..FrameworkDis
      70 6c 61 79 4e 61 6d 65 14 2e 4e 45 54 20 46 72 // playName..NET Fr
      61 6d 65 77 6f 72 6b 20 34 2e 38 2e 31          // amework 4.8.1
    )
    // string('.NETFramework,Version=v4.8.1')
    // property string 'FrameworkDisplayName' = string('.NET Framework 4.8.1')
  .hash algorithm 0x00008004
  .ver 1:0:0:0
}