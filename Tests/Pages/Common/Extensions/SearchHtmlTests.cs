using System.Collections.Generic;
using Abc.Facade.Quantities;
using Abc.Pages.Common.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Common.Extensions {

    [TestClass]
    public class SearchHtmlTests : TestsBase {

        private const string filter = "filter";
        private const string linkToFullList = "url";
        private const string text = "text";

        [TestInitialize] public virtual void TestInitialize() => type = typeof(SearchHtml);

        [TestMethod]
        public void SearchTest() {
            var obj = new mockHtmlHelper<UnitView>().Search(filter, linkToFullList, text);
            Assert.IsInstanceOfType(obj, typeof(HtmlContentBuilder));
        }

        [TestMethod]
        public void HtmlStringsTest() {
            var expected = new List<string> {
                "<form asp-action=\"./Index\" method=\"get\">",
                "<div class=\"form-inline col-md-6\">",
                "Find by:",
                "&nbsp;",
                $"<input class=\"form-control\" type=\"text\" name=\"SearchString\" value=\"{filter}\" />",
                "&nbsp;",
                "<input type=\"submit\" value=\"Search\" class=\"btn btn-default\" />",
                "&nbsp;",
                $"<a href=\"{linkToFullList}\">{text}</a>",
                "</div>",
                "</form>"
            };
            var actual = SearchHtml.htmlStrings(filter, linkToFullList, text);
            htmlContains(actual, expected);
        }

    }

}