using Abc.Pages.Common.Consts;
using Abc.Pages.Common.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Pages.Classifiers;

namespace Abc.Tests.Pages.Common.Extensions {
    [TestClass]
    public class ButtonForCreateWithDropDownHtmlExtensionTests : TestsBase {

        private mockHtmlHelper<ClassifiersPage> htmlMock;
        private testRepo r;
        private ClassifiersPage page;

        private class testRepo :
            mockRepo<IClassifier, ClassifierData>,
            IClassifiersRepo { }

        

        [TestInitialize]
        public virtual void TestInitialize() {
            type = typeof(ButtonForCreateWithDropDownHtmlExtension);
            htmlMock = new mockHtmlHelper<ClassifiersPage>();
            r = new testRepo();
            page = new ClassifiersPage(r);
            page.OnGetIndexAsync(page.SortOrder, "", page.CurrentFilter, page.SearchString, page.PageIndex,
                page.FixedFilter, page.FixedValue).GetAwaiter();
        }

        [TestMethod]
        public void ShowCreateDropDownTest() {
            var obj = htmlMock.ShowCreateDropDown(page);
            Assert.IsInstanceOfType(obj, typeof(HtmlContentBuilder));
        }

        [TestMethod]
        public void HtmlStringsTest() {
            var expected = new List<string> {
                "<div class=\"dropdown\">",
                $"<button class=\"btn btn-link\" ",
                $"id=\"dropdownMenuButton\"  ",
                $"data-toggle=\"dropdown\"> ",
                $"{Captions.Create} ",
                $"</button>",
                $"<div class=\"dropdown-menu\" ",
                $"aria-labelledby=\"dropdownMenuButton\">"
            };
            for (int i = 0; i < page.DropDownEntryCount; i++) 
                expected.Add(ButtonForCreateWithDropDownHtmlExtension.GetDropDownRow(i, page));
            expected.Add("</div>");
            expected.Add("</div>");
            var actual = ButtonForCreateWithDropDownHtmlExtension.HtmlStrings(page);
            htmlContains(actual, expected);
        }

        [TestMethod]
        public void HtmlStrings2Test() {

            var expected = new List<string> {
                "<div class=\"dropdown\">",
                $"<button class=\"btn btn-link\" id=\"dropdownMenuButton\" data-toggle=\"dropdown\">{Captions.Create}</button>",
                "<div class=\"dropdown-menu\" aria-labelledby=\"dropdownMenuButton\">"
            };
            for (int i = 0; i < page.DropDownEntryCount; i++) {
                expected.Add(ButtonForCreateWithDropDownHtmlExtension.GetDropDownRow(i, page));
            }
            expected.Add("</div>");
            expected.Add("</div>");

            var actual = ButtonForCreateWithDropDownHtmlExtension.HtmlStrings2(page, htmlMock);

            htmlContains(actual, expected);
        }

        [TestMethod]
        public void GetDropDownRowTest() {
            
            for (int i = 0; i < page.DropDownEntryCount; i++) {
                var expected = "<a class=\"dropdown-item\" " +
                $"href=\"{page.GetDropDownEntryUri(i)}\">" +
                $"{page.GetDropDownEntryName(i)}</a>";

                var actual = ButtonForCreateWithDropDownHtmlExtension.GetDropDownRow(i, page);

                Assert.AreEqual(actual, expected);
            }
        }
    }
}
