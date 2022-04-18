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
    public class DropMenuHtmlTests : TestsBase {

        private string name;
        private Link[] items;

        [TestInitialize]
        public virtual void TestInitialize() {
            type = typeof(DropMenuHtml);
            name = rndStr;
            items = new[] {
                new Link("A", new Uri("A.A", UriKind.Relative)),
                new Link("B",new Uri("B.B", UriKind.Relative))
            };
        }

        [TestMethod]
        public void DropMenuTest() {
            var obj = new mockHtmlHelper<UnitView>().DropMenu(name, items);
            Assert.IsInstanceOfType(obj, typeof(HtmlContentBuilder));
        }

        [TestMethod]
        public void HtmlStringsTest() {
            var expected = new List<string> {
                "<li class=\"nav-item dropdown\">",
                "<a class=\"nav-link text-dark dropdown-toggle\" href=\"#\" id=\"navbardrop\" data-toggle=\"dropdown\">",
                name,
                "</a>",
                "<div class=\"dropdown-menu\">",
                "<a class='dropdown-item text-dark' href=\"A.A\">A</a>",
                "<a class='dropdown-item text-dark' href=\"B.B\">B</a>",
                "</div>",
                "</li>"
            };
            var actual = DropMenuHtml.htmlStrings(name, items);
            htmlContains(actual, expected);
        }

    }

}