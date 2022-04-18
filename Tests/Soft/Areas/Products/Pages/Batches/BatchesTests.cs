using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Facade.Products;
using Abc.Pages.Products;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Products.Pages.Batches {
    public abstract class BatchesTests :BaseProductTests<BatchView, BatchData> {
        protected List<SelectListItem> productTypes => createSelectList(db.ProductTypes);
        protected override string baseUrl() => ProductUrls.Batches;
        protected override string baseClassName() => "Batch";
        protected override BatchView toView(BatchData d)
            => new BatchViewFactory().Create(new Abc.Domain.Products.Batch(d));
        protected override string performTitleCorrection(string title) => title[0..^2];
        protected override void changeValuesExceptId(BatchData d) {
            var id = d.Id;
            d = random<BatchData>();
            d.Id = id;
        }
        protected override string getItemId(BatchData d) => d.Id;
        protected override void setItemId(BatchData d, string id) => d.Id = id;
        protected override void addRelatedItems(BatchData d) {
            if (d is null) return;
            addProductType(d.ProductTypeId);
        }
        protected override BatchData randomItem() {
            var d = GetRandom.ObjectOf<BatchData>();
            d.SellBy = d.SellBy.Date;
            d.UseBy = d.UseBy.Date;
            d.BestBefore = d.BestBefore.Date;
            d.DateProduced = d.DateProduced.Date;
            if (d.ValidFrom != null) d.ValidFrom = ((DateTime)d.ValidFrom).Date;
            if (d.ValidTo != null) d.ValidTo = ((DateTime)d.ValidTo).Date;
            return d;
        }
        protected override IEnumerable<Expression<Func<BatchView, object>>> indexPageColumns =>
            new Expression<Func<BatchView, object>>[] {
                x => x.Name,
                x => x.ProductTypeId,
                x => x.Code,
                x => x.Details,
                x => x.ValidFrom,
                x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, m => m.Id);
            canView(item, m => m.Name);
            canView(item, m => m.Code);
            canView(item, m => m.ProductTypeId, Unspecified.String);
            canView(item, m => m.ProductsCount, "0");
            canView(item, m => m.FirstSerialNumber);
            canView(item, m => m.LastSerialNumber);
            canView(item, m => m.DateProduced);
            canView(item, m => m.BestBefore);
            canView(item, m => m.SellBy);
            canView(item, m => m.UseBy);
            canView(item, m => m.Details);
            canView(item, m => m.ValidFrom);
            canView(item, m => m.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, m => m.Name, true);
            canSelect(item, m => m.ProductTypeId, productTypes);
            canEdit(item, m => m.Code);
            canEdit(item, m => m.Details);
            canEdit(item, m => m.FirstSerialNumber);
            canEdit(item, m => m.LastSerialNumber);
            canEdit(item, m => m.DateProduced);
            canEdit(item, m => m.BestBefore);
            canEdit(item, m => m.SellBy);
            canEdit(item, m => m.UseBy);
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, m => m.Name, true);
            canSelect(null, x => x.ProductTypeId, productTypes);
            canEdit(null, m => m.Code);
            canEdit(null, m => m.Details);
            canEdit(null, m => m.FirstSerialNumber);
            canEdit(null, m => m.LastSerialNumber);
            canEdit(null, m => m.DateProduced);
            canEdit(null, m => m.BestBefore);
            canEdit(null, m => m.SellBy);
            canEdit(null, m => m.UseBy);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(item, x => x.Id, true);
        }
    }
    [TestClass] public class CreatePageTests :BatchesTests { }
    [TestClass] public class DeletePageTests :BatchesTests { }
    [TestClass] public class DetailsPageTests :BatchesTests {
        [TestMethod]
        public void CanClickProductsNavigationLink()
            => clickNavigationLink("showProducts", ProductUrls.Products);
        [TestMethod]
        public void CanClickCheckedByNavigationLink()
            => clickNavigationLink("showCheckedBy", ProductUrls.BatchCheckedBy);
    }
    [TestClass] public class EditPageTests :BatchesTests { }
    [TestClass] public class IndexPageTests :BatchesTests { }
}
