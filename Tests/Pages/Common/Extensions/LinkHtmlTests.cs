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
    public class LinkHtmlTests : TestsBase {

        [TestInitialize] public virtual void TestInitialize() => type = typeof(LinkHtml);

        [TestMethod]
        public void LinkTest() {
            var s = rndStr;
            var items = new[] { new Link("AA", new Uri("AAA", UriKind.Relative)), 
                new Link("BB", new Uri("BBB", UriKind.Relative)) };
            var obj = new mockHtmlHelper<UnitView>().Link(s, items);
            isInstanceOfType(obj, typeof(HtmlContentBuilder));
        }

        [TestMethod]
        public void HtmlStringsTest() {
            var s = rndStr;
            var items = new[] { new Link("AA", new Uri("AAA", UriKind.Relative)), new Link("BB", new Uri("BBB", UriKind.Relative)) };
            var expected = new List<string> {
                "<p>", $"<a>{s}</a>", "<a> </a><a href=\"AAA\">AA</a>",
                "<a> </a><a href=\"BBB\">BB</a>", "</p>"
            };
            var actual = LinkHtml.htmlStrings(s, items);
            htmlContains(actual, expected);
        }

    }

}