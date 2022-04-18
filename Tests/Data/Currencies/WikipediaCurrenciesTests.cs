using System.Collections.Generic;
using System.Xml;
using Abc.Aids.Random;
using Abc.Data.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Currencies {

    [TestClass] public class WikipediaCurrenciesTests : TestsBase {

        [TestInitialize] public void TestInitialize() {
            type = typeof(WikipediaCurrencies);
            WikipediaCurrencies.currencies = null;
            WikipediaCurrencies.page = null;
            WikipediaCurrencies.doc = null;
            WikipediaCurrencies.table = null;
            WikipediaCurrencies.row = null;
            WikipediaCurrencies.state = null;
        }
        [TestMethod] public void GetTest() {
            var l = WikipediaCurrencies.Get;
            Assert.IsTrue(l.Count > 262, $"t.Count == {l.Count} is not greater than 262");
        }
        [TestMethod] public void InitTest() {
            WikipediaCurrencies.init();
            Assert.IsNotNull(WikipediaCurrencies.currencies);
            Assert.IsNotNull(WikipediaCurrencies.page);
            Assert.IsNotNull(WikipediaCurrencies.doc);
            Assert.IsNotNull(WikipediaCurrencies.table);
            Assert.IsNull(WikipediaCurrencies.row);
            Assert.AreEqual("", WikipediaCurrencies.state);
        }
        [TestMethod] public void ToCurrencyTest() {
            var d = new XmlDocument();
            d.LoadXml("<tr><td>000</td><td>111</td><td>222</td><td>333</td><td>444</td><td>555</td></tr>");
            WikipediaCurrencies.row = d.ChildNodes[0];
            WikipediaCurrencies.isComplete = true;
            var c = WikipediaCurrencies.toCurrency();
            Assert.AreEqual("333", c.Id);
            Assert.AreEqual("111", c.Name);
            Assert.AreEqual("333", c.Code);
            Assert.AreEqual("000", c.Details);
            Assert.AreEqual("222", c.MajorUnitSymbol);
            Assert.AreEqual("444", c.MinorUnitSymbol);
            Assert.AreEqual(555, c.RatioOfMinorUnit);
            Assert.AreEqual(null, c.ValidTo);
            Assert.AreEqual(null, c.ValidFrom);
        }
        [TestMethod] public void GetDoubleTest() {
            var d = new XmlDocument();
            d.LoadXml("<tr><td>111</td><td>222</td><td>333</td><td>444</td><td>555</td></tr>");
            WikipediaCurrencies.row = d.ChildNodes[0];
            WikipediaCurrencies.isComplete = false;
            Assert.AreEqual(111, WikipediaCurrencies.getDouble(1));
            Assert.AreEqual(222, WikipediaCurrencies.getDouble(2));
            Assert.AreEqual(333, WikipediaCurrencies.getDouble(3));
            Assert.AreEqual(444, WikipediaCurrencies.getDouble(4));
            Assert.AreEqual(555, WikipediaCurrencies.getDouble(5));
        }
        [TestMethod] public void GetStringTest() {
            var d = new XmlDocument();
            d.LoadXml("<tr><td>aaa</td><td>bbb</td><td>ccc</td><td>ddd</td><td>eee</td><td>fff</td></tr>");
            WikipediaCurrencies.row = d.ChildNodes[0];
            WikipediaCurrencies.isComplete = true;
            Assert.AreEqual("aaa", WikipediaCurrencies.getString(0));
            Assert.AreEqual("bbb", WikipediaCurrencies.getString(1));
            Assert.AreEqual("ccc", WikipediaCurrencies.getString(2));
            Assert.AreEqual("ddd", WikipediaCurrencies.getString(3));
            Assert.AreEqual("eee", WikipediaCurrencies.getString(4));
            Assert.AreEqual("fff", WikipediaCurrencies.getString(5));
        }
        [TestMethod]
        public void GetTableTest() {
            WikipediaCurrencies.page = WikipediaCurrencies.getPage();
            WikipediaCurrencies.doc = WikipediaCurrencies.getDocument();
            var t = WikipediaCurrencies.getTable();
            Assert.IsNotNull(t);
            Assert.IsInstanceOfType(t, typeof(XmlNodeList));
            Assert.IsTrue(t.Count > 262, $"t.Count == {t.Count} is not greater than 262");
        }
        [TestMethod]
        public void GetDocumentTest() {
            WikipediaCurrencies.page = WikipediaCurrencies.getPage();
            var d = WikipediaCurrencies.getDocument();
            Assert.IsInstanceOfType(d, typeof(XmlDocument));
            Assert.IsTrue(d.InnerText.Contains("List of circulating currencies by state or territory"));
        }
        [TestMethod] public void NewListTest() {
            WikipediaCurrencies.currencies = new List<CurrencyData> {
                GetRandom.ObjectOf<CurrencyData>(),
                GetRandom.ObjectOf<CurrencyData>(),
                GetRandom.ObjectOf<CurrencyData>(),
                GetRandom.ObjectOf<CurrencyData>()
            };
            Assert.AreNotEqual(0, WikipediaCurrencies.currencies.Count);
            WikipediaCurrencies.newList();
            Assert.AreEqual(0, WikipediaCurrencies.currencies.Count);
        }
        [TestMethod] public void GetPageTest() {
            var s = WikipediaCurrencies.getPage();
            Assert.IsTrue(s.Contains("<title>List of circulating currencies - Wikipedia</title>"));
        }
    }
}
