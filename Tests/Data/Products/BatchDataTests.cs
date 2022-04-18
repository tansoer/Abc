using System;
using Abc.Data.Common;
using Abc.Data.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Products {

    [TestClass]
    public class BatchDataTests : SealedTests<BatchData, EntityBaseData> {

        [TestMethod] public void ProductTypeIdTest() => isNullable<string>();
        [TestMethod] public void FirstSerialNumberTest() => isNullable<string>();
        [TestMethod] public void LastSerialNumberTest() => isNullable<string>();
        [TestMethod] public void SellByTest() => isProperty<DateTime>();
        [TestMethod] public void UseByTest() => isProperty<DateTime>();
        [TestMethod] public void BestBeforeTest() => isProperty<DateTime>();
        [TestMethod] public void DateProducedTest() => isProperty<DateTime>();

    }

}