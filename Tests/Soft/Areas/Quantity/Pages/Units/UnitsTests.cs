using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Facade.Quantities;
using Abc.Pages.Quantities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Soft.Areas.Quantity.Pages.Units {

    public abstract class UnitsTests : BaseQuantityTests<UnitView, UnitData> {
        protected List<SelectListItem> measures => createSelectList(db.Measures);
        protected override string baseUrl() => QuantityUrls.Units;
        protected override UnitView toView(UnitData d)
            => new UnitViewFactory().Create(UnitFactory.Create(d));
        protected override void changeValuesExceptId(UnitData d) {
            var id = d.Id;
            d = randomItem();
            d.Id = id;
        }
        protected override UnitData randomItem() {
            var d = base.randomItem();
            d.UnitType = random<bool>() ? UnitType.Factored : UnitType.Derived;
            d.Id = d.Code;
            return d;
        }
        protected override void addRelatedItems(UnitData d) {
            if (d is null) return;
            addMeasure(d.MeasureId);
        }
        protected override IEnumerable<Expression<Func<UnitView, object>>> indexPageColumns =>
            new Expression<Func<UnitView, object>>[] {
                x => x.Name,
                x => x.Details,
                x => x.Formula,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            var formula = UnitFactory.Create(item).Formula(true);
            canView(item, m => m.Name);
            canView(item, m => m.Formula, formula);
            canView(item, m => m.MeasureId, "Unspecified");
            canView(item, m => m.Details);
            canView(item, m => m.ValidFrom);
            canView(item, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, m => m.Code, true);
            canSelectEnum(item, m => m.UnitType, true, UnitType.Unspecified);
            canEdit(item, m => m.Name, true);
            canSelect(item, m => m.MeasureId, measures, true);
            canEdit(item, m => m.Details);
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, m => m.Code, true);
            canSelectEnum(null, m => m.UnitType, true, UnitType.Unspecified);
            canEdit(null, m => m.Name, true);
            canSelect(null, m => m.MeasureId, measures, true);
            canEdit(null, m => m.Details);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(item, x => x.Id, true);
            hasHidden(item, x => x.Timestamp, true);
        }
    }
    [TestClass] public class CreatePageTests :UnitsTests { }
    [TestClass] public class DeletePageTests :UnitsTests { }
    [TestClass] public class DetailsPageTests :UnitsTests { }
    [TestClass] public class EditPageTests :UnitsTests { }
    [TestClass] public class IndexPageTests :UnitsTests { }
    [TestClass] public class SelectPageTests :UnitsTests { }
}
