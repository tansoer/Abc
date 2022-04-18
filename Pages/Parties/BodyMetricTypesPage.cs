using System.Collections.Generic;
using Abc.Data.Parties;
using Abc.Data.Rules;
using Abc.Domain.Parties.Attributes;
using Abc.Domain.Rules;
using Abc.Facade.Parties;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace Abc.Pages.Parties {
    public sealed class BodyMetricTypesPage : ViewFactoryPage<BodyMetricTypesPage, IBodyMetricTypesRepo, BodyMetricType, BodyMetricTypeView,
        BodyMetricTypeData, BodyMetricTypeViewFactory> {
        protected override string title => PartyTitles.BodyMetricTypes;
        public BodyMetricTypesPage(IBodyMetricTypesRepo r): base(r) { }
        private IEnumerable<SelectListItem> baseTypes;
        private IEnumerable<SelectListItem> ruleSets;
        public IEnumerable<SelectListItem> BaseTypes => baseTypes ??= selectListByName<IBodyMetricTypesRepo, BodyMetricType, BodyMetricTypeData>();
        public IEnumerable<SelectListItem> RuleSets => ruleSets ??= selectListByName<IRuleSetsRepo, IRuleSet, RuleSetData>();
        protected override List<ExpressionHelper> hiddenInputs() => new() { field(x => Item.Id) };
        protected override void addFields() {
            addField(x => Item.BaseTypeId, () => BaseTypes, () => BaseTypeName(Item.BaseTypeId));
            addField(x => Item.RuleSetId, () => RuleSets, () => BaseTypeName(Item.RuleSetId));
            addField(x => Item.Name);
            addField(x => Item.Code);
            addField(x => Item.Details);
            addField(x => Item.ValidFrom);
            addField(x => Item.ValidTo);
        }

        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) =>
            Item = new BodyMetricTypeView {Id = Guid.NewGuid().ToString()};
            

        protected internal override string pageUrl => PartyUrls.BodyMetricTypes;
        public string BaseTypeName(string id) => itemName(BaseTypes, id);
    }
}

