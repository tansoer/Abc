using System;
using System.Collections.Generic;
using Abc.Aids.Reflection;
using Abc.Data.Quantities;
using Abc.Data.Rules;
using Abc.Domain.Quantities;
using Abc.Domain.Rules;
using Abc.Facade.Quantities;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abc.Pages.Quantities {
    internal class unitsPageRules: masterDetailsManager<UnitView, UnitRulesData, UnitRules,
            UnitRulesView, IUnitRulesRepo, UnitRulesViewFactory> {
        public unitsPageRules(Func<UnitView> f): base(f, GetMember.Name<UnitRulesData>(x => x.UnitId)) {
            rules = BasePageAids.selectListByName<IRulesRepo, BaseRule, RuleData>();
            systemsOfUnits = BasePageAids.selectListByName<ISystemsOfUnitsRepo, SystemOfUnits, SystemOfUnitsData>();
        }
        protected internal readonly IEnumerable<SelectListItem> systemsOfUnits;
        protected internal readonly IEnumerable<SelectListItem> rules;
        public List<ComponentArgs> Editors => new() {
            editorFor(nameof(UnitRulesView.SystemOfUnitsId), systemsOfUnits),
            editorFor(nameof(UnitRulesView.FromBaseUnitRuleId), rules),
            editorFor(nameof(UnitRulesView.ToBaseUnitRuleId), rules),
            editorFor(nameof(UnitRulesView.ValidFrom)),
            editorFor(nameof(UnitRulesView.ValidTo)),
            editorFor(nameof(UnitRulesView.Id), true),
            editorFor(nameof(UnitRulesView.UnitId), true, itemId)
        };
        protected internal override void updateMasterId(UnitRulesView x, string id) => x.UnitId = id;
        protected override bool isMasterDetail(UnitRules detail, string masterId) => detail.UnitId == masterId;
    }
}
