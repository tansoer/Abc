using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Parties;
using Abc.Facade.Common;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Parties {
    [TestClass] public class PartyViewFactoryTests : 
        SealedTests<PartyViewFactory, 
            AbstractViewFactory<PartyData, IParty, PartyView>> {

        [TestMethod] public void CreateTest() {}

        [TestMethod] public void CreateViewTest() {
            var d = GetRandom.ObjectOf<PartyData>();
            var v = obj.Create(PartyFactory.Create(d));        
            allPropertiesAreEqual(d, v, nameof(v.Name));
            Assert.AreEqual(PartyFactory.Create(d).GetName(), v.Name);
        }
    }
}
