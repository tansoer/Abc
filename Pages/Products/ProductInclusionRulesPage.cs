using Abc.Aids.Methods;
using Abc.Data.Products;
using Abc.Data.Quantities;
using Abc.Domain.Products;
using Abc.Domain.Products.Packets;
using Abc.Domain.Quantities;
using Abc.Facade.Products;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Abc.Pages.Products {
    public sealed class ProductInclusionRulesPage :ViewsFactoryPage<ProductInclusionRulesPage, IProductInclusionRulesRepo,
        IProductInclusionRule, ProductInclusionRuleView, ProductInclusionRuleData, ProductInclusionRuleViewFactory, ProductInclusionKind> {
        protected override string title => ProductTitles.ProductInclusionRules;
        protected internal override string subtitle => HasFixedFilter ? $"{PackageTypeName(FixedValue)}" : "";
        public ProductInclusionRulesPage(IProductInclusionRulesRepo r) :base (r) {}
        private IEnumerable<SelectListItem> units;
        public IEnumerable<SelectListItem> Units => units ??= selectListByName<IUnitsRepo, Unit, UnitData>();
        private IEnumerable<SelectListItem> productSets;
        public IEnumerable<SelectListItem> ProductSets => productSets ??= selectListByName<IProductSetsRepo, ProductSet, ProductSetData>();
        private IEnumerable<SelectListItem> productInclusionRules;
        public IEnumerable<SelectListItem> ProductInclusionRules => productInclusionRules ??= selectListByName<IProductInclusionRulesRepo, IProductInclusionRule, ProductInclusionRuleData>();
        private IEnumerable<SelectListItem> packageTypes;
        public IEnumerable<SelectListItem> PackageTypes => packageTypes ??= selectListByName<IProductTypesRepo, IProductType, ProductTypeData>(x => x.ProductKind == ProductKind.Package);
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id),
            field(x => Item.InclusionKind)
        };
        protected override void addFields() {
            addField(x => Item.Name);
            addField(x => Item.InclusionKind);
            addField(x => Item.MinimumAmount);
            addField(x => Item.MaximumAmount);
            addField(x => Item.UnitId, () => Units, () => UnitName(Item.UnitId));
            addField(x => Item.PackageTypeId, () => PackageTypes, () => PackageTypeName(Item.PackageTypeId));
            addField(x => Item.ProductSetId, () => ProductSets, () => ProductSetName(Item.ProductSetId));
            addField(x => Item.ValidFrom);
            addField(x => Item.ValidTo);
        }
        protected override void fieldsForViewers() {
            addFieldBefore(field(x => Item.Id), x => Item.Name);
            addFieldAfter(field(x => Item.Code), x => Item.Name);
            addFieldBefore(field(x => Item.Details), x => Item.InclusionKind);
            if (ShouldShowMasterInclusionRuleId(Item.InclusionKind))
                addFieldBefore(field(x => Item.ProductInclusionId, null, () => ProductInclusionRules, () => MasterInclusionRuleName(Item.ProductInclusionId)),
                    x => Item.ValidFrom);
        }
        protected override void fieldsForEditors() {
            fieldsForViewers();
            removeField(x => Item.Id);
            removeField(x => Item.InclusionKind);
        }
        protected internal override string pageUrl => ProductUrls.ProductInclusionRules;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) {
            Item = new();
            Item.InclusionKind = Safe.Run(() => (ProductInclusionKind)(switchOfCreate ?? 1000), ProductInclusionKind.Unspecified);
            if (HasFixedFilter) Item.PackageTypeId = fixedValue;
        }
        public string UnitName(string id) => itemName(Units, id);
        public string ProductSetName(string id) => itemName(ProductSets, id);
        public string MasterInclusionRuleName(string id) => itemName(ProductInclusionRules, id);
        public string PackageTypeName(string id) => itemName(PackageTypes, id);
        public bool ShouldShowMasterInclusionRuleId(ProductInclusionKind kind) => kind switch {
                ProductInclusionKind.Conditional or ProductInclusionKind.Detail => true,
                _ => false };
    }
}
