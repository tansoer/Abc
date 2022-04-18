using System;
using System.ComponentModel;
using System.Reflection;

namespace Abc.Aids.Extensions {
    public static class Enums {
        public static string ToStr(this Enum value) {
            var i = value.GetType().GetField(value.ToString());
            var a = i?.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
            return a?.Description?? value.ToString();
        }
    }
}
