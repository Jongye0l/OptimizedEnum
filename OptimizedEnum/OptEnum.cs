using System;
using OptimizedEnum.Tool;

namespace OptimizedEnum;

public static class OptEnum {
    public static string GetName<T>(this T eEnum) where T : struct, Enum {
        return EnumData<T>.enumType switch {
            EnumType.Sorted => ILUtils.GetOrNull(SortedEnumData<T>.Names, eEnum, SortedEnumData<T>.Length),
            EnumType.Unsorted => UnsortedEnumData<T>.dictionary[eEnum],
            EnumType.Flag => FlagEnumData<T>.dictionary != null && eEnum.BitCount() > 1        ? FlagEnumData<T>.dictionary[eEnum] ?? eEnum.GetNumberString() :
                             eEnum.AsLong() == 0                                               ? FlagEnumData<T>.zeroString :
                             !EnumData<T>.AllFlags.HasAllFlags(eEnum) || eEnum.BitCount() != 1 ? eEnum.GetNumberString() :
                             eEnum.AsLong() == 1                                               ? FlagEnumData<T>.FlagEnums[0] : FlagEnumData<T>.FlagEnums[(int) Utils.Log2(eEnum.AsDouble())],
            _ => throw new NotSupportedException()
        };
    }

    public static string GetString<T>(this T eEnum) where T : struct, Enum {
        return EnumData<T>.enumType switch {
            EnumType.Sorted => ILUtils.GetOrDefault(SortedEnumData<T>.Names, eEnum, SortedEnumData<T>.Length),
            EnumType.Unsorted => UnsortedEnumData<T>.dictionary[eEnum] ?? eEnum.GetNumberString(),
            EnumType.Flag => FlagEnumData<T>.dictionary == null ? FlagEnumData<T>.GetStringNormal(eEnum) : FlagEnumData<T>.GetStringDict(eEnum),
            _ => throw new NotSupportedException()
        };
    }

    public static T Parse<T>(string str) where T : struct, Enum {
        return EnumData<T>.NameDictionary.GetValue(str);
    }

    public static T Parse<T>(string str, bool ignoreCase) where T : struct, Enum {
        return ignoreCase ? EnumData<T>.NameDictionary.GetValueIgnoreCase(str) : EnumData<T>.NameDictionary.GetValue(str);
    }

    public static bool TryParse<T>(string str, out T eEnum) where T : struct, Enum {
        return EnumData<T>.NameDictionary.TryGetValue(str, out eEnum);
    }

    public static bool TryParse<T>(string str, bool ignoreCase, out T eEnum) where T : struct, Enum {
        return ignoreCase ? EnumData<T>.NameDictionary.TryGetValueIgnoreCase(str, out eEnum) : EnumData<T>.NameDictionary.TryGetValue(str, out eEnum);
    }
    
    public static T[] GetValues<T>() where T : struct, Enum {
        return EnumData<T>.Values;
    }

    public static bool IsDefined<T>(T eEnum) where T : struct, Enum {
        return EnumData<T>.enumType switch {
            EnumType.Sorted => (uint) eEnum.AsInteger() < SortedEnumData<T>.Length,
            EnumType.Unsorted => UnsortedEnumData<T>.dictionary[eEnum] != null,
            EnumType.Flag => FlagEnumData<T>.IsDefined(eEnum),
            _ => throw new NotSupportedException()
        };
    }

    private static void CheckType(Type type) {
        if(!type.IsEnum) throw new ArgumentException($"Type {type} is not an enum.");
    }

    public static string GetName(object eEnum) => GetName(eEnum.GetType(), eEnum);

    public static string GetName(Type type, object eEnum) {
        CheckType(type);
        return EnumData.EnumDataDictionary.GetOrCreate(type).GetName(eEnum);
    }
    
    public static string GetString(object eEnum) => GetString(eEnum.GetType(), eEnum);
    
    public static string GetString(Type type, object eEnum) {
        CheckType(type);
        return EnumData.EnumDataDictionary.GetOrCreate(type).GetString(eEnum);
    }
    
    public static object Parse(string str) => Parse(str.GetType(), str);
    
    public static object Parse(Type type, string str) {
        CheckType(type);
        return EnumData.EnumDataDictionary.GetOrCreate(type).ParseObj(str);
    }
    
    public static object Parse(string str, bool ignoreCase) => Parse(str.GetType(), str, ignoreCase);
    
    public static object Parse(Type type, string str, bool ignoreCase) {
        CheckType(type);
        return EnumData.EnumDataDictionary.GetOrCreate(type).ParseObj(str, ignoreCase);
    }
    
    public static bool TryParse(Type type, string str, out object eEnum) {
        CheckType(type);
        return EnumData.EnumDataDictionary.GetOrCreate(type).TryParse(str, out eEnum);
    }
    
    public static bool TryParse(Type type, string str, bool ignoreCase, out object eEnum) {
        CheckType(type);
        return EnumData.EnumDataDictionary.GetOrCreate(type).TryParse(str, ignoreCase, out eEnum);
    }

    public static Array GetValues(Type type) {
        CheckType(type);
        return EnumData.EnumDataDictionary.GetOrCreate(type).ValuesArray;
    }
    
    public static bool IsDefined(object eEnum) => IsDefined(eEnum.GetType(), eEnum);
    
    public static bool IsDefined(Type type, object eEnum) {
        CheckType(type);
        return EnumData.EnumDataDictionary.GetOrCreate(type).IsDefined(eEnum);
    }
}