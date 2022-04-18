using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using System.Collections.Generic;
using System.Linq;

namespace Abc.Infra.Classifiers.Initializers {
    public static class ClassifierInitializer {

        public static void Initialize(ClassifierDb c, ClassifierType t, params string[] items) {
            var l = new List<ClassifierData>();
            foreach (var item in items) 
                l.Add(new ClassifierData {
                        Name = item, 
                        ClassifierType = t,
                    });
            Initialize(c, l);
        }
        public static void Initialize(ClassifierDb c, List<ClassifierData> items) {
            foreach (var item in items) add(c, item);
        }
        internal static void add(ClassifierDb c, ClassifierData d) {
            if (d is null) return;
            if (ClassifierFactory.Create(d).IsUnspecified) return;
            var set = c?.Classifiers;
            if (set is null) return;
            if (set.Any(x => (x.Name == d.Name) && (x.ClassifierType == d.ClassifierType))) return;
            updateIdAndCode(c, d);
            set?.Add(d);
            c?.SaveChanges();
        }
        internal static void updateIdAndCode(ClassifierDb c, ClassifierData d) {
            if (d is null) return;
            var count = (c?.Classifiers?.Where(x => x.ClassifierType == d.ClassifierType).ToList().Count + 1) ?? 1;
            d.Id = replaceAnd($"{d.ClassifierType}:{d.Name}");
            d.BaseTypeId = string.IsNullOrWhiteSpace(d.BaseTypeId) ? null: replaceAnd($"{d.ClassifierType}:{d.BaseTypeId}");
            d.Code = string.IsNullOrWhiteSpace(d.Code) || d.Code == d.Name ? count.ToString(): d.Code; 
        }

        private static string replaceAnd(string s) {
            if (string.IsNullOrWhiteSpace(s)) return s;
            s = s.Replace("&", "and");
            return s;
        }
    }
}
