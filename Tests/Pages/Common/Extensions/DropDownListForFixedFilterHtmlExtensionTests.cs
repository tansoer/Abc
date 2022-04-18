using System;
using System.Collections.Generic;
using Abc.Pages.Common.Extensions;
using Abc.Tests.Aids;
using Abc.Tests.Data;
using Abc.Tests.Domain;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Common.Extensions
{
    [TestClass]
    public class DropDownListForFixedFilterHtmlExtensionTests :TestsBase {
        private mockHtmlHelper<TestPage> htmlMock;
        private testRepo r;
        private TestPage page;

        private class testRepo :
            mockRepo<TestObject, TestData>,
            ITestRepo { }

        [TestInitialize]
        public virtual void TestInitialize() {
            type = typeof(DropDownListForFixedFilterHtmlExtension);
            htmlMock = new mockHtmlHelper<TestPage>();
            r = new testRepo();
            page = new TestPage(r);
            page.OnGetIndexAsync(page.SortOrder, "", page.CurrentFilter, page.SearchString, page.PageIndex,
                page.FixedFilter, page.FixedValue).GetAwaiter();
        }

        [TestMethod]
        public void ShowFixedFilterDropDownTest() {
            var obj = htmlMock.ShowFixedFilterDropDown(x => x.FixedFilter, new SelectList(Enum.GetValues<TestType>()), page);
            Assert.IsInstanceOfType(obj, typeof(HtmlContentBuilder));
        }

        [TestMethod]
        public void HtmlStringsTest() {

            var expected = new List<string> {
                "<form id=\"indexForm\" asp-page=\"./Index\" method=\"get\">",
                $"<input type=\"hidden\" name=\"SearchString\" value=\"{page.SearchString}\" />",
                $"<input type=\"hidden\" name=\"FixedFilter\" value=ClassificatorType />",
                "<input type=\"hidden\" name=\"Handler\" value=\"Index\" />",

                "<div class=\"form-inline\">",
                "<p>",
                "Select Type:  ",
                "&nbsp"
            };

            expected.Add(htmlMock.DropDownListFor(x => x.FixedValue,
                new SelectList(Enum.GetValues<TestType>()),
                "Select Filter",
                new { @class = "form-control", @name = "FixedValue", @value = "@Model.FixedValue", @onchange = "submit()" }).ToString());
            expected.Add("</p>");
            expected.Add("</div>");
            expected.Add("</form>");

            var actual = DropDownListForFixedFilterHtmlExtension.HtmlStrings(htmlMock, x => x.FixedFilter,
                new SelectList(Enum.GetValues<TestType>()), page);

            htmlContains(actual, expected);
        }

        [TestMethod]
        public void HtmlStrings2Test() {

            var expected = new List<string> {
                "<form id=\"indexForm\" asp-page=\"./Index\" method=\"get\">",
                $"<input type=\"hidden\" name=\"SearchString\" value=\"{page.SearchString}\" />",
                $"<input type=\"hidden\" name=\"FixedFilter\" value=\"ClassificatorType\" />",
                "<input type=\"hidden\" name=\"Handler\" value=\"Index\" />",

                "<div class=\"form-inline\">",
                "<p>",
                "Select Type:  ",
                "&nbsp"
            };

            expected.Add(htmlMock.DropDownListFor(x => x.FixedValue,
                new SelectList(Enum.GetValues<TestType>()),
                "Select Filter",
                new { @class = "form-control", @name = "FixedValue", @value = "@Model.FixedValue", @onchange = "submit()" }).ToString());
            expected.Add("</p>");
            expected.Add("</div>");
            expected.Add("</form>");

            var actual = DropDownListForFixedFilterHtmlExtension.HtmlStrings2(htmlMock, x => x.FixedFilter,
                new SelectList(Enum.GetValues<TestType>()), page);

            htmlContains(actual, expected);
        }

    }
}