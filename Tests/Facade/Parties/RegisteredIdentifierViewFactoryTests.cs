using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Parties.Identifiers;
using Abc.Facade.Common;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Parties {
    [TestClass]
    public class RegisteredIdentifierViewFactoryTests : SealedTests<RegisteredIdentifierViewFactory,
            AbstractViewFactory<RegisteredIdentifierData, RegisteredIdentifier, RegisteredIdentifierView>> {

        [TestMethod]
        public void CreateTest() {
            var d = GetRandom.ObjectOf<RegisteredIdentifierData>();
            var o = new RegisteredIdentifier(d);
            var v = new RegisteredIdentifierViewFactory().Create(o);
            allPropertiesAreEqual(v, o.Data);
        }

        [TestMethod]
        public void CreateObjectTest() {
            var v = GetRandom.ObjectOf<RegisteredIdentifierView>();
            var o = new RegisteredIdentifierViewFactory().Create(v);
            allPropertiesAreEqual(v, o.Data);
        }

    }
}
