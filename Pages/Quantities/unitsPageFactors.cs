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
    internal class unitsPageFactors :
        masterDetailsManager<UnitView, UnitFactorData, UnitFactor,
            UnitFactorView, IUnitFactorsRepo, UnitFactorViewFactory>  {
        public unitsPageFactors(Func<UnitView> f)
            : base(f, GetMember.Name<UnitFactorData>(x => x.UnitId)) {
            systemsOfUnits = BasePageAids.selectListByName<ISystemsOfUnitsRepo, SystemOfUnits, SystemOfUnitsData>();
        }
        protected internal readonly IEnumerable<SelectListItem> systemsOfUnits;
        public List<ComponentArgs> Editors => new()
        {
            editorFor(nameof(UnitFactorView.SystemOfUnitsId), systemsOfUnits),
            editorFor(nameof(UnitFactorView.Factor)),
            editorFor(nameof(UnitFactorView.ValidFrom)),
            editorFor(nameof(UnitFactorView.ValidTo)),
            editorFor(nameof(UnitFactorView.Id), true),
            editorFor(nameof(UnitFactorView.UnitId), true, itemId)
        };
        protected internal override void updateMasterId(UnitFactorView x, string id) => x.UnitId = id;

        protected override bool isMasterDetail(UnitFactor detail, string masterId) => detail.UnitId == masterId;
    }
}
