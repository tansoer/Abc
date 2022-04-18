using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Facade.Products;
using Abc.Pages.Products;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Products.Pages.ProductSets {
    public abstract class ProductSetsTests :BaseProductTests<ProductSetView, ProductSetData> {
        protected List<SelectListItem> packageTypes => createSelectList(db.ProductTypes);
        protected override string baseUrl() => ProductUrls.ProductSets;
        protected override ProductSetView toView(ProductSetData d)
            => new ProductSetViewFactory().Create(new Abc.Domain.Products.Packets.ProductSet(d));
        protected override void changeValuesExceptId(ProductSetData d) {
            var id = d.Id;
            d = random<ProductSetData>();
            d.Id = id;
        }
        protected override string getItemId(ProductSetData d) => d.Id;
        protected override void setItemId(ProductSetData d, string id) => d.Id = id;
        protected override void addRelatedItems(ProductSetData d) {
            if (d is null) return;
            addProductType(d.PackageTypeId, ProductKind.Package);
        }
        protected override IEnumerable<Expression<Func<ProductSetView, object>>> indexPageColumns =>
            new Expression<Func<ProductSetView, object>>[] {
                x => x.Name,
                x => x.PackageTypeId,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, m => m.Id);
            canView(item, m => m.Name);
            canView(item, m => m.Code);
            canView(item, m => m.PackageTypeId, Unspecified.String);
            canView(item, m => m.Details);
            canView(item, m => m.ValidFrom);
            canView(item, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, m => m.Name, true);
            canEdit(item, m => m.Code);
            canSelect(item, m => m.PackageTypeId, packageTypes);
            canEdit(item, m => m.Details);
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, m => m.Name, true);
            canEdit(null, m => m.Code);
            canSelect(null, x => x.PackageTypeId, packageTypes);
            canEdit(null, m => m.Details);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(item, x => x.Id, true);
        }
    }
    [TestClass] public class CreatePageTests :ProductSetsTests { }
    [TestClass] public class DeletePageTests :ProductSetsTests { }
    [TestClass] public class DetailsPageTests :ProductSetsTests {
        [TestMethod] public void CanClickSetContentsNavigationLink()
            => clickNavigationLink("showProductSetContents", ProductUrls.ProductSetContents);
    }
    [TestClass] public class EditPageTests :ProductSetsTests { }
    [TestClass] public class IndexPageTests :ProductSetsTests { }
}
