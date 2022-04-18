using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Facade.Products;
using Abc.Pages.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Products.Pages.ProductCatalogs {
    public abstract class ProductCatalogsTests :BaseProductTests<ProductCatalogView, ProductCatalogData> {
        protected override string baseUrl() => ProductUrls.ProductCatalogs;
        protected override ProductCatalogView toView(ProductCatalogData d)
            => new ProductCatalogViewFactory().Create(new Abc.Domain.Products.Catalog.ProductCatalog(d));
        protected override void changeValuesExceptId(ProductCatalogData d) {
            var id = d.Id;
            d = GetRandom.ObjectOf<ProductCatalogData>();
            d.Id = id;
        }
        protected override string getItemId(ProductCatalogData d) => d.Id;
        protected override void setItemId(ProductCatalogData d, string id) => d.Id = id;
        protected override string baseClassName() => "ProductCatalog";
        protected override IEnumerable<Expression<Func<ProductCatalogView, object>>> indexPageColumns =>
            new Expression<Func<ProductCatalogView, object>>[] {
                x => x.Name,
                x => x.Code,
                x => x.Details,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, m => m.Id);
            canView(item, m => m.Name);
            canView(item, m => m.Code);
            canView(item, m => m.Details);
            canView(item, m => m.ValidFrom);
            canView(item, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, m => m.Name, true);
            canEdit(item, m => m.Code);
            canEdit(item, m => m.Details);
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, m => m.Name, true);
            canEdit(null, m => m.Code);
            canEdit(null, m => m.Details);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(item, x => x.Id, true);
        }
    }
    [TestClass] public class IndexPageTests :ProductCatalogsTests { }
    [TestClass] public class EditPageTests :ProductCatalogsTests { }
    [TestClass] public class DetailsPageTests :ProductCatalogsTests {
        [TestMethod]
        public void CanClickShowCatalogEntriesNavigationLink()
            => clickNavigationLink("showEntries", ProductUrls.CatalogEntries);
    }
    [TestClass] public class DeletePageTests :ProductCatalogsTests { }
    [TestClass] public class CreatePageTests :ProductCatalogsTests { }
}
