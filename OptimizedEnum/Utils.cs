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
        char[] buffer = new char[i = 11];
        while(value > 0) {
            buffer[--i] = (char)('0' + value % 10);
            value /= 10;
        }
        if(isNegative) buffer[--i] = '-';
        return new string(buffer, i, 11 - i);
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
        char[] buffer = new char[i = 21];
        while(value > 0) {
            buffer[--i] = (char)('0' + value % 10);
            value /= 10;
        }
        if(isNegative) buffer[--i] = '-';
        return new string(buffer, i, 21 - i);
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
}