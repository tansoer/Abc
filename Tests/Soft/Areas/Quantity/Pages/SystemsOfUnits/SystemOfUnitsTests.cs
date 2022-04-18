using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Abc.Data.Quantities;
using Abc.Facade.Quantities;
using Abc.Pages.Quantities;
using AngleSharp.Text;
using Abc.Domain.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Soft.Areas.Quantity.Pages.SystemsOfUnits {
    public abstract class SystemOfUnitsTests :BaseQuantityTests<SystemOfUnitsView, SystemOfUnitsData> {
        protected override string baseUrl() => QuantityUrls.SystemsOfUnits;
        protected override SystemOfUnitsView toView(SystemOfUnitsData d)
            => new SystemOfUnitsViewFactory().Create(new SystemOfUnits(d));
        protected override SystemOfUnitsData randomItem() {
            var d = base.randomItem();
            d.Id = d.Code;
            return d;
        }
        protected override string performTitleCorrection(string title) {
            var v = title.ReplaceFirst("Systems", "System");
            return base.performTitleCorrection(v);
        }
        protected override IEnumerable<Expression<Func<SystemOfUnitsView, object>>> indexPageColumns =>
            new Expression<Func<SystemOfUnitsView, object>>[] {
                x => x.Code,
                x => x.Name,
                x => x.Details,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, m => m.Code);
            canView(item, m => m.Name);
            canView(item, m => m.Details);
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
        }
    }
    [TestClass] public class IndexPageTests :SystemOfUnitsTests { }
    [TestClass] public class EditPageTests :SystemOfUnitsTests { }
    [TestClass] public class DetailsPageTests :SystemOfUnitsTests { }
    [TestClass] public class DeletePageTests :SystemOfUnitsTests { }
    [TestClass] public class CreatePageTests :SystemOfUnitsTests { }
}
