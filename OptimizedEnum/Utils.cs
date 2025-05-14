using System;

namespace OptimizedEnum;

static class Utils {
    private static readonly double _log2 = Math.Log(2);

    internal static double Log2(double value) => Math.Log(value) / _log2;
    public static string Int32ToString(this int value) {
        if(value == 0) return "0";
        bool isNegative = value < 0;
        if(isNegative) value = -value;
        int i;
        char[] buffer = new char[i = isNegative ? 11 : 10];
        while(value > 0) {
            buffer[--i] = (char)('0' + value % 10);
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
            buffer[--i] = (char)('0' + value % 10);
            value /= 10;
        }
        return new string(buffer, i, 10 - i);
    }
    
    public static string Int64ToString(this long value) {
        if(value == 0) return "0";
        bool isNegative = value < 0;
        if(isNegative) value = -value;
        int i;
        char[] buffer = new char[i = isNegative ? 21 : 20];
        while(value > 0) {
            buffer[--i] = (char)('0' + value % 10);
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
            buffer[--i] = (char)('0' + value % 10);
            value /= 10;
        }
        return new string(buffer, i, 20 - i);
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
            if(ch is < '0' or > '9') throw new FormatException($"Invalid character '{ch}' in string '{value}'");
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
            if(ch is < '0' or > '9') throw new FormatException($"Invalid character '{ch}' in string '{value}'");
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
            if(ch is < '0' or > '9') throw new FormatException($"Invalid character '{ch}' in string '{value}'");
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
            if(ch is < '0' or > '9') throw new FormatException($"Invalid character '{ch}' in string '{value}'");
            result = result * 10 + (ulong) (ch - '0');
            i++;
        }
        return result;
    }
}