using Abc.Domain.Common;
using Abc.Domain.Inventory;
using Abc.Infra.Inventory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Inventory {

    [TestClass]
    public class InventoryDbReposTests :TestsHost {
        [TestInitialize] public void TestInitialize() => type = typeof(InventoryDbRepos);
        [DataTestMethod]
        [DataRow(typeof(IInventoryEntriesRepo))]
        [DataRow(typeof(IReservationsRepo))]
        [DataRow(typeof(IReservationRequestsRepo))]
        [DataRow(typeof(IRestockPoliciesRepo))]
        [DataRow(typeof(IOutstandingOrdersRepo))]
        [DataRow(typeof(IReservationReceiversRepo))]
        [DataRow(typeof(ICapacityManagersRepo))]
        public void RegisterTest(Type t) => Assert.IsNotNull(GetRepo.Instance(t));
    }
}