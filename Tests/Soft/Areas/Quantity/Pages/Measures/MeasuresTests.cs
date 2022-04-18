using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Facade.Quantities;
using Abc.Pages.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Soft.Areas.Quantity.Pages.Measures {
    public abstract class MeasuresTests : BaseQuantityTests<MeasureView, MeasureData> {
        protected override MeasureData randomItem() {
            var d = base.randomItem();
            d.MeasureType = random<bool>() ? MeasureType.Base : MeasureType.Derived;
            d.Id = d.Code;
            return d;
        }
        protected override string baseUrl() => QuantityUrls.Measures;
        protected override MeasureView toView(MeasureData d)
            => new MeasureViewFactory().Create(MeasureFactory.Create(d));
        protected override IEnumerable<Expression<Func<MeasureView, object>>> indexPageColumns =>
            new Expression<Func<MeasureView, object>>[] {
                x => x.Name,
                x => x.Details,
                x => x.Formula,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            var formula = MeasureFactory.Create(item).Formula(true);
            canView(item, m => m.Name);
            canView(item, m => m.Details);
            canView(item, m => m.Formula, formula);
            canView(item, m => m.ValidFrom);
            canView(item, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, m => m.Code, true);
            canEdit(item, m => m.Name, true);
            canEdit(item, m => m.Details);
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, m => m.Code, true);
            canEdit(null, m => m.Name, true);
            canEdit(null, m => m.Details);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(item, x => x.Id, true);
            hasHidden(item, x => x.Timestamp, true);
            hasHidden(item, x => x.MeasureType, true);
        }
    }
    [TestClass] public class CreatePageTests :MeasuresTests { }
    [TestClass] public class DeletePageTests :MeasuresTests { }
    [TestClass] public class DetailsPageTests :MeasuresTests { }
    [TestClass] public class EditPageTests :MeasuresTests { }
    [TestClass] public class IndexPageTests :MeasuresTests { }
    [TestClass] public class SelectPageTests :MeasuresTests { }
}
