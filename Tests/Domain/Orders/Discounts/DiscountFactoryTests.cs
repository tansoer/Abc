using Abc.Aids.Random;
using Abc.Data.Orders;
using Abc.Domain.Orders.Discounts;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Domain.Orders.Discounts {

    [TestClass]
    public class DiscountFactoryTests :TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(DiscountFactory);

        [DataTestMethod]
        [DataRow(DiscountsType.Monetary, typeof(MonetaryDiscount))]
        [DataRow(DiscountsType.Percentage, typeof(PercentageDiscount))]
        public void CreateTest(DiscountsType type, Type t) {
            var d = GetRandom.ObjectOf<DiscountData>();
            d.DiscountType = type;
            var o = DiscountFactory.Create(d);
            Assert.IsInstanceOfType(o, t);
            allPropertiesAreEqual(d, o.Data);
        }
    }
}