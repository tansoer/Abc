using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Parties.Names;
using Abc.Facade.Common;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Parties {
    [TestClass]
    public class PersonNamePrefixViewFactoryTests:
        SealedTests<PersonNamePrefixViewFactory, 
            AbstractViewFactory<NamePrefixData, NamePrefix, PersonNamePrefixView>> {

        [TestMethod]
        public void CreateTest() {
            var d = GetRandom.ObjectOf<NamePrefixData>();
            var o = new NamePrefix(d);
            var v = new PersonNamePrefixViewFactory().Create(o);
            allPropertiesAreEqual(v, o.Data);
        }

        [TestMethod]
        public void CreateObjectTest() {
            var v = GetRandom.ObjectOf<PersonNamePrefixView>();
            var o = new PersonNamePrefixViewFactory().Create(v);
            allPropertiesAreEqual(v, o.Data);
        }
    }
}
