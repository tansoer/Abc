using Abc.Domain.Common;
using Abc.Domain.Orders;
using Abc.Domain.Orders.Delivery;
using Abc.Domain.Orders.Discounts;
using Abc.Domain.Orders.Lines;
using Abc.Domain.Orders.Terms;
using Abc.Infra.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Orders {

    [TestClass]
    public class OrderDbReposTests :TestsHost {
        [TestInitialize] public void TestInitialize() => type = typeof(OrderDbRepos);
        [DataTestMethod]
        [DataRow(typeof(IOrdersRepo))]
        [DataRow(typeof(IDiscountsRepo))]
        [DataRow(typeof(IDiscountTypesRepo))]
        [DataRow(typeof(IReturnedItemsRepo))]
        [DataRow(typeof(IOrderLineItemsRepo))]
        [DataRow(typeof(ISalesTaxPoliciesRepo))]
        [DataRow(typeof(IOrderEventsRepo))]
        [DataRow(typeof(IOrderLinesRepo))]
        [DataRow(typeof(IDiscountTypesRepo))]
        [DataRow(typeof(ITermsAndConditionsRepo))]
        public void RegisterTest(Type t) => Assert.IsNotNull(GetRepo.Instance(t));
    }
}