using Abc.Aids.Random;
using Abc.Data.Classifiers;
using Abc.Infra.Classifiers;
using Abc.Infra.Classifiers.Initializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Abc.Tests.Infra.Classifiers.Initializers {
    [TestClass]
    public class ClassifierInitializerTests :DbInitializerTests<ClassifierDb> {
        private int count;
        public ClassifierInitializerTests() {
            type = typeof(ClassifierInitializer);
            db = new ClassifierDb(options);
            removeAll(db.Classifiers);
            Assert.AreEqual(0, db.Classifiers.Count());
            count = GetRandom.Int32(3, 5);
            for (var i = 0; i < count; i++) {
                var d = random<ClassifierData>();
                db.Classifiers.Add(d);
                db.SaveChanges();
            }
        }
        [TestMethod] public void InitializeTest() { }
        [TestMethod] public void InitializeByNamesTest() { 
            var s = new List<string>();
            for (var i = 0; i < GetRandom.Int32(5, 15); i++) s.Add(random<string>());
            var t = random<ClassifierType>(); 
            ClassifierInitializer.Initialize(db, t, s.ToArray());
            areEqual(count + s.Count, db?.Classifiers?.Count());
        }
        [TestMethod] public void InitializeByDataTest() {
            var s = new List<ClassifierData>();
            for (var i = 0; i < GetRandom.Int32(5, 15); i++)  s.Add(random<ClassifierData>());
            ClassifierInitializer.Initialize(db, s);
            areEqual(count + s.Count, db?.Classifiers?.Count());
        }
        [TestMethod] public void AddTest() {
            var d = random<ClassifierData>();
            d.Code = null;
            var c = (db?.Classifiers?.Where(x => x.ClassifierType == d.ClassifierType).ToList().Count + 1) ?? 1;
            ClassifierInitializer.add(db, d);
            areEqual($"{d.ClassifierType}:{d.Name}", d.Id);
            areEqual(c.ToString(), d.Code);
            areEqual(count+1, db?.Classifiers?.Count());
        }
        [TestMethod] public void UpdateIdAndCodeTest() {
            var d = random<ClassifierData>();
            d.Id = null;
            d.Code = null;
            var baseId = d.BaseTypeId;
            var c = (db?.Classifiers?.Where(x => x.ClassifierType == d.ClassifierType).ToList().Count + 1) ?? 1;
            ClassifierInitializer.updateIdAndCode(db, d);
            areEqual($"{d.ClassifierType}:{d.Name}", d.Id);
            areEqual($"{d.ClassifierType}:{baseId}", d.BaseTypeId);
            areEqual(c.ToString(), d.Code);
        }
    }
}
