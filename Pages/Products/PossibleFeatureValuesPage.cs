using Abc.Aids.Enums;
using Abc.Aids.Methods;
using System;
using System.Collections.Generic;
using Abc.Data.Currencies;
using Abc.Data.Products;
using Abc.Data.Quantities;
using Abc.Domain.Currencies;
using Abc.Domain.Products.Features;
using Abc.Domain.Quantities;
using Abc.Facade.Products;
using Abc.Pages.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using ValueType = Abc.Data.Common.ValueType;
using Abc.Pages.Common.Aids;

namespace Abc.Pages.Products {
   //TODO Gunnar: Urgent
    public sealed class PossibleFeatureValuesPage : ViewsFactoryPage<PossibleFeatureValuesPage, IPossibleFeatureValuesRepo, PossibleFeatureValue,
        PossibleFeatureValueView, PossibleFeatureValueData, PossibleFeatureValueViewFactory, ValueType> {
        protected override string title => ProductTitles.PossibleFeatureValues;
        public PossibleFeatureValuesPage(IPossibleFeatureValuesRepo r) : base(r) {}
        private IEnumerable<SelectListItem> featureTypes;
        public IEnumerable<SelectListItem> FeatureTypes => featureTypes ??= selectListByName<IFeatureTypesRepo,FeatureType, FeatureTypeData>();
        private IEnumerable<SelectListItem> currencies;
        public IEnumerable<SelectListItem> Currencies => currencies ??= selectListByName<ICurrencyRepo, Currency, CurrencyData>();
        private IEnumerable<SelectListItem> units;
        public IEnumerable<SelectListItem> Units => units ??= selectListByName<IUnitsRepo, Unit, UnitData>();
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id),
//            field(x => Item.ValueType),
        };
        protected override void tableColumns() {
            tableColumn(x => Item.FeatureTypeId, () => FeatureTypeName(Item.FeatureTypeId));
            tableColumn(x => Item.Relation);
            //tableColumn(x => Item.ValueType);
            //tableColumn(x => Item.FeatureValue);
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Id);
            valueViewer(x => Item.FeatureTypeId, () => FeatureTypeName(Item.FeatureTypeId));
            valueViewer(x => Item.Relation);
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
            valueEditor(x => Item.Name);
            valueEditor(x => Item.Relation, () => new SelectList(Enum.GetNames(typeof(Relation))));

            //switch (Item.ValueType)
            //{
            //    case ValueType.Boolean:
            //        valueEditor(x => Item.BooleanValue);
            //        break;
            //    case ValueType.Decimal:
            //        valueEditor(x => Item.DecimalValue);
            //        break;
            //    case ValueType.Double:
            //        valueEditor(x => Item.DoubleValue);
            //        break;
            //    case ValueType.Integer:
            //        valueEditor(x => Item.IntegerValue);
            //        break;
            //    case ValueType.DateTime:
            //        valueEditor(x => Item.DateTimeValue);
            //        break;
            //    case ValueType.Money:
            //        valueEditor(x => Item.DecimalValue);
            //        valueEditor(x => Item.CurrencyId, () => Currencies);
            //        break;
            //    case ValueType.Quantity:
            //        valueEditor(x => Item.DoubleValue);
            //        valueEditor(x => Item.UnitId, () => Units);
            //        break;
            //    case ValueType.String or ValueType.Unspecified or ValueType.Error:
            //        valueEditor(x => Item.StringValue);
            //        break;
            //}

            valueEditor(x => Item.Code);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => ProductUrls.PossibleFeatureValues;
        protected internal override string subtitle => subTitle(removeId(FixedFilter), FeatureTypeName(FixedValue));
        private string subTitle(string item, string name) => HasFixedFilter ? $"For {item} ({name})" : string.Empty;
        private static string removeId(string idField) => idField?.Replace("Id", string.Empty) ?? string.Empty;
        public string FeatureTypeName(string id) => itemName(FeatureTypes, id);
        public string UnitName(string id) => itemName(Units, id);
        public string CurrencyName(string id) => itemName(Currencies, id);
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) {
            Item = new PossibleFeatureValueView {
                //ValueType = toValueType(switchOfCreate)
            };
            if (HasFixedFilter) Item.FeatureTypeId = fixedValue;
        }
        private static ValueType toValueType(int? valueType) => Safe.Run(() => (ValueType) (valueType ?? 1000), ValueType.Unspecified);
    }
}



