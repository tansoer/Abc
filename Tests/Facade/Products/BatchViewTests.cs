using Abc.Facade.Common;
using Abc.Facade.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Facade.Products {

    [TestClass]
    public class BatchViewTests : SealedTests<BatchView, EntityBaseView> {
        [TestMethod] public void ProductTypeIdTest() => isNullableProperty<string>("Product Type");
        [TestMethod] public void FirstSerialNumberTest() => isNullableProperty<string>("First Serial Number");
        [TestMethod] public void LastSerialNumberTest() => isNullableProperty<string>("Last Serial Number");
        [TestMethod] public void ProductsCountTest() => isProperty<int>("Products Count");
        [TestMethod] public void SellByTest() => isNullableProperty<DateTime?>("Sell By Date");
        [TestMethod] public void UseByTest() => isNullableProperty<DateTime?>("Use By Date");
        [TestMethod] public void BestBeforeTest() => isNullableProperty<DateTime?>("Best Before Date");
        [TestMethod] public void DateProducedTest() => isNullableProperty<DateTime?>("Date Produced");
    }
}