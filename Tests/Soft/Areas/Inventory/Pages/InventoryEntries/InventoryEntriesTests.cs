using Abc.Data.Inventory;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Inventory;
using Abc.Domain.Products;
using Abc.Facade.Inventory;
using Abc.Infra.Products;
using Abc.Pages.Inventory;
using AngleSharp.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Inventory.Pages.InventoryEntries {
    public abstract class InventoryEntriesTests :BaseInventoryTests<InventoryEntryView, InventoryEntryData> {

        private ProductDb productDb;
        protected ProductKind? entryType;

        protected List<SelectListItem> inventories => createSelectList(db.Inventories);
        protected List<SelectListItem> productTypes => createSelectList(productDb.ProductTypes, x => x.ProductKind == entryType);
        protected List<SelectListItem> capacityManagers => createSelectList(db.CapacityManagers);
        protected List<SelectListItem> restockPolicies => createSelectList(db.RestockPolicies);

        [TestInitialize]
        public override void TestInitialize() {
            productDb = getContext<ProductDb>();
            base.TestInitialize();
        }

        protected override string baseUrl() => InventoryUrls.InventoryEntries;
        protected override void addRelatedItems(InventoryEntryData d) {
            if (d is null) return;
            addInventory(d.InventoryId);
            addProductType(d.ProductTypeId, d.InventoryEntryType);
            addCapacityManager(d.CapacityManagerId);
            addRestockPolicy(d.RestockPolicyId);
            setNullValues(d);
        }
        private void addProductType(string id, ProductKind kind) {
            var d = random<ProductTypeData>();
            d.Id = id;
            d.ProductKind = kind;
            add<IProductTypesRepo, IProductType>(ProductTypeFactory.Create(d));
        }
        private void setNullValues(InventoryEntryData d) {
            if (d.InventoryEntryType is ProductKind.Service) {
                d.RestockPolicyId = null;
            } else {
                d.CapacityManagerId = null;
            }
        }
        protected override void changeValuesExceptId(InventoryEntryData d) {
            var id = d.Id;
            d = random<InventoryEntryData>();
            d.Id = id;
        }
        protected override InventoryEntryData correctOriginal(InventoryEntryData oldItem, InventoryEntryData newItem) {
            oldItem.InventoryEntryType = newItem.InventoryEntryType;
            update<IInventoryEntriesRepo, InventoryEntry>(InventoryEntryFactory.Create(oldItem));
            return oldItem;
        }
        protected override string getItemId(InventoryEntryData d) => d.Id;
        protected override void setItemId(InventoryEntryData d, string id) => d.Id = id;
        protected override InventoryEntryView toView(InventoryEntryData d) {
            var o = InventoryEntryFactory.Create(d);
            var v = new InventoryEntryViewFactory().Create(o);
            return v;
        }
        protected override string pageUrl() {
            var url = base.pageUrl();
            if (entryType is null) return url;
            var typeIdx = Convert.ToInt32(entryType);
            url += $"&switchOfCreate={typeIdx}";
            return url;
        }
        protected override string baseClassName() {
            var n = GetType().BaseType?.Name;
            n = n?.ReplaceFirst("Base", string.Empty);
            n = n?.ReplaceFirst("iesTests", "y");
            return n;
        }
        protected override string performTitleCorrection(string n) => n.Replace("ies", "y");
        protected override InventoryEntryData randomItem() {
            var d = base.randomItem();
            d.InventoryEntryType = entryType ?? d.InventoryEntryType;
            return d;
        }

        protected override IEnumerable<Expression<Func<InventoryEntryView, object>>> indexPageColumns =>
            new Expression<Func<InventoryEntryView, object>>[] {
                x => x.InventoryId,
                x => x.InventoryEntryType,
                x => x.ProductTypeId,
                x => x.ValidFrom,
                x => x.ValidTo,
            };

        protected override void dataInDetailsPage() {
            InventoryEntryView v = toView(item);
            canView(v, x => x.Id);
            canView(v, x => x.Name);
            canView(v, x => x.Code);
            canView(v, x => x.InventoryEntryType);
            canView(v, x => x.InventoryId, Unspecified.String);
            canView(v, x => x.ProductTypeId, Unspecified.String);

            if (v.InventoryEntryType is ProductKind.Service) {
                canView(v, x => x.CapacityManagerId, Unspecified.String);
            } else {
                canView(v, x => x.RestockPolicyId, Unspecified.String);
            }

            canView(v, x => x.Details);
            canView(v, x => x.ValidFrom);
            canView(v, x => x.ValidTo);
        }

        protected override void dataInEditPage() {
            InventoryEntryView v = toView(item);
            entryType = v.InventoryEntryType;

            canSelect(v, x => x.InventoryId, inventories);
            canSelect(v, x => x.ProductTypeId, productTypes);
            canEdit(v, x => x.Name, true);
            canEdit(v, x => x.Code);

            if (v.InventoryEntryType is ProductKind.Service) {
                canSelect(v, x => x.CapacityManagerId, capacityManagers);
            } else {
                canSelect(v, x => x.RestockPolicyId, restockPolicies);
            }

            canEdit(v, x => x.Details);
            canEdit(v, x => x.ValidFrom);
            canEdit(v, x => x.ValidTo);
        }

        protected override void dataInCreatePage() {
            canSelect(null, x => x.InventoryId, inventories);
            canSelect(null, x => x.ProductTypeId, productTypes);
            canEdit(null, x => x.Name, true);
            canEdit(null, x => x.Code);

            if (entryType is ProductKind.Service) {
                canSelect(null, x => x.CapacityManagerId, capacityManagers);
            } else {
                canSelect(null, x => x.RestockPolicyId, restockPolicies);
            }

            canEdit(null, x => x.Details);
            canEdit(null, x => x.ValidFrom);
            canEdit(null, x => x.ValidTo);
        }

        [DataTestMethod]
        [DataRow(ProductKind.MeasuredProduct)]
        [DataRow(ProductKind.UniqueProduct)]
        [DataRow(ProductKind.IdenticalProduct)]
        [DataRow(ProductKind.Service)]
        [DataRow(ProductKind.Container)]
        [DataRow(ProductKind.Package)]
        [DataRow(ProductKind.Product)]
        [DataRow(ProductKind.Unspecified)]
        public void CanDisplayDataTest(ProductKind t) {
            entryType = t; 
            CanDisplayDataTest();
        }
        protected override void dataHiddenInPage() {
            hasHidden(item, x => x.Id, true);
            HiddenInputs.Clear();
        }

    }
    [TestClass] public class CreatePageTests :InventoryEntriesTests {
        [DataTestMethod]
        [DataRow(ProductKind.MeasuredProduct)]
        [DataRow(ProductKind.UniqueProduct)]
        [DataRow(ProductKind.IdenticalProduct)]
        [DataRow(ProductKind.Service)]
        [DataRow(ProductKind.Container)]
        [DataRow(ProductKind.Package)]
        [DataRow(ProductKind.Product)]
        [DataRow(ProductKind.Unspecified)]
        public void CanClickCreateButtonTest(ProductKind t) {
            entryType = t;
            canClickCreateButtonTest();
        }
    } 
    [TestClass] public class DeletePageTests :InventoryEntriesTests {
        [DataTestMethod]
        [DataRow(ProductKind.MeasuredProduct)]
        [DataRow(ProductKind.UniqueProduct)]
        [DataRow(ProductKind.IdenticalProduct)]
        [DataRow(ProductKind.Service)]
        [DataRow(ProductKind.Container)]
        [DataRow(ProductKind.Package)]
        [DataRow(ProductKind.Product)]
        [DataRow(ProductKind.Unspecified)]
        public void CanClickDeleteButtonTest(ProductKind t) {
            entryType = t;
            canClickDeleteButtonTest();
        }
    }
    [TestClass] public class DetailsPageTests :InventoryEntriesTests {
        [DataTestMethod]
        [DataRow(ProductKind.MeasuredProduct)]
        [DataRow(ProductKind.UniqueProduct)]
        [DataRow(ProductKind.IdenticalProduct)]
        [DataRow(ProductKind.Service)]
        [DataRow(ProductKind.Container)]
        [DataRow(ProductKind.Package)]
        [DataRow(ProductKind.Product)]
        [DataRow(ProductKind.Unspecified)]
        public void CanClickEditButtonInDetailsTest(ProductKind t) {
            entryType = t;
            canClickEditButtonInDetailsTest();
        }
    }
    [TestClass] public class EditPageTests :InventoryEntriesTests {
        [DataTestMethod]
        [DataRow(ProductKind.MeasuredProduct)]
        [DataRow(ProductKind.UniqueProduct)]
        [DataRow(ProductKind.IdenticalProduct)]
        [DataRow(ProductKind.Service)]
        [DataRow(ProductKind.Container)]
        [DataRow(ProductKind.Package)]
        [DataRow(ProductKind.Product)]
        [DataRow(ProductKind.Unspecified)]
        public void CanClickEditButtonTest(ProductKind t) {
            entryType = t;
            canClickEditButtonTest();
        }
    }
    [TestClass] public class IndexPageTests :InventoryEntriesTests { }
}
