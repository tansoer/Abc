using Abc.Domain.Common;
using Abc.Domain.Products;
using Abc.Domain.Quantities;
using Abc.Infra.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Products {

    [TestClass]
    public class MeasuredProductTypeTests : SealedTests<MeasuredProductType, QuantifiedProductType> {

        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
        }

        [TestCleanup]
        public override void TestCleanup() {
            var units = GetRepo.Instance<IUnitsRepo>() as UnitsRepo;
            var measures = GetRepo.Instance<IMeasuresRepo>() as MeasuresRepo;
            removeAll(units?.dbSet, units?.db);
            removeAll(measures?.dbSet, measures?.db);
            base.TestCleanup();
        }

    }

}