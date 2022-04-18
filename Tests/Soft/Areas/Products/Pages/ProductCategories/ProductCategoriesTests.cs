using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Facade.Products;
using Abc.Pages.Products;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Soft.Areas.Products.Pages.ProductCategories {
    public abstract class ProductCategoriesTests : BaseProductTests<ProductCategoryView, ProductCategoryData> {

        private List<SelectListItem> categories;
        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            categories = createSelectList(db.ProductCategories);
        }
        protected override string baseUrl() => ProductUrls.ProductCategories;
        protected override ProductCategoryView toView(ProductCategoryData d)
            => new ProductCategoryViewFactory().Create(new Abc.Domain.Products.Catalog.ProductCategory(d));
        protected override void changeValuesExceptId(ProductCategoryData d) {
            var id = d.Id;
            d = GetRandom.ObjectOf<ProductCategoryData>();
            d.Id = id;
        }
        protected override void addRelatedItems(ProductCategoryData d) {
            if (d is null) return;
            addProductCategory(d.BaseCategoryId);
        }
        protected override string getItemId(ProductCategoryData d) => d.Id;
        protected override void setItemId(ProductCategoryData d, string id) => d.Id = id;
        protected override string baseClassName() => "ProductCategory";
        protected override void isCorrectPageName() {
            var n = base.baseClassName().ToLower();
            var title = pageTitle().ToLower();
            Assert.AreEqual(title, n);
        }
        protected override IEnumerable<Expression<Func<ProductCategoryView, object>>> indexPageColumns =>
            new Expression<Func<ProductCategoryView, object>>[] {
                x => x.Name,
                x => x.BaseCategoryId,
                x => x.Code,
                x => x.Details,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, m => m.Name);
            canView(item, m => m.BaseCategoryId, Unspecified.String);
            canView(item, m => m.Code);
            canView(item, m => m.Details);
            canView(item, m => m.ValidFrom);
            canView(item, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, m => m.Name, true);
            canSelect(item, m => m.BaseCategoryId, categories);
            canEdit(item, m => m.Code);
            canEdit(item, m => m.Details);
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, m => m.Name, true);
            canSelect(null, m => m.BaseCategoryId, categories);
            canEdit(null, m => m.Code);
            canEdit(null, m => m.Details);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(item, x => x.Id, true);
        }
    }
    [TestClass] public class IndexPageTests :ProductCategoriesTests { }
    [TestClass] public class EditPageTests :ProductCategoriesTests { }
    [TestClass] public class DetailsPageTests :ProductCategoriesTests {
        [TestMethod]
        public void CanClickNavigationLinksTest() {
            clickNavigationLink("showCatalogEntries", ProductUrls.CatalogEntries);
        }
    }
    [TestClass] public class DeletePageTests :ProductCategoriesTests { }
    [TestClass] public class CreatePageTests :ProductCategoriesTests { }
}
