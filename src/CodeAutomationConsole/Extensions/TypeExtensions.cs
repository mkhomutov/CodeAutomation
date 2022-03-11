using System;
using System.Collections.Generic;

namespace CodeAutomationConsole;

public static class TypeExtensions
{
    private static readonly Dictionary<Type, string> TypeStringByType = new Dictionary<Type, string>
    {
        { typeof(int), "int" },
        { typeof(string), "string" },
        { typeof(double), "double" },
        { typeof(DateTime), "DateTime" },
    };

    public static string ToStringType(this Type type)
    {
        if (TypeStringByType.TryGetValue(type, out var stringType))
        {
            return stringType;
        }

        return "string";
    }
}