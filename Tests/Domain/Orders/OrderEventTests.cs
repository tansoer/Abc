using Abc.Data.Orders;
using Abc.Data.Parties;
using Abc.Domain.Common;
using Abc.Domain.Orders;
using Abc.Domain.Orders.Lines;
using Abc.Domain.Parties.Signatures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Orders {

    [TestClass] public class OrderEventTests :AbstractTests<OrderEvent, Entity<OrderEventData>> {
        private class testClass :OrderEvent {
            public testClass() : this(null) { }
            public testClass(OrderEventData d) : base(d) { }
        }
        protected override OrderEvent createObject() => new testClass(random<OrderEventData>());
        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            obj = createObject();
        }
        [TestMethod] public async Task OrderLinesTest()
            => await testListAsync<IOrderLine, OrderLineData, IOrderLinesRepo>(
                x => x.OrderEventId = obj.Id, OrderLineFactory.Create);
        [TestMethod] public async Task ApprovalsTest()
            => await testListAsync<PartySignature, PartySignatureData, IPartySignaturesRepo>(
                x => { x.SignedEntityId = obj.Id; x.ValidTo = null; }, d => new PartySignature(d));
        [TestMethod] public void IsProcessedTest() => isReadOnly(obj.Data.IsProcessed);
        [TestMethod] public void OrderIdTest() => isReadOnly(obj.Data.OrderId);
        [TestMethod] public void AuthorizedPartySignatureIdTest() => isReadOnly(obj.Data.AuthorizedPartySignatureId);
        [TestMethod] public async Task OrderTest()
            => await testItemAsync<OrderData, IOrder, IOrdersRepo>(
                obj.Data?.OrderId, () => obj.Order.Data, OrderFactory.Create);
        [TestMethod] public void DateAuthorizedTest() => isReadOnly(obj.Data.ValidFrom);
        [TestMethod] public async Task AuthorizationTest()
            => await testItemAsync<PartySignatureData, PartySignature, IPartySignaturesRepo>(
                getData(obj.Data?.AuthorizedPartySignatureId), () => obj.Authorization.Data, d => new PartySignature(d));
        private PartySignatureData getData(string id) {
            var d = random<PartySignatureData>();
            d.Id = id;
            d.ValidTo = null;
            return d;
        }
        [TestMethod] public async Task IsAuthorizedTest() {
            await AuthorizationTest();
            isReadOnly(obj.Authorization.IsSigned());
            isTrue(obj.IsAuthorized);
        }
        [TestMethod] public void IsNotAuthorizedTest() {
            obj = new testClass();
            var b = obj.IsAuthorized;
            areEqual(false, b);
        }
        [TestMethod] public void IsCorrectTest() {
            var isCorrect = obj.IsCorrect();
            var isProcessed = obj.IsProcessed;
            var isAuthorized = obj.IsAuthorized;
            areEqual(isCorrect, isAuthorized && !isProcessed);
        }
    }
}