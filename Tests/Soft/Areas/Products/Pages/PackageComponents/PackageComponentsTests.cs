using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Facade.Products;
using Abc.Pages.Products;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Products.Pages.PackageComponents {
    public abstract class PackageComponentsTests :BaseProductTests<PackageComponentView, PackageComponentData> {
        protected List<SelectListItem> productTypes => createSelectList(db.ProductTypes);
        protected List<SelectListItem> packageTypes => createSelectList(db.ProductTypes, (x => x.ProductKind == ProductKind.Package));
        protected override string baseUrl() => ProductUrls.PackageComponents;
        protected override PackageComponentView toView(PackageComponentData d)
            => new PackageComponentViewFactory().Create(new Abc.Domain.Products.Packets.PackageComponent(d));
        protected override void changeValuesExceptId(PackageComponentData d) {
            var id = d.Id;
            d = random<PackageComponentData>();
            d.Id = id;
        }
        protected override string getItemId(PackageComponentData d) => d.Id;
        protected override void setItemId(PackageComponentData d, string id) => d.Id = id;
        protected override void addRelatedItems(PackageComponentData d) {
            if (d is null) return;
            addProductType(d.ProductTypeId, ProductKind.Product);
            addProductType(d.PackageTypeId, ProductKind.Package);
        }
        protected override IEnumerable<Expression<Func<PackageComponentView, object>>> indexPageColumns =>
            new Expression<Func<PackageComponentView, object>>[] {
                x => x.Name,
                x => x.PackageTypeId,
                x => x.ProductTypeId,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, m => m.Id);
            canView(item, m => m.Name);
            canView(item, m => m.PackageTypeId, Unspecified.String);
            canView(item, m => m.ProductTypeId, Unspecified.String);
            canView(item, m => m.Code);
            canView(item, m => m.Details);
            canView(item, m => m.ValidFrom);
            canView(item, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, m => m.Name, true);
            canSelect(item, m => m.PackageTypeId, packageTypes);
            canSelect(item, m => m.ProductTypeId, productTypes);           
            canEdit(item, m => m.Code);
            canEdit(item, m => m.Details);
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, m => m.Name, true);
            canSelect(null, x => x.PackageTypeId, packageTypes);
            canSelect(null, x => x.ProductTypeId, productTypes);
            canEdit(null, m => m.Code);
            canEdit(null, m => m.Details);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(item, x => x.Id, true);
        }
    }
    [TestClass] public class CreatePageTests :PackageComponentsTests { }
    [TestClass] public class DeletePageTests :PackageComponentsTests { }
    [TestClass] public class DetailsPageTests :PackageComponentsTests { }
    [TestClass] public class EditPageTests :PackageComponentsTests { }
    [TestClass] public class IndexPageTests :PackageComponentsTests { }
}
