using System.Collections.Generic;
using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Domain.Products.Catalog;
using Abc.Facade.Products;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abc.Pages.Products {
    public sealed class CatalogedProductPage : ViewFactoryPage<CatalogedProductPage, ICatalogedProductsRepo, CatalogedProduct,
        CatalogedProductView, CatalogedProductData, CatalogedProductViewFactory> {
        protected override string title => ProductTitles.CatalogedProducts;
        protected internal override string subtitle => HasFixedFilter ? $"For entry: {CatalogEntryName(FixedValue)}" : "";
        protected internal override IPageUrl masterPage() => new CatalogEntriesPage(default);

        public CatalogedProductPage(ICatalogedProductsRepo r) :base(r) {}
        private IEnumerable<SelectListItem> catalogEntries;
        public IEnumerable<SelectListItem> CatalogEntries => catalogEntries ??= selectListByName< ICatalogEntriesRepo , CatalogEntry, CatalogEntryData>();
        private IEnumerable<SelectListItem> productTypes;
        public IEnumerable<SelectListItem> ProductTypes => productTypes ??= selectListByName<IProductTypesRepo, IProductType, ProductTypeData>();
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id),
        };
        protected override void tableColumns() {
            tableColumn(x => Item.CatalogEntryId, () => CatalogEntryName(Item.CatalogEntryId));
            tableColumn(x => Item.ProductTypeId, () => ProductTypeName(Item.ProductTypeId));
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Id);
            valueViewer(x => Item.Name);
            valueViewer(x => Item.Code);
            valueViewer(x => Item.Details);
            valueViewer(x => Item.CatalogEntryId, () => CatalogEntryName(Item.CatalogEntryId));
            valueViewer(x => Item.ProductTypeId, () => CatalogEntryName(Item.ProductTypeId));
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.Name);
            valueEditor(x => Item.Code);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.CatalogEntryId, () => CatalogEntries);
            valueEditor(x => Item.ProductTypeId, () => ProductTypes);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => ProductUrls.CatalogedProducts;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) {
            Item = new();
            if (HasFixedFilter) Item.CatalogEntryId = fixedValue;
        }
        public string CatalogEntryName(string id) => itemName(CatalogEntries, id);
        public string ProductTypeName(string id) => itemName(ProductTypes, id);
    }
}
