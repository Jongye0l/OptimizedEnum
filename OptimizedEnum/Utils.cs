using System;

namespace OptimizedEnum;

static class Utils {
    private static readonly double _log2 = Math.Log(2);

    public static double Log2(double value) {
        return Math.Log(value) / _log2;
    }
}