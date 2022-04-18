using System;

namespace Abc.Tests.Soft {

    public static class HtmlHelper {
        public static string Value(object o, bool inEditor = false) {
            return o switch {
                null => string.Empty,
                bool b => b.ToString().ToLower(),
                DateTime d => ToString(d, inEditor),
                byte[] x => System.Text.Encoding.Default.GetString(x),
                _ => o.ToString()
            };
        }
        public static string ToString(in DateTime d, in bool isEditor)
            => isEditor ? d.Date.ToString("yyyy-MM-dd") : d.Date.ToShortDateString();
        public static string Type<T>(string name) {
            var p = typeof(T).GetProperty(name);
            var t = GetType(p?.PropertyType);
            if (t == typeof(DateTime)) return "date";
            return IsNumber(t) ? "number" : "text";
        }
        public static Type GetType(Type t) {
            if (t is null) return null;
            var x = Nullable.GetUnderlyingType(t);
            return x ?? t;
        }
        public static bool IsNumber(Type t) => t == typeof(int) ||
                                               t == typeof(uint) ||
                                               t == typeof(long) ||
                                               t == typeof(ulong) ||
                                               t == typeof(short) ||
                                               t == typeof(ushort) ||
                                               t == typeof(byte) ||
                                               t == typeof(sbyte);
    }
}
