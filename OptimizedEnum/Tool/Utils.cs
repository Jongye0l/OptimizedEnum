using System;
using System.Runtime.CompilerServices;

namespace OptimizedEnum.Tool;

public static class Utils {
#if NETCOREAPP2_0 || NETCOREAPP2_1
    private static readonly float _log2 = MathF.Log(2);
#elif !NETCOREAPP3_0 && !NET5_0
    private static readonly double _log2 = Math.Log(2);
#endif
    public static float Log2(float value) {
#if NETCOREAPP2_0 || NETCOREAPP2_1
        return MathF.Log(value) / _log2;
#elif NETCOREAPP3_0 || NET5_0
        return MathF.Log2(value);
#else
        return (float) (Math.Log(value) / _log2);
#endif
    }

    public static double Log2(double value) {
#if NETCOREAPP3_0 || NET5_0
        return Math.Log2(value);
#else
        return Math.Log(value) / _log2;
#endif
    }

    public static string Int32ToString(this int value) {
        if(value == 0) return "0";
        if(value == int.MinValue) return "-2147483648";
        bool isNegative = value < 0;
        if(isNegative) value = -value;
        int i;
        char[] buffer = new char[i = isNegative ? 11 : 10];
        while(value > 0) {
            buffer[--i] = (char) ('0' + value % 10);
            value /= 10;
        }
        if(isNegative) buffer[--i] = '-';
        return new string(buffer, i, buffer.Length - i);
    }

    public static string UInt32ToString(this uint value) {
        if(value == 0) return "0";
        int i;
        char[] buffer = new char[i = 10];
        while(value > 0) {
            buffer[--i] = (char) ('0' + value % 10);
            value /= 10;
        }
        return new string(buffer, i, 10 - i);
    }

    public static string Int64ToString(this long value) {
        if(value == 0) return "0";
        if(value == long.MinValue) return "-9223372036854775808";
        bool isNegative = value < 0;
        if(isNegative) value = -value;
        int i;
        char[] buffer = new char[i = isNegative ? 21 : 20];
        while(value > 0) {
            buffer[--i] = (char) ('0' + value % 10);
            value /= 10;
        }
        if(isNegative) buffer[--i] = '-';
        return new string(buffer, i, buffer.Length - i);
    }

    public static string UInt64ToString(this ulong value) {
        if(value == 0) return "0";
        int i;
        char[] buffer = new char[i = 20];
        while(value > 0) {
            buffer[--i] = (char) ('0' + value % 10);
            value /= 10;
        }
        return new string(buffer, i, 20 - i);
    }

    public static string Int32ToHexString(this int value, int length) => UInt32ToHexString((uint) value, length);

    public static string UInt32ToHexString(this uint value, int length) {
        int i;
        char[] buffer = new char[i = length];
        while(value > 0) {
            uint val = value % 16;
            buffer[--i] = val > 9 ? (char) ('7' + val) : (char) ('0' + val); // 'A' - (char) 10 = '7'
            value /= 16;
        }
        while(i > 0) buffer[--i] = '0';
        return new string(buffer, 0, length);
    }

    public static string Int64ToHexString(this long value, int length) => UInt64ToHexString((ulong) value, length);

    public static string UInt64ToHexString(this ulong value, int length) {
        int i;
        char[] buffer = new char[i = length];
        while(value > 0) {
            uint val = (uint) (value % 16);
            buffer[--i] = val > 9 ? (char) ('7' + val) : (char) ('0' + val); // 'A' - (char) 10 = '7'
            value /= 16;
        }
        while(i > 0) buffer[--i] = '0';
        return new string(buffer, 0, length);
    }

    public static int ParseInt32(this string value) {
        int result = 0;
        bool isNegative = false;
        int i = 0;
        if(value[i] == '-') {
            isNegative = true;
            i++;
        } else if(value[i] == '+') i++;
        while(i < value.Length) {
            char ch = value[i];
            if(ch is < '0' or > '9') throw new ArgumentException($"Invalid enum value: {value}");
            result = result * 10 + (ch - '0');
            i++;
        }
        return isNegative ? -result : result;
    }

    public static uint ParseUInt32(this string value) {
        uint result = 0;
        int i = 0;
        if(value[i] == '+') i++;
        while(i < value.Length) {
            char ch = value[i];
            if(ch is < '0' or > '9') throw new ArgumentException($"Invalid enum value: {value}");
            result = result * 10 + (uint) (ch - '0');
            i++;
        }
        return result;
    }

    public static long ParseInt64(this string value) {
        long result = 0;
        bool isNegative = false;
        int i = 0;
        if(value[i] == '-') {
            isNegative = true;
            i++;
        } else if(value[i] == '+') i++;
        while(i < value.Length) {
            char ch = value[i];
            if(ch is < '0' or > '9') throw new ArgumentException($"Invalid enum value: {value}");
            result = result * 10 + (ch - '0');
            i++;
        }
        return isNegative ? -result : result;
    }

    public static ulong ParseUInt64(this string value) {
        ulong result = 0;
        int i = 0;
        if(value[i] == '+') i++;
        while(i < value.Length) {
            char ch = value[i];
            if(ch is < '0' or > '9') throw new ArgumentException($"Invalid enum value: {value}");
            result = result * 10 + (ulong) (ch - '0');
            i++;
        }
        return result;
    }

    public static bool TryParseInt32(this string value, out int result) {
        result = 0;
        bool isNegative = false;
        int i = 0;
        if(value[i] == '-') {
            isNegative = true;
            i++;
        } else if(value[i] == '+') i++;
        while(i < value.Length) {
            char ch = value[i];
            if(ch is < '0' or > '9') return false;
            result = result * 10 + (ch - '0');
            i++;
        }
        if(isNegative) result = -result;
        return true;
    }

    public static bool TryParseUInt32(this string value, out uint result) {
        result = 0;
        int i = 0;
        if(value[i] == '+') i++;
        while(i < value.Length) {
            char ch = value[i];
            if(ch is < '0' or > '9') return false;
            result = result * 10 + (uint) (ch - '0');
            i++;
        }
        return true;
    }

    public static bool TryParseInt64(this string value, out long result) {
        result = 0;
        bool isNegative = false;
        int i = 0;
        if(value[i] == '-') {
            isNegative = true;
            i++;
        } else if(value[i] == '+') i++;
        while(i < value.Length) {
            char ch = value[i];
            if(ch is < '0' or > '9') return false;
            result = result * 10 + (ch - '0');
            i++;
        }
        if(isNegative) result = -result;
        return true;
    }

    public static bool TryParseUInt64(this string value, out ulong result) {
        result = 0;
        int i = 0;
        if(value[i] == '+') i++;
        while(i < value.Length) {
            char ch = value[i];
            if(ch is < '0' or > '9') return false;
            result = result * 10 + (ulong) (ch - '0');
            i++;
        }
        return true;
    }
#if NETCOREAPP2_1_OR_GREATER || NET5_0
    public static int ParseInt32(this ReadOnlySpan<char> value) {
        int result = 0;
        bool isNegative = false;
        int i = 0;
        if(value[i] == '-') {
            isNegative = true;
            i++;
        } else if(value[i] == '+') i++;
        while(i < value.Length) {
            char ch = value[i];
            if(ch is < '0' or > '9') throw new ArgumentException("Invalid enum value: " + new string(value));
            result = result * 10 + (ch - '0');
            i++;
        }
        return isNegative ? -result : result;
    }

    public static uint ParseUInt32(this ReadOnlySpan<char> value) {
        uint result = 0;
        int i = 0;
        if(value[i] == '+') i++;
        while(i < value.Length) {
            char ch = value[i];
            if(ch is < '0' or > '9') throw new ArgumentException("Invalid enum value: " + new string(value));
            result = result * 10 + (uint) (ch - '0');
            i++;
        }
        return result;
    }

    public static long ParseInt64(this ReadOnlySpan<char> value) {
        long result = 0;
        bool isNegative = false;
        int i = 0;
        if(value[i] == '-') {
            isNegative = true;
            i++;
        } else if(value[i] == '+') i++;
        while(i < value.Length) {
            char ch = value[i];
            if(ch is < '0' or > '9') throw new ArgumentException("Invalid enum value: " + new string(value));
            result = result * 10 + (ch - '0');
            i++;
        }
        return isNegative ? -result : result;
    }

    public static ulong ParseUInt64(this ReadOnlySpan<char> value) {
        ulong result = 0;
        int i = 0;
        if(value[i] == '+') i++;
        while(i < value.Length) {
            char ch = value[i];
            if(ch is < '0' or > '9') throw new ArgumentException("Invalid enum value: " + new string(value));
            result = result * 10 + (ulong) (ch - '0');
            i++;
        }
        return result;
    }

    public static bool TryParseInt32(this ReadOnlySpan<char> value, out int result) {
        result = 0;
        bool isNegative = false;
        int i = 0;
        if(value[i] == '-') {
            isNegative = true;
            i++;
        } else if(value[i] == '+') i++;
        while(i < value.Length) {
            char ch = value[i];
            if(ch is < '0' or > '9') return false;
            result = result * 10 + (ch - '0');
            i++;
        }
        if(isNegative) result = -result;
        return true;
    }

    public static bool TryParseUInt32(this ReadOnlySpan<char> value, out uint result) {
        result = 0;
        int i = 0;
        if(value[i] == '+') i++;
        while(i < value.Length) {
            char ch = value[i];
            if(ch is < '0' or > '9') return false;
            result = result * 10 + (uint) (ch - '0');
            i++;
        }
        return true;
    }

    public static bool TryParseInt64(this ReadOnlySpan<char> value, out long result) {
        result = 0;
        bool isNegative = false;
        int i = 0;
        if(value[i] == '-') {
            isNegative = true;
            i++;
        } else if(value[i] == '+') i++;
        while(i < value.Length) {
            char ch = value[i];
            if(ch is < '0' or > '9') return false;
            result = result * 10 + (ch - '0');
            i++;
        }
        if(isNegative) result = -result;
        return true;
    }

    public static bool TryParseUInt64(this ReadOnlySpan<char> value, out ulong result) {
        result = 0;
        int i = 0;
        if(value[i] == '+') i++;
        while(i < value.Length) {
            char ch = value[i];
            if(ch is < '0' or > '9') return false;
            result = result * 10 + (ulong) (ch - '0');
            i++;
        }
        return true;
    }
#endif

    public static string GetNumberStringFast<T>(this T eEnum) where T : struct, Enum {
        return
#if NETCOREAPP || NET5_0_OR_GREATER
            eEnum.GetNumberString();
#else
            eEnum.GetNumberStringCustom();
#endif
    }

    public static string GetHexStringFast<T>(this T eEnum) where T : struct, Enum {
        return 
#if NETCOREAPP || NET5_0_OR_GREATER
            eEnum.GetHexString();
#else
            eEnum.GetHexStringCustom();
#endif
    }

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern bool HasAnyFlags<T>(this T flags) where T : struct, Enum;

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern bool HasAnyFlags<T>(this T flags, T otherFlags) where T : struct, Enum;

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern bool HasAllFlags<T>(this T flags, T otherFlags) where T : struct, Enum;

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern T ToggleFlags<T>(this T flags, T otherFlags) where T : struct, Enum;

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern T CommonFlags<T>(this T flags, T otherFlags) where T : struct, Enum;

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern T CombineFlags<T>(this T flags, T otherFlags) where T : struct, Enum;

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern T RemoveFlags<T>(this T flags, T otherFlags) where T : struct, Enum;

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern int AsInteger<T>(this T flags) where T : struct, Enum;

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern uint AsUnsignedInteger<T>(this T flags) where T : struct, Enum;

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern long AsLong<T>(this T flags) where T : struct, Enum;

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern ulong AsUnsignedLong<T>(this T flags) where T : struct, Enum;

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern char AsChar<T>(this T flags) where T : struct, Enum;

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern float AsFloat<T>(this T flags) where T : struct, Enum;

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern double AsDouble<T>(this T flags) where T : struct, Enum;

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern float AsFloatUnsigned<T>(this T flags) where T : struct, Enum;

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern double AsDoubleUnsigned<T>(this T flags) where T : struct, Enum;

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern T AsUnsigned<T>(this T flags) where T : struct, Enum;

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern T2 As<T, T2>(this T flags) where T : struct where T2 : struct;

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern T As<T>(this object value);

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern bool Equal<T>(this T flags, T otherFlags) where T : struct, Enum;

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern bool LessThan<T>(this T flags, T otherFlags) where T : struct, Enum;

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern bool LessThanUnsigned<T>(this T flags, T otherFlags) where T : struct, Enum;

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern bool GreaterThan<T>(this T flags, T otherFlags) where T : struct, Enum;

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern bool GreaterThanUnsigned<T>(this T flags, T otherFlags) where T : struct, Enum;

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern int BitCount<T>(this T eEnum) where T : struct, Enum;

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern int[] GetBitLocations<T>(this T eEnum) where T : struct, Enum;

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern int GetSize<T>() where T : struct, Enum;

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern string GetOrDefault<T>(string[] array, T eEnum, uint length) where T : struct, Enum;

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern string? GetOrNull<T>(string[] array, T eEnum, uint length) where T : struct, Enum;

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern T[] GetSystemEnumValues<T>() where T : struct, Enum;

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern string GetNumberStringCustom<T>(this T eEnum) where T : struct, Enum;

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern string GetNumberString<T>(this T eEnum) where T : struct, Enum;

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern string GetHexStringCustom<T>(this T eEnum) where T : struct, Enum;

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern string GetHexString<T>(this T eEnum) where T : struct, Enum;

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern T ParseAsNumber<T>(this string value) where T : struct, Enum;

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern bool TryParseAsNumber<T>(this string value, out T result) where T : struct, Enum;
    
#if NETCOREAPP2_1_OR_GREATER || NET5_0

    [MethodImpl(MethodImplOptions.ForwardRef)]
    public static extern T ParseAsNumber<T>(this ReadOnlySpan<char> value) where T : struct, Enum;

    [MethodImpl(MethodImplOptions.ForwardRef)]
    public static extern bool TryParseAsNumber<T>(this ReadOnlySpan<char> value, out T result) where T : struct, Enum;
    
#endif

    [MethodImpl((MethodImplOptions) 16)] // ForwardRef
    public static extern T GetZero<T>();
}