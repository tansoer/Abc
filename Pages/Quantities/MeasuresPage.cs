using System;
using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Reflection;
using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Facade.Quantities;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Abc.Pages.Common.Consts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abc.Pages.Quantities {

    public sealed class MeasuresPage
        :ViewsFactoryPage<MeasuresPage, IMeasuresRepo, Measure, MeasureView, MeasureData, MeasureViewFactory, MeasureType> {
        protected override string title => QuantityTitles.Measures;
        private IMeasureTermsRepo terms => getRepo<IMeasureTermsRepo>();
        private IUnitsRepo units => getRepo<IUnitsRepo>();
        public MeasuresPage(IMeasuresRepo r) : base(r) {
            MeasureTerms = new List<MeasureTermView>();
            Units = new List<UnitView>();
        }

        private IEnumerable<SelectListItem> measures;
        public IEnumerable<SelectListItem> Measures => measures ??= selectListByName<IMeasuresRepo,Measure, MeasureData>();
        protected override List<MeasureType> removeFromCreateDropdown() => new() { MeasureType.Error, MeasureType.Unspecified };
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id),
            field(x => Item.Timestamp),
            field(x => Item.MeasureType)
        };
        protected override void addFields() {
            addField(x => Item.Name);
            addField(x => Item.Details);
            addField(x => Item.Formula);
            addField(x => Item.ValidFrom);
            addField(x => Item.ValidTo);
        }
        protected override void fieldsForViewers() {
            addFieldAfter(field(x => Item.Code), x => Item.Name);
        }
        protected override void fieldsForEditors() {
            fieldsForViewers();
            removeField(x => Item.Formula);
        }
        protected override void addButtons() {
            createLocalButton(Captions.Select, Actions.Index);
            base.addButtons();
            createForeignButton("Units", Actions.Index, unitsUrl(), GetMember.Name<UnitData>(x => x.MeasureId));
        }
        public List<ComponentArgs> TermInputs => new() {
            expandingEditorField(nameof(MeasureTermView.TermId), Measures),
            expandingEditorField(nameof(MeasureTermView.Power)),
            expandingEditorField(nameof(MeasureTermView.ValidFrom)),
            expandingEditorField(nameof(MeasureTermView.ValidTo)),
            expandingEditorField(nameof(MeasureTermView.Id), true),
            expandingEditorField(nameof(MeasureTermView.MasterId), true, Item.Id)
        };

        protected internal override string pageUrl => QuantityUrls.Measures;
        private static Uri unitsUrl() => new(QuantityUrls.Units, UriKind.Relative);

        [BindProperty] public List<MeasureTermView> MeasureTerms { get; set; }
        public IList<UnitView> Units { get; private set; }

        public override void LoadDetails() {
            MeasureTerms = loadDetails(terms, nameof(CommonTermData.MasterId), ItemId, new MeasureTermViewFactory().Create);
            Units = loadDetails(units, nameof(UnitData.MeasureId), ItemId, new UnitViewFactory().Create);
        }

        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) {
            base.doBeforeOnGetCreate(fixedFilter, fixedValue, switchOfCreate);
            Item = new MeasureView {
                MeasureType = measureType(switchOfCreate ?? 0)
            };
            if (Item.MeasureType == MeasureType.Derived) MeasureTerms = new();
        }
        protected override void doBeforeOnPostCreate() => Item.Id = Item.Code;
        protected override void doAfterOnPostCreate() => createTerms();
        protected override void doBeforeOnGetEdit(string id) => MeasureTerms = loadDetails(terms, nameof(CommonTermData.MasterId), id, new MeasureTermViewFactory().Create) as List<MeasureTermView>;
        protected override void doAfterOnPostEdit() => handleTermsEdit();
        protected override void doAfterOnPostDelete(string id) => deleteRelatedData(id);

        internal static MeasureType measureType(int i) => Enum.IsDefined(typeof(MeasureType), i) ? (MeasureType)i : MeasureType.Unspecified;
        private static MeasureTerm viewToTerm(MeasureTermView v) => new MeasureTermViewFactory().Create(v);      
        private void createTerms() {
            MeasureTerms.ForEach(x => x.MasterId = Item.Id);
            foreach(var term in MeasureTerms) terms.Add(viewToTerm(term));
        }
        private void handleTermsEdit() {
            var beforeEdit = loadDetails(terms, nameof(CommonTermData.MasterId), ItemId, new MeasureTermViewFactory().Create) as List<MeasureTermView>;
            foreach(var term in MeasureTerms) {
                if (!beforeEdit.Any(x => x.Id == term.Id)) terms.Add(viewToTerm(term));
                else updateEditedTerms(ref beforeEdit, term);   
            }
            removeDeletedTerms(ref beforeEdit);
        }
        private void updateEditedTerms(ref List<MeasureTermView> termsBeforeEdit, MeasureTermView updatedTerm) {
            terms.Update(viewToTerm(updatedTerm));
            termsBeforeEdit.RemoveAll(x => x.Id == updatedTerm.Id);
        }
        private void removeDeletedTerms(ref List<MeasureTermView> removedTerms) => removedTerms.ForEach(x => terms.Delete(x.Id));
        private void deleteRelatedData(string id) => terms.Get().Where(x => x.Data.MasterId == id).ToList().ForEach(x => terms.Delete(x.Id));
    }
}
