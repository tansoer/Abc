using Abc.Aids.Random;
using Abc.Data.Roles;
using Abc.Domain.Parties.Capabilities;
using Abc.Facade.Common;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Parties {
    [TestClass]
    public class PartyCapabilityViewFactoryTests : SealedTests<PartyCapabilityViewFactory,
            AbstractViewFactory<PartyCapabilityData, PartyCapability, PartyCapabilityView>> {

        [TestMethod]
        public void CreateTest() {
            var d = GetRandom.ObjectOf<PartyCapabilityData>();
            var o = new PartyCapability(d);
            var v = new PartyCapabilityViewFactory().Create(o);
            allPropertiesAreEqual(v, o.Data);
        }

        [TestMethod]
        public void CreateObjectTest() {
            var v = GetRandom.ObjectOf<PartyCapabilityView>();
            var o = new PartyCapabilityViewFactory().Create(v);
            allPropertiesAreEqual(v, o.Data);
        }

    }
}
