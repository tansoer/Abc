using System;
using System.Linq;
using System.Threading.Tasks;
using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Products;
using Abc.Facade.Products;
using Abc.Pages.Products;
using Abc.Tests.Pages.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Products {

    [TestClass]
    public class BatchesPageTests : SealedViewFactoryPageTests<
        BatchesPage,
        IBatchesRepo,
        Batch,
        BatchView,
        BatchData, BatchViewFactory> {

        protected override string pageTitle => ProductTitles.Batches;
        protected override string pageUrl => ProductUrls.Batches;
        protected override Batch toObject(BatchData d) => new(d);

        private class batchesRepo :mockRepo<Batch, BatchData>, IBatchesRepo { }
        private class typesRepo :mockRepo<IProductType, ProductTypeData>, IProductTypesRepo { }

        private batchesRepo batches;
        private typesRepo types;

        private ProductTypeData typeData;
        private BatchData batchData;

        protected override BatchesPage createObject() {
            batches = new batchesRepo();
            types = new typesRepo();
            typeData = GetRandom.ObjectOf<ProductTypeData>();
            batchData = GetRandom.ObjectOf<BatchData>();
            addRandomProductTypes();
            addRandomBatches();
            return new BatchesPage(batches);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(batches, types);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }

        private void addRandomBatches() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? batchData : GetRandom.ObjectOf<BatchData>();
                batches.Add(new Batch(d));
            }
        }

        private void addRandomProductTypes() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? typeData : GetRandom.ObjectOf<ProductTypeData>();
                types.Add(ProductTypeFactory.Create(d));
            }
        }

        [TestMethod]
        public override void ToObjectTest() {
            var view = GetRandom.ObjectOf<BatchView>();
            var o = obj.toObject(view);
            allPropertiesAreEqual(view, o.Data, nameof(view.ProductsCount));
        }

        [TestMethod]
        public override void ToViewTest() {
            var d = GetRandom.ObjectOf<BatchData>();
            var view = obj.toView(toObject(d));
            allPropertiesAreEqual(view, d, nameof(view.ProductsCount));
        }

        [TestMethod]
        public void ProductTypeNameTest() {
            var name = obj.ProductTypeName(typeData.Id);
            Assert.AreEqual(typeData.Name, name);
        }

        [TestMethod]
        public void ProductTypesTest() {
            var list = types.Get();
            Assert.AreEqual(list.Count + 1, obj.ProductTypes.Count());
        }

        [TestMethod]
        public async Task OnGetShowProductsAsyncTest() {
            var page = await obj.OnGetShowProductsAsync(rndStr, sortOrder, searchString, pageIndex,
                fixedFilter, fixedValue);
            Assert.IsInstanceOfType(page, typeof(RedirectResult));
        }

        [TestMethod]
        public async Task OnGetShowChecksAsyncTest() {
            var page = await obj.OnGetShowChecksAsync(rndStr, sortOrder, searchString, pageIndex,
                fixedFilter, fixedValue);
            Assert.IsInstanceOfType(page, typeof(RedirectResult));
        }
    }

}