using Abc.Infra.Parties;
using Abc.Infra.Parties.Initializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Abc.Tests.Infra.Parties.Initializers {
    [TestClass]
    public class SignedEntityTypesInitializerTests : DbInitializerTests<PartyDb> {
        public SignedEntityTypesInitializerTests() {
            type = typeof(SignedEntityTypesInitializer);
            db = new PartyDb(options);
            SignedEntityTypesInitializer.Initialize(db);
        }
        [TestMethod] public void InitializeTest() { }
        [TestMethod] public void SignedEntityTypesTest()=> areEqual(9, db.SignedEntityTypes.Count());
    }
}
