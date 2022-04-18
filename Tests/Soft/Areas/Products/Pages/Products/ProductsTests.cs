using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Data.Quantities;
using Abc.Domain.Common;
using Abc.Domain.Products;
using Abc.Domain.Quantities;
using Abc.Facade.Products;
using Abc.Pages.Products;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Soft.Areas.Products.Pages.Products {

    public abstract class ProductsTests :BaseProductTests<ProductView, ProductData> {
        protected ProductKind? productKind;
        private List<SelectListItem> units => createSelectList(quantityDb.Units);
        private List<SelectListItem> types => createSelectList(db.ProductTypes);
        private List<SelectListItem> batches => createSelectList(db.Batches);
        protected override string baseUrl() => ProductUrls.Products;
        protected override void addRelatedItems(ProductData d) {
            if (d is null) return;
            addProductType(d.ProductTypeId, d.ProductKind);
            addUnit(d.UnitId);
            addBatch(d.BatchId);
        }
        private void addUnit(string id) {
            var ud = GetRandom.ObjectOf<UnitData>();
            ud.Id = id;
            GetRepo.Instance<IUnitsRepo>().Add(UnitFactory.Create(ud));
        }
        protected override void changeValuesExceptId(ProductData d) {
            var id = d.Id;
            d = random<ProductData>();
            d.Id = id;
        }
        protected override ProductData correctOriginal(ProductData oldItem, ProductData newItem) {
            oldItem.ProductKind = newItem.ProductKind;
            update<IProductsRepo, IProduct>(ProductFactory.Create(oldItem));
            return oldItem;
        }
        protected override string getItemId(ProductData d) => d.Id;
        protected override void setItemId(ProductData d, string id) => d.Id = id;
        protected override ProductData randomItem() {
            var d = base.randomItem();
            d.ProductKind = productKind ?? d.ProductKind;
            correctProperties(d);
            return d;
        }
        private static void correctProperties(ProductData d) {
            d.Amount = GetRandom.Double(-1000, 1000);
            d.ValidFrom = GetRandom.DateTime(DateTime.Now.AddYears(-10), DateTime.Now).Date;
            d.ValidTo = GetRandom.DateTime(DateTime.Now, DateTime.Now.AddYears(10)).Date;
            d.DeliveredFrom = GetRandom.DateTime(DateTime.Now.AddYears(-10), DateTime.Now).Date;
            d.DeliveredTo = GetRandom.DateTime(DateTime.Now, DateTime.Now.AddYears(10)).Date;
            d.ScheduledFrom = GetRandom.DateTime(DateTime.Now.AddYears(-10), DateTime.Now).Date;
            d.ScheduledTo = GetRandom.DateTime(DateTime.Now, DateTime.Now.AddYears(10)).Date;
        }
        protected override ProductView toView(ProductData d) {
            var o = ProductFactory.Create(d);
            var v = new ProductViewFactory().Create(o);
            return v;
        }
        protected override string pageUrl() {
            var url = base.pageUrl();
            if (productKind is null) return url;
            var typeIdx = Convert.ToInt32(productKind);
            url += $"&switchOfCreate={typeIdx}";
            return url;
        }
        protected override void validateItems(ProductData d1, ProductData d2) {
            var except = composeExcepted(d1);
            allPropertiesAreEqual(d1, d2, except);
        }
        private string[] composeExcepted(ProductData d) {
            var l = new List<string> {
                    nameof(d.Amount),
                    nameof(d.UnitId),
                    nameof(d.DeliveredFrom),
                    nameof(d.DeliveredTo),
                    nameof(d.DeliveryStatus),
                    nameof(d.ScheduledFrom),
                    nameof(d.ScheduledTo)
                };

            var k = d?.ProductKind ?? ProductKind.Unspecified;

            switch (k) {
                case ProductKind.Unspecified:
                case ProductKind.UniqueProduct:
                case ProductKind.Product:
                case ProductKind.Container:
                case ProductKind.Package:
                    break;
                case ProductKind.MeasuredProduct:
                case ProductKind.IdenticalProduct:
                    l.Remove(nameof(d.Amount));
                    l.Remove(nameof(d.UnitId));
                    break;
                case ProductKind.Service:
                    l.Remove(nameof(d.DeliveredTo));
                    l.Remove(nameof(d.DeliveredFrom));
                    l.Remove(nameof(d.ScheduledTo));
                    l.Remove(nameof(d.ScheduledFrom));
                    l.Remove(nameof(d.DeliveryStatus));
                    break;
            }

            return l.ToArray();
        }
        protected override IEnumerable<Expression<Func<ProductView, object>>> indexPageColumns =>
            new Expression<Func<ProductView, object>>[] {
                    x => x.Code,
                    x => x.ProductTypeId,
                    x => x.Name,
                    x => x.ProductKind,
                    x => x.Details,
                    x => x.ValidFrom,
                    x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            ProductView v = toView(item);
            canView(v, m => m.Code);
            canView(v, m => m.Name);
            canView(v, m => m.ProductKind);
            canView(v, m => m.ProductTypeId, Unspecified.String);
            canView(v, m => m.Details);
            canView(v, m => m.BatchId, Unspecified.String);

            if (v.ProductKind == ProductKind.Service) {
                canView(v, m => m.DeliveredFrom);
                canView(v, m => m.DeliveredTo);
                canView(v, m => m.ScheduledFrom);
                canView(v, m => m.ScheduledTo);
                canView(v, m => m.DeliveryStatus);
            }

            if (v.ProductKind == ProductKind.MeasuredProduct || v.ProductKind == ProductKind.IdenticalProduct) {
                canView(v, m => m.Amount);
                canView(v, m => m.UnitId, Unspecified.String);
            }

            canView(v, m => m.ReservationStatus);
            canView(v, m => m.ValidFrom);
            canView(v, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            ProductView v = toView(item);
            canEdit(v, m => m.Code, true);
            canEdit(v, m => m.Name);
            canEdit(v, m => m.Details);
            canSelect(v, m => m.ProductTypeId, types);
            canSelect(v, m => m.BatchId, batches);

            if (v.ProductKind == ProductKind.Service) {
                canEdit(v, m => m.DeliveredFrom);
                canEdit(v, m => m.DeliveredTo);
                canEdit(v, m => m.ScheduledFrom);
                canEdit(v, m => m.ScheduledTo);
                canSelectEnum(v, m => m.DeliveryStatus, true, DeliveryStatus.Unspecified);
            }
            else if (v.ProductKind == ProductKind.MeasuredProduct || v.ProductKind == ProductKind.IdenticalProduct) {
                canEdit(v, m => m.Amount, true, true, 0);
                canSelect(v, m => m.UnitId, units);
            }

            canSelectEnum(v, m => m.ReservationStatus, true, ReservationStatus.Unspecified);
            canEdit(v, m => m.ValidFrom);
            canEdit(v, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, m => m.Code, true);
            canEdit(null, m => m.Name);
            canEdit(null, m => m.Details);
            canSelect(null, m => m.ProductTypeId, types);
            canSelect(null, m => m.BatchId, batches);
            if (productKind == ProductKind.Service) {
                canEdit(null, m => m.DeliveredFrom);
                canEdit(null, m => m.DeliveredTo);
                canEdit(null, m => m.ScheduledFrom);
                canEdit(null, m => m.ScheduledTo);
                canSelectEnum(null, m => m.DeliveryStatus, true, DeliveryStatus.Unspecified);
            }
            else if (productKind == ProductKind.MeasuredProduct || productKind == ProductKind.IdenticalProduct) {
                canEdit(null, m => m.Amount, true, true, 0);
                canSelect(null, m => m.UnitId, units);
            }

            canSelectEnum(null, m => m.ReservationStatus, true, ReservationStatus.Unspecified);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(item, x => x.Id, true);
            hasHidden(item, x => x.ProductKind, true);
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
        public void CanDisplayDataTest(ProductKind k) {
            productKind = k;
            CanDisplayDataTest();
        }
    }
    [TestClass] public class CreatePageTests :ProductsTests {
        [DataTestMethod]
        [DataRow(ProductKind.MeasuredProduct)]
        [DataRow(ProductKind.UniqueProduct)]
        [DataRow(ProductKind.IdenticalProduct)]
        [DataRow(ProductKind.Service)]
        [DataRow(ProductKind.Container)]
        [DataRow(ProductKind.Package)]
        [DataRow(ProductKind.Product)]
        [DataRow(ProductKind.Unspecified)]
        public void CanClickCreateButtonTest(ProductKind k) {
            productKind = k;
            canClickCreateButtonTest();
        }
    }
    [TestClass] public class DeletePageTests :ProductsTests {
        [DataTestMethod]
        [DataRow(ProductKind.MeasuredProduct)]
        [DataRow(ProductKind.UniqueProduct)]
        [DataRow(ProductKind.IdenticalProduct)]
        [DataRow(ProductKind.Service)]
        [DataRow(ProductKind.Container)]
        [DataRow(ProductKind.Package)]
        [DataRow(ProductKind.Product)]
        [DataRow(ProductKind.Unspecified)]
        public void CanClickDeleteButtonTest(ProductKind k) {
            productKind = k;
            canClickDeleteButtonTest();
        }
    }
    [TestClass] public class DetailsPageTests :ProductsTests {
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
            productKind = t;
            canClickEditButtonInDetailsTest();
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
        public void CanClickPackageTypeNavigationLinks(ProductKind kind) {
            productKind = kind;
            item = randomItem();
            addToDatabase(item);

            clickNavigationLink("showFeatures", ProductUrls.Features);
            clickNavigationLink("showPrices", ProductUrls.Prices);
            if (kind is ProductKind.Package) clickNavigationLink("showPackageContents", ProductUrls.PackageContents);
            if (kind is ProductKind.Container) clickNavigationLink("showContainerItems", ProductUrls.ContainerItems);
        }
    }
    [TestClass] public class EditPageTests :ProductsTests {
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
            productKind = t;
            if (productKind is ProductKind.UniqueProduct) notTested("UniqueProduct test fails, because updating old item in the database fails. " +
                "Update can't find right object, because UniqueProduct domain object overrides it's Id as it's types id");
            else canClickEditButtonTest();
        }
    }
    [TestClass] public class IndexPageTests :ProductsTests { }
}

