using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Parties.Attributes;
using Abc.Facade.Common;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Parties {
    public class PersonEthnicityViewFactoryTests :SealedTests<PersonEthnicityViewFactory,
        AbstractViewFactory<PersonEthnicityData, PersonEthnicity, PersonEthnicityView>> {

        [TestMethod]
        public void CreateTest() {
            var d = GetRandom.ObjectOf<PersonEthnicityData>();
            var o = new PersonEthnicity(d);
            var v = new PersonEthnicityViewFactory().Create(o);
            allPropertiesAreEqual(v, o.Data);
        }

        [TestMethod]
        public void CreateObjectTest() {
            var v = GetRandom.ObjectOf<PersonEthnicityView>();
            var o = new PersonEthnicityViewFactory().Create(v);
            allPropertiesAreEqual(v, o.Data);
        }
    }
}