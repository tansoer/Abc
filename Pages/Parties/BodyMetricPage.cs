using System;
using System.Collections.Generic;
using Abc.Aids.Enums;
using Abc.Core.Loinc.Response;
using Abc.Data.Parties;
using Abc.Data.Quantities;
using Abc.Domain.Parties;
using Abc.Domain.Parties.Attributes;
using Abc.Domain.Quantities;
using Abc.Facade.Parties;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abc.Pages.Parties {
    public sealed class BodyMetricPage :ViewsFactoryPage<BodyMetricPage, IBodyMetricsRepo, IBodyMetric,
        BodyMetricView, BodyMetricData, BodyMetricViewFactory, MetricType> {
        protected override string title => PartyTitles.BodyMetrics;
        protected internal override string subtitle => $"{PartyName(FixedValue)}";
        public BodyMetricPage(IBodyMetricsRepo r) :base(r) {
        }
        private IEnumerable<SelectListItem> types;
        private IEnumerable<SelectListItem> units;
        public IEnumerable<SelectListItem> Types => types ??= selectListByName<IBodyMetricTypesRepo, BodyMetricType, BodyMetricTypeData>();
        public IEnumerable<SelectListItem> Units => units ??= selectListByName<IUnitsRepo, Unit, UnitData>();
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.MetricType),
            field(x => Item.Id),
            field(x => Item.PartyId),
            field(x => Item.SignatureId)
        };
        protected override void addFields() {
            addField(x => Item.MetricType);
            addField(x => Item.TypeId, () => Types, () => TypeName(Item.TypeId));
            addField(x => Item.Name);
            addField(x => Item.Code);
            addField(x => Item.Details);
            addField(x => Item.SignatureId);
            addField(x => Item.Value,() => Item.ToString());
            addField(x => Item.ValidFrom);
            addField(x => Item.ValidTo);
        }
        protected override void fieldsForEditors() {
            removeField(x => Item.MetricType);
            removeField(x => Item.SignatureId);
            removeField(x => Item.Value);
            if (Item?.MetricType.IsString() ?? false) addFieldBefore(field(x => Item.StringValue), x => Item.ValidFrom);
            else if (Item?.MetricType.IsQuantity() ?? false) {
                addFieldBefore(field(x => Item.QuantityValue), x => Item.ValidFrom);
                addFieldAfter(field(x => Item.UnitId, () => Units), x => Item.QuantityValue);
            }
        }
        protected internal override string pageUrl => PartyUrls.BodyMetrics;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) 
            => createItem(fixedValue, switchOfCreate ?? 0);
        private void createItem(string partyId, int switchOfCreate) {
            Item = new BodyMetricView {
                Id = Guid.NewGuid().ToString(),
                PartyId = partyId,
                SignatureId = getLoggedUser(),
                MetricType = metricType(switchOfCreate)
            };
        }
        private string getLoggedUser() => User?.Identity?.Name;
        private string PartyName(string id) => getRepo<IPartiesRepo>().Get(id).GetName();
        private string TypeName(string id) => itemName(Types, id);
        internal static MetricType metricType(int i) =>
            Enum.IsDefined(typeof(MetricType), i) ? (MetricType)i : MetricType.Unspecified;
    }
}