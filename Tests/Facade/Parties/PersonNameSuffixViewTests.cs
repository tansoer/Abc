using Abc.Aids.Random;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Parties {
    [TestClass]
    public class PersonNameSuffixViewTests :SealedTests<PersonNameSuffixView, NameAttributeView> {
        protected override PersonNameSuffixView createObject() => GetRandom.ObjectOf<PersonNameSuffixView>();
        [TestMethod] public void IndexTest() => isProperty<byte>("Index", true);
        [TestMethod] public void NameIdTest() => isNullableProperty<string>("Name", true);
        [TestMethod] public void SuffixTypeIdTest() => isNullableProperty<string>("Suffix");
    }
}
