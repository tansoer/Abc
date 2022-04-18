using System;
using System.Collections.Generic;
using Abc.Aids.Random;
using Abc.Facade.Quantities;
using Abc.Pages.Common.Aids;
using Abc.Pages.Common.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Common.Extensions {

    [TestClass]
    public class RowHtmlTests : TestsBase {

        private string s;

        [TestInitialize]
        public virtual void TestInitialize() {
            type = typeof(RowHtml);
            s = rndStr;
        }

        [TestMethod]
        public void RowTest() {

            var obj = new mockHtmlHelper<UnitView>().Row(
                rndBool,
                new Uri(rndStr, UriKind.Relative),

                rndStr,
                new mockHtmlContent(rndStr),
                new mockHtmlContent(rndStr));

            Assert.IsInstanceOfType(obj, typeof(HtmlContentBuilder));
        }

        [TestMethod]
        public void FilteredRowTest() {

            var obj = new mockHtmlHelper<UnitView>().Row(
                rndBool,
                new Uri(rndStr, UriKind.Relative),
                rndStr,
                rndStr,
                rndStr,
                new mockHtmlContent(rndStr),
                new mockHtmlContent(rndStr));

            Assert.IsInstanceOfType(obj, typeof(HtmlContentBuilder));
        }

        [TestMethod]
        public void SearchedRowTest() {
            var obj = new mockHtmlHelper<UnitView>().Row(
                rndBool,
                new Uri(rndStr, UriKind.Relative),
                rndStr,
                rndStr, rndStr,
                GetRandom.UInt8(),
                rndStr, rndStr,
                new mockHtmlContent(rndStr),
                new mockHtmlContent(rndStr));
            Assert.IsInstanceOfType(obj, typeof(HtmlContentBuilder));
        }

        [TestMethod]
        public void InternalRowTest() {
            var a = GetRandom.ObjectOf<Args>();
            var obj = RowHtml.row(
                rndBool,
                a,
                new mockHtmlContent(rndStr),
                new mockHtmlContent(rndStr));
            Assert.IsInstanceOfType(obj, typeof(HtmlContentBuilder));
        }

        [TestMethod]
        public void StringsTest() {
            var expected = new List<string> {
                "<td>",
                s,
                "</td>",
                "<td>",
                "Edit",
                "&nbsp;",
                "Details",
                "&nbsp;",
                "Delete",
                "</td>"
            };
            var a = GetRandom.ObjectOf<Args>();
            var actual = RowHtml.htmlStrings(
                false,
                a,
                new mockHtmlContent(s));
            htmlContains(actual, expected);
        }

        [TestMethod]
        public void SelectStringsTest() {
            var expected = new List<string> {
                "<td>",
                s,
                "</td>",
                "<td>",
                "Select",
                "&nbsp;",
                "Edit",
                "&nbsp;",
                "Details",
                "&nbsp;",
                "Delete",
                "</td>"
            };
            var a = GetRandom.ObjectOf<Args>();
            var actual = RowHtml.htmlStrings(
                true,
                a,
                new mockHtmlContent(s));
            htmlContains(actual, expected);
        }

        [TestMethod]
        public void AddValueTest() {
            var value = new mockHtmlContent(s);
            var l = new List<object>();
            RowHtml.addValue(l, value);
            Assert.AreEqual(3, l.Count);
            Assert.AreEqual("<td>", l[0].ToString());
            Assert.AreEqual(s, l[1].ToString());
            Assert.AreEqual("</td>", l[2].ToString());
        }

    }

}