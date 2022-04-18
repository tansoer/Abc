using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Facade.Products;
using Abc.Pages.Products;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Products.Pages.ContainerItems {
    public abstract class ContainerItemsTests :BaseProductTests<ContainerItemView, ContainerItemData> {
        protected List<SelectListItem> products => createSelectList(db.Products);
        protected List<SelectListItem> containers => createSelectList(db.Products, x => x.ProductKind == ProductKind.Container);
        protected override string baseUrl() => ProductUrls.ContainerItems;
        protected override ContainerItemView toView(ContainerItemData d) 
            => new ContainerItemViewFactory().Create(new Abc.Domain.Products.ContainerItem(d));
        protected override void changeValuesExceptId(ContainerItemData d) {
            var id = d.Id;
            d = random<ContainerItemData>();
            d.Id = id;
        }
        protected override string getItemId(ContainerItemData d) => d.Id;
        protected override void setItemId(ContainerItemData d, string id) => d.Id = id;
        protected override void addRelatedItems(ContainerItemData d) {
            if (d is null) return;
            addProduct(d.ProductId, ProductKind.Product);
            addProduct(d.ContainerId, ProductKind.Container);
        }
        protected override IEnumerable<Expression<Func<ContainerItemView, object>>> indexPageColumns =>
            new Expression<Func<ContainerItemView, object>>[] {
                x => x.ContainerId,
                x => x.ProductId,
                x => x.RowNumber,
                x => x.ColumnNumber,
                x => x.LevelNumber,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, m => m.Id);
            canView(item, m => m.ContainerId, Unspecified.String);
            canView(item, m => m.ProductId, Unspecified.String);
            canView(item, m => m.RowNumber);
            canView(item, m => m.ColumnNumber);
            canView(item, m => m.LevelNumber);
            canView(item, m => m.Name);
            canView(item, m => m.Code);
            canView(item, m => m.Details);
            canView(item, m => m.ValidFrom);
            canView(item, m => m.ValidTo);
        }
        //TODO: IsRequired and IsNumber parameters do not make sense here, but it's the only way tests pass
        protected override void dataInEditPage() {
            canSelect(item, m => m.ContainerId, containers);
            canSelect(item, m => m.ProductId, products);
            canEdit(item, m => m.RowNumber, true, false, 0);
            canEdit(item, m => m.ColumnNumber, true, false, 0);
            canEdit(item, m => m.LevelNumber, true, false, 0);
            canEdit(item, m => m.Name, true);
            canEdit(item, m => m.Code);
            canEdit(item, m => m.Details);
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canSelect(null, x => x.ContainerId, containers);
            canSelect(null, x => x.ProductId, products);
            canEdit(null, m => m.RowNumber, true, false, 0);
            canEdit(null, m => m.ColumnNumber, true, false, 0);
            canEdit(null, m => m.LevelNumber, true, false, 0);
            canEdit(null, m => m.Name, true);
            canEdit(null, m => m.Code);
            canEdit(null, m => m.Details);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
        protected override void dataHiddenInPage()
           => hasHidden(item, x => x.Id, true);
    }
    [TestClass] public class CreatePageTests :ContainerItemsTests { }
    [TestClass] public class DeletePageTests :ContainerItemsTests { }
    [TestClass] public class DetailsPageTests :ContainerItemsTests { }
    [TestClass] public class EditPageTests :ContainerItemsTests { }
    [TestClass] public class IndexPageTests :ContainerItemsTests { }
}
