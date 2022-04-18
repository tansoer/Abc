using System.Collections.Generic;
using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Domain.Parties.Contacts;
using Abc.Pages.Common.Aids;
using Abc.Pages.Common.Extensions;
using Abc.Pages.Currencies;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Common.Extensions {
    [TestClass]
    public class TableHtmlExtensionTests :TestsBase {
        private mockHtmlHelper<CountriesPage> htmlMock;
        private testRepo r;
        private CountriesPage page;

        private class testRepo :
            mockRepo<Country, CountryData>,
            ICountriesRepo { }


        [TestInitialize]
        public virtual void TestInitialize() {
            type = typeof(TableHtmlExtension);
            htmlMock = new mockHtmlHelper<CountriesPage>();
            r = new testRepo();
            addRandomCountries();
            page = new CountriesPage(r);
            page.OnGetIndexAsync(page.SortOrder, "", page.CurrentFilter, page.SearchString, page.PageIndex,
                page.FixedFilter, page.FixedValue).GetAwaiter();
        }

        private void addRandomCountries() {
            r.Add(random<Country>());
            r.Add(random<Country>());
            r.Add(random<Country>());
        }

        [TestMethod]
        public void ShowTableTest() {
            var obj = htmlMock.ShowTable(page);
            Assert.IsInstanceOfType(obj, typeof(HtmlContentBuilder));
        }

        [TestMethod]
        public void HtmlStringsTest() {
            var obj = new mockHtmlHelper<CountriesPage>().ShowTable(page);
            var expected = new List<string> {
                "<body>",
                "<table class=\"table\">",
                "<thead>",
                htmlMock.Header(createHeader()).ToString(),
                "</thead>",
                "<tbody>"
            };
            foreach (var item in r.list) {
                expected.Add("<tr class=>");
                expected.Add(htmlMock.Row(
                    false,
                    page.PageUrl,
                    page.ItemId,
                    page.SortOrder,
                    page.SearchString,
                    page.PageIndex,
                    page.FixedFilter,
                    page.FixedValue,
                    htmlMock.DisplayFor(modelItem => item.Id),
                    htmlMock.DisplayFor(modelItem => item.Name),
                    htmlMock.DisplayFor(modelItem => item.Code),
                    htmlMock.DisplayFor(modelItem => item.Details),
                    htmlMock.DisplayFor(modelItem => item.ValidFrom),
                    htmlMock.DisplayFor(modelItem => item.ValidTo)).ToString());
                expected.Add("</tr>");
            }
            expected.Add("</tbody>");
            expected.Add("</table>");
            expected.Add("</body>");

            var actual = htmlMock.HtmlStrings(page);
            htmlContains(actual, expected);
        }

        private Link[] createHeader() {
            var l = new List<Link>() {
                new Link(htmlMock.DisplayNameFor(m => m.Item.Id), page.SortUrl(m => m.Id)),
                new Link(htmlMock.DisplayNameFor(m => m.Item.Name), page.SortUrl(m => m.Name)),
                new Link(htmlMock.DisplayNameFor(m => m.Item.Code), page.SortUrl(m => m.Code)),
                new Link(htmlMock.DisplayNameFor(m => m.Item.Details), page.SortUrl(m => m.Details)),
                new Link(htmlMock.DisplayNameFor(m => m.Item.ValidFrom), page.SortUrl(m => m.ValidFrom)),
                new Link(htmlMock.DisplayNameFor(m => m.Item.ValidTo), page.SortUrl(m => m.ValidTo))
            };
            return l.ToArray();
        }
    }
}
