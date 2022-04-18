using Abc.Data.Orders;
using Abc.Domain.Orders;
using Abc.Domain.Orders.Lines;
using Abc.Domain.Orders.Parties;
using Abc.Domain.Parties.Signatures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Abc.Data.Parties;
using System.Threading.Tasks;
using Abc.Domain.Roles;
using System.Collections.Generic;
using System.Linq;
using System;
using Abc.Aids.Random;
using Abc.Facade.Orders;
using Abc.Facade.Parties;

namespace Abc.Tests.Domain.Orders.Parties {

    internal class MockOrderPartiesManager :MockInterface, IInternalOrderPartiesManager {
        internal bool isAddNewEvent;
        internal bool isRemoveEvent;
        public IReadOnlyList<OrderLineReceiver> LineReceivers => registerMethod<IReadOnlyList<OrderLineReceiver>>();
        public PartyInOrder Purchaser => registerMethod<PartyInOrder>();
        public PartyInOrder OrderInitiator => registerMethod<PartyInOrder>();
        public PartyInOrder PaymentReceiver => registerMethod<PartyInOrder>();
        public PartyInOrder DeliveryReceiver => registerMethod<PartyInOrder>();
        public PartyInOrder Vendor => registerMethod<PartyInOrder>();
        public PartyInOrder SalesAgent => registerMethod<PartyInOrder>();
        public bool Add(IPartyInOrder p) => registerMethod(true, p);
        public bool Add(PartySignature s) => registerMethod(true, s);
        public bool CanAdd(IPartyInOrder p) => registerMethod(true, p);
        public bool CanRemove(IPartyInOrder p) => registerMethod(true, p);
        public bool IsAddNewCorrect(AmendPartySummaryEvent e) => registerMethod(true, e);
        public bool IsCorrect(AmendPartySummaryEvent e) => registerMethod(true, e);
        public bool IsEventCorrect(AmendPartySummaryEvent e) => registerMethod(true, e);
        public bool IsNewEvent(AmendPartySummaryEvent e) => registerMethod(isAddNewEvent, e);
        public bool IsOrderCorrect(AmendPartySummaryEvent e) => registerMethod(true, e);
        public bool IsOrderLineCorrect(AmendPartySummaryEvent e) => registerMethod(true, e);
        public bool IsRemoveCorrect(AmendPartySummaryEvent e) => registerMethod(true, e);
        public bool IsRemoveEvent(AmendPartySummaryEvent e) => registerMethod(isRemoveEvent, e);
        public bool IsReplaceCorrect(AmendPartySummaryEvent e) => registerMethod(true, e);
        public bool IsSignatureCorrect(AmendPartySummaryEvent e) => registerMethod(true, e);
        public PartyInOrder LineReceiver(OrderLine l) => registerMethod<PartyInOrder>(l);
        public bool Remove(IPartyInOrder p) => registerMethod(true, p);
    }

    [TestClass]
    public class orderPartiesManagerTests : TestsHost {
        private IOrder testOrder;
        private IOrder testOrderInLine;
        private IOrderLine testLine;
        private PartySignature testSignature;
        private IPartyInOrder testParty;
        private IPartyInOrder testOldParty;
        private AmendPartySummaryEvent testAddNewEvent;
        private AmendPartySummaryEvent testRemoveEvent;
        private AmendPartySummaryEvent testReplaceEvent;
        private AmendPartySummaryEvent testAddNewLineEvent;
        private AmendPartySummaryEvent testRemoveLineEvent;
        private AmendPartySummaryEvent testReplaceLineEvent;
        private orderPartiesManager obj {
            get => objUnderTests as orderPartiesManager; 
            set => objUnderTests = value;
        }

        internal orderPartiesManager createObject() => new(testOrder);
        [TestInitialize] public void TestInitialize() {
            var d = random<OrderData>();
            d.OrderType = random<bool>() ? OrderType.SalesOrder : OrderType.PurchaseOrder;
            testOrder = OrderFactory.Create(d);
            obj = createObject();
            type = typeof(orderPartiesManager);
            testSignature = new PartySignature(random<PartySignatureData>());
            testParty = PartyInOrderFactory.Create(randomPartyData());
            testOldParty = PartyInOrderFactory.Create(randomPartyData());

            testRemoveEvent = new AmendPartySummaryEvent(
               testOrder, testSignature, null, testOldParty);
            testAddNewEvent = new AmendPartySummaryEvent(
                testOrder, testSignature, testParty, null);
            testReplaceEvent = new AmendPartySummaryEvent(
                testOrder, testSignature, testParty, testOldParty);

            d = random<OrderData>();
            d.OrderType = random<bool>() ? OrderType.SalesOrder : OrderType.PurchaseOrder;
            testOrderInLine = OrderFactory.Create(d);
            add<IOrdersRepo, IOrder>(testOrderInLine);

            var dl = random<OrderLineData>();
            dl.OrderId = testOrderInLine.Id;
            dl.OrderLineType = OrderLineType.ProductLine;
            testLine = OrderLineFactory.Create(dl);

            testRemoveLineEvent = new AmendPartySummaryEvent(
               testLine, testSignature, null, testOldParty);
            testAddNewLineEvent = new AmendPartySummaryEvent(
                testLine, testSignature, testParty, null);
            testReplaceLineEvent = new AmendPartySummaryEvent(
                testLine, testSignature, testParty, testOldParty);
        }
        private static PartySummaryData randomPartyData(PartyRoleInOrder? r = null, string orderId = null, string lineId = null) {
            var d = random<PartySummaryData>();
            if (orderId is not null) d.OrderId = orderId;
            if (lineId is not null) d.OrderLineId = lineId;
            d.RoleInOrder = (r is null) ? (PartyRoleInOrder)GetRandom.UInt8(1, 7): (PartyRoleInOrder) r ;
            return d;
        }

        [TestMethod] public async Task PartySummariesTest() => 
            await testListAsync<IPartySummary, PartySummaryData, IPartySummariesRepo>(
                d => d.OrderId = testOrder.Id, d => PartyInOrderFactory.Create(d), true);
        [TestMethod] public async Task PartiesTest() { 
            await PartySummariesTest();
            var c = obj.partySummaries.Where(x => x is PartyInOrder).ToList().Count;
            areNotEqual(0,c);
            areEqual(c, obj.parties.Count);
            isInstanceOfType<IReadOnlyList<PartyInOrder>>(obj.parties);
        }
        [TestMethod] public async Task LineReceiversTest() {
            await PartiesTest();
            var count = GetRandom.UInt8(10, 20);
            for (var i = 0; i < count; i++) {
                var d = random<PartySummaryData>();
                d.RoleInOrder = PartyRoleInOrder.OrderLineReceiver;
                d.OrderId = testOrder.Id;
                add<IPartySummariesRepo, IPartySummary>(PartyInOrderFactory.Create(d));
            }
            var c = obj.partySummaries.Where(x => x is OrderLineReceiver).ToList().Count;
            areNotEqual(0, c);
            areEqual(c, obj.LineReceivers.Count);
            isInstanceOfType<IReadOnlyList<OrderLineReceiver>>(obj.LineReceivers);
        }
        [TestMethod] public async Task PurchaserTest()
            => await testRole<Purchaser>(PartyRoleInOrder.Purchaser, () => obj.Purchaser); 
        [TestMethod] public async Task OrderInitiatorTest() 
            => await testRole<OrderInitiator>(PartyRoleInOrder.OrderInitiator, () => obj.OrderInitiator);
        [TestMethod] public async Task PaymentReceiverTest() 
            => await testRole<PaymentReceiver>(PartyRoleInOrder.PaymentReceiver, () => obj.PaymentReceiver);
        [TestMethod] public async Task DeliveryReceiverTest() 
            => await testRole<OrderReceiver>(PartyRoleInOrder.OrderReceiver, () => obj.DeliveryReceiver);
        [TestMethod] public async Task VendorTest() 
            => await testRole<Vendor>(PartyRoleInOrder.Vendor, () => obj.Vendor);
        [TestMethod] public async Task SalesAgentTest() 
            => await testRole<SalesAgent>(PartyRoleInOrder.SalesAgent, () => obj. SalesAgent);
        [TestMethod] public async Task LineReceiverTest() {
            await LineReceiversTest();
            var d = random<PartySummaryData>();
            var orderline = random<OrderLineData>();
            orderline.OrderLineType = OrderLineType.ProductLine;
            d.RoleInOrder = PartyRoleInOrder.OrderLineReceiver;
            d.OrderId = testOrder.Id;
            d.OrderLineId = orderline.Id;
            add<IPartySummariesRepo, IPartySummary>(PartyInOrderFactory.Create(d));
            var o = obj.LineReceivers.FirstOrDefault(x => x.Data.OrderLineId == orderline.Id);
            allPropertiesAreEqual(o.Data, obj.LineReceiver(OrderLineFactory.Create(orderline) as OrderLine).Data);
        }
        private async Task testRole<T>(PartyRoleInOrder r, Func<PartyInOrder> getObj) where T : PartyInOrder {
            await PartySummariesTest();
            var p = obj.partySummaries.FirstOrDefault(x => x is T) as T;
            while (p is null) {
                var d = random<PartySummaryData>();
                d.RoleInOrder = r;
                d.OrderId = testOrder.Id;
                add<IPartySummariesRepo, IPartySummary>(PartyInOrderFactory.Create(d));
                p = obj.partySummaries.FirstOrDefault(x => x is T) as T;
            }
            allPropertiesAreEqual(p.Data, getObj().Data);
        }
        [DataRow(PartyRoleInOrder.OrderInitiator)]
        [DataRow(PartyRoleInOrder.OrderLineReceiver)]
        [DataRow(PartyRoleInOrder.OrderReceiver)]
        [DataRow(PartyRoleInOrder.PaymentReceiver)]
        [DataRow(PartyRoleInOrder.Purchaser)]
        [DataRow(PartyRoleInOrder.SalesAgent)]
        [DataRow(PartyRoleInOrder.Unspecified)]
        [DataRow(PartyRoleInOrder.Vendor)]
        [DataTestMethod] public void GetPartyRoleTest(PartyRoleInOrder r) {
            var d = random<PartySummaryData>();
            d.RoleInOrder = r;
            areEqual(r, orderPartiesManager.getPartyRole(PartyInOrderFactory.Create(d)));
        }
        [TestMethod] public void GetPartyRoleTest() 
            => areEqual(PartyRoleInOrder.Unspecified, 
                orderPartiesManager.getPartyRole((IPartyInOrder)null));
        [DataRow(PartyRoleInOrder.OrderInitiator)]
        [DataRow(PartyRoleInOrder.OrderLineReceiver)]
        [DataRow(PartyRoleInOrder.OrderReceiver)]
        [DataRow(PartyRoleInOrder.PaymentReceiver)]
        [DataRow(PartyRoleInOrder.Purchaser)]
        [DataRow(PartyRoleInOrder.SalesAgent)]
        [DataRow(PartyRoleInOrder.Vendor)]
        [DataTestMethod] public void GetPartyInRoleTest(PartyRoleInOrder r) {
            var d = random<PartySummaryData>();
            d.OrderId = testOrder.Id;
            d.OrderLineId = testLine.Id;
            d.RoleInOrder = r;
            isInstanceOfType<UnspecifiedRoleInOrder>(obj.getPartyInRole(r));
            addItem(PartyInOrderFactory.Create(d));
            var p = obj.getPartyInRole(r);
            allPropertiesAreEqual(d, p.Data);
        }
        [TestMethod] public async Task GetPartyInRoleTest() {
            await LineReceiversTest();
            isNull(obj.getPartyInRole(PartyRoleInOrder.Unspecified));
        }
        [DataRow(PartyRoleInOrder.OrderInitiator)]
        [DataRow(PartyRoleInOrder.OrderReceiver)]
        [DataRow(PartyRoleInOrder.PaymentReceiver)]
        [DataRow(PartyRoleInOrder.Purchaser)]
        [DataRow(PartyRoleInOrder.SalesAgent)]
        [DataRow(PartyRoleInOrder.Vendor)]
        [DataTestMethod] public void GetCurrentPartyInRoleTest(PartyRoleInOrder r) {
            var d = random<PartySummaryData>();
            d.OrderId = testOrder.Id;
            d.OrderLineId = testLine.Id;
            d.RoleInOrder = r;
            var x = random<PartySummaryData>();
            x.RoleInOrder = r;
            isInstanceOfType<UnspecifiedRoleInOrder>(obj.getCurrentPartyInRole(PartyInOrderFactory.Create(x)));
            addItem(PartyInOrderFactory.Create(d));
            var p = obj.getCurrentPartyInRole(PartyInOrderFactory.Create(x));
            allPropertiesAreEqual(d, p.Data);
        }
        [TestMethod] public void GetCurrentPartyInRoleIsNullTest() {
            isInstanceOfType<UnspecifiedRoleInOrder>(obj.getCurrentPartyInRole(null)); 
            var d = randomPartyData(PartyRoleInOrder.Unspecified, testOrderInLine.Id, testLine.Id);
            var x = randomPartyData(PartyRoleInOrder.Unspecified);
            isInstanceOfType<UnspecifiedRoleInOrder>(obj.getCurrentPartyInRole(PartyInOrderFactory.Create(x)));
            addItem(PartyInOrderFactory.Create(d));
            isInstanceOfType<UnspecifiedRoleInOrder>(obj.getCurrentPartyInRole(PartyInOrderFactory.Create(x)));
        }
        [TestMethod]
        public void GetCurrentPartyInLineReceiverRoleTest() {
            obj = new orderPartiesManager(testOrderInLine);
            var d = randomPartyData(PartyRoleInOrder.OrderLineReceiver, testOrderInLine.Id, testLine.Id);
            var x = randomPartyData(PartyRoleInOrder.OrderLineReceiver);
            isInstanceOfType<UnspecifiedRoleInOrder>(obj.getCurrentPartyInRole(PartyInOrderFactory.Create(x)));
            addItem(PartyInOrderFactory.Create(d));
            isInstanceOfType<UnspecifiedRoleInOrder>(obj.getCurrentPartyInRole(PartyInOrderFactory.Create(x)));
            x.OrderId = d.OrderId;
            x.OrderLineId = d.OrderLineId;
            var p = obj.getCurrentPartyInRole(PartyInOrderFactory.Create(x));
            isNotNull(p);
            isInstanceOfType<UnspecifiedRoleInOrder>(p);
            add<IOrdersRepo, IOrder>(testOrderInLine);
            add<IOrderLinesRepo, IOrderLine>(testLine);
            p = obj.getCurrentPartyInRole(PartyInOrderFactory.Create(x));
            allPropertiesAreEqual(d, p.Data);
        }
        [TestMethod] public void IsOrderCorrectTest() {
            isFalse(obj.isOrderCorrect(null));
            isTrue(obj.isOrderCorrect(testReplaceEvent));
            isTrue(obj.isOrderCorrect(testRemoveEvent));
            isTrue(obj.isOrderCorrect(testAddNewEvent));
            isFalse(obj.isOrderCorrect(
                new AmendPartySummaryEvent((IOrder)null, testSignature, testParty, testOldParty)));
            isTrue(obj.isOrderCorrect(
                new AmendPartySummaryEvent(obj.order, null, null, null)));
            isTrue(obj.isOrderCorrect(
                new AmendPartySummaryEvent(testOrder, null, null, null)));
            isFalse(obj.isOrderCorrect(
                new AmendPartySummaryEvent(
                    OrderFactory.Create(random<OrderData>()), null, null, null)));
        }
        [TestMethod] public void IsOrderGivenByLineCorrectTest() {
            obj = new orderPartiesManager(testOrderInLine);
            isTrue(obj.isOrderCorrect(testReplaceLineEvent));
            isTrue(obj.isOrderCorrect(testRemoveLineEvent));
            isTrue(obj.isOrderCorrect(testAddNewLineEvent));
            isTrue(obj.isOrderCorrect(
                new AmendPartySummaryEvent(testLine, null, null, null)));
            isFalse(obj.isOrderCorrect(
                new AmendPartySummaryEvent((IOrderLine)null, testSignature, testParty, testOldParty)));
        }
        [TestMethod] public void IsSignatureCorrectTest() {
            isFalse(OrderManager.isSignatureCorrect(null));
            isTrue(OrderManager.isSignatureCorrect(testReplaceEvent));
            isTrue(OrderManager.isSignatureCorrect(testRemoveEvent));
            isTrue(OrderManager.isSignatureCorrect(testAddNewEvent));
            isFalse(OrderManager.isSignatureCorrect(
                new AmendPartySummaryEvent(testOrder, null, testParty, testOldParty)));
            isTrue(OrderManager.isSignatureCorrect(
                new AmendPartySummaryEvent(
                    (IOrder)null, new PartySignature(random<PartySignatureData>()), null, null)));
        }
        [TestMethod] public void IsSignatureGivenByLineCorrectTest() {
            isFalse(OrderManager.isSignatureCorrect(null));
            isTrue(OrderManager.isSignatureCorrect(testReplaceLineEvent));
            isTrue(OrderManager.isSignatureCorrect(testRemoveLineEvent));
            isTrue(OrderManager.isSignatureCorrect(testAddNewLineEvent));
            isFalse(OrderManager.isSignatureCorrect(
                new AmendPartySummaryEvent(testOrder, null, testParty, testOldParty)));
            isTrue(OrderManager.isSignatureCorrect(
                new AmendPartySummaryEvent(
                    (IOrderLine)null, new PartySignature(random<PartySignatureData>()), null, null)));
        }
        [TestMethod] public void IsEventCorrectTest() {
            isFalse(obj.IsEventCorrect(null));
            isTrue(obj.IsEventCorrect(testReplaceEvent));
            isTrue(obj.IsEventCorrect(testRemoveEvent));
            isTrue(obj.IsEventCorrect(testAddNewEvent));
            isFalse(obj.IsEventCorrect(
                new AmendPartySummaryEvent(testOrder, testSignature)));
        }
        [TestMethod] public void IsEventGivenByLineCorrectTest() {
            isTrue(obj.IsEventCorrect(testReplaceLineEvent));
            isTrue(obj.IsEventCorrect(testRemoveLineEvent));
            isTrue(obj.IsEventCorrect(testAddNewLineEvent));
            isFalse(obj.IsEventCorrect(
                new AmendPartySummaryEvent(testLine, testSignature)));
        }
        [DataRow(PartyRoleInOrder.OrderInitiator)]
        [DataRow(PartyRoleInOrder.OrderLineReceiver)]
        [DataRow(PartyRoleInOrder.OrderReceiver)]
        [DataRow(PartyRoleInOrder.PaymentReceiver)]
        [DataRow(PartyRoleInOrder.Purchaser)]
        [DataRow(PartyRoleInOrder.SalesAgent)]
        [DataRow(PartyRoleInOrder.Vendor)]
        [DataRow(PartyRoleInOrder.Unspecified)]
        [DataTestMethod] public void IsSamePartyTest(PartyRoleInOrder r) {
            var x = random<PartySummaryData>();
            x.RoleInOrder = r;
            var y = random<PartySummaryData>();
            y.RoleInOrder = r;
            isFalse(orderPartiesManager.isSameParty(
                PartyInOrderFactory.Create(x) as PartyInOrder,
                PartyInOrderFactory.Create(y)));
            x.Id = y.Id;
            x.OrderLineId = y.OrderLineId;
            isTrue(orderPartiesManager.isSameParty(
                PartyInOrderFactory.Create(x) as PartyInOrder,
                PartyInOrderFactory.Create(y)));
        }
        [DataRow(PartyRoleInOrder.OrderInitiator)]
        [DataRow(PartyRoleInOrder.OrderLineReceiver)]
        [DataRow(PartyRoleInOrder.OrderReceiver)]
        [DataRow(PartyRoleInOrder.PaymentReceiver)]
        [DataRow(PartyRoleInOrder.Purchaser)]
        [DataRow(PartyRoleInOrder.SalesAgent)]
        [DataRow(PartyRoleInOrder.Vendor)]
        [DataRow(PartyRoleInOrder.Unspecified)]
        [DataTestMethod] public void IsSpecifiedTest(PartyRoleInOrder r) {
            var x = random<PartySummaryData>();
            x.RoleInOrder = r;
            isTrue(orderPartiesManager.isSpecified(PartyInOrderFactory.Create(x)));
        }
        [TestMethod] public void IsSpecifiedTest() { 
            isFalse(orderPartiesManager.isSpecified(PartyInOrderFactory.Create(null)));
            isFalse(orderPartiesManager.isSpecified((IPartyInOrder)null));
        }
        [TestMethod]
        public async Task ExistsTest() {
            await LineReceiversTest();
            areNotEqual(0, obj.parties.Count);
            foreach (var t in obj.parties) isTrue(obj.exists(t));
            isFalse(obj.exists(null));
            var o = testReplaceEvent.OldPartySummary;
            isFalse(o?.IsUnspecified ?? true);
            isFalse(obj.exists(o), o.ToString());
            addItem(o);
            isTrue(obj.exists(o), o.ToString());
        }
        private static void addItem(IPartyInOrder t) => add<IPartySummariesRepo, IPartySummary>(t);
        [TestMethod]
        public async Task IsReplaceCorrectTest() {
            var e = testReplaceEvent;
            await LineReceiversTest();
            areNotEqual(0, obj.parties.Count);
            foreach (var t in obj.parties) {
                if (t is OrderLineReceiver) continue;
                isTrue(obj.IsReplaceCorrect(
                new AmendPartySummaryEvent(testOrder, testSignature, testParty, t)));
            }
            isFalse(obj.IsReplaceCorrect(e));
            addItem(e.OldPartySummary);
            isTrue(obj.IsReplaceCorrect(e));
            addItem(e.PartySummary);
            isFalse(obj.IsReplaceCorrect(e));
            isFalse(obj.IsReplaceCorrect(null));
        }
        [TestMethod]
        public async Task IsReplaceLineCorrectTest() {
            testOrder = testOrderInLine;
            obj = new orderPartiesManager(testOrder);
            var e = testReplaceLineEvent;
            await LineReceiversTest();
            areNotEqual(0, obj.parties.Count);
            isFalse(obj.IsReplaceCorrect(e));
            addItem(e.OldPartySummary);
            isTrue(obj.IsReplaceCorrect(e));
            addItem(e.PartySummary);
            isFalse(obj.IsReplaceCorrect(e));
            isFalse(obj.IsReplaceCorrect(null));
        }
        [TestMethod]
        public async Task IsRemoveCorrectTest() {
            var e = testRemoveEvent;
            await LineReceiversTest();
            areNotEqual(0, obj.parties.Count);
            foreach (var t in obj.parties) {
                if (t is OrderLineReceiver) continue;
                isTrue(obj.IsRemoveCorrect(
                new AmendPartySummaryEvent(testOrder, testSignature, null, t)));
            }
            isFalse(obj.IsRemoveCorrect(e));
            addItem(e.OldPartySummary);
            isTrue(obj.IsRemoveCorrect(e));
            isFalse(obj.IsRemoveCorrect(null));
        }
        [TestMethod] public async Task IsRemoveLineCorrectTest() {
            testOrder = testOrderInLine;
            obj = new orderPartiesManager(testOrder);
            var e = testRemoveLineEvent;
            await LineReceiversTest();
            areNotEqual(0, obj.parties.Count);
            isFalse(obj.IsRemoveCorrect(e));
            addItem(e.OldPartySummary);
            isTrue(obj.IsRemoveCorrect(e));
            isFalse(obj.IsRemoveCorrect(null));
        }
        [TestMethod] public async Task IsAddNewCorrectTest() {
            var e = testAddNewEvent;
            await LineReceiversTest();
            areNotEqual(0, obj.parties.Count);
            foreach (var t in obj.parties) {
                if (t is OrderLineReceiver) continue;
                isFalse(obj.IsAddNewCorrect(
                    new AmendPartySummaryEvent(testOrder, testSignature, t))
                );
            }
            isTrue(obj.IsAddNewCorrect(e));
            addItem(e.PartySummary);
            isFalse(obj.IsAddNewCorrect(e));
            isFalse(obj.IsAddNewCorrect(null));
        }
        [TestMethod] public async Task IsAddNewLineCorrectTest() {
            testOrder = testOrderInLine;
            obj = new orderPartiesManager(testOrder);
            var e = testAddNewLineEvent;
            await LineReceiversTest();
            areNotEqual(0, obj.parties.Count);
            isTrue(obj.IsAddNewCorrect(e));
            addItem(e.PartySummary);
            isFalse(obj.IsAddNewCorrect(e));
            isFalse(obj.IsAddNewCorrect(null));
        }
        [TestMethod] public void IsOrderLineCorrectTest() {
            obj = new orderPartiesManager(testOrderInLine);
            isFalse(obj.IsOrderLineCorrect(testRemoveLineEvent));
            isFalse(obj.IsOrderLineCorrect(testAddNewLineEvent));
            isFalse(obj.IsOrderLineCorrect(testReplaceLineEvent));
            add<IOrderLinesRepo, IOrderLine>(testLine);
            isTrue(obj.IsOrderLineCorrect(testRemoveLineEvent));
            isTrue(obj.IsOrderLineCorrect(testAddNewLineEvent));
            isTrue(obj.IsOrderLineCorrect(testReplaceLineEvent));
        }
        [TestMethod]
        public void IsOrderLineCorrectNoLineEventTest() {
            isFalse(obj.IsOrderLineCorrect(null));
            isTrue(obj.IsOrderLineCorrect(testRemoveEvent));
            isTrue(obj.IsOrderLineCorrect(testAddNewEvent));
            isTrue(obj.IsOrderLineCorrect(testReplaceEvent));
        }
        [DataRow(PartyRoleInOrder.OrderInitiator)]
        [DataRow(PartyRoleInOrder.OrderReceiver)]
        [DataRow(PartyRoleInOrder.PaymentReceiver)]
        [DataRow(PartyRoleInOrder.Purchaser)]
        [DataRow(PartyRoleInOrder.SalesAgent)]
        [DataRow(PartyRoleInOrder.Vendor)]
        [DataTestMethod] public void CanAddTest(PartyRoleInOrder r) {
            var d = random<PartySummaryData>();
            d.RoleInOrder = r;
            d.OrderId = testOrder.Id;
            isTrue(obj.CanAdd(PartyInOrderFactory.Create(d)));
            add<IPartySummariesRepo, IPartySummary>(PartyInOrderFactory.Create(d));
            isFalse(obj.CanAdd(PartyInOrderFactory.Create(d)));
            d.Id = random<string>();
            isFalse(obj.CanAdd(PartyInOrderFactory.Create(d)));
        }
        [TestMethod] public void CanAddLineRoleTest() {
            obj = new orderPartiesManager(testOrderInLine);
            var d = randomPartyData(
                PartyRoleInOrder.OrderLineReceiver, testOrderInLine.Id, testLine.Id);
            isTrue(obj.CanAdd(PartyInOrderFactory.Create(d)));
            add<IPartySummariesRepo, IPartySummary>(PartyInOrderFactory.Create(d));
            add<IOrdersRepo, IOrder>(testOrderInLine);
            add<IOrderLinesRepo, IOrderLine>(testLine);
            isFalse(obj.CanAdd(PartyInOrderFactory.Create(d)));
            d.Id = random<string>();
            isFalse(obj.CanAdd(PartyInOrderFactory.Create(d)));
            var ld = testLine.Data;
            ld.Id = random<string>();
            d.OrderLineId = ld.Id;
            add<IOrderLinesRepo, IOrderLine>(OrderLineFactory.Create(ld));
            isTrue(obj.CanAdd(PartyInOrderFactory.Create(d)));
        }
        [TestMethod]
        public void CanAddTest() {
            isFalse(obj.CanAdd(null));
            var d = random<PartySummaryData>();
            d.RoleInOrder = PartyRoleInOrder.Unspecified;
            isFalse(obj.CanAdd(PartyInOrderFactory.Create(d)));
        }
        [DataRow(PartyRoleInOrder.OrderInitiator)]
        [DataRow(PartyRoleInOrder.OrderReceiver)]
        [DataRow(PartyRoleInOrder.PaymentReceiver)]
        [DataRow(PartyRoleInOrder.Purchaser)]
        [DataRow(PartyRoleInOrder.SalesAgent)]
        [DataRow(PartyRoleInOrder.Vendor)]
        [DataTestMethod]
        public void CanRemoveTest(PartyRoleInOrder r) {
            var d = random<PartySummaryData>();
            d.RoleInOrder = r;
            d.OrderId = testOrder.Id;
            isFalse(obj.CanRemove(PartyInOrderFactory.Create(d)));
            add<IPartySummariesRepo, IPartySummary>(PartyInOrderFactory.Create(d));
            isTrue(obj.CanRemove(PartyInOrderFactory.Create(d)));
            d.Id = random<string>();
            isFalse(obj.CanRemove(PartyInOrderFactory.Create(d)));
        }
        [TestMethod]
        public void CanRemoveLineRoleTest() {
            obj = new orderPartiesManager(testOrderInLine);
            var d = randomPartyData(
                PartyRoleInOrder.OrderLineReceiver, testOrderInLine.Id, testLine.Id);
            isFalse(obj.CanRemove(PartyInOrderFactory.Create(d)));
            add<IPartySummariesRepo, IPartySummary>(PartyInOrderFactory.Create(d));
            add<IOrdersRepo, IOrder>(testOrderInLine);
            add<IOrderLinesRepo, IOrderLine>(testLine);
            isTrue(obj.CanRemove(PartyInOrderFactory.Create(d)));
            d.Id = random<string>();
            isFalse(obj.CanRemove(PartyInOrderFactory.Create(d)));
            var ld = testLine.Data;
            ld.Id = random<string>();
            d.OrderLineId = ld.Id;
            add<IOrderLinesRepo, IOrderLine>(OrderLineFactory.Create(ld));
            isFalse(obj.CanRemove(PartyInOrderFactory.Create(d)));
        }
        [TestMethod]
        public void CanRemoveTest() {
            isFalse(obj.CanRemove(null));
            var d = random<PartySummaryData>();
            d.RoleInOrder = PartyRoleInOrder.Unspecified;
            isFalse(obj.CanRemove(PartyInOrderFactory.Create(d)));
        }
        [TestMethod] public async Task AddTest() {
            await LineReceiversTest();
            var d = random<PartySummaryData>();
            d.OrderId = obj.order.Id;
            d.RoleInOrder = (PartyRoleInOrder) GetRandom.UInt8(1,7);
            var count = obj.parties.Count;
            isTrue(obj.Add(PartyInOrderFactory.Create(d)));
            areEqual(count +1, obj.parties.Count);
            var p = obj.parties.FirstOrDefault(x => x.Id == d.Id);
            allPropertiesAreEqual(d, p.Data);
            isFalse(obj.Add(PartyInOrderFactory.Create(d)));
            areEqual(count + 1, obj.parties.Count());
        }
        [TestMethod] public async Task RemoveTest() {
            await AddTest();
            var count = obj.parties.Count;    
            var idx = GetRandom.Int32(0, count-1);
            isFalse(obj.Remove(
                PartyInOrderFactory.Create(random<PartySummaryData>())));
            var p = obj.parties[idx];
            isTrue(obj.Remove(p));
            areEqual(count - 1, obj.parties.Count);
        }

        [TestMethod] public void IsCorrectTest() {
            areEqual(testAddNewEvent.IsCorrect(), obj.IsCorrect(testAddNewEvent));
            areEqual(testReplaceEvent.IsCorrect(), obj.IsCorrect(testReplaceEvent));
            areEqual(testRemoveEvent.IsCorrect(), obj.IsCorrect(testRemoveEvent));
            areEqual(testAddNewLineEvent.IsCorrect(), obj.IsCorrect(testAddNewLineEvent));
            areEqual(testReplaceLineEvent.IsCorrect(), obj.IsCorrect(testReplaceLineEvent));
            areEqual(testRemoveLineEvent.IsCorrect(), obj.IsCorrect(testRemoveLineEvent));
        }
        [TestMethod]
        public void IsNewEventTest() {
            areEqual(testAddNewEvent.IsNewEvent, obj.IsNewEvent(testAddNewEvent));
            areEqual(testReplaceEvent.IsNewEvent, obj.IsNewEvent(testReplaceEvent));
            areEqual(testRemoveEvent.IsNewEvent, obj.IsNewEvent(testRemoveEvent));
            areEqual(testAddNewLineEvent.IsNewEvent, obj.IsNewEvent(testAddNewLineEvent));
            areEqual(testReplaceLineEvent.IsNewEvent, obj.IsNewEvent(testReplaceLineEvent));
            areEqual(testRemoveLineEvent.IsNewEvent, obj.IsNewEvent(testRemoveLineEvent));
        }
        [TestMethod] public void IsRemoveEventTest() {
            areEqual(testAddNewEvent.IsRemoveEvent, obj.IsRemoveEvent(testAddNewEvent));
            areEqual(testReplaceEvent.IsRemoveEvent, obj.IsRemoveEvent(testReplaceEvent));
            areEqual(testRemoveEvent.IsRemoveEvent, obj.IsRemoveEvent(testRemoveEvent));
            areEqual(testAddNewLineEvent.IsRemoveEvent, obj.IsRemoveEvent(testAddNewLineEvent));
            areEqual(testReplaceLineEvent.IsRemoveEvent, obj.IsRemoveEvent(testReplaceLineEvent));
            areEqual(testRemoveLineEvent.IsRemoveEvent, obj.IsRemoveEvent(testRemoveLineEvent));
        }
    }
}