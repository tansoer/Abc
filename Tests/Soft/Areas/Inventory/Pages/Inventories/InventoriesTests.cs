using Abc.Data.Inventory;
using Abc.Facade.Inventory;
using Abc.Pages.Inventory;
using AngleSharp.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Inventory.Pages.Inventories {
    public abstract class InventoriesTests :BaseInventoryTests<InventoryView, InventoryData> {
        protected override string baseUrl() => InventoryUrls.Inventories;
        protected override void changeValuesExceptId(InventoryData d) {
            var id = d.Id;
            d = random<InventoryData>();
            d.Id = id;
        }
        protected override string getItemId(InventoryData d) => d.Id;
        protected override void setItemId(InventoryData d, string id) => d.Id = id;
        protected override string baseClassName() {
            var n = GetType().BaseType?.Name;
            n = n?.ReplaceFirst("Base", string.Empty);
            n = n?.ReplaceFirst("iesTests", "y");
            return n;
        }
        protected override string performTitleCorrection(string n) => n.Replace("ies", "y");
        protected override InventoryView toView(InventoryData d)
            => new InventoryViewFactory().Create(new Abc.Domain.Inventory.Inventory(d));
        protected override IEnumerable<Expression<Func<InventoryView, object>>> indexPageColumns =>
            new Expression<Func<InventoryView, object>>[] {
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
    [TestClass] public class CreatePageTests :InventoriesTests { }
    [TestClass] public class DeletePageTests :InventoriesTests { }
    [TestClass] public class DetailsPageTests :InventoriesTests { }
    [TestClass] public class EditPageTests :InventoriesTests { }
    [TestClass] public class IndexPageTests :InventoriesTests { }
}
