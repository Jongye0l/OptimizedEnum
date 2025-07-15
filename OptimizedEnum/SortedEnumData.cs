using System;
using System.Reflection;
using OptimizedEnum.Tool;

namespace OptimizedEnum;

class SortedEnumData<T> : EnumData<T> where T : struct, Enum {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public static string[] Names;
    public static uint Length;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public static void Setup(FieldInfo[] fields, bool isSorted, uint count) {
        Length = count;
        if(count == Dictionary.Count) Names = Dictionary.Values;
        else {
            string?[] names = Names = new string[count];
            if(isSorted)
                for(uint i = 0; i < count; i++)
                    names[i] = fields[i].Name;
            else {
                bool[] isSet = new bool[count];
                foreach(FieldInfo field in fields) {
#pragma warning disable CS8605 // Unboxing a possibly null value.
                    T enumValue = (T) field.GetValue(null);
#pragma warning restore CS8605 // Unboxing a possibly null value.
                    int value = enumValue.AsInteger();
                    if(names[value] != null) {
                        if(!isSet[value]) {
                            isSet[value] = true;
                            names[value] = Dictionary[enumValue];
                        }
                    } else names[value] ??= field.Name;
                }
            }
        }
    }

    public static void SetEnumName(T eEnum, string name) {
        ulong value = eEnum.AsUnsignedLong();
        uint length = Length;
        int newLength = (int) length + 1;
        string[] currentNames = Names;
        if(value < length) currentNames[value] = name;
        else if(value == length) {
            if(newLength == Dictionary.Count) Names = Dictionary.Values;
            else {
                string[] names = new string[newLength];
                Array.Copy(currentNames, names, (int) length);
                names[length] = name;
                Names = names;
                Length++;
            }
        } else {
            if(newLength == Dictionary.Count) UnsortedEnumData<T>.NotDupDict = Dictionary;
            else {
                T[] newValues = new T[newLength];
                string[] newNames = new string[newLength];
                Array.Copy(currentNames, newNames, (int) length);
                UnsortedEnumData<T>.NotDupDict = new SortedIndexedDictionary<T>(newValues, newNames, (int) length);
                if(Utils.GetSize<T>() == 8) for(ulong j = 0; j < length; j++) newValues[j] = j.As<T>();
                else for(uint j = 0; j < length; j++) newValues[j] = j.As<T>();
                UnsortedEnumData<T>.NotDupDict.Add(eEnum, name);
            }
            EnumDataDictionary.Set(typeof(T), Instance = new UnsortedEnumData<T>());
            EnumType = EnumType.Unsorted;
        }
    }

    public override string GetString(object eEnum) {
        return Utils.GetOrDefault(Names, (T) eEnum, Length);
    }
    
    public override string GetString(object eEnum, string? format) {
        if(format == null || format.Length == 0) goto GetString;
        if(format.Length == 1) {
            switch(format[0]) {
                case 'G':
                case 'g':
                    goto GetString;
                case 'D':
                case 'd':
                    return ((T) eEnum).GetNumberStringFast();
                case 'X':
                case 'x':
                    return ((T) eEnum).GetHexStringFast();
                case 'F':
                case 'f':
                    return FlagEnumData<T>.RemoveFlagDictionary == null ? FlagEnumData<T>.GetStringNormal((T) eEnum) : FlagEnumData<T>.GetStringDict((T) eEnum);
            }
        }
        throw new FormatException();
    GetString:
        return Utils.GetOrDefault(Names, (T) eEnum, Length);
    }

    public override string? GetName(object eEnum) {
        return Utils.GetOrNull(Names, (T) eEnum, Length);
    }
    
    public override bool IsDefined(object eEnum) {
        return (uint) ((T) eEnum).AsInteger() < Length;
    }
    public override void SetEnumName(object eEnum, string name) => SetEnumName((T) eEnum, name);
}