using Abc.Aids.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Abc.Aids.Classes {
    public static class AreProperties {
        public static bool Equal(object x, object y, params string[] exceptWithNames) {
            if (x is null) return false;
            foreach (var px in props(x)) {
                if (listed(exceptWithNames, px)) continue;
                if (equal(x, y, px)) continue;
                return false;
            }
            return true;
        }
        public static bool Guids(object x, object y, string propertyName) =>
            isGuid(propertyName, x) && isGuid(propertyName, y);
        internal static IEnumerable<PropertyInfo> props(object x) => x?.GetType()?.GetProperties();
        internal static bool equal(object x, object y, PropertyInfo p) {
            var py = propInfo(p.Name, y);
            if (py is null) return false;
            var expected = p.GetValue(x);
            var actual = py.GetValue(y);
            if (bothNull(expected, actual)) return true;
            return equal(expected, actual);
        }
        internal static bool bothNull(object x, object y) => (x is null) && (y is null);
        internal static bool listed(string[] names, PropertyInfo p) => names?.Contains(p.Name) ?? false;
        internal static bool equal(object x, object y) => x.Equals(y);
        internal static PropertyInfo propInfo(string name, object o) => propInfo(name, o?.GetType());
        internal static PropertyInfo propInfo(string name, Type t) => t?.GetProperty(name);
        internal static bool isGuid(string propertyName, object o) {
            var p = propInfo(propertyName, o);
            if (p is null) return true;
            var v = p.GetValue(o);
            return isGuid(v as string);
        }
        internal static bool isGuid(string s) => Safe.Run(() => toGuid(s), false);
        internal static bool toGuid(string s) {
            _ = new Guid(s);
            return true;
        }
    }
}
