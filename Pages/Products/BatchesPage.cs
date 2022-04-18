using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abc.Aids.Reflection;
using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Facade.Products;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abc.Pages.Products {

    public sealed class BatchesPage : ViewFactoryPage<BatchesPage, IBatchesRepo, Batch, BatchView, 
        BatchData, BatchViewFactory> {
        protected override string title => ProductTitles.Batches;
        public BatchesPage(IBatchesRepo r): base(r) { }
        private IEnumerable<SelectListItem> productTypes;
        public IEnumerable<SelectListItem> ProductTypes => productTypes ??= selectListByName<IProductTypesRepo, IProductType, ProductTypeData>();
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id),
        };
        protected override void tableColumns() {
            tableColumn(x => Item.Name);
            tableColumn(x => Item.ProductTypeId, () => ProductTypeName(Item.ProductTypeId));
            tableColumn(x => Item.Code);
            tableColumn(x => Item.Details);
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Id);
            valueViewer(x => Item.Name);
            valueViewer(x => Item.Code);
            valueViewer(x => Item.ProductTypeId, () => ProductTypeName(Item.ProductTypeId));
            valueViewer(x => Item.ProductsCount);
            valueViewer(x => Item.FirstSerialNumber);
            valueViewer(x => Item.LastSerialNumber);
            valueViewer(x => Item.DateProduced);
            valueViewer(x => Item.BestBefore);
            valueViewer(x => Item.SellBy);
            valueViewer(x => Item.UseBy);
            valueViewer(x => Item.Details);
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.Name);
            valueEditor(x => Item.ProductTypeId, () => ProductTypes);
            valueEditor(x => Item.Code);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.FirstSerialNumber);
            valueEditor(x => Item.LastSerialNumber);
            valueEditor(x => Item.DateProduced);
            valueEditor(x => Item.BestBefore);
            valueEditor(x => Item.SellBy);
            valueEditor(x => Item.UseBy);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => ProductUrls.Batches;
        public string ProductTypeName(string id) => itemName(ProductTypes, id);
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) 
            => Item = new();
        public async Task<IActionResult> OnGetShowProductsAsync(string id, string sortOrder, string searchString,
            int pageIndex, string fixedFilter, string fixedValue) {
            var name = GetMember.Name<ProductData>(x => x.BatchId);
            var page = ProductUrls.Products;
            var serialNumbers = GetMember.Name<ProductData>(x => x.Code);
            var url = new Uri($"{page}/Index?handler=Index&fixedFilter={name}&fixedValue={id}" +
                              $"&sortOrder={serialNumbers}", UriKind.Relative);
            await Task.CompletedTask;
            return Redirect(url.ToString());
        }
        public async Task<IActionResult> OnGetShowChecksAsync(string id, string sortOrder, string searchString,
            int pageIndex, string fixedFilter, string fixedValue) {
            var name = GetMember.Name<BatchCheckedByData>(x => x.BatchId);
            var page = ProductUrls.BatchCheckedBy;
            var dateChecked = GetMember.Name<BatchCheckedByData>(x => x.ValidFrom);
            var url = new Uri($"{page}/Index?handler=Index&fixedFilter={name}&fixedValue={id}" +
                              $"&sortOrder={dateChecked}", UriKind.Relative);
            await Task.CompletedTask;
            return Redirect(url.ToString());
        }
    }
}