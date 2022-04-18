using Abc.Infra.Classifiers;
using Abc.Infra.Classifiers.Initializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Abc.Tests.Infra.Classifiers.Initializers {
    [TestClass]
    public class ClassifierDbInitializerTests :DbInitializerTests<ClassifierDb> {
        public ClassifierDbInitializerTests() { 
            type = typeof(ClassifierDbInitializer);
            db = new ClassifierDb(options);
            ClassifierDbInitializer.Initialize(db);
        }
        [TestMethod] public void InitializeTest() { }
        [TestMethod] public void ClassifiersTest() => areNotEqual(0, db.Classifiers.Count());
    }
}
