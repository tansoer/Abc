using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abc.Aids.Reflection;
using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Domain.Products.Features;
using Abc.Facade.Products;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abc.Pages.Products {
    public sealed class FeatureTypesPage : ViewFactoryPage<FeatureTypesPage, IFeatureTypesRepo, FeatureType, 
        FeatureTypeView, FeatureTypeData, FeatureTypeViewFactory> {
        protected override string title => ProductTitles.FeatureTypes;
        public FeatureTypesPage(IFeatureTypesRepo r) : base(r) { }
        private IEnumerable<SelectListItem> productTypes;
        public IEnumerable<SelectListItem> ProductTypes => productTypes ??= selectListByName<IProductTypesRepo, IProductType, ProductTypeData>();
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id),
        };
        protected override void tableColumns() {
            tableColumn(x => Item.IsMandatory);
            tableColumn(x => Item.ProductTypeId, () => ProductTypeName(Item.ProductTypeId));
            tableColumn(x => Item.Name);
            tableColumn(x => Item.Code);
            tableColumn(x => Item.NumericCode);
            tableColumn(x => Item.Details);
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Id);
            valueViewer(x => Item.IsMandatory);
            valueViewer(x => Item.MustSatisfyAll);
            valueViewer(x => Item.BaseTypeId);
            valueViewer(x => Item.ProductTypeId, () => ProductTypeName(Item.ProductTypeId));
            valueViewer(x => Item.ValueType);
            valueViewer(x => Item.Name);
            valueViewer(x => Item.Code);
            valueViewer(x => Item.NumericCode);
            valueViewer(x => Item.Details);
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.IsMandatory);
            valueEditor(x => Item.MustSatisfyAll);
            valueEditor(x => Item.BaseTypeId);
            valueEditor(x => Item.ProductTypeId, () => ProductTypes);
            valueEditor(x => Item.ValueType, () => new SelectList(Enum.GetNames(typeof(Data.Common.ValueType))));
            valueEditor(x => Item.Name);
            valueEditor(x => Item.Code);
            valueEditor(x => Item.NumericCode);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => ProductUrls.FeatureTypes;
        protected internal override string subtitle => subTitle(removeId(FixedFilter), ProductTypeName(FixedValue));
        private string subTitle(string item, string name) => HasFixedFilter ? $"For {item} ({name})" : string.Empty;
        private static string removeId(string idField) => idField?.Replace("Id", string.Empty) ?? string.Empty;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) {
            Item = new FeatureTypeView();
            if (HasFixedFilter) Item.ProductTypeId = fixedValue;
        }
        public string ProductTypeName(string id) => itemName(ProductTypes, id);
        public async Task<IActionResult> OnGetShowFeaturesAsync(string id, string sortOrder, string searchString,
            int pageIndex, string fixedFilter, string fixedValue) {
            var name = GetMember.Name<FeatureView>(x => x.FeatureTypeId);
            var page = ProductUrls.Features;
            var url = new Uri($"{page}/Index?handler=Index&fixedFilter={name}&fixedValue={id}" +
                              $"&sortOrder=", UriKind.Relative);
            await Task.CompletedTask;
            return Redirect(url.ToString());
        }
        public async Task<IActionResult> OnGetShowPossibleValuesAsync(string id, string sortOrder, string searchString,
            int pageIndex, string fixedFilter, string fixedValue) {
            var name = GetMember.Name<PossibleFeatureValueView>(x => x.FeatureTypeId);
            var page = ProductUrls.PossibleFeatureValues;
            var url = new Uri($"{page}/Index?handler=Index&fixedFilter={name}&fixedValue={id}" +
                              $"&sortOrder=", UriKind.Relative);
            await Task.CompletedTask;
            return Redirect(url.ToString());
        }
    }
}


