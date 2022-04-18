using Abc.Infra.Classifiers;
using Abc.Infra.Classifiers.Initializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Abc.Tests.Infra.Classifiers.Initializers {
    [TestClass]
    public class ContactUsageTypesInitializerTests :DbInitializerTests<ClassifierDb> {
        public ContactUsageTypesInitializerTests() {
            type = typeof(ContactUsageTypesInitializer);
            db = new ClassifierDb(options);
            removeAll(db.Classifiers);
            ContactUsageTypesInitializer.Initialize(db);
        }
        [TestMethod] public void InitializeTest() => areEqual(6, db.Classifiers.Count());
    }
}
