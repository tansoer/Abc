using Abc.Aids.Random;
using Abc.Aids.Reflection;
using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Facade.Products;
using Abc.Tests.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Products {

    [TestClass]
    public class BatchViewFactoryTests : ViewFactoryTests<BatchViewFactory, BatchData, Batch, BatchView> {
        [TestMethod] public void ViewHasCorrectProductCountTest() { 
            var d = random<BatchData>();
            addSomeProductsToBatch(d.Id);
            var o = new Batch(d);
            var v = new BatchViewFactory().Create(o);
            areEqual(o.ProductsCount, v.ProductsCount);
        }
        private void addSomeProductsToBatch(string batchId) {
            int count = GetRandom.Int32(5, 10);
            for (int i = 0; i < count; i++) {
                var d = random<ProductData>();
                d.BatchId = batchId;
                add<IProductsRepo, IProduct>(ProductFactory.Create(d));
            }
        }

        protected override void evaluateData(BatchView v, BatchData d) => allPropertiesAreEqual(v, d, nameof(v.ProductsCount));
    }
}