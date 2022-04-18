using System;
using Abc.Aids.Random;
using Abc.Aids.Reflection;
using Abc.Data.Products;
using Abc.Domain.Products.Catalog;
using Abc.Facade.Products;
using Abc.Pages.Products;
using Abc.Tests.Pages.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Products {
    [TestClass]
    public class ProductCatalogsPageTests : SealedViewFactoryPageTests<
        ProductCatalogsPage,
        IProductCatalogsRepo,
        ProductCatalog,
        ProductCatalogView,
        ProductCatalogData,
        ProductCatalogViewFactory> {

        protected override string pageTitle => ProductTitles.ProductCatalogs;
        protected override string pageUrl => ProductUrls.ProductCatalogs;
        protected override ProductCatalog toObject(ProductCatalogData d) => new(d);
        private class testRepo
            : mockRepo<ProductCatalog, ProductCatalogData>,
                IProductCatalogsRepo { }

        private testRepo Repo;
        protected override ProductCatalogsPage createObject() {
            Repo = new testRepo();
            addRandomCatalogs();
            return new ProductCatalogsPage(Repo);
        }
        private void addRandomCatalogs() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++)
                Repo.Add(new ProductCatalog(GetRandom.ObjectOf<ProductCatalogData>()));
        }

        [TestMethod]
        public void OnGetShowEntriesAsyncTest() {
            var id = rndStr;
            var r = obj.OnGetShowEntriesAsync(id,
                rndStr,
                rndStr,
                GetRandom.UInt8(),
                rndStr,
                rndStr).GetAwaiter().GetResult() as RedirectResult;

            Assert.IsNotNull(r);

            var name = GetMember.Name<CatalogEntryData>(x => x.CatalogId);
            var page = ProductUrls.CatalogEntries;

            var url = new Uri($"{page}/Index?handler=Index&fixedFilter={name}&fixedValue={id}", UriKind.Relative);

            Assert.AreEqual(url.ToString(), r.Url);
        }
    }
}