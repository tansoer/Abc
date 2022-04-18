using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Facade.Products;
using Abc.Pages.Products;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Products.Pages.PackageContents {
    public abstract class PackageContentsTests :BaseProductTests<PackageContentView, PackageContentData> {
        protected List<SelectListItem> products => createSelectList(db.Products);
        protected List<SelectListItem> packages => createSelectList(db.ProductTypes, x => x.ProductKind == ProductKind.Package);
        protected List<SelectListItem> components => createSelectList(db.PackageComponents);
        protected override string baseUrl() => ProductUrls.PackageContents;
        protected override PackageContentView toView(PackageContentData d)
            => new PackageContentViewFactory().Create(new Abc.Domain.Products.Packets.PackageContent(d));
        protected override void changeValuesExceptId(PackageContentData d) {
            var id = d.Id;
            d = random<PackageContentData>();
            d.Id = id;
        }
        protected override string getItemId(PackageContentData d) => d.Id;
        protected override void setItemId(PackageContentData d, string id) => d.Id = id;
        protected override void addRelatedItems(PackageContentData d) {
            if (d is null) return;
            addProduct(d.PackageId, ProductKind.Package);
            addProduct(d.ProductId, ProductKind.Product);
            addPackageComponent(d.ComponentId);
        }
        protected override IEnumerable<Expression<Func<PackageContentView, object>>> indexPageColumns =>
            new Expression<Func<PackageContentView, object>>[] {
                x => x.PackageId,
                x => x.ProductId,
                x => x.ComponentId,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, m => m.Id);
            canView(item, m => m.Name);
            canView(item, m => m.PackageId, Unspecified.String);
            canView(item, m => m.ProductId, Unspecified.String);
            canView(item, m => m.ComponentId, Unspecified.String);
            canView(item, m => m.Code);
            canView(item, m => m.Details);
            canView(item, m => m.ValidFrom);
            canView(item, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, m => m.Name, true);
            canSelect(item, m => m.PackageId, packages);
            canSelect(item, m => m.ProductId, products);
            canSelect(item, m => m.ComponentId, components);
            canEdit(item, m => m.Code);
            canEdit(item, m => m.Details);
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, m => m.Name, true);
            canSelect(null, x => x.PackageId, packages);
            canSelect(null, x => x.ProductId, products);
            canSelect(null, x => x.ComponentId, components);
            canEdit(null, m => m.Code);
            canEdit(null, m => m.Details);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(item, x => x.Id, true);
        }
    }
    [TestClass] public class CreatePageTests :PackageContentsTests { }
    [TestClass] public class DeletePageTests :PackageContentsTests { }
    [TestClass] public class DetailsPageTests :PackageContentsTests { }
    [TestClass] public class EditPageTests :PackageContentsTests { }
    [TestClass] public class IndexPageTests :PackageContentsTests { }
}
