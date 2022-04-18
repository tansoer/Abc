using Abc.Aids.Random;
using Abc.Facade.Common;
using Abc.Facade.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Currencies {

    [TestClass]
    public class CountryViewTests : SealedTests<CountryView, EntityBaseView> {

        protected override CountryView createObject() => GetRandom.ObjectOf<CountryView>();

        [TestMethod] public void OfficialNameTest() => isNullableProperty<string>("Official Name");
        

        [TestMethod] public void NativeNameTest() => isNullableProperty<string>("Native Name");

        [TestMethod] public void NumericCodeTest() => isNullableProperty<string>("Numeric Code");

        [TestMethod] public void IsIsoCountryTest() => isProperty<bool>("Is Iso Country");

        [TestMethod] public void IsLoyaltyProgramTest() => isProperty<bool>("Is Loyalty Program");

    }

}
