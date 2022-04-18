using Abc.Aids.Random;
using Abc.Data.Orders;
using Abc.Data.Parties;
using Abc.Domain.Orders;
using Abc.Domain.Orders.Lines;
using Abc.Domain.Orders.Parties;
using Abc.Domain.Orders.Terms;
using Abc.Domain.Parties.Signatures;
using Abc.Domain.Roles;
using Abc.Facade.Orders;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Orders.Parties {

    [TestClass]
    public class AmendPartySummaryEventTests :AmendOrderEventTests<AmendPartySummaryEvent, AmendEvent> {
        private IOrder testOrder;
        private BaseOrderLine testLine;
        private PartySignature testSignature;
        private PartyInOrder testParty;
        private PartyInOrder testOldParty;
        private AmendPartySummaryEvent testEvent;
        private AmendPartySummaryEvent testLineEvent;

        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            testOrder = OrderFactory.Create(random<OrderData>());
            var d = random<OrderLineData>();
            d.OrderId = testOrder.Id;
            testLine = OrderLineFactory.Create(d) as BaseOrderLine;
            testLine.order = testOrder;
            testSignature = new PartySignature(random<PartySignatureData>());
            testParty = PartyInOrderFactory.Create(random<PartySummaryData>()) as PartyInOrder;
            testOldParty = PartyInOrderFactory.Create(random<PartySummaryData>()) as PartyInOrder;
            testEvent = new AmendPartySummaryEvent(
                testOrder, testSignature, testParty, testOldParty);
            testLineEvent = new AmendPartySummaryEvent(
                testLine, testSignature, testParty, testOldParty);
        }
        protected override AmendPartySummaryEvent createObject() {
            var d = random<OrderEventData>();
            d.OrderEventType = OrderEventType.AmendPartySummaryEvent;
            return OrderEventFactory.Create(d) as AmendPartySummaryEvent;
        }
        [TestMethod] public void PartySummaryIdTest() => isReadOnly(obj.Data.PartySummaryId, true);
        [TestMethod] public void OldPartySummaryIdTest() => isReadOnly(obj.Data.OldPartySummaryId, true);
        [TestMethod] public async Task OldPartySummaryTest() =>
            await testItemAsync<PartySummaryData, IPartySummary, IPartySummariesRepo>(
                getPartySummary(obj.oldPartySummaryId), () => obj.OldPartySummary.Data, PartySummaryFactory.Create);
        [TestMethod] public async Task PartySummaryTest()
            => await testItemAsync<PartySummaryData, IPartySummary, IPartySummariesRepo>(
                getPartySummary(obj.partySummaryId), () => obj.PartySummary.Data, PartySummaryFactory.Create);
        private static PartySummaryData getPartySummary(string id) {
            var d = random<PartySummaryData>();
            while (!d.IsPartyRoleInOrder)
                d.RoleInOrder = GetRandom.EnumOf<PartyRoleInOrder>();
            d.Id = id;
            return d;
        }
        [TestMethod] public async Task IsNewEventTest() {
            isFalse(obj.IsNewEvent);
            await PartySummaryTest();
            isTrue(obj.IsNewEvent);
            await OldPartySummaryTest();
            isFalse(obj.IsNewEvent);
        }
        [TestMethod] public async Task IsRemoveEventTest() {
            isFalse(obj.IsRemoveEvent);
            await OldPartySummaryTest();
            isTrue(obj.IsRemoveEvent);
            await PartySummaryTest();
            isFalse(obj.IsRemoveEvent);
        }
        [TestMethod] public void OrderLineIdTest()  => isReadOnly(obj.Data.OrderLineId, true);
        [TestMethod] public async Task OrderLineTest() => 
            await testItemAsync<OrderLineData, IOrderLine, IOrderLinesRepo>(
                obj.orderLineId, () => obj.OrderLine.Data, d => OrderLineFactory.Create(d));
        [TestMethod] public async Task IsOrderLineAmendTest() {
            isFalse(obj.IsOrderLineAmend());
            await OrderLineTest();
            isTrue(obj.IsOrderLineAmend());
        }
        [TestMethod] public async Task IsRoleTypeOfTest() {
            var t = random<PartyRoleInOrder>();
            while (t == obj.PartySummary.Data.RoleInOrder) t = random<PartyRoleInOrder>();
            isFalse(obj.IsRoleTypeOf(t));
            await isTypeTest(() => obj.PartySummary, PartySummaryTest);
            await isTypeTest(() => obj.OldPartySummary, OldPartySummaryTest);
            await isBothOrderLineTypeTest();
        }
        private async Task isTypeTest(Func<IPartyInOrder> l, Func<Task> f) {
            obj = createObject();
            await f();
            var t = l().Data.RoleInOrder;
            var otherT = random<PartyRoleInOrder>();
            while (t == otherT) otherT = random<PartyRoleInOrder>();
            isTrue(obj.IsRoleTypeOf(t));
            isFalse(obj.IsRoleTypeOf(otherT));
        }
        private async Task isBothOrderLineTypeTest() {
            obj = createObject();
            var t = randomType(PartyRoleInOrder.Unspecified);
            var otherT = randomType(t);
            await addItem(t, obj.oldPartySummaryId);
            await addItem(t, obj.partySummaryId);
            isTrue(obj.IsRoleTypeOf(t));
            isFalse(obj.IsRoleTypeOf(otherT));
        }
        private static PartyRoleInOrder randomType(PartyRoleInOrder t) {
            var x = random<PartyRoleInOrder>();
            while (x == t) x = random<PartyRoleInOrder>();
            return x;
        }
        private static async Task addItem(PartyRoleInOrder t, string id) {
            var d = random<PartySummaryData>();
            d.RoleInOrder = t;
            d.Id = id;
            await addAsync<IPartySummariesRepo, IPartySummary>(PartyInOrderFactory.Create(d));
        }
        [TestMethod]
        public async Task IsCorrectTest() {
            isFalse(obj.IsCorrect(random<PartyRoleInOrder>()));
            await isCorrectTest(true, true, true, true);
            await isCorrectTest(false, false, false, false);
            await isCorrectTest(true, false, false, false);
            await isCorrectTest(false, true, false, false);
            await isCorrectTest(false, false, true, false);
            await isCorrectTest(true, true, false, false);
            await isCorrectTest(false, true, true, false);
            await isCorrectTest(true, false, true, false);
        }
        private async Task isCorrectTest(bool isSigned, bool isNotProcessed, bool isTypeOf, bool expected) {
            var d = random<OrderEventData>();
            d.OrderEventType = OrderEventType.AmendPartySummaryEvent;
            await setIsSigned(d, isSigned);
            setIsNotProcessed(d, isNotProcessed);
            var t = randomType(PartyRoleInOrder.Unspecified);
            await setIsCorrectTypeOf(d, isTypeOf, t);
            obj = OrderEventFactory.Create(d) as AmendPartySummaryEvent;
            Assert.IsNotNull(obj);
            areEqual(expected, obj.IsCorrect(t));
        }
        private static async Task setIsCorrectTypeOf(OrderEventData d, bool isTypeOf, PartyRoleInOrder t) {
            var neededType = isTypeOf ? t : randomType(t);
            if (random<bool>()) await addItem(neededType, d.OldPartySummaryId);
            else if (random<bool>()) await addItem(neededType, d.PartySummaryId);
            else {
                await addItem(neededType, d.OldPartySummaryId);
                await addItem(neededType, d.PartySummaryId);
            }
        }

        [TestMethod]
        public void CanCreateWithArgumentsTest() {
            obj = testEvent;
            isFalse(obj.Authorization.IsUnspecified);
            isFalse(obj.Order.IsUnspecified);
            isFalse(obj.OldPartySummary.IsUnspecified);
            isFalse(obj.PartySummary.IsUnspecified);
            isTrue(obj.OrderLine.IsUnspecified);
            validateSignature(obj, obj.Authorization);
            validateParty(obj, obj.OldPartySummary as PartyInOrder, testOldParty);
            validateParty(obj, obj.PartySummary as PartyInOrder, testParty);
            validateData(obj, obj.Data);
            areSame(testOrder, obj.Order);
            obj.signature = null;
            obj.partySummary = null;
            obj.oldPartySummary = null;
            obj.order = null;
            isTrue(obj.Authorization.IsUnspecified);
            isTrue(obj.OldPartySummary.IsUnspecified);
            isTrue(obj.PartySummary.IsUnspecified);
            isTrue(obj.Order.IsUnspecified);
            isTrue(obj.OrderLine.IsUnspecified);
        }
        [TestMethod]
        public void CanCreateWithOrderLineArgumentsTest() {
            obj = testLineEvent;
            isFalse(obj.Authorization.IsUnspecified);
            isFalse(obj.Order.IsUnspecified);
            isFalse(obj.OrderLine.IsUnspecified);
            isFalse(obj.OldPartySummary.IsUnspecified);
            isFalse(obj.PartySummary.IsUnspecified);
            validateSignature(obj, obj.Authorization);
            validateParty(obj, obj.OldPartySummary as PartyInOrder, testOldParty);
            validateParty(obj, obj.PartySummary as PartyInOrder, testParty);
            validateData(obj, obj.Data);
            areSame(testOrder, obj.Order);
            areSame(testLine, obj.OrderLine);
            obj.signature = null;
            obj.partySummary = null;
            obj.oldPartySummary = null;
            obj.order = null;
            obj.orderLine = null;
            isTrue(obj.Authorization.IsUnspecified);
            isTrue(obj.OldPartySummary.IsUnspecified);
            isTrue(obj.PartySummary.IsUnspecified);
            isTrue(obj.Order.IsUnspecified);
            isTrue(obj.OrderLine.IsUnspecified);
        }

        [TestMethod]
        public void ValidateSignatureTest() {
            isNull(obj.signature);
            var s = obj.validateSignature(testSignature);
            isNull(obj.signature);
            validateSignature(obj, s);
        }
        private void validateSignature(AmendPartySummaryEvent e, PartySignature s) {
            areEqual(e.Id, s.SignedEntityId);
            areEqual(typeof(ITermsAndConditionsRepo).FullName, s.SignedEntityTypeId);
            areEqual(DateTime.MaxValue, s.ValidTo);
            isTrue(s.ValidFrom < DateTime.Now);
            isTrue(s.ValidFrom > DateTime.Now.AddSeconds(-1));
            allPropertiesAreEqual(s.Data, testSignature.Data,
                nameof(s.SignedEntityId),
                nameof(s.SignedEntityTypeId),
                nameof(s.ValidTo),
                nameof(s.ValidFrom));
        }
        [TestMethod]
        public void ValidateOrderTest() {
            areSame(testOrder, OrderEvent.validateOrder(testOrder));
            isTrue(OrderEvent.validateOrder(null).IsUnspecified);
        }

        [TestMethod]
        public void ValidatePartyTest() {
            obj.order = testOrder;
            isNull(obj.partySummary);
            isNull(obj.oldPartySummary);
            var t = obj.validatePartySummary(testParty) as PartyInOrder;
            isNull(obj.partySummary);
            isNull(obj.oldPartySummary);
            validateParty(obj, t, testParty);
        }

        private void validateParty(AmendPartySummaryEvent o, PartyInOrder p, PartyInOrder test) {
            areEqual(o.Order.Id, p.orderId);
            isTrue(p.ValidFrom < DateTime.Now);
            isTrue(p.ValidFrom > DateTime.Now.AddSeconds(-1));
            areEqual(DateTime.MaxValue, p.ValidTo);
            allPropertiesAreEqual(p.Data, test.Data,
                nameof(p.Data.OrderId), nameof(p.Data.OrderLineId), 
                nameof(p.Data.ValidFrom), nameof(p.Data.ValidTo));
        }

        [TestMethod]
        public void UpdateDataTest() {
            obj.partySummary = testParty;
            obj.oldPartySummary = testOldParty;
            obj.order = testOrder;
            obj.signature = testSignature;
            var d = new OrderEventData();
            obj.setData(d);
            obj.updateData();
            validateData(obj, d);
        }
        [TestMethod]
        public void SetDataTest() {
            obj.partySummary = testParty;
            obj.oldPartySummary = testOldParty;
            obj.order = testOrder;
            obj.signature = testSignature;
            var d = new OrderEventData();
            obj.setData(d);
            validateData(obj, d);
        }
        private static void validateData(AmendPartySummaryEvent o, OrderEventData d) {
            areEqual(false, d.IsProcessed);
            areEqual(OrderEventType.AmendPartySummaryEvent, d.OrderEventType);
            areEqual(o.PartySummary.Id, d.PartySummaryId);
            areEqual(o.OldPartySummary.Id, d.OldPartySummaryId);
            areEqual(o.Authorization.Id, d.AuthorizedPartySignatureId);
            areEqual(o.Order.Id, d.OrderId);
            isTrue(d.ValidFrom < DateTime.Now);
            isTrue(d.ValidFrom > DateTime.Now.AddSeconds(-1));
        }

    }
}