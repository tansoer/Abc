using System;
using System.Collections.Generic;
using Abc.Aids.Methods;
using Abc.Aids.Reflection;
using Abc.Data.Products;
using Abc.Data.Quantities;
using Abc.Domain.Products;
using Abc.Domain.Quantities;
using Abc.Facade.Products;
using Abc.Pages.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Abc.Pages.Common.Aids;

namespace Abc.Pages.Products {

    public sealed class ProductTypesPage : ViewsFactoryPage<ProductTypesPage, IProductTypesRepo, IProductType, 
        ProductTypeView, ProductTypeData, ProductTypeViewFactory, ProductKind> {
        protected override string title => ProductTitles.ProductTypes;
        private IEnumerable<SelectListItem> units;
        private IEnumerable<SelectListItem> productTypes;
        public ProductTypesPage(IProductTypesRepo r) : base(r) {}
        public IEnumerable<SelectListItem> Units => units ??= selectListByName<IUnitsRepo, Unit, UnitData>();
        public IEnumerable<SelectListItem> ProductTypes => productTypes ??= selectListByName<IProductTypesRepo, IProductType, ProductTypeData>(x => x.IsBaseType);


        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id),
            field(x => Item.Timestamp)
        };
        protected override void addFields() {
            addField(x => Item.Name);
            addField(x => Item.ProductKind);
            addField(x => Item.BaseTypeId, () => ProductTypes, () => BaseTypeName(Item.BaseTypeId));
            addField(x => Item.IsBaseType);
            addField(x => Item.Code);
            addField(x => Item.AlternativeCodes);
            addField(x => Item.Details);
            addField(x => Item.ValidFrom);
            addField(x => Item.ValidTo);
        }
        protected override void fieldsForEditors() => fieldsForViewers();
        protected override void fieldsForViewers() {
            switch (Item.ProductKind) {
                case ProductKind.Service:
                    addFieldBefore(field(x => Item.PeriodOfOperationFrom), x => Item.ValidFrom);
                    addFieldBefore(field(x => Item.PeriodOfOperationTo), x => Item.ValidFrom);
                    break;
                case ProductKind.MeasuredProduct or ProductKind.IdenticalProduct:
                    addFieldBefore(field(x => Item.Amount), x => Item.ValidFrom);
                    addFieldBefore(field(x => Item.UnitId, () => Units, () => UnitName(Item.UnitId)), x => Item.ValidFrom);
                    break;
                case ProductKind.Package:
                    addFieldBefore(field(x => Item.PricingStrategy), x => Item.ValidFrom);
                    break;
                case ProductKind.Container:
                    addFieldBefore(field(x => Item.LevelsCount), x => Item.ValidFrom);
                    addFieldBefore(field(x => Item.ColumnsCount), x => Item.ValidFrom);
                    addFieldBefore(field(x => Item.RowsCount), x => Item.ValidFrom);
                    break;
            }
        }
        protected internal override string pageUrl => ProductUrls.ProductTypes;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) {
            var productKind = Safe.Run(() => (ProductKind)(switchOfCreate ?? 1000), ProductKind.Unspecified);
            Item = new ProductTypeView { ProductKind = productKind };
            updateProductTypes(productKind);
        }
        protected override void doAfterOnGetEdit() => updateProductTypes(Item.ProductKind);
        internal void updateProductTypes(ProductKind currentKind) => productTypes = selectListByName<IProductTypesRepo, IProductType, ProductTypeData>(x => x.IsBaseType && (x.ProductKind == currentKind) );
        public string BaseTypeName(string id) => itemName(ProductTypes, id);
        public string UnitName(string id) => itemName(Units, id);
        public async Task<IActionResult> OnGetShowProductInstancesAsync(string id, string sortOrder, string searchString,
            int pageIndex, string fixedFilter, string fixedValue) {
            var name = GetMember.Name<ProductView>(x => x.ProductTypeId);
            var page = ProductUrls.Products;
            var url = new Uri($"{page}/Index?handler=Index&fixedFilter={name}&fixedValue={id}" +
                              $"&sortOrder=", UriKind.Relative);
            await Task.CompletedTask;
            return Redirect(url.ToString());
        }
        public async Task<IActionResult> OnGetShowFeatureTypesAsync(string id, string sortOrder, string searchString,
            int pageIndex, string fixedFilter, string fixedValue) {
            var name = GetMember.Name<FeatureTypeView>(x => x.ProductTypeId);
            var page = ProductUrls.FeatureTypes;
            var url = new Uri($"{page}/Index?handler=Index&fixedFilter={name}&fixedValue={id}" +
                              $"&sortOrder=", UriKind.Relative);
            await Task.CompletedTask;
            return Redirect(url.ToString());
        }
        public async Task<IActionResult> OnGetShowPricesAsync(string id, string sortOrder, string searchString,
            int pageIndex, string fixedFilter, string fixedValue) {
            var name = GetMember.Name<PriceView>(x => x.ProductTypeId);
            var page = ProductUrls.Prices;
            var url = new Uri($"{page}/Index?handler=Index&fixedFilter={name}&fixedValue={id}" +
                              $"&sortOrder=", UriKind.Relative);
            await Task.CompletedTask;
            return Redirect(url.ToString());
        }
        public async Task<IActionResult> OnGetShowPackageComponentsAsync(string id, string sortOrder, string searchString,
            int pageIndex, string fixedFilter, string fixedValue) {
            var name = GetMember.Name<PackageComponentData>(x => x.PackageTypeId);
            var page = ProductUrls.PackageComponents;
            var url = new Uri($"{page}/Index?handler=Index&fixedFilter={name}&fixedValue={id}" +
                              $"&sortOrder=", UriKind.Relative);
            await Task.CompletedTask;
            return Redirect(url.ToString());
        }
        public async Task<IActionResult> OnGetShowInclusionRulesAsync(string id, string sortOrder, string searchString,
            int pageIndex, string fixedFilter, string fixedValue) {
            var name = GetMember.Name<ProductInclusionRuleData>(x => x.PackageTypeId);
            var page = ProductUrls.ProductInclusionRules;
            var url = new Uri($"{page}/Index?handler=Index&fixedFilter={name}&fixedValue={id}" +
                              $"&sortOrder=", UriKind.Relative);
            await Task.CompletedTask;
            return Redirect(url.ToString());
        }
        public async Task<IActionResult> OnGetShowProductSetsAsync(string id, string sortOrder, string searchString,
            int pageIndex, string fixedFilter, string fixedValue) {
            var name = GetMember.Name<ProductSetData>(x => x.PackageTypeId);
            var page = ProductUrls.ProductSets;
            var url = new Uri($"{page}/Index?handler=Index&fixedFilter={name}&fixedValue={id}" +
                              $"&sortOrder=", UriKind.Relative);
            await Task.CompletedTask;
            return Redirect(url.ToString());
        }
    }
}

