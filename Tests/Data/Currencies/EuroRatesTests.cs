using System.Collections.Generic;
using System.Xml;
using Abc.Aids.Random;
using Abc.Data.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Currencies {

    [TestClass]
    public class EuroRatesTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(EuroRates);

        [TestMethod]
        public void GetPageTest() {
            EuroRates.url = EuroRates.dailyUrl;
            var s = EuroRates.getPage();
            Assert.IsTrue(s.Contains("<gesmes:name>European Central Bank</gesmes:name>"));

        }
        [TestMethod]
        public void NewListTest() {
            EuroRates.rates = new List<ExchangeRateData> {
                GetRandom.ObjectOf<ExchangeRateData>(),
                GetRandom.ObjectOf<ExchangeRateData>(),
                GetRandom.ObjectOf<ExchangeRateData>(),
                GetRandom.ObjectOf<ExchangeRateData>()
            };
            Assert.AreNotEqual(0, EuroRates.rates.Count);
            EuroRates.newList();
            Assert.AreEqual(0, EuroRates.currencies.Count);
        }
        [TestMethod]
        public void GetDocumentTest() {
            EuroRates.page = EuroRates.getPage();
            var d = EuroRates.getDocument();
            Assert.IsInstanceOfType(d, typeof(XmlDocument));
            Assert.IsTrue(d.InnerText.Contains("European Central Bank"));
        }
        [TestMethod]
        public void GetTableTest() {
            EuroRates.page = EuroRates.getPage();
            EuroRates.doc = EuroRates.getDocument();
            var t = EuroRates.getTable();
            Assert.IsNotNull(t);
            Assert.IsInstanceOfType(t, typeof(XmlNodeList));
            Assert.AreEqual(1, t.Count);
        }
        [TestMethod]
        public void InitTest() {
            EuroRates.init();
            Assert.IsNotNull(EuroRates.rates);
            Assert.IsNotNull(EuroRates.page);
            Assert.IsNotNull(EuroRates.doc);
            Assert.IsNotNull(EuroRates.table);
            Assert.IsNotNull(EuroRates.currencies);

        }
        [TestMethod]
        public void GetTest() {
            var l = EuroRates.Get();
            Assert.AreEqual(31, l.Count);
        }

    }

}
