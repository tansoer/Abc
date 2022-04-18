using Abc.Infra.Classifiers;
using Abc.Infra.Classifiers.Initializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Abc.Tests.Infra.Classifiers.Initializers {
    [TestClass] public class EthnicitiesInitializerTests :DbInitializerTests<ClassifierDb> {
        public EthnicitiesInitializerTests() {
            type = typeof(EthnicitiesInitializer);
            db = new ClassifierDb(options);
            removeAll(db.Classifiers);
            EthnicitiesInitializer.Initialize(db);
        }
        [TestMethod] public void InitializeTest() => areEqual(11, db.Classifiers.Count());
    }
}
