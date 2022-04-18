using System;
using Abc.Data.Common;
using Abc.Data.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Products {

    [TestClass]
    public class ProductDataTests : SealedTests<ProductData, EntityBaseData> {

        [TestMethod] public void ProductTypeIdTest() => isNullable<string>();
        [TestMethod] public void ProductKindTest() => isProperty<ProductKind>();
        [TestMethod] public void AmountTest() => isProperty<double>();
        [TestMethod] public void UnitIdTest() => isNullable<string>();
        [TestMethod] public void BatchIdTest() => isNullable<string>();
        [TestMethod] public void ScheduledFromTest() => isNullable<DateTime?>();
        [TestMethod] public void ScheduledToTest() => isNullable<DateTime?>();
        [TestMethod] public void DeliveredFromTest() => isNullable<DateTime?>();
        [TestMethod] public void DeliveredToTest() => isNullable<DateTime?>();
        [TestMethod] public void DeliveryStatusTest() => isProperty<DeliveryStatus>();
        [TestMethod] public void ReservationStatusTest() => isProperty<ReservationStatus>();

    }

}