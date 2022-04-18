using System.Linq;

namespace Abc.Aids.Methods {

    public static class Copy {

        public static void Members(object from, object to) {
            if (from is null) return;
            if (to is null) return;
            foreach (var property in from.GetType().GetProperties()) {
                if (!property.CanWrite) continue;
                var name = property.Name;
                var p = to.GetType().GetProperty(name);
                var v = property.GetValue(from);
                Safe.Run(() => p?.SetValue(to, v));
            }
        }
        public static void Members(object from, object to, params string[] exclude) {
            if (from is null) return;
            if (to is null) return;
            foreach (var property in from.GetType().GetProperties()) {
                if (!property.CanWrite) continue;
                var name = property.Name;
                if (exclude.Contains(name)) continue;
                var p = to.GetType().GetProperty(name);
                var v = property.GetValue(from);
                Safe.Run(() => p?.SetValue(to, v));
            }
        }

    }

}