using Abc.Aids.Random;
using Abc.Data.Orders;
using Abc.Data.Parties;
using Abc.Domain.Orders.Parties;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace Abc.Tests.Domain.Orders.Parties {

    [TestClass]
    public class PartyInOrderFactoryTests :TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(PartyInOrderFactory);

        [DataTestMethod]
        [DataRow(PartyRoleInOrder.Vendor, typeof(Vendor))]
        [DataRow(PartyRoleInOrder.SalesAgent, typeof(SalesAgent))]
        [DataRow(PartyRoleInOrder.PaymentReceiver, typeof(PaymentReceiver))]
        [DataRow(PartyRoleInOrder.OrderInitiator, typeof(OrderInitiator))]
        [DataRow(PartyRoleInOrder.Purchaser, typeof(Purchaser))]
        [DataRow(PartyRoleInOrder.OrderReceiver, typeof(OrderReceiver))]
        [DataRow(PartyRoleInOrder.OrderLineReceiver, typeof(OrderLineReceiver))]
        public void CreateTest(PartyRoleInOrder type, Type t) {
            var d = GetRandom.ObjectOf<PartySummaryData>();
            d.RoleInOrder = type;
            var o = PartyInOrderFactory.Create(d);
            Assert.IsInstanceOfType(o, t);
            allPropertiesAreEqual(d, o.Data);
        }
    }
}