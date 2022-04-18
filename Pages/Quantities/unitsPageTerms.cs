using System;
using System.Collections.Generic;
using Abc.Aids.Reflection;
using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Facade.Quantities;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abc.Pages.Quantities {
    internal class unitsPageTerms : 
        masterDetailsManager<UnitView, CommonTermData, UnitTerm, 
            UnitTermView, IUnitTermsRepo, UnitTermViewFactory> {
        public unitsPageTerms(Func<UnitView> f) 
            : base(f, GetMember.Name<CommonTermData>(x => x.MasterId)) {
            units = BasePageAids.selectListByName<IUnitsRepo, Unit, UnitData>();
        }
        protected internal IEnumerable<SelectListItem> units { get; }
        public List<ComponentArgs> Editors => new() {
            editorFor(nameof(UnitTermView.TermId), units),
            editorFor(nameof(UnitTermView.Power)),
            editorFor(nameof(UnitTermView.ValidFrom)),
            editorFor(nameof(UnitTermView.ValidTo)),
            editorFor(nameof(UnitTermView.Id), true),
            editorFor(nameof(UnitTermView.MasterId), true, itemId)
        };
        protected internal override void updateMasterId(UnitTermView x, string id) => x.MasterId = id;
        protected override bool isMasterDetail(UnitTerm detail, string masterId) => detail.MasterUnitId == masterId;
    }
}
