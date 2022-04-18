using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Facade.Products;
using Abc.Pages.Products;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Products.Pages.ProductSetContents {
    public abstract class ProductSetContentsTests :BaseProductTests<ProductSetContentView, ProductSetContentData> {
        protected List<SelectListItem> productSets => createSelectList(db.ProductSets);
        protected List<SelectListItem> productTypes => createSelectList(db.ProductTypes);
        protected override string baseUrl() => ProductUrls.ProductSetContents;
        protected override ProductSetContentView toView(ProductSetContentData d)
            => new ProductSetContentViewFactory().Create(new Abc.Domain.Products.Packets.ProductSetContent(d));
        protected override void changeValuesExceptId(ProductSetContentData d) {
            var id = d.Id;
            d = random<ProductSetContentData>();
            d.Id = id;
        }
        protected override string getItemId(ProductSetContentData d) => d.Id;
        protected override void setItemId(ProductSetContentData d, string id) => d.Id = id;
        protected override void addRelatedItems(ProductSetContentData d) {
            if (d is null) return;
            addProductSet(d.ProductSetId);
            addProductType(d.ProductTypeId);
        }
        protected override IEnumerable<Expression<Func<ProductSetContentView, object>>> indexPageColumns =>
           new Expression<Func<ProductSetContentView, object>>[] {
                x => x.Name,
                x => x.ProductSetId,
                x => x.ProductTypeId,
                x => x.ValidFrom,
                x => x.ValidTo
           };
        protected override void dataInDetailsPage() {
            canView(item, m => m.Id);
            canView(item, m => m.Name);
            canView(item, m => m.Code);
            canView(item, m => m.ProductSetId, Unspecified.String);
            canView(item, m => m.ProductTypeId, Unspecified.String);
            canView(item, m => m.Details);
            canView(item, m => m.ValidFrom);
            canView(item, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, m => m.Name, true);
            canEdit(item, m => m.Code);
            canSelect(item, m => m.ProductSetId, productSets);
            canSelect(item, m => m.ProductTypeId, productTypes);
            canEdit(item, m => m.Details);
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, m => m.Name, true);
            canEdit(null, m => m.Code);
            canSelect(null, x => x.ProductSetId, productSets);
            canSelect(null, x => x.ProductTypeId, productTypes);
            canEdit(null, m => m.Details);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(item, x => x.Id, true);
        }
    }
    [TestClass] public class CreatePageTests :ProductSetContentsTests { }
    [TestClass] public class DeletePageTests :ProductSetContentsTests { }
    [TestClass] public class DetailsPageTests :ProductSetContentsTests { }
    [TestClass] public class EditPageTests :ProductSetContentsTests { }
    [TestClass] public class IndexPageTests :ProductSetContentsTests { }
}
