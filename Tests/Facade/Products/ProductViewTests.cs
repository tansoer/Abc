using System;
using Abc.Data.Products;
using Abc.Facade.Common;
using Abc.Facade.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Products {

    [TestClass]
    public class ProductViewTests : SealedTests<ProductView, EntityBaseView> {

        [TestMethod] public void ProductTypeIdTest() => isNullableProperty<string>("Product Type");

        [TestMethod] public void BatchIdTest() => isNullableProperty<string>("Batch");

        [TestMethod] public void CodeTest() => isNullableProperty<string>("Serial Number", true); 
        
        [TestMethod] public void NameTest() => isNullable<string>();

        [TestMethod] public void ProductKindTest() => isProperty<ProductKind>("Product Kind");

        [TestMethod] public void AmountTest() => isProperty<double>("Amount");

        [TestMethod] public void UnitIdTest() => isNullableProperty<string>("Unit");

        [TestMethod] public void ScheduledFromTest() => isNullableProperty<DateTime?>("Scheduled From");

        [TestMethod] public void ScheduledToTest() => isNullableProperty<DateTime?>("Scheduled To");

        [TestMethod] public void DeliveredFromTest() => isNullableProperty<DateTime?>("Delivered From");

        [TestMethod] public void DeliveredToTest() => isNullableProperty<DateTime?>("Delivered To");

        [TestMethod] public void DeliveryStatusTest() => isProperty<DeliveryStatus>("Delivery Status");

        [TestMethod] public void ReservationStatusTest() => isProperty<ReservationStatus>("Reservation Status");

    }

}
