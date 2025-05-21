using System;
#if NETCOREAPP1_0 || NETSTANDARD1_0 || NETSTANDARD1_5
using System.Reflection;
#endif
using OptimizedEnum.Tool;

namespace OptimizedEnum;

public static class OptEnum {
    public static string? GetName<T>(this T eEnum) where T : struct, Enum {
        return EnumData<T>.enumType switch {
            EnumType.Sorted => Utils.GetOrNull(SortedEnumData<T>.Names, eEnum, SortedEnumData<T>.Length),
            EnumType.Unsorted => UnsortedEnumData<T>.dictionary[eEnum],
            EnumType.Flag => eEnum.AsLong() == 0                                               ? FlagEnumData<T>.HasZero ? FlagEnumData<T>.zeroString : null :
                             !EnumData<T>.AllFlags.HasAllFlags(eEnum) || eEnum.BitCount() != 1 ? FlagEnumData<T>.dictionary?[eEnum] :
                             eEnum.AsLong() == 1                                               ? FlagEnumData<T>.FlagEnums[0] : FlagEnumData<T>.FlagEnums[(int) 
#if NETCOREAPP2_0
                                                                                                     Utils.Log2(eEnum.AsFloatUnsigned())
#elif NETCOREAPP3_0 || NET5_0
                                                                                                     MathF.Log2(eEnum.AsFloatUnsigned())
#else
                                                                                                     Utils.Log2(eEnum.AsDoubleUnsigned())
#endif
                                                                                                 ],
            _ => throw new NotSupportedException()
        };
    }

    public static string GetString<T>(this T eEnum) where T : struct, Enum {
        return EnumData<T>.enumType switch {
            EnumType.Sorted => Utils.GetOrDefault(SortedEnumData<T>.Names, eEnum, SortedEnumData<T>.Length),
            EnumType.Unsorted => UnsortedEnumData<T>.dictionary[eEnum] ?? eEnum.GetNumberStringFast(),
            EnumType.Flag => FlagEnumData<T>.dictionary == null ? FlagEnumData<T>.GetStringNormal(eEnum) : FlagEnumData<T>.GetStringDict(eEnum),
            _ => throw new NotSupportedException()
        };
    }

    public static T Parse<T>(string str) where T : struct, Enum {
        if(EnumData<T>.enumType == EnumType.Flag) {
            string[] split = str.Split(',');
            if(split.Length > 1) {
                T value = Utils.GetZero<T>();
                foreach(string s in split) value.CombineFlags(EnumData<T>.NameDictionary.GetValue(s.Trim()));
                return value;
            }
        }
        return EnumData<T>.NameDictionary.GetValue(str);
    }

    public static T Parse<T>(string str, bool ignoreCase) where T : struct, Enum {
        if(EnumData<T>.enumType == EnumType.Flag) {
            if(!ignoreCase) return Parse<T>(str);
            string[] split = str.Split(',');
            if(split.Length > 1) {
                T value = Utils.GetZero<T>();
                foreach(string s in split) value = value.CombineFlags(EnumData<T>.NameDictionary.GetValueIgnoreCase(s.Trim()));
                return value;
            }
        }
        return ignoreCase ? EnumData<T>.NameDictionary.GetValueIgnoreCase(str) : EnumData<T>.NameDictionary.GetValue(str);
    }

    public static bool TryParse<T>(string str, out T eEnum) where T : struct, Enum {
        if(EnumData<T>.enumType == EnumType.Flag) {
            string[] split = str.Split(',');
            if(split.Length > 1) {
                eEnum = Utils.GetZero<T>();
                foreach(string s in split) {
                    if(!EnumData<T>.NameDictionary.TryGetValue(s.Trim(), out T flag)) return false;
                    eEnum = eEnum.CombineFlags(flag);
                }
                return true;
            }
        }
        return EnumData<T>.NameDictionary.TryGetValue(str, out eEnum);
    }

    public static bool TryParse<T>(string str, bool ignoreCase, out T eEnum) where T : struct, Enum {
        if(EnumData<T>.enumType == EnumType.Flag) {
            if(!ignoreCase) return TryParse(str, out eEnum);
            string[] split = str.Split(',');
            if(split.Length > 1) {
                eEnum = Utils.GetZero<T>();
                foreach(string s in split) {
                    if(!EnumData<T>.NameDictionary.TryGetValueIgnoreCase(s.Trim(), out T flag)) return false;
                    eEnum = eEnum.CombineFlags(flag);
                }
                return true;
            }
        }
        return ignoreCase ? EnumData<T>.NameDictionary.TryGetValueIgnoreCase(str, out eEnum) : EnumData<T>.NameDictionary.TryGetValue(str, out eEnum);
    }
    
    public static T[] GetValues<T>() where T : struct, Enum {
        return EnumData<T>.Values;
    }

    public static string[] GetNames<T>() where T : struct, Enum {
        return EnumData<T>.Names;
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
        if(
#if NETCOREAPP1_0 || NETSTANDARD1_0 || NETSTANDARD1_5
            type.GetTypeInfo().BaseType != typeof(Enum)
#else
            !type.IsEnum
#endif
            ) throw new ArgumentException($"Type {type} is not an enum.");
    }

    public static string? GetName(object eEnum) => GetName(eEnum.GetType(), eEnum);

    public static string? GetName(Type type, object eEnum) {
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

    public static string[] GetNames(Type type) {
        CheckType(type);
        return EnumData.EnumDataDictionary.GetOrCreate(type).NamesArray;
    }
    
    public static bool IsDefined(object eEnum) => IsDefined(eEnum.GetType(), eEnum);
    
    public static bool IsDefined(Type type, object eEnum) {
        CheckType(type);
        return EnumData.EnumDataDictionary.GetOrCreate(type).IsDefined(eEnum);
    }
    
    public static bool HasAllFlag(object eEnum1, object eEnum2) => HasAllFlag(eEnum1.GetType(), eEnum1, eEnum2);

    public static bool HasAllFlag(Type type, object eEnum1, object eEnum2) {
        CheckType(type);
        return EnumData.EnumDataDictionary.GetOrCreate(type).HasAllFlag(eEnum1, eEnum2);
    }
    
    public static bool HasAnyFlag(object eEnum1, object eEnum2) => HasAnyFlag(eEnum1.GetType(), eEnum1, eEnum2);
    
    public static bool HasAnyFlag(Type type, object eEnum1, object eEnum2) {
        CheckType(type);
        return EnumData.EnumDataDictionary.GetOrCreate(type).HasAnyFlag(eEnum1, eEnum2);
    }
}