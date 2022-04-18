using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abc.Aids.Reflection;
using Abc.Data.Products;
using Abc.Domain.Products.Catalog;
using Abc.Facade.Products;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abc.Pages.Products {
    public sealed class CatalogEntriesPage : ViewFactoryPage<CatalogEntriesPage, ICatalogEntriesRepo, CatalogEntry,
        CatalogEntryView, CatalogEntryData, CatalogEntryViewFactory> {
        protected override string title => ProductTitles.CatalogEntries;
        public CatalogEntriesPage(ICatalogEntriesRepo r) : base(r) {}
        private IEnumerable<SelectListItem> catalogs;
        public IEnumerable<SelectListItem> Catalogs => catalogs ??= selectListByName<IProductCatalogsRepo, ProductCatalog, ProductCatalogData>();
        private IEnumerable<SelectListItem> categories;
        public IEnumerable<SelectListItem> Categories => categories ??= selectListByName<IProductCategoriesRepo, ProductCategory, ProductCategoryData>();
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id),
        };
        protected override void tableColumns() {
            tableColumn(x => Item.Name);
            tableColumn(x => Item.Code);
            tableColumn(x => Item.Details);
            tableColumn(x => Item.CatalogId, () => CatalogName(Item.CatalogId));
            tableColumn(x => Item.CategoryId, () => CategoryName(Item.CategoryId));
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Id);
            valueViewer(x => Item.Name);
            valueViewer(x => Item.Code);
            valueViewer(x => Item.CatalogId, () => CatalogName(Item.CatalogId));
            valueViewer(x => Item.CategoryId, () => CategoryName(Item.CategoryId));
            valueViewer(x => Item.Details);
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.Name);
            valueEditor(x => Item.Code);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.CatalogId, () => Catalogs);
            valueEditor(x => Item.CategoryId, () => Categories);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => ProductUrls.CatalogEntries;
        protected internal override string subtitle => getSubTitle();
        private string getSubTitle() {
            if (fixedFilterIsCatalogId) return $"For {removeId(FixedFilter)} : {CatalogName(FixedValue)}";
            if (fixedFilterIsCategoryId) return $"For {removeId(FixedFilter)} : {CategoryName(FixedValue)}";
            return string.Empty;
        }
        private static string removeId(string idField) => idField?.Replace("Id", string.Empty) ?? string.Empty;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) {
            Item = new();
            if (fixedFilterIsCatalogId) Item.CatalogId = fixedValue;
            if (fixedFilterIsCategoryId) Item.CategoryId = fixedValue;
        }
        private bool fixedFilterIsCatalogId => Equals(FixedFilter, GetMember.Name<CatalogEntryData>(x => x.CatalogId));
        private bool fixedFilterIsCategoryId => Equals(FixedFilter, GetMember.Name<CatalogEntryData>(x => x.CategoryId));
       public string CatalogName(string id) => itemName(Catalogs, id);
        public string CategoryName(string id) => itemName(Categories, id);
        public async Task<IActionResult> OnGetShowCatalogedProductsAsync(string id, string sortOrder, string searchString,
            int pageIndex, string fixedFilter, string fixedValue) {
            var name = GetMember.Name<CatalogedProductData>(x => x.CatalogEntryId);
            var page = ProductUrls.CatalogedProducts;
            var url = new Uri($"{page}/Index?handler=Index&fixedFilter={name}&fixedValue={id}", UriKind.Relative);
            await Task.CompletedTask;
            return Redirect(url.ToString());
        }
    }
}
