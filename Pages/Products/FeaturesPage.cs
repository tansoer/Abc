using Abc.Aids.Methods;
using System.Collections.Generic;
using Abc.Data.Currencies;
using Abc.Data.Products;
using Abc.Data.Quantities;
using Abc.Domain.Currencies;
using Abc.Domain.Products;
using Abc.Domain.Products.Features;
using Abc.Domain.Quantities;
using Abc.Facade.Products;
using Abc.Pages.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using ValueType = Abc.Data.Common.ValueType;
using Abc.Aids.Reflection;
using Abc.Pages.Common.Aids;

namespace Abc.Pages.Products {
    //TODO Gunnar : Urgent
    public sealed class FeaturesPage :ViewsFactoryPage<FeaturesPage, IFeaturesRepo, Feature, FeatureView, 
        FeatureData, FeatureViewFactory, ValueType> {
        protected override string title => ProductTitles.Features;
        public FeaturesPage(IFeaturesRepo r) : base(r) {}
        private IEnumerable<SelectListItem> currencies;
        public IEnumerable<SelectListItem> Currencies => currencies ??= selectListByName<ICurrencyRepo, Currency, CurrencyData>();
        private IEnumerable<SelectListItem> units;
        public IEnumerable<SelectListItem> Units => units ??= selectListByName<IUnitsRepo, Unit, UnitData>();
        private IEnumerable<SelectListItem> products;
        public IEnumerable<SelectListItem> Products => products ??= selectListByName<IProductsRepo, IProduct, ProductData>();
        private IEnumerable<SelectListItem> featureTypes;
        public IEnumerable<SelectListItem> FeatureTypes => featureTypes ??= selectListByName<IFeatureTypesRepo, FeatureType, FeatureTypeData>();
        public ValueType ValueType { get; private set; }
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id),
            //field(x => Item.ValueType),
        };
        protected override void tableColumns() {
            tableColumn(x => Item.ProductId, () => ProductName(Item.ProductId));
            tableColumn(x => Item.FeatureTypeId, () => FeatureTypeName(Item.FeatureTypeId));
            //tableColumn(x => Item.ValueType);
            //tableColumn(x => Item.FeatureValue);
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Id);
            valueViewer(x => Item.FeatureTypeId, () => FeatureTypeName(Item.FeatureTypeId));
            valueViewer(x => Item.ProductId, () => ProductName(Item.ProductId));
            //valueViewer(x => Item.ValueType);
            //valueViewer(x => Item.FeatureValue);
            valueViewer(x => Item.Name);
            valueViewer(x => Item.Code);
            valueViewer(x => Item.Details);
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.FeatureTypeId, () => FeatureTypes);
            valueEditor(x => Item.ProductId, () => Products);
            valueEditor(x => Item.Name);

            //switch (Item.ValueType)
            //{
            //    case ValueType.Boolean:
            //        valueEditor(x => Item.BooleanValue);
            //        break;
            //    case ValueType.DateTime:
            //        valueEditor(x => Item.DateTimeValue);
            //        break;
            //    case ValueType.Decimal:
            //        valueEditor(x => Item.DecimalValue);
            //        break;
            //    case ValueType.Money:
            //        valueEditor(x => Item.DecimalValue);
            //        valueEditor(x => Item.CurrencyId, () => Currencies);
            //        break;
            //    case ValueType.Double:
            //        valueEditor(x => Item.DoubleValue);
            //        break;
            //    case ValueType.Quantity:
            //        valueEditor(x => Item.DoubleValue);
            //        valueEditor(x => Item.UnitId, () => Units);
            //        break;
            //    case ValueType.Integer:
            //        valueEditor(x => Item.IntegerValue);
            //        break;
            //    case ValueType.String or ValueType.Error or ValueType.Unspecified:
            //        valueEditor(x => Item.StringValue);
            //        break;
            //}

            valueEditor(x => Item.Code);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => ProductUrls.Features;
        protected internal override string subtitle => getSubTitle();
        private string getSubTitle() {
            if (fixedFilterIsFeatureTypeId) return toSubTitle(removeId(FixedFilter), FeatureTypeName(FixedValue));
            if (fixedFilterIsProductId) return toSubTitle(removeId(FixedFilter), ProductName(FixedValue));
            return string.Empty;
        }
        private static string toSubTitle(string item, string name) => $"For {item} ({name})";
        private static string removeId(string idField) => idField?.Replace("Id", string.Empty) ?? string.Empty;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) {
            ValueType = toValueType(switchOfCreate);
            Item = new FeatureView {
                //ValueType = ValueType
            };
            if (fixedFilterIsProductId) Item.ProductId = fixedValue;
            if (fixedFilterIsFeatureTypeId) Item.FeatureTypeId = fixedValue;
        }
        public string ProductName(string id) => itemName(Products, id);
        public string FeatureTypeName(string id) => itemName(FeatureTypes, id);
        internal static ValueType toValueType(int? elementType) => Safe.Run(() => (ValueType)(elementType ?? 1000), ValueType.Unspecified);
        private bool fixedFilterIsProductId => Equals(FixedFilter, GetMember.Name<FeatureView>(x => x.ProductId));
        private bool fixedFilterIsFeatureTypeId => Equals(FixedFilter, GetMember.Name<FeatureView>(x => x.FeatureTypeId));
    }
}


