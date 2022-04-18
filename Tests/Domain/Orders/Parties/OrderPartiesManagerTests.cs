using Abc.Aids.Random;
using Abc.Data.Orders;
using Abc.Data.Parties;
using Abc.Domain.Orders;
using Abc.Domain.Orders.Lines;
using Abc.Domain.Orders.Parties;
using Abc.Domain.Parties.Signatures;
using Abc.Domain.Roles;
using Abc.Facade.Orders;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Abc.Tests.Domain.Orders.Parties {
    [TestClass]
    public class OrderPartiesManagerTests :SealedTests<OrderPartiesManager, object> {
        private static MockOrderPartiesManager mockManager;
        private IOrder testOrder;
        private IOrder testOrderInLine;
        private OrderLine testLine;
        private PartySignature testSignature;
        private IPartyInOrder testParty;
        private IPartyInOrder testOldParty;
        private OrderLineReceiver testLineParty;
        private OrderLineReceiver testLineOldParty;
        private AmendPartySummaryEvent testAddNewEvent;
        private AmendPartySummaryEvent testRemoveEvent;
        private AmendPartySummaryEvent testReplaceEvent;
        private AmendPartySummaryEvent testAddNewLineEvent;
        private AmendPartySummaryEvent testRemoveLineEvent;
        private AmendPartySummaryEvent testReplaceLineEvent;
        private static readonly string[] amendAddNewMethods = new string[]{
                nameof(mockManager.IsCorrect),
                nameof(mockManager.IsOrderCorrect),
                nameof(mockManager.IsOrderLineCorrect),
                nameof(mockManager.IsSignatureCorrect),
                nameof(mockManager.IsEventCorrect),
                nameof(mockManager.IsNewEvent)};
        private static readonly string amendRemoveMethods = nameof(mockManager.IsRemoveEvent);
        private static readonly string[] replaceMethods = new string[] {
                nameof(mockManager.IsReplaceCorrect),
                nameof(mockManager.CanRemove),
                nameof(mockManager.Remove),
                nameof(mockManager.CanAdd),
                nameof(mockManager.Add),
                nameof(mockManager.Add)
        };
        private static readonly string[] addNewMethods = new string[] {
                nameof(mockManager.IsAddNewCorrect),
                nameof(mockManager.CanAdd),
                nameof(mockManager.Add),
                nameof(mockManager.Add)
        };
        private static readonly string[] removeMethods = new string[] {
                nameof(mockManager.IsRemoveCorrect),
                nameof(mockManager.CanRemove),
                nameof(mockManager.Remove),
                nameof(mockManager.Add)
        };

        protected override OrderPartiesManager createObject() => new(mockManager);
        [TestInitialize]
        public override void TestInitialize() {
            mockManager = new MockOrderPartiesManager();
            base.TestInitialize();
            var d = random<OrderData>();
            d.OrderType = random<bool>() ? OrderType.SalesOrder : OrderType.PurchaseOrder;
            testOrder = OrderFactory.Create(d);
            testSignature = new PartySignature(random<PartySignatureData>());
            testParty = PartyInOrderFactory.Create(randomPartyData());
            testOldParty = PartyInOrderFactory.Create(randomPartyData());
            testLineParty = PartyInOrderFactory.Create(randomPartyData(PartyRoleInOrder.OrderLineReceiver)) as OrderLineReceiver;
            testLineOldParty = PartyInOrderFactory.Create(randomPartyData(PartyRoleInOrder.OrderLineReceiver)) as OrderLineReceiver;

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
            testLine = OrderLineFactory.Create(dl) as OrderLine;

            testRemoveLineEvent = new AmendPartySummaryEvent(
               testLine, testSignature, null, testLineOldParty);
            testAddNewLineEvent = new AmendPartySummaryEvent(
                testLine, testSignature, testLineParty, null);
            testReplaceLineEvent = new AmendPartySummaryEvent(
                testLine, testSignature, testLineParty, testLineOldParty);
        }
        private static PartySummaryData randomPartyData(PartyRoleInOrder? r = null, string orderId = null, string lineId = null) {
            var d = random<PartySummaryData>();
            if (orderId is not null) d.OrderId = orderId;
            if (lineId is not null) d.OrderLineId = lineId;
            d.RoleInOrder = (r is null) ? (PartyRoleInOrder)GetRandom.UInt8(1, 6) : (PartyRoleInOrder)r;
            return d;
        }

        [TestMethod] public void LineReceiversTest()
            => mockManager.ValidateMethods(() => obj.LineReceivers, "get_" + nameof(mockManager.LineReceivers));
        [TestMethod] public void PurchaserTest()
             => mockManager.ValidateMethods(() => obj.Purchaser, "get_" + nameof(mockManager.Purchaser));
        [TestMethod] public void OrderInitiatorTest()
            => mockManager.ValidateMethods(() => obj.OrderInitiator, "get_" + nameof(mockManager.OrderInitiator));
        [TestMethod] public void PaymentReceiverTest()
            => mockManager.ValidateMethods(() => obj.PaymentReceiver, "get_" + nameof(mockManager.PaymentReceiver));
        [TestMethod] public void DeliveryReceiverTest()
            => mockManager.ValidateMethods(() => obj.DeliveryReceiver, "get_" + nameof(mockManager.DeliveryReceiver));
        [TestMethod] public void VendorTest()
            => mockManager.ValidateMethods(() => obj.Vendor, "get_" + nameof(mockManager.Vendor));
        [TestMethod] public void SalesAgentTest()
            => mockManager.ValidateMethods(() => obj.SalesAgent, "get_" + nameof(mockManager.SalesAgent));
        [TestMethod] public void LineReceiverTest()
            => mockManager.ValidateMethods(() => obj.LineReceiver(null), nameof(mockManager.LineReceiver));
        [TestMethod] public void LineReceiversIsEmptyTest() => areEqual(0, obj.LineReceivers.Count);
        [TestMethod] public void PurchaserIsUnspecifiedTest() => isTrue(obj.Purchaser.IsUnspecified);
        [TestMethod] public void OrderInitiatorIsUnspecifiedTest() => isTrue(obj.OrderInitiator.IsUnspecified);
        [TestMethod] public void PaymentReceiverIsUnspecifiedTest() => isTrue(obj.PaymentReceiver.IsUnspecified);
        [TestMethod] public void DeliveryReceiverIsUnspecifiedTest() => isTrue(obj.DeliveryReceiver.IsUnspecified);
        [TestMethod] public void VendorIsUnspecifiedTest() => isTrue(obj.Vendor.IsUnspecified);
        [TestMethod] public void SalesAgentIsUnspecifiedTest() => isTrue(obj.SalesAgent.IsUnspecified);
        public void LineReceiverIsUnspecifiedTest() => isTrue(obj.LineReceiver(null).IsUnspecified);
        [TestMethod] public void AddNewTest() => mockManager.ValidateMethods(() => obj.addNew(testAddNewEvent), addNewMethods);
        [TestMethod] public void RemoveTest() => mockManager.ValidateMethods(() => obj.remove(testRemoveEvent), removeMethods);
        [TestMethod] public void ReplaceTest() => mockManager.ValidateMethods(() => obj.replace(testReplaceLineEvent), replaceMethods);

        [DataRow(false, false)]
        [DataRow(true, false)]
        [DataRow(false, true)]
        [DataRow(true, true)]
        [DataTestMethod]
        public void AmendTest(bool isNew, bool isRemove) {
            mockManager.isAddNewEvent = isNew;
            mockManager.isRemoveEvent = isRemove;
            if (isNew && !isRemove) mockManager.ValidateMethods(
                () => obj.Amend(testAddNewEvent), addNew());
            if (!isNew && isRemove) mockManager.ValidateMethods(
                () => obj.Amend(testRemoveEvent), remove());
            if (!isNew && !isRemove) mockManager.ValidateMethods(
                () => obj.Amend(testReplaceEvent), replace());
            if (isNew && isRemove) mockManager.ValidateMethods(
                () => obj.Amend(testAddNewEvent), addNew());
        }

        private static string[] replace() {
            var l = new List<string>();
            l.AddRange(amendAddNewMethods);
            l.Add(amendRemoveMethods);
            l.AddRange(replaceMethods);
            return l.ToArray();
        }

        private static string[] remove() {
            var l = new List<string>();
            l.AddRange(amendAddNewMethods);
            l.Add(amendRemoveMethods);
            l.AddRange(removeMethods);
            return l.ToArray();
        }

        private static string[] addNew() {
            var l = new List<string>();
            l.AddRange(amendAddNewMethods);
            l.AddRange(addNewMethods);
            return l.ToArray();
        }

        [TestMethod] public void AmendAddNewRoleTest() {
            obj = new OrderPartiesManager(testOrder);
            var b = obj.Amend(testAddNewEvent);
            isTrue(b);
            validateParty(testParty.RoleInOrder);
        }

        private void validateParty(PartyRoleInOrder r) {
            if (r == PartyRoleInOrder.Vendor) validate(obj.Vendor);
            else if (r == PartyRoleInOrder.SalesAgent) validate(obj.SalesAgent);
            else if (r == PartyRoleInOrder.OrderInitiator) validate(obj.OrderInitiator);
            else if (r == PartyRoleInOrder.OrderReceiver) validate(obj.DeliveryReceiver);
            else if (r == PartyRoleInOrder.Purchaser) validate(obj.Purchaser);
            else if (r == PartyRoleInOrder.PaymentReceiver) validate(obj.PaymentReceiver);
            else fail(testParty.RoleInOrder.ToString());
        }

        private void validate(PartyInOrder p) {
            allPropertiesAreEqual(testParty.Data, p.Data, nameof(p.Data.OrderId)
                , nameof(p.Data.OrderLineId)
                , nameof(p.Data.ValidFrom)
                , nameof(p.Data.ValidTo));
            areEqual(testOrder.Id, p.Data.OrderId);
            areEqual((string)null, p.Data.OrderLineId);
            areEqual((DateTime?)null, p.Data.ValidTo);
            isTrue(p.Data.ValidFrom < DateTime.Now);
            isTrue(p.Data.ValidFrom > DateTime.Now.AddSeconds(-1));
        }

        [TestMethod]
        public void AmendRemoveRoleTest() {
            obj = new OrderPartiesManager(testOrder);
            isFalse(obj.Amend(testRemoveEvent));
            add<IPartySummariesRepo, IPartySummary>(testReplaceEvent.OldPartySummary);
            var b = obj.Amend(testRemoveEvent);
            isTrue(b);
            isUnspecified();
        }

        private void isUnspecified(IPartyInOrder p = null) {
            if (p is not Vendor) isTrue(obj.Vendor.IsUnspecified);
            if (p is not SalesAgent) isTrue(obj.SalesAgent.IsUnspecified);
            if (p is not OrderInitiator) isTrue(obj.OrderInitiator.IsUnspecified);
            if (p is not OrderReceiver) isTrue(obj.DeliveryReceiver.IsUnspecified);
            if (p is not Purchaser) isTrue(obj.Purchaser.IsUnspecified);
            if (p is not PaymentReceiver) isTrue(obj.PaymentReceiver.IsUnspecified);
        }

        [TestMethod] public void AmendReplaceRoleTest() {
            obj = new OrderPartiesManager(testOrder);
            isFalse(obj.Amend(testReplaceEvent));
            add<IPartySummariesRepo, IPartySummary>(testReplaceEvent.OldPartySummary);
            var b = obj.Amend(testReplaceEvent);
            isTrue(b);
            isUnspecified(testParty);
            validateParty(testParty.RoleInOrder);
        }
        [TestMethod] public void AmendAddNewLineReceiverTest() {
            obj = new OrderPartiesManager(testOrderInLine);
            isFalse(obj.Amend(testAddNewLineEvent));
            add<IOrderLinesRepo, IOrderLine>(testLine);
            var b = obj.Amend(testAddNewLineEvent);
            isTrue(b);
            isUnspecified();
            validateLineParty(obj.LineReceiver(testLine));
        }
        private void validateLineParty(OrderLineReceiver p) {
            allPropertiesAreEqual(testLineParty.Data, p.Data, nameof(p.Data.OrderId)
                , nameof(p.Data.OrderLineId)
                , nameof(p.Data.ValidFrom)
                , nameof(p.Data.ValidTo));
            areEqual(testOrderInLine.Id, p.Data.OrderId);
            areEqual(testLine.Id, p.Data.OrderLineId);
            areEqual((DateTime?)null, p.Data.ValidTo);
            isTrue(p.Data.ValidFrom < DateTime.Now);
            isTrue(p.Data.ValidFrom > DateTime.Now.AddSeconds(-1));
        }
        [TestMethod] public void AmendRemoveLineReceiverTest() {
            obj = new OrderPartiesManager(testOrderInLine);
            isFalse(obj.Amend(testRemoveLineEvent));
            add<IOrderLinesRepo, IOrderLine>(testLine);
            add<IPartySummariesRepo, IPartySummary>(testReplaceLineEvent.OldPartySummary);
            var b = obj.Amend(testRemoveLineEvent);
            isTrue(b);
            isUnspecified();
            isTrue(obj.LineReceiver(testLine).IsUnspecified);
        }
        [TestMethod] public void AmendReplaceLineReceiverTest() {
            obj = new OrderPartiesManager(testOrderInLine);
            isFalse(obj.Amend(testReplaceLineEvent));
            add<IOrderLinesRepo, IOrderLine>(testLine);
            add<IPartySummariesRepo, IPartySummary>(testReplaceLineEvent.OldPartySummary);
            var b = obj.Amend(testReplaceLineEvent);
            isTrue(b);
            isUnspecified();
            validateLineParty(obj.LineReceiver(testLine));
        }
    }
}