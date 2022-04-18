using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Parties.Contacts;
using Abc.Facade.Common;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Parties {
    [TestClass]
    public class PartyContactUsageViewFactoryTests :SealedTests<PartyContactUsageViewFactory,
        AbstractViewFactory<PartyContactUsageData, PartyContactUsage, PartyContactUsageView>> {

        [TestMethod]
        public void CreateTest() {
            var d = GetRandom.ObjectOf<PartyContactUsageData>();
            var o = new PartyContactUsage(d);
            var v = new PartyContactUsageViewFactory().Create(o);
            allPropertiesAreEqual(v, o.Data);
        }

        [TestMethod]
        public void CreateObjectTest() {
            var v = GetRandom.ObjectOf<PartyContactUsageView>();
            var o = new PartyContactUsageViewFactory().Create(v);
            allPropertiesAreEqual(v, o.Data);
        }
    }
}