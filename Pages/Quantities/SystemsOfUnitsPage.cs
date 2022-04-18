using System.Collections.Generic;
using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Facade.Quantities;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;

namespace Abc.Pages.Quantities {

    public sealed class SystemsOfUnitsPage
        : ViewFactoryPage<SystemsOfUnitsPage, ISystemsOfUnitsRepo, SystemOfUnits, 
            SystemOfUnitsView, SystemOfUnitsData, SystemOfUnitsViewFactory> {
        public SystemsOfUnitsPage(ISystemsOfUnitsRepo r) : base(r) { }
        protected override string title => QuantityTitles.SystemsOfUnits;
        protected internal override string pageUrl => QuantityUrls.SystemsOfUnits;
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id),
            field(x => Item.Timestamp)
        };
        protected override void addFields() {
            addField(x => Item.Code);
            addField(x => Item.Name);
            addField(x => Item.Details);
            addField(x => Item.ValidFrom);
            addField(x => Item.ValidTo);
        }
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) {
            base.doBeforeOnGetCreate(fixedFilter, fixedValue, switchOfCreate);
            Item = new SystemOfUnitsView();
        }
        protected override void doBeforeOnPostCreate() => Item.Id = Item.Code;
    }
}

