using System;
using System.Collections.Generic;
using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Facade.Quantities;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Abc.Pages.Common.Consts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abc.Pages.Quantities {
    public sealed class UnitsPage : ViewsFactoryPage<UnitsPage, IUnitsRepo, Unit, UnitView, UnitData, UnitViewFactory, UnitType>
    {
        protected override string title => QuantityTitles.Units;
        protected internal override string subtitle => MeasureName(FixedValue);
        protected internal override IPageUrl masterPage() => new MeasuresPage(null);
        public IEnumerable<SelectListItem> UnitTypes = new SelectList(Enum.GetNames(typeof(UnitType)));
        private unitsPageTerms terms;
        private unitsPageFactors factors;
        private unitsPageRules rules;
        public UnitsPage(IUnitsRepo r) : base(r) {
            terms = new unitsPageTerms(() => Item);
            factors = new unitsPageFactors(() => Item);
            rules = new unitsPageRules(() => Item);
        }
        public override void LoadDetails() {
            terms.loadDetails();
            factors.loadDetails();
            rules.loadDetails();
        }
        protected override List<UnitType> removeFromCreateDropdown() => new() { UnitType.Error, UnitType.Unspecified };
        private IEnumerable<SelectListItem> measures;
        public IEnumerable<SelectListItem> Measures => measures ??= selectListByName<IMeasuresRepo, Measure, MeasureData>();
        [BindProperty] public List<UnitTermView> Terms { get => terms.details; set => terms.details = value; }
        [BindProperty] public List<UnitFactorView> Factors { get => factors.details; set => factors.details = value; }
        [BindProperty] public List<UnitRulesView> UnitRules { get => rules.details; set => rules.details = value; }
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id),
            field(x => Item.Timestamp)
        };
        protected override void addFields() {
            addField(x => Item.Name);
            addField(x => Item.Details);
            addField(x => Item.Formula);
            addField(x => Item.ValidFrom);
            addField(x => Item.ValidTo);
        }
        protected override void fieldsForViewers() {
            addFieldBefore(field(x => Item.UnitType), x => Item.Name);
            addFieldBefore(field(x => Item.Code), x => Item.Name);
            addFieldBefore(field(x => Item.MeasureId, () => Measures, () => MeasureName(Item.MeasureId)), x => Item.Code);
        }
        protected override void fieldsForEditors() {
            fieldsForViewers();
            removeField(x => Item.Formula);
        }
        protected internal override string pageUrl => QuantityUrls.Units;
        public string MeasureName(string id) => itemName(Measures, id);
        protected override void addButtons() {
            createLocalButton(Captions.Select, Actions.Index);
            base.addButtons();
        }
        internal static UnitType unitType(int i) => Enum.IsDefined(typeof(UnitType), i) ? (UnitType)i : UnitType.Unspecified;
        public List<ComponentArgs> RulesInputs => rules.Editors;
        public List<ComponentArgs> TermsInputs => terms.Editors;
        public List<ComponentArgs> FactorsInputs => factors.Editors;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) {
            base.doBeforeOnGetCreate(fixedFilter, fixedValue, switchOfCreate);
            Item = new UnitView {
                UnitType = unitType(switchOfCreate ?? 0)
            };
        }
        protected override void doAfterOnPostCreate() => createDetails();
        protected override void doBeforeOnPostCreate() => Item.Id = Item.Code;
        protected override void doBeforeOnGetEdit(string id) => LoadDetails();
        protected override void doAfterOnPostEdit() => editDetails();
        protected override void doAfterOnPostDelete(string id) => deleteDetails(id);
        private void deleteDetails(string id) {
            terms.deleteDetails(id);
            factors.deleteDetails(id);
            rules.deleteDetails(id);
        }
        private void createDetails() {
            terms.createDetails();
            factors.createDetails();
            rules.createDetails();
        }
        private void editDetails() {
            terms.editDetails();
            factors.editDetails();
            rules.editDetails();
        }
    }
}
