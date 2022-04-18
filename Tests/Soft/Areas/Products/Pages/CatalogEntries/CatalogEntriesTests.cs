using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Facade.Products;
using Abc.Pages.Products;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Products.Pages.CatalogEntries {
    public abstract class CatalogEntriesTests :BaseProductTests<CatalogEntryView, CatalogEntryData> {
        protected List<SelectListItem> catalogs => createSelectList(db.ProductCatalogs);
        protected List<SelectListItem> categories => createSelectList(db.ProductCategories);
        protected override string baseUrl() => ProductUrls.CatalogEntries;
        protected override CatalogEntryView toView(CatalogEntryData d) 
            => new CatalogEntryViewFactory().Create(new Abc.Domain.Products.Catalog.CatalogEntry(d));   
        protected override void changeValuesExceptId(CatalogEntryData d) {
            var id = d.Id;
            d = random<CatalogEntryData>();
            d.Id = id;
        }
        protected override string getItemId(CatalogEntryData d) => d.Id;
        protected override void setItemId(CatalogEntryData d, string id) => d.Id = id;
        protected override string performTitleCorrection(string title) => "CatalogEntry";
        protected override string baseClassName() => "CatalogEntry";
        protected override void addRelatedItems(CatalogEntryData d) {
            if (d is null) return;
            addProductCatalog(d.CatalogId);
            addProductCategory(d.CategoryId);
        }
        protected override IEnumerable<Expression<Func<CatalogEntryView, object>>> indexPageColumns =>
            new Expression<Func<CatalogEntryView, object>>[] {
                x => x.Name,
                x => x.Code,
                x => x.Details,
                x => x.CatalogId,
                x => x.CategoryId,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, m => m.Id);
            canView(item, m => m.Name);
            canView(item, m => m.Code);
            canView(item, m => m.CatalogId, Unspecified.String);
            canView(item, m => m.CategoryId, Unspecified.String);
            canView(item, m => m.Details);
            canView(item, m => m.ValidFrom);
            canView(item, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, m => m.Name, true);
            canEdit(item, m => m.Code);
            canEdit(item, m => m.Details);
            canSelect(item, m => m.CatalogId, catalogs);
            canSelect(item, m => m.CategoryId, categories);
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, m => m.Name, true);
            canEdit(null, m => m.Code);
            canEdit(null, m => m.Details);
            canSelect(null, x => x.CatalogId, catalogs);
            canSelect(null, x => x.CategoryId, categories);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
        protected override void dataHiddenInPage() => hasHidden(item, x => x.Id, true);
    }
    [TestClass] public class CreatePageTests :CatalogEntriesTests { }
    [TestClass] public class DeletePageTests :CatalogEntriesTests { }
    [TestClass] public class DetailsPageTests :CatalogEntriesTests {
        [TestMethod]
        public void CanClickCatalogedProductsNavigationLink()
            => clickNavigationLink("showCatalogedProducts", ProductUrls.CatalogedProducts);
    }
    [TestClass] public class EditPageTests :CatalogEntriesTests { }
    [TestClass] public class IndexPageTests :CatalogEntriesTests { }
}
