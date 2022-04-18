using Abc.Data.Orders;
using Abc.Facade.Orders;
using Abc.Pages.Orders;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Orders.Pages.TermsAndConditions {

    public abstract class TermsAndConditionsTests
        :BaseOrdersTests<TermsAndConditionsView, TermsAndConditionsData> {
        protected override string baseUrl() => OrderUrls.TermsAndConditions;
        protected List<SelectListItem> types;
        protected List<SelectListItem> orders;
        protected override TermsAndConditionsView toView(TermsAndConditionsData d)
            => new TermsAndConditionsViewFactory().Create(new Abc.Domain.Orders.Terms.TermsAndConditions(d));
        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            types = new List<SelectListItem>();
            orders = new List<SelectListItem>();
            foreach (var c in db.Orders) orders.Add(new SelectListItem(c.Name, c.Id));
        }
        protected override void changeValuesExceptId(TermsAndConditionsData d) {
            var id = d.Id;
            d = random<TermsAndConditionsData>();
            d.Id = id;
        }
        protected override string getItemId(TermsAndConditionsData d) => d.Id;
        protected override void setItemId(TermsAndConditionsData d, string id) => d.Id = id;
        protected override void isCorrectPageName() {
            var n = baseClassName().ToLower();
            var title = pageTitle().ToLower();
            isTrue(title.Contains(n));
        }
        protected override IEnumerable<Expression<Func<TermsAndConditionsView, object>>> indexPageColumns =>
            new Expression<Func<TermsAndConditionsView, object>>[] {
                x => x.Name,
                x => x.Code,
                x => x.Details,
                x => x.TypeId,
                x => x.OrderId,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, m => m.Name);
            canView(item, m => m.Code);
            canView(item, m => m.Details);
            canView(item, m => m.TypeId, "Unspecified");
            canView(item, m => m.OrderId, "Unspecified");
            canView(item, m => m.ValidFrom);
            canView(item, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, m => m.Name, true);
            canEdit(item, m => m.Code);
            canEdit(item, m => m.Details);
            canSelect(item, m => m.TypeId, types);
            canSelect(item, m => m.OrderId, orders);
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
            for (var i = HiddenInputs.Count - 1; i >= 0; i--) {
                var s = HiddenInputs[i];
                if (s.Contains(item.Id)) HiddenInputs.RemoveAt(i);
            }
        }
        protected override void dataInCreatePage() {
            canEdit(null, m => m.Name, true);
            canEdit(null, m => m.Code);
            canEdit(null, m => m.Details);
            canSelect(null, m => m.TypeId, types);
            canSelect(null, m => m.OrderId, orders);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
            for (var i = HiddenInputs.Count - 1; i >= 0; i--) {
                var s = HiddenInputs[i];
                if (s.Contains("name=\"Item.Id\"")) HiddenInputs.RemoveAt(i);
            }
        }
        protected override void validateItems(TermsAndConditionsData d1, TermsAndConditionsData d2)
            => allPropertiesAreEqual(d1, d2, nameof(item.TypeId), nameof(item.OrderId) );
    }
    [TestClass] public class CreatePageTests :TermsAndConditionsTests { }
    [TestClass] public class DeletePageTests :TermsAndConditionsTests { }
    [TestClass] public class DetailsPageTests :TermsAndConditionsTests { }
    [TestClass] public class EditPageTests :TermsAndConditionsTests { }
    [TestClass] public class IndexPageTests :TermsAndConditionsTests { }

}
