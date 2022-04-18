using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Parties.Names;
using Abc.Facade.Common;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Parties {
    [TestClass]
    public class PersonNameSuffixViewFactoryTests :
        SealedTests<PersonNameSuffixViewFactory,
            AbstractViewFactory<NameSuffixData, NameSuffix, PersonNameSuffixView>> {

        [TestMethod]
        public void CreateTest() {
            var d = GetRandom.ObjectOf<NameSuffixData>();
            var o = new NameSuffix(d);
            var v = new PersonNameSuffixViewFactory().Create(o);
            allPropertiesAreEqual(v, o.Data);
        }

        [TestMethod]
        public void CreateObjectTest() {
            var v = GetRandom.ObjectOf<PersonNameSuffixView>();
            var o = new PersonNameSuffixViewFactory().Create(v);
            allPropertiesAreEqual(v, o.Data);
        }
    }
}
