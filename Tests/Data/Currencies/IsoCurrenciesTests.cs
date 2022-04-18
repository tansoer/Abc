using System.Collections.Generic;
using System.Globalization;
using Abc.Aids.Random;
using Abc.Data.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Currencies {

    [TestClass]
    public class IsoCurrenciesTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(IsoCurrencies);

        [TestMethod]
        public void GetTest() {
            var l = IsoCountries.RegionInfo;
            var c = IsoCurrencies.Get;
            Assert.IsInstanceOfType(c, typeof(List<CurrencyData>));
            Assert.IsTrue(c.Count > 0);
            Assert.IsTrue(c.Count < l.Count);
            foreach (var r in l) Assert.IsNotNull(c.Find(x => x.Id == r.ISOCurrencySymbol));
        }
        [TestMethod]
        public void CreateListTest() {
            var d = GetRandom.ObjectOf<CurrencyData>();
            IsoCurrencies.toList(d);
            Assert.IsTrue(IsoCurrencies.currencies.Count > 0);
            IsoCurrencies.createList();
            Assert.AreEqual(0, IsoCurrencies.currencies.Count);
        }
        [TestMethod]
        public void IsInListTest() {
            var d = GetRandom.ObjectOf<CurrencyData>();
            Assert.IsFalse(IsoCurrencies.isInList(d.Id));
            IsoCurrencies.toList(d);
            Assert.IsTrue(IsoCurrencies.isInList(d.Id));
        }
        [TestMethod]
        public void ToCurrencyDataTest() {
            var r = new RegionInfo("EE");
            var d = IsoCurrencies.toCurrencyData(r);
            Assert.AreEqual(r.Name, "EE");
            Assert.AreEqual(r.NativeName, "Eesti");
            Assert.AreEqual(r.EnglishName, "Estonia");
            Assert.AreEqual(d.Id, r.ISOCurrencySymbol);
            Assert.AreEqual(d.Code, r.ISOCurrencySymbol);
            Assert.AreEqual(d.Name, r.CurrencyEnglishName);
            Assert.AreEqual(d.MajorUnitSymbol, r.CurrencySymbol);
            Assert.AreEqual(d.MinorUnitSymbol, null);
            Assert.AreEqual(d.NumericCode, null);
            Assert.AreEqual(d.RatioOfMinorUnit, 0);
            Assert.AreEqual(d.Details, null);
            Assert.AreEqual(d.ValidTo, null);
            Assert.AreEqual(d.ValidFrom, null);
        }

    }

}

