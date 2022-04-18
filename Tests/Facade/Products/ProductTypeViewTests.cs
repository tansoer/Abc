using System;
using Abc.Data.Products;
using Abc.Facade.Common;
using Abc.Facade.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Products {
    [TestClass] public class ProductTypeViewTests : SealedTests<ProductTypeView, EntityTypeView> {
        [TestMethod] public void ProductKindTest() => isProperty<ProductKind>("Product type");
        [TestMethod] public void PricingStrategyTest() => isProperty<PricingStrategy>("Pricing strategy");
        [TestMethod] public void AmountTest() => isProperty<double>("Amount");
        [TestMethod] public void UnitIdTest() => isNullableProperty<string>("Unit");
        [TestMethod] public void PeriodOfOperationFromTest() => isNullableProperty<DateTime?>("Period of operation from");
        [TestMethod] public void PeriodOfOperationToTest() => isNullableProperty<DateTime?>("Period of operation to");
        [TestMethod] public void ColumnsCountTest() => isProperty<byte>("Container columns count");
        [TestMethod] public void RowsCountTest() => isProperty<byte>("Container rows count");
        [TestMethod] public void LevelsCountTest() => isProperty<byte>("Container levels count");
        [TestMethod] public void AlternativeCodesTest() => isProperty<string>("Alternative codes");
        [TestMethod] public void IsBaseTypeTest() => isProperty<bool>("Is base type");
        [TestMethod] public void CodeTest() => isNullableProperty<string>("Code", true);
    }
}