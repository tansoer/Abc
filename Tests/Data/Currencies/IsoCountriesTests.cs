using System.Collections.Generic;
using System.Globalization;
using Abc.Data.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Currencies {

    [TestClass]
    public class IsoCountriesTests : TestsBase {

        private List<RegionInfo> l;

        [TestInitialize]
        public void TestInitialize() {
            type = typeof(IsoCountries);
            l = IsoCountries.RegionInfo;
            Assert.IsNotNull(l);
        }
        [TestMethod]
        public void RegionInfoTest() {
            Assert.IsInstanceOfType(l, typeof(List<RegionInfo>));
            Assert.AreNotEqual(0, l.Count);
        }

        [TestMethod]
        public void GetTest() {
            var list = IsoCountries.Get;
            Assert.IsInstanceOfType(list, typeof(List<CountryData>));
            Assert.AreEqual(l.Count, list.Count);
        }
        [TestMethod]
        public void HasCorrectCountryTest() {
            var list = IsoCountries.Get;
            var x = l.Find(z => z.EnglishName == "Estonia");
            var y = list.Find(z => z.Name == "Estonia");
            Assert.AreEqual(x.EnglishName, y.Name);
            Assert.AreEqual(x.DisplayName, y.OfficialName);
            Assert.AreEqual(x.ThreeLetterISORegionName, y.Id);
            Assert.AreEqual(x.TwoLetterISORegionName, y.Code);
            Assert.AreEqual(x.NativeName, y.NativeName);
            Assert.AreEqual(x.GeoId.ToString(), y.NumericCode);
            Assert.AreEqual(false, y.IsLoyaltyProgram);
            Assert.AreEqual(true, y.IsIsoCountry);
        }
        [TestMethod]
        public void RegionInfoIncludesNoWorldTest() {
            Assert.IsNull(l.Find(x => x.EnglishName == "World"));
        }
        [TestMethod]
        public void RegionInfoIncludesNoCaribbeanTest() {
            Assert.IsNull(l.Find(x => x.EnglishName == "Caribbean"));
        }
        [TestMethod]
        public void RegionInfoIncludesNoLatinAmericaTest() {
            Assert.IsNull(l.Find(x => x.EnglishName == "Latin America"));
        }

    }

}
