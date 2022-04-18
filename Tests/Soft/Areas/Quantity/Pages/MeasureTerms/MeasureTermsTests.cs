using Abc.Data.Quantities;
using Abc.Facade.Quantities;
using Abc.Pages.Quantities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Quantity.Pages.MeasureTerms {
    public abstract class MeasureTermsTests : BaseQuantityTests<MeasureTermView, CommonTermData> {
        protected override string baseUrl() => QuantityUrls.MeasureTerms;
        protected override MeasureTermView toView(CommonTermData d)
            => new MeasureTermViewFactory().Create(new Abc.Domain.Quantities.MeasureTerm(d));
        protected List<SelectListItem> measures => db.Measures.Select(m => new SelectListItem(m.Name, m.Id)).ToList();
        protected override string baseClassName() => "CommonTerm";
        protected override void isCorrectPageName() {}
        protected override IEnumerable<Expression<Func<MeasureTermView, object>>> indexPageColumns =>
            new Expression<Func<MeasureTermView, object>>[] {
                x => x.MasterId,
                x => x.TermId,
                x => x.Power,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, m => m.MasterId, "Unspecified");
            canView(item, m => m.TermId, "Unspecified");
            canView(item, m => m.Power);
            canView(item, m => m.ValidFrom);
            canView(item, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            canSelect(item, m => m.MasterId, measures, true);
            canSelect(item, m => m.TermId, measures, true);
            canEdit(item, m => m.Power, true, true);
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canSelect(null, m => m.MasterId, measures, true);
            canSelect(null, m => m.TermId, measures, true);
            canEdit(null, m => m.Power, true, true, 0);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
        protected override void dataHiddenInPage() { 
            hasHidden(null, x => x.Id, true);
            hasHidden(null, x => x.Timestamp, true);
        }
        protected override void addRelatedItems(CommonTermData d) {
            if (d is null) return;
            addMeasure(d.MasterId);
            addMeasure(d.TermId);
        }
    }
    [TestClass] public class CreatePageTests : MeasureTermsTests {}
    [TestClass] public class DeletePageTests : MeasureTermsTests {}
    [TestClass] public class DetailsPageTests :MeasureTermsTests {}
    [TestClass] public class EditPageTests : MeasureTermsTests {}
    [TestClass] public class IndexPageTests : MeasureTermsTests {}
}
