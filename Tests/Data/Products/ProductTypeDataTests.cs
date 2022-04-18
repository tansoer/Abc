using System;
using Abc.Data.Common;
using Abc.Data.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Products {
    [TestClass] public class ProductTypeDataTests : SealedTests<ProductTypeData, EntityTypeData> {
        [TestMethod] public void ProductKindTest() => isProperty<ProductKind>();
        [TestMethod] public void PricingStrategyTest() => isProperty<PricingStrategy>();
        [TestMethod] public void AmountTest() => isProperty<double>();
        [TestMethod] public void UnitIdTest() => isNullable<string>();
        [TestMethod] public void PeriodOfOperationFromTest() => isNullable<DateTime?>();
        [TestMethod] public void PeriodOfOperationToTest() => isNullable<DateTime?>();
        [TestMethod] public void ColumnsCountTest() => isProperty<byte>();
        [TestMethod] public void RowsCountTest() => isProperty<byte>();
        [TestMethod] public void LevelsCountTest() => isProperty<byte>();
        [TestMethod] public void AlternativeCodesTest() => isProperty<string>();
        [TestMethod] public void IsBaseTypeTest() => isProperty<bool>();
    }
}