using Abc.Aids.Random;
using Abc.Facade.Common;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Parties {
    [TestClass] public class PersonNamePrefixViewTests:
        SealedTests<PersonNamePrefixView, NameAttributeView> {
        protected override PersonNamePrefixView createObject() 
            => GetRandom.ObjectOf<PersonNamePrefixView>();
        [TestMethod] public void IndexTest() => isProperty<byte>("Index", true);
        [TestMethod] public void NameIdTest() => isNullableProperty<string>("Name", true);
        [TestMethod] public void PrefixTypeIdTest() => isNullableProperty<string>("Prefix");
    }
}
