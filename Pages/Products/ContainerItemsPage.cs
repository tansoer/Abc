using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Facade.Products;
using Abc.Pages.Common;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Abc.Pages.Products {
    public sealed class ContainerItemsPage :ViewPage<ContainerItemsPage, IContainerItemsRepo,
        ContainerItem, ContainerItemView, ContainerItemData> {
        protected override string title => ProductTitles.ContainerItems;
        private IEnumerable<SelectListItem> products;
        private IEnumerable<SelectListItem> containers;
        public IEnumerable<SelectListItem> Products => products ??= selectListByName<IProductsRepo, IProduct, ProductData>();
        public IEnumerable<SelectListItem> Containers => containers ??= selectListByName<IProductsRepo, IProduct, ProductData>(x=> x.ProductKind == ProductKind.Container);
        public ContainerItemsPage(IContainerItemsRepo r) :base(r) {
        }
        protected override void tableColumns() {
            tableColumn(x => Item.ContainerId);
            tableColumn(x => Item.ProductId);
            tableColumn(x => Item.RowNumber);
            tableColumn(x => Item.ColumnNumber);
            tableColumn(x => Item.LevelNumber);
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        public override IHtmlContent ValueFor(IHtmlHelper<ContainerItemsPage> html, int i) => i switch {
            0 => html.Raw(ProductName(Item?.ContainerId)),
            1 => html.Raw(ProductName(Item?.ProductId)),
            _ => base.ValueFor(html, i)
        };
        protected override void valueViewers() {
            valueViewer(x => Item.Id);
            valueViewer(x => Item.ContainerId, () => ProductName(Item.ContainerId));
            valueViewer(x => Item.ProductId, () => ProductName(Item.ProductId));
            valueViewer(x => Item.RowNumber);
            valueViewer(x => Item.ColumnNumber);
            valueViewer(x => Item.LevelNumber);
            valueViewer(x => Item.Name);
            valueViewer(x => Item.Code);
            valueViewer(x => Item.Details);
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.ContainerId, () => Containers);
            valueEditor(x => Item.ProductId, () => Products);
            valueEditor(x => Item.RowNumber);
            valueEditor(x => Item.ColumnNumber);
            valueEditor(x => Item.LevelNumber);
            valueEditor(x => Item.Name);
            valueEditor(x => Item.Code);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => ProductUrls.ContainerItems;
        protected internal override ContainerItem toObject(ContainerItemView v) => new ContainerItemViewFactory().Create(v);
        protected internal override ContainerItemView toView(ContainerItem o) => new ContainerItemViewFactory().Create(o);
        public override IActionResult OnGetCreate(string sortOrder, string searchString, int? pageIndex, string fixedFilter, string fixedValue, int? switchOfCreate) {
            createItem(fixedValue);
            return base.OnGetCreate(sortOrder, searchString, pageIndex, fixedFilter, fixedValue, switchOfCreate);
        }
        private void createItem(string containerId) => Item = new() {ContainerId =  containerId};
        public string ProductName(string id) => itemName(Products, id);
    }
}
