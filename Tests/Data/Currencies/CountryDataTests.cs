using Abc.Data.Common;
using Abc.Data.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Currencies {

    [TestClass]
    public class CountryDataTests : SealedTests<CountryData, EntityBaseData> {

        [TestMethod] public void OfficialNameTest() => isNullable<string>();

        [TestMethod] public void NativeNameTest() => isNullable<string>();

        [TestMethod] public void NumericCodeTest() => isNullable<string>();

        [TestMethod] public void IsIsoCountryTest() => isProperty<bool>();

        [TestMethod] public void IsLoyaltyProgramTest() => isProperty<bool>();
    }
}