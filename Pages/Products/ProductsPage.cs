using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abc.Aids.Methods;
using Abc.Aids.Reflection;
using Abc.Data.Products;
using Abc.Data.Quantities;
using Abc.Domain.Products;
using Abc.Domain.Products.Packets;
using Abc.Domain.Quantities;
using Abc.Facade.Products;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abc.Pages.Products {
    public sealed class ProductsPage :ViewsFactoryPage<ProductsPage, IProductsRepo, IProduct, ProductView, ProductData, 
        ProductViewFactory, ProductKind> {
        protected override string title => ProductTitles.Products;
        public ProductsPage(IProductsRepo r) : base(r) {}
        private IEnumerable<SelectListItem> productTypes;
        public IEnumerable<SelectListItem> ProductTypes => productTypes ??= selectListByName<IProductTypesRepo, IProductType, ProductTypeData>();
        private IEnumerable<SelectListItem> units;
        public IEnumerable<SelectListItem> Units => units ??= selectListByName<IUnitsRepo, Unit, UnitData>();
        private IEnumerable<SelectListItem> batches;
        public IEnumerable<SelectListItem> Batches => batches ??= selectListByName<IBatchesRepo, Batch, BatchData>();
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id),
            field(x => Item.ProductKind)
        };
        protected override void tableColumns() {
            tableColumn(x => Item.Code);
            tableColumn(x => Item.Name);
            tableColumn(x => Item.ProductKind);
            tableColumn(x => Item.ProductTypeId, () => ProductTypeName(Item.ProductTypeId));
            tableColumn(x => Item.Details);
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Id);
            valueViewer(x => Item.Code);
            valueViewer(x => Item.Name);
            valueViewer(x => Item.ProductKind);
            valueViewer(x => Item.ProductTypeId, () => ProductTypeName(Item.ProductTypeId));
            valueViewer(x => Item.Details);
            valueViewer(x => Item.BatchId, () => BatchName(Item.BatchId));

            switch (Item.ProductKind) {
                case ProductKind.Service:
                    valueViewer(x => Item.ScheduledFrom);
                    valueViewer(x => Item.ScheduledTo);
                    valueViewer(x => Item.DeliveredFrom);
                    valueViewer(x => Item.DeliveredTo);
                    valueViewer(x => Item.DeliveryStatus);
                    break;
                case ProductKind.MeasuredProduct or ProductKind.IdenticalProduct:
                    valueViewer(x => Item.Amount);
                    valueViewer(x => Item.UnitId, () => UnitName(Item.UnitId));
                    break;
            }
            valueViewer(x => Item.ReservationStatus);
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.Code);
            valueEditor(x => Item.Name);
            valueEditor(x => Item.ProductTypeId, () => ProductTypes);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.BatchId, () => Batches);
            switch (Item.ProductKind) {
                case ProductKind.Service:
                    valueEditor(x => Item.ScheduledFrom);
                    valueEditor(x => Item.ScheduledTo);
                    valueEditor(x => Item.DeliveredFrom);
                    valueEditor(x => Item.DeliveredTo);
                    valueEditor(x => Item.DeliveryStatus, () => new SelectList(Enum.GetNames(typeof(DeliveryStatus))));
                    break;
                case ProductKind.MeasuredProduct or ProductKind.IdenticalProduct:
                    valueEditor(x => Item.Amount);
                    valueEditor(x => Item.UnitId, () => Units);
                    break;
            }

            valueEditor(x => Item.ReservationStatus, () => new SelectList(Enum.GetNames(typeof(ReservationStatus))));
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => ProductUrls.Products;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) {
            var productKind = Safe.Run(() => (ProductKind)(switchOfCreate ?? 1000), ProductKind.Unspecified);
            Item = new ProductView() { ProductKind = productKind };
            if (fixedFilterIsProductTypeId) Item.ProductTypeId = fixedValue;
            if (fixedFilterIsBatchId) Item.BatchId = fixedValue;
            updateProductTypes(productKind);
        }
        protected override void doAfterOnGetEdit() => updateProductTypes(Item.ProductKind);
        internal void updateProductTypes(ProductKind currentKind) => productTypes = selectListByName<IProductTypesRepo, IProductType, ProductTypeData>(d => d.ProductKind == currentKind);
        public string ProductTypeName(string id) => itemName(ProductTypes, id);
        public string UnitName(string id) => itemName(Units, id);
        public string BatchName(string id) => itemName(Batches, id);
        protected internal override string subtitle => getSubTitle();
        private string getSubTitle() {
            if (fixedFilterIsBatchId) return toSubTitle(removeId(FixedFilter), BatchName(FixedValue));
            if (fixedFilterIsProductTypeId) return toSubTitle(removeId(FixedFilter), ProductTypeName(FixedValue));
            return string.Empty; 
        }
        private static string toSubTitle(string item, string name) => $"For {item} ({name})";
        private bool fixedFilterIsProductTypeId => Equals(FixedFilter, GetMember.Name<ProductView>(x => x.ProductTypeId));
        private bool fixedFilterIsBatchId => Equals(FixedFilter, GetMember.Name<ProductData>(x => x.BatchId));
        private static string removeId(string idField) => idField?.Replace("Id", string.Empty) ?? string.Empty;
        public async Task<IActionResult> OnGetShowFeaturesAsync(string id, string sortOrder, string searchString,
            int pageIndex, string fixedFilter, string fixedValue) {
            var name = GetMember.Name<FeatureView>(x => x.ProductId);
            var page = ProductUrls.Features;
            var url = new Uri($"{page}/Index?handler=Index&fixedFilter={name}&fixedValue={id}" +
                              $"&sortOrder=", UriKind.Relative);
            await Task.CompletedTask;
            return Redirect(url.ToString());
        }
        public async Task<IActionResult> OnGetShowPricesAsync(string id, string sortOrder, string searchString,
            int pageIndex, string fixedFilter, string fixedValue) {
            var name = GetMember.Name<PriceView>(x => x.ProductId);
            var page = ProductUrls.Prices;
            var url = new Uri($"{page}/Index?handler=Index&fixedFilter={name}&fixedValue={id}" +
                              $"&sortOrder=", UriKind.Relative);
            await Task.CompletedTask;
            return Redirect(url.ToString());
        }
        public async Task<IActionResult> OnGetShowPackageContentsAsync(string id, string sortOrder, string searchString,
            int pageIndex, string fixedFilter, string fixedValue) {
            var name = GetMember.Name<PackageContentData>(x => x.PackageId);
            var page = ProductUrls.PackageContents;
            var url = new Uri($"{page}/Index?handler=Index&fixedFilter={name}&fixedValue={id}" +
                              $"&sortOrder=", UriKind.Relative);
            await Task.CompletedTask;
            return Redirect(url.ToString());
        }
        public async Task<IActionResult> OnGetShowContainerItemsAsync(string id, string sortOrder, string searchString,
            int pageIndex, string fixedFilter, string fixedValue) {
            var name = GetMember.Name<ContainerItemData>(x => x.ContainerId);
            var page = ProductUrls.ContainerItems;
            var url = new Uri($"{page}/Index?handler=Index&fixedFilter={name}&fixedValue={id}" +
                              $"&sortOrder=", UriKind.Relative);
            await Task.CompletedTask;
            return Redirect(url.ToString());
        }
        //All code related to package is below
        //To be reviewed when validating corresponding class diagram
        public List<ProductInclusionRuleView> InclusionRules { get; internal set; }
        public string GetPackageValidationMessage(bool isValid) => isValid ? "This package is valid" : "This package is not valid";
        public bool ValidatePackage() {
            Package package = (Package)toObject(Item);
            return Safe.Run(() => package.Type.Validate(package), false);
        }
        public void LoadInclusionRules() {
            var ruleObjects = ((Package)toObject(Item)).Type.InclusionRules;
            List<ProductInclusionRuleView> ruleViews = new();
            if (ruleObjects != null)
                foreach (var r in ruleObjects) ruleViews.Add(new ProductInclusionRuleViewFactory().Create(r));
            InclusionRules = ruleViews;
        }
    }
}

