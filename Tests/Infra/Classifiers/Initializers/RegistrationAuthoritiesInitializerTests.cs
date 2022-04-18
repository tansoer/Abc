using Abc.Infra.Classifiers;
using Abc.Infra.Classifiers.Initializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Abc.Tests.Infra.Classifiers.Initializers {
    [TestClass] public class RegistrationAuthoritiesInitializerTests :DbInitializerTests<ClassifierDb> {
        public RegistrationAuthoritiesInitializerTests() {
            type = typeof(RegistrationAuthoritiesInitializer);
            db = new ClassifierDb(options);
            removeAll(db.Classifiers);
            RegistrationAuthoritiesInitializer.Initialize(db);
        }
        [TestMethod] public void InitializeTest() => areEqual(3, db.Classifiers.Count());
    }
}