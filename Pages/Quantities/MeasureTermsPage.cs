using System.Collections.Generic;
using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Facade.Quantities;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abc.Pages.Quantities {
    public sealed class MeasureTermsPage: ViewFactoryPage<
        MeasureTermsPage, IMeasureTermsRepo, MeasureTerm, MeasureTermView, CommonTermData, MeasureTermViewFactory> {
        protected override string title => QuantityTitles.MeasureTerms;
        protected internal override string subtitle => MeasureName(FixedValue);
        protected internal override IPageUrl masterPage() => new MeasuresPage(null);
        public MeasureTermsPage(IMeasureTermsRepo r) : base(r) { }
        protected override void addFields() {
            addField(x => Item.MasterId, () => Measures, () => MeasureName(Item.MasterId));
            addField(x => Item.TermId, () => Measures, () => MeasureName(Item.TermId));
            addField(x => Item.Power);
            addField(x => Item.ValidFrom);
            addField(x => Item.ValidTo);
        }
        protected internal override string pageUrl => QuantityUrls.MeasureTerms;
        private IEnumerable<SelectListItem> measures;
        public IEnumerable<SelectListItem> Measures => measures ??= selectListByName<IMeasuresRepo, Measure, MeasureData>();
        public string MeasureName(string id) => itemName(Measures, id);
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id),
            field(x => Item.Timestamp)
        };
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) {
            base.doBeforeOnGetCreate(fixedFilter, fixedValue, switchOfCreate);
            Item = new MeasureTermView();
        }
    }
}

