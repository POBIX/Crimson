using System;
using System.Collections.Generic;
using System.Reflection;

namespace Crimson
{
    // C# doesn't provide a common interface for PropertyInfo and FieldInfo for some ungodly reason.
    internal static class ReflectionExtensions
    {
        internal static object GetValue(this MemberInfo info, object obj)
        {
            return info.MemberType switch
            {
                MemberTypes.Property => ((PropertyInfo)info).GetValue(obj),
                MemberTypes.Field => ((FieldInfo)info).GetValue(obj),
                _ => throw new ArgumentOutOfRangeException(nameof(info), "must be a FieldInfo or a PropertyInfo.")
            };
        }

        internal static void SetValue(this MemberInfo info, object obj, object value)
        {
            switch (info.MemberType)
            {
                case MemberTypes.Property:
                    ((PropertyInfo)info).SetValue(obj, value);
                    break;
                case MemberTypes.Field:
                    ((FieldInfo)info).SetValue(obj, value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(info), "must be a FieldInfo or a PropertyInfo.");
            }
        }

        internal static MemberInfo GetVariable(this Type t, string name, BindingFlags flags = BindingFlags.Default)
        {
            PropertyInfo p = t.GetProperty(name, flags);
            if (p != null) return p;
            return t.GetField(name, flags);
        }

        internal static Type GetVariableType(this MemberInfo info)
        {
            return info.MemberType switch
            {
                MemberTypes.Property => ((PropertyInfo)info).PropertyType,
                MemberTypes.Field => ((FieldInfo)info).FieldType,
                _ => throw new ArgumentOutOfRangeException(nameof(info), "must be a FieldInfo or a PropertyInfo.")
            };
        }
    }
}
