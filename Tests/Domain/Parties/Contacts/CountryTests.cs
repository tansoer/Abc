using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Domain.Common;
using Abc.Domain.Parties.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Parties.Contacts {

    [TestClass]
    public class CountryTests
        : SealedTests<Country, Entity<CountryData>> {

        protected override Country createObject() => new Country(GetRandom.ObjectOf<CountryData>());

        [TestMethod] public void IsLoyaltyProgramTest() => isReadOnly(obj.Data.IsLoyaltyProgram);
        [TestMethod] public void IsIsoCountryTest() => isReadOnly(obj.Data.IsIsoCountry);
        [TestMethod] public void IsoCodeTest() => isReadOnly(obj.Data.Id);
        [TestMethod] public void NativeNameTest() => isReadOnly(obj.Data.NativeName);
        [TestMethod] public void NumericCodeTest() => isReadOnly(obj.Data.NumericCode);
        [TestMethod] public void OfficialNameTest() => isReadOnly(obj.Data.OfficialName);

    }

}