using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Facade.Products;
using Abc.Pages.Products;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Products.Pages.CataloguedProducts {
    public abstract class CatalogedProductsTests :BaseProductTests<CatalogedProductView, CatalogedProductData> {
        protected List<SelectListItem> productTypes => createSelectList(db.ProductTypes);
        protected List<SelectListItem> catalogEntries => createSelectList(db.CatalogEntries);
        protected override string baseUrl() => ProductUrls.CatalogedProducts;
        protected override CatalogedProductView toView(CatalogedProductData d)
            => new CatalogedProductViewFactory().Create(new Abc.Domain.Products.Catalog.CatalogedProduct(d));
        protected override void changeValuesExceptId(CatalogedProductData d) {
            var id = d.Id;
            d = random<CatalogedProductData>();
            d.Id = id;
        }
        protected override string getItemId(CatalogedProductData d) => d.Id;
        protected override void setItemId(CatalogedProductData d, string id) => d.Id = id;
        protected override void addRelatedItems(CatalogedProductData d) {
            if (d is null) return;
            addProductType(d.ProductTypeId);
            addCatalogEntry(d.CatalogEntryId);
        }
        protected override IEnumerable<Expression<Func<CatalogedProductView, object>>> indexPageColumns =>
            new Expression<Func<CatalogedProductView, object>>[] {
                x => x.CatalogEntryId,
                x => x.ProductTypeId,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, m => m.Id);
            canView(item, m => m.Name);
            canView(item, m => m.Code);
            canView(item, m => m.Details);
            canView(item, m => m.CatalogEntryId, Unspecified.String);
            canView(item, m => m.ProductTypeId, Unspecified.String);
            canView(item, m => m.ValidFrom);
            canView(item, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, m => m.Name, true);
            canEdit(item, m => m.Code);
            canEdit(item, m => m.Details);
            canSelect(item, m => m.CatalogEntryId, catalogEntries);
            canSelect(item, m => m.ProductTypeId, productTypes);
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, m => m.Name, true);
            canEdit(null, m => m.Code);
            canEdit(null, m => m.Details);
            canSelect(null, x => x.CatalogEntryId, catalogEntries);
            canSelect(null, x => x.ProductTypeId, productTypes);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
        protected override void dataHiddenInPage()
            => hasHidden(item, x => x.Id, true);
    }
    [TestClass] public class CreatePageTests :CatalogedProductsTests { }
    [TestClass] public class DeletePageTests :CatalogedProductsTests { }
    [TestClass] public class DetailsPageTests :CatalogedProductsTests { }
    [TestClass] public class EditPageTests :CatalogedProductsTests { }
    [TestClass] public class IndexPageTests :CatalogedProductsTests { }
}
