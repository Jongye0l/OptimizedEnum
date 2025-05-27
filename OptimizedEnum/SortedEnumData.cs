using System;
using System.Reflection;
using OptimizedEnum.Tool;

namespace OptimizedEnum;

class SortedEnumData<T> : EnumData<T> where T : struct, Enum {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public static string[] Names;
    public static int Length;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public static void Setup(FieldInfo[] fields, bool isSorted, int count) {
        Length = count;
        if(count == Dictionary.Count) Names = Dictionary.Values;
        else {
            string?[] names = Names = new string[count];
            if(isSorted)
                for(int i = 0; i < count; i++)
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

    public override string GetString(object eEnum) {
        return Utils.GetOrDefault(Names, (T) eEnum, Length);
    }
    
    public override string? GetName(object eEnum) {
        return Utils.GetOrNull(Names, (T) eEnum, Length);
    }
    
    public override bool IsDefined(object eEnum) {
        return (uint) ((T) eEnum).AsInteger() < Length;
    }
}