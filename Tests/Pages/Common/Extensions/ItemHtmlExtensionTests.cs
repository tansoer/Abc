using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Pages.Classifiers;
using Abc.Pages.Common.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Common.Extensions {
    [TestClass]
    public class ItemHtmlExtensionTests: TestsBase {
        private mockHtmlHelper<RegistrationAuthoritiesPage> htmlMock;
        private testRepo r;
        private RegistrationAuthoritiesPage page;
        private IClassifier item;
        private class testRepo :
            mockRepo<IClassifier, ClassifierData>,
            IClassifiersRepo { }

        [TestInitialize]
        public void TestInitialize() {
            type = typeof(ItemHtmlExtension);
            htmlMock = new mockHtmlHelper<RegistrationAuthoritiesPage>();
            r = new testRepo();
            addRandomAuthorities();
            item = authority();
            r.Add(item);
            addRandomAuthorities();
            page = new RegistrationAuthoritiesPage(r);
        }

        private void addRandomAuthorities() {
            r.Add(authority());
            r.Add(authority());
            r.Add(authority());
        }

        private static IClassifier authority() {
            var d = random<ClassifierData>();
            d.ClassifierType = ClassifierType.RegistrationAuthority;
            return ClassifierFactory.Create(d);
        }

        [TestMethod]
        public void ShowItemTest() {
            page.OnGetDetailsAsync(item.Id, page.SortOrder, page.SearchString, page.PageIndex,
                page.FixedFilter, page.FixedValue).GetAwaiter();
            var obj = htmlMock.ShowItem(page);
            Assert.IsInstanceOfType(obj, typeof(HtmlContentBuilder));
        }
        [TestMethod]
        public void EditItemTest() {
            page.OnGetEditAsync(item.Id, page.SortOrder, page.SearchString, page.PageIndex,
                page.FixedFilter, page.FixedValue).GetAwaiter();
            var obj = htmlMock.EditItem(page);
            Assert.IsInstanceOfType(obj, typeof(HtmlContentBuilder));
        }
    }
}
