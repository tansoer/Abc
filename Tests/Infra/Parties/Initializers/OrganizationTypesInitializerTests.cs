using Abc.Infra.Parties.Initializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Abc.Infra.Classifiers;

namespace Abc.Tests.Infra.Parties.Initializers {
    [TestClass]
    public class OrganizationTypesInitializerTests :DbInitializerTests<ClassifierDb> {

        public OrganizationTypesInitializerTests() {
            type = typeof(OrganizationTypesInitializer);
            db = new ClassifierDb(options);
            removeAll(db.Classifiers);
            OrganizationTypesInitializer.Initialize(db);
        }
        [TestMethod] public void InitializeTest() => areEqual(9, db.Classifiers.Count());
    }
}
