using Abc.Data.Orders;
using Abc.Data.Parties;
using Abc.Domain.Orders;
using Abc.Domain.Orders.Terms;
using Abc.Domain.Parties.Signatures;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Orders.Terms {

    [TestClass]
    public class AmendTermsAndConditionsEventTests :AmendOrderEventTests<AmendTermsAndConditionsEvent, AmendEvent> {
        private IOrder testOrder;
        private PartySignature testSignature;
        private TermsAndConditions testTerms;
        private TermsAndConditions testOldTerms;
        private AmendTermsAndConditionsEvent testEvent;

        protected override AmendTermsAndConditionsEvent createObject() {
            var d = random<OrderEventData>();
            d.OrderEventType = OrderEventType.AmendTermsAndConditionsEvent;
            return OrderEventFactory.Create(d) as AmendTermsAndConditionsEvent;
        }

        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            testOrder = OrderFactory.Create(random<OrderData>());
            testSignature = new PartySignature(random<PartySignatureData>());
            testTerms = new TermsAndConditions(random<TermsAndConditionsData>());
            testOldTerms = new TermsAndConditions(random<TermsAndConditionsData>());
            testEvent = new AmendTermsAndConditionsEvent(
                testOrder, testSignature, testTerms, testOldTerms );        
        }
        [TestMethod] public void OldTermsAndConditionsIdTest() => isReadOnly(obj.Data.OldTermsAndConditionsId, true);
        [TestMethod] public void TermsAndConditionsIdTest() => isReadOnly(obj.Data.TermsAndConditionsId, true);
        [TestMethod] public async Task OldTermsAndConditionsTest() =>
            await testItemAsync<TermsAndConditionsData, TermsAndConditions, ITermsAndConditionsRepo>(
                obj.oldTermsAndConditionsId, () => obj.OldTermsAndConditions.Data, d => new TermsAndConditions(d));
        [TestMethod] public async Task TermsAndConditionsTest() =>
            await testItemAsync<TermsAndConditionsData, TermsAndConditions, ITermsAndConditionsRepo>(
                obj.termsAndConditionsId, () => obj.TermsAndConditions.Data, d => new TermsAndConditions(d));
        [TestMethod] public async Task IsNewEventTest() {
            isFalse(obj.IsNewEvent);
            await TermsAndConditionsTest();
            isTrue(obj.IsNewEvent);
            await OldTermsAndConditionsTest();
            isFalse(obj.IsNewEvent);
        }
        [TestMethod] public async Task IsRemoveEventTest() {
            isFalse(obj.IsRemoveEvent);
            await OldTermsAndConditionsTest();
            isTrue(obj.IsRemoveEvent);
            await TermsAndConditionsTest();
            isFalse(obj.IsRemoveEvent);
        }
        [TestMethod]
        public async Task IsCorrectTest() {
            isFalse(obj.IsCorrect());
            await isCorrectTest(true, true, true);
            await isCorrectTest(false, false, false);
            await isCorrectTest(true, false, false);
            await isCorrectTest(false, true, false);
        }

        [TestMethod]
        public void CanCreateWithArgumentsTest() {
            obj = testEvent;
            isFalse(obj.Authorization.IsUnspecified);
            isFalse(obj.Order.IsUnspecified);
            isFalse(obj.OldTermsAndConditions.IsUnspecified);
            isFalse(obj.TermsAndConditions.IsUnspecified);
            validateSignature(obj, obj.Authorization);
            validateTerm(obj, obj.OldTermsAndConditions, testOldTerms);
            validateTerm(obj, obj.TermsAndConditions, testTerms);
            validateData(obj, obj.Data);
            areSame(testOrder, obj.Order);
            obj.signature = null;
            obj.termsAndConditions = null;
            obj.oldTermsAndConditions = null;
            obj.order = null;
            isTrue(obj.Authorization.IsUnspecified);
            isTrue(obj.OldTermsAndConditions.IsUnspecified);
            isTrue(obj.TermsAndConditions.IsUnspecified);
            isTrue(obj.Order.IsUnspecified);
        }

        [TestMethod] public void ValidateSignatureTest() {
            isNull(obj.signature);
            var s = obj.validateSignature(testSignature);
            isNull(obj.signature);
            validateSignature(obj, s);
        }
        private void validateSignature(AmendTermsAndConditionsEvent e, PartySignature s) {
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
        [TestMethod] public void ValidateOrderTest() { 
            areSame(testOrder, OrderEvent.validateOrder(testOrder));
            isTrue(OrderEvent.validateOrder(null).IsUnspecified);
        }

        [TestMethod] public void ValidateTermTest() {
            obj.order = testOrder;
            isNull(obj.termsAndConditions);
            isNull(obj.oldTermsAndConditions);
            var t = obj.validateTerm(testTerms);
            isNull(obj.termsAndConditions);
            isNull(obj.oldTermsAndConditions);
            validateTerm(obj, t, testTerms);
        }

        private void validateTerm(AmendTermsAndConditionsEvent o, TermsAndConditions t, TermsAndConditions test) {
            areEqual(o.Order.Id, t.orderId);
            isTrue(t.ValidFrom < DateTime.Now);
            isTrue(t.ValidFrom > DateTime.Now.AddSeconds(-1));
            areEqual(DateTime.MaxValue, t.ValidTo);
            allPropertiesAreEqual(t.Data, test.Data, 
                nameof(t.Data.OrderId), nameof(t.Data.ValidFrom), nameof(t.Data.ValidTo));
        }

        [TestMethod] public void UpdateDataTest() {
            obj.termsAndConditions = testTerms;
            obj.oldTermsAndConditions = testOldTerms;
            obj.order = testOrder;
            obj.signature = testSignature;
            var d = new OrderEventData();
            obj.setData(d);
            obj.updateData();
            validateData(obj, d);
        }
        [TestMethod] public void SetDataTest() {
            obj.termsAndConditions = testTerms;
            obj.oldTermsAndConditions = testOldTerms;
            obj.order = testOrder;
            obj.signature = testSignature;
            var d = new OrderEventData(); 
            obj.setData(d);
            validateData(obj, d);
        }
        private static void validateData(AmendTermsAndConditionsEvent o, OrderEventData d) { 
            areEqual(false, d.IsProcessed);
            areEqual(OrderEventType.AmendTermsAndConditionsEvent, d.OrderEventType);
            areEqual(o.TermsAndConditions.Id, d.TermsAndConditionsId);
            areEqual(o.OldTermsAndConditions.Id, d.OldTermsAndConditionsId);
            areEqual(o.Authorization.Id, d.AuthorizedPartySignatureId);
            areEqual(o.Order.Id, d.OrderId);
            isTrue(d.ValidFrom < DateTime.Now);
            isTrue(d.ValidFrom > DateTime.Now.AddSeconds(-1));
        }
        private async Task isCorrectTest(bool isSigned, bool isNotProcessed, bool expected) {
            var d = random<OrderEventData>();
            d.OrderEventType = OrderEventType.AmendTermsAndConditionsEvent;
            await setIsSigned(d, isSigned);
            setIsNotProcessed(d, isNotProcessed);
            obj = OrderEventFactory.Create(d) as AmendTermsAndConditionsEvent;
            Assert.IsNotNull(obj);
            areEqual(expected, obj.IsCorrect());
        }
    }
}