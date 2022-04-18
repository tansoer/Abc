using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Parties.Names;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Parties {

    [TestClass]
    public class PartyNameViewFactoryTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(PartyNameViewFactory);

        [TestMethod]
        public void CreateTest() {
            var d = GetRandom.ObjectOf<PartyNameData>();
            d.PartyType = Abc.Aids.Enums.PartyType.Person;
            var o = PartyNameFactory.Create(d) as PersonName;
            var v = new PartyNameViewFactory().Create(o);
            allPropertiesAreEqual(v, o.Data, nameof(o.FamilyName));
            Assert.AreEqual(v.Name, o.FamilyName);
        }

        [DataRow(PartyType.Organization)]
        [DataRow(PartyType.Person)]
        [DataRow(PartyType.Unspecified)]
        [TestMethod]
        public void CreateObjectTest(PartyType t) {
            var v = GetRandom.ObjectOf<PartyNameView>();
            v.PartyType = t;
            var o = new PartyNameViewFactory().Create(v);
            allPropertiesAreEqual(v, o.Data);
        }
    }
}
