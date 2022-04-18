using System.Collections.Generic;
using Abc.Aids.Random;
using Abc.Facade.Quantities;
using Abc.Pages.Common.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Common.Extensions {

    [TestClass]
    public class TitleHtmlTests : TestsBase {

        [TestInitialize] public virtual void TestInitialize() => type = typeof(TitleHtml);

        [TestMethod]
        public void TitleTest() {
            var obj = new mockHtmlHelper<UnitView>().Title(rndStr);
            Assert.IsInstanceOfType(obj, typeof(HtmlContentBuilder));
        }

        [TestMethod]
        public void HtmlStringsTest() {
            var expected = new List<string> { "<h1>", rndStr, "</h1>" };
            var actual = TitleHtml.htmlStrings(expected[1]);
            htmlContains(actual, expected);
        }

    }

}