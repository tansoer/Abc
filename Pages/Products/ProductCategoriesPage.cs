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
    public sealed class ProductCategoriesPage :ViewFactoryPage<ProductCategoriesPage, IProductCategoriesRepo, ProductCategory, 
            ProductCategoryView, ProductCategoryData, ProductCategoryViewFactory> {
        protected override string title => ProductTitles.ProductCategories;
        public ProductCategoriesPage(IProductCategoriesRepo r) : base(r) { } 
        private IEnumerable<SelectListItem> categories;
        public IEnumerable<SelectListItem> Categories => categories ??= selectListByName<IProductCategoriesRepo, ProductCategory, ProductCategoryData>();
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id),
        };
        protected override void tableColumns() {
            tableColumn(x => Item.Name);
            tableColumn(x => Item.BaseCategoryId, () => CategoryName(Item.BaseCategoryId));
            tableColumn(x => Item.Code);
            tableColumn(x => Item.Details);
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Id);
            valueViewer(x => Item.Name);
            valueViewer(x => Item.BaseCategoryId, () => CategoryName(Item.BaseCategoryId));
            valueViewer(x => Item.Code);
            valueViewer(x => Item.Details);
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.Name);
            valueEditor(x => Item.BaseCategoryId, () => Categories);
            valueEditor(x => Item.Code);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => ProductUrls.ProductCategories;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) => Item = new();
        public string CategoryName(string id) => itemName(Categories, id);
        public async Task<IActionResult> OnGetShowCatalogEntriesAsync(string id, string sortOrder, string searchString,
            int pageIndex, string fixedFilter, string fixedValue) {
            var name = GetMember.Name<CatalogEntryView>(x => x.CategoryId);
            var page = ProductUrls.CatalogEntries;
            var url = new Uri($"{page}/Index?handler=Index&fixedFilter={name}&fixedValue={id}", UriKind.Relative);
            await Task.CompletedTask;
            return Redirect(url.ToString());
        }
    }
}
