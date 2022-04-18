using System.Collections.Generic;
using System.Globalization;
using Abc.Aids.Random;
using Abc.Data.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Currencies {
    [TestClass]
    public class CurrencyUsagesTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(CurrencyUsages);

        [TestMethod]
        public void GetTest() {
            var l = IsoCountries.RegionInfo;
            var c = CurrencyUsages.Get;
            Assert.IsInstanceOfType(c, typeof(List<CurrencyUsageData>));
            Assert.IsTrue(c.Count > 0);
            Assert.IsTrue(c.Count == l.Count);
            foreach (var r in l) Assert.IsNotNull(
                c.Find(x => x.CurrencyId == r.ISOCurrencySymbol && x.CountryId == r.ThreeLetterISORegionName));
        }
        [TestMethod]
        public void CreateListTest() {
            var d = GetRandom.ObjectOf<CurrencyUsageData>();
            CurrencyUsages.toList(d);
            Assert.IsTrue(CurrencyUsages.usages.Count > 0);
            CurrencyUsages.createList();
            Assert.AreEqual(0, CurrencyUsages.usages.Count);
        }
        [TestMethod]
        public void IsInListTest() {
            var d = GetRandom.ObjectOf<CurrencyUsageData>();
            Assert.IsFalse(CurrencyUsages.isInList(d.CountryId, d.CurrencyId));
            CurrencyUsages.toList(d);
            Assert.IsTrue(CurrencyUsages.isInList(d.CountryId, d.CurrencyId));
        }
        [TestMethod]
        public void ToCurrencyDataTest() {
            var r = new RegionInfo("EE");
            var d = CurrencyUsages.toCurrencyData(r);
            Assert.AreEqual(r.Name, "EE");
            Assert.AreEqual(r.NativeName, "Eesti");
            Assert.AreEqual(r.EnglishName, "Estonia");
            Assert.AreEqual(d.CurrencyId, r.ISOCurrencySymbol);
            Assert.AreEqual(d.CountryId, r.ThreeLetterISORegionName);
            Assert.AreEqual(d.CurrencyNativeName, r.CurrencyNativeName);
            Assert.AreEqual(d.ValidTo, null);
            Assert.AreEqual(d.ValidFrom, null);
        }

    }

}

