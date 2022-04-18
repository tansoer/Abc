using Abc.Data.Inventory;
using Abc.Domain.Inventory;
using Abc.Facade.Inventory;
using Abc.Pages.Inventory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Inventory.Pages.CapacityManagers {
    public abstract class CapacityManagersTests :BaseInventoryTests<CapacityManagerView, CapacityManagerData> {
        protected override string baseUrl() => InventoryUrls.CapacityManagers;
        protected override void changeValuesExceptId(CapacityManagerData d) {
            var id = d.Id;
            d = random<CapacityManagerData>();
            d.Id = id;
        }
        protected override string getItemId(CapacityManagerData d) => d.Id;
        protected override void setItemId(CapacityManagerData d, string id) => d.Id = id;
        protected override CapacityManagerView toView(CapacityManagerData d)
            => new CapacityManagerViewFactory().Create(new CapacityManager(d));
        protected override IEnumerable<Expression<Func<CapacityManagerView, object>>> indexPageColumns =>
            new Expression<Func<CapacityManagerView, object>>[] {
                x => x.Name,
                x => x.Code,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, m => m.Id);
            canView(item, m => m.Name);
            canView(item, m => m.Code);
            canView(item, m => m.Details);
            canView(item, m => m.ValidFrom);
            canView(item, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, m => m.Name, true);
            canEdit(item, m => m.Code);
            canEdit(item, m => m.Details);
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, m => m.Name, true);
            canEdit(null, m => m.Code);
            canEdit(null, m => m.Details);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(item, x => x.Id, true);
            HiddenInputs.Clear();
        }
    }
    [TestClass] public class CreatePageTests :CapacityManagersTests { }
    [TestClass] public class DeletePageTests :CapacityManagersTests { }
    [TestClass] public class DetailsPageTests :CapacityManagersTests { }
    [TestClass] public class EditPageTests :CapacityManagersTests { }
    [TestClass] public class IndexPageTests :CapacityManagersTests { }
}
