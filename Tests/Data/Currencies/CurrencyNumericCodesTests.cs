using System.Collections.Generic;
using System.Xml;
using Abc.Aids.Random;
using Abc.Data.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Currencies {

    [TestClass]
    public class CurrencyNumericCodesTests : TestsBase {

        [TestInitialize]
        public void TestInitialize() {
            type = typeof(CurrencyNumericCodes);
            CurrencyNumericCodes.currencies = null;
            CurrencyNumericCodes.page = null;
            CurrencyNumericCodes.doc = null;
            CurrencyNumericCodes.table = null;
            CurrencyNumericCodes.row = null;
        }

        [TestMethod]
        public void GetTest() {
            var l = CurrencyNumericCodes.Get;
            Assert.AreEqual(269, l.Count);
        }

        [TestMethod]
        public void InitTest() {
            CurrencyNumericCodes.init();
            Assert.IsNotNull(CurrencyNumericCodes.currencies);
            Assert.IsNotNull(CurrencyNumericCodes.page);
            Assert.IsNotNull(CurrencyNumericCodes.doc);
            Assert.IsNotNull(CurrencyNumericCodes.table);
            Assert.IsNull(CurrencyNumericCodes.row);
        }

        [TestMethod]
        public void ToCurrencyTest() {
            var d = new XmlDocument();
            d.LoadXml("<tr><td>000</td><td>111</td><td>222</td><td>333</td></tr>");
            CurrencyNumericCodes.row = d.ChildNodes[0];
            var c = CurrencyNumericCodes.toCurrency();
            Assert.AreEqual("222", c.Id);
            Assert.AreEqual("111", c.Name);
            Assert.AreEqual("222", c.Code);
            Assert.AreEqual("333", c.NumericCode);
            Assert.AreEqual(null, c.Details);
            Assert.AreEqual(null, c.MajorUnitSymbol);
            Assert.AreEqual(null, c.MinorUnitSymbol);
            Assert.AreEqual(0, c.RatioOfMinorUnit);
            Assert.AreEqual(null, c.ValidTo);
            Assert.AreEqual(null, c.ValidFrom);
        }

        [TestMethod]
        public void GetStringTest() {
            var d = new XmlDocument();
            d.LoadXml("<tr><td>aaa</td><td>bbb</td><td>ccc</td><td>ddd</td></tr>");
            CurrencyNumericCodes.row = d.ChildNodes[0];
            Assert.AreEqual("aaa", CurrencyNumericCodes.getString(0));
            Assert.AreEqual("bbb", CurrencyNumericCodes.getString(1));
            Assert.AreEqual("ccc", CurrencyNumericCodes.getString(2));
            Assert.AreEqual("ddd", CurrencyNumericCodes.getString(3));

        }

        [TestMethod]
        public void GetTableTest() {
            CurrencyNumericCodes.page = CurrencyNumericCodes.getPage();
            CurrencyNumericCodes.doc = CurrencyNumericCodes.getDocument();
            var t = CurrencyNumericCodes.getTable();
            Assert.IsNotNull(t);
            Assert.IsInstanceOfType(t, typeof(XmlNodeList));
            Assert.AreEqual(269, t.Count);
        }

        [TestMethod]
        public void GetDocumentTest() {
            CurrencyNumericCodes.page = CurrencyNumericCodes.getPage();
            var d = CurrencyNumericCodes.getDocument();
            Assert.IsInstanceOfType(d, typeof(XmlDocument));
            Assert.IsTrue(d.InnerText.Contains("CountryCurrencyCodeNumber"));
        }

        [TestMethod]
        public void NewListTest() {
            CurrencyNumericCodes.currencies = new List<CurrencyData> {
                GetRandom.ObjectOf<CurrencyData>(),
                GetRandom.ObjectOf<CurrencyData>(),
                GetRandom.ObjectOf<CurrencyData>(),
                GetRandom.ObjectOf<CurrencyData>()
            };
            Assert.AreNotEqual(0, CurrencyNumericCodes.currencies.Count);
            CurrencyNumericCodes.newList();
            Assert.AreEqual(0, CurrencyNumericCodes.currencies.Count);
        }

        [TestMethod]
        public void GetPageTest() {
            var s = CurrencyNumericCodes.getPage();
            Assert.IsTrue(s.Contains("Country Currency Codes"));

        }

    }

}