using System.Linq;
using Abc.Infra.Parties;
using Abc.Infra.Parties.Initializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Parties.Initializers {

    [TestClass]
    public class BodyMetricTypesInitializerTests : DbInitializerTests<PartyDb> {
        public BodyMetricTypesInitializerTests() {
            type = typeof(BodyMetricTypesInitializer);
            db = new PartyDb(options);
            BodyMetricTypesInitializer.Initialize(db);
        }
        [TestMethod] public void InitializeTest() { }
        [TestMethod] public void BodyMetricTypesTest() => areEqual(3, db.BodyMetricTypes.Count());
    }
}
