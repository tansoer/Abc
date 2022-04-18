using Abc.Aids.Random;
using Abc.Data.Orders;
using Abc.Data.Parties;
using Abc.Domain.Orders;
using Abc.Domain.Orders.Lines;
using Abc.Domain.Orders.Parties;
using Abc.Domain.Roles;
using Abc.Facade.Orders;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Orders.Parties {
    [TestClass] public class PartyInOrderTests :AbstractTests<PartyInOrder, BasePartySummary> {
        private class testClass :PartyInOrder {
            public testClass(PartySummaryData d) :base(d) { }
        }
        protected override PartyInOrder createObject() 
            => new testClass(random<PartySummaryData>());
        
        [DataRow (PartyRoleInOrder.OrderInitiator)]
        [DataRow(PartyRoleInOrder.OrderLineReceiver)]
        [DataRow(PartyRoleInOrder.OrderReceiver)]
        [DataRow(PartyRoleInOrder.PaymentReceiver)]
        [DataRow(PartyRoleInOrder.Purchaser)]
        [DataRow(PartyRoleInOrder.SalesAgent)]
        [DataRow(PartyRoleInOrder.Unspecified)]
        [DataRow(PartyRoleInOrder.Vendor)]
        [DataTestMethod] public void IsRoleTypeOfTest(PartyRoleInOrder r) {
            var d = obj.Data;
            d.RoleInOrder = r;
            IPartyInOrder o = PartyInOrderFactory.Create(d);
            foreach (var e in Enum.GetValues<PartyRoleInOrder>()) {
                var b = e == r;
                areEqual(b, o?.IsRoleTypeOf(e));
            }
        }
        [TestMethod] public void OrderIdTest() => isReadOnly(obj.Data.OrderId, true);
        [TestMethod] public void OrderLineIdTest() => isReadOnly(obj.Data.OrderLineId, true);
        [TestMethod] public void RoleInOrderTest() => isReadOnly(obj.Data.RoleInOrder);
        [TestMethod] public async Task OrderLineTest() =>
            await testItemAsync<OrderLineData, IOrderLine, IOrderLinesRepo>(
                getLineData(), () => obj.OrderLine.Data, d => OrderLineFactory.Create(d));

        private OrderLineData getLineData() {
            var d = random<OrderLineData>();
            d.Id = obj.orderLineId;
            d.OrderLineType = OrderLineType.ProductLine;
            return d;
        }

        [TestMethod] public async Task OrderTest() =>
            await testItemAsync<OrderData, IOrder, IOrdersRepo>(
                obj.orderId, () => obj.Order.Data, d => OrderFactory.Create(d));
        [TestMethod] public void IsCorrectTest() {
            var d = new PartySummaryData();
            isFalse(PartyInOrderFactory.Create(d).IsCorrect());
            d.Id = random<string>();
            d.OrderId = random<string>();
            d.OrderLineId = random<string>();
            d.ValidFrom = random<DateTime>();
            d.ValidTo = random<DateTime>();
            isFalse(PartyInOrderFactory.Create(d).IsCorrect());
            d.RoleInOrder = (PartyRoleInOrder)GetRandom.UInt8(1, 7);
            isTrue(PartyInOrderFactory.Create(d).IsCorrect());
            isTrue(PartyInOrderFactory.Create(random<PartySummaryData>()).IsCorrect());
            isTrue(obj.IsCorrect());
        }
    }
}