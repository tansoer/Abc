using Abc.Data.Orders;
using Abc.Data.Parties;
using Abc.Domain.Orders;
using Abc.Domain.Orders.Terms;
using Abc.Domain.Parties.Signatures;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Orders.Terms {
    [TestClass]
    public class TermsAndConditionsManagerTests: SealedTests<TermsAndConditionsManager, OrderManager> {
        private IOrder testOrder;
        private PartySignature testSignature;
        private TermsAndConditions testTerms;
        private TermsAndConditions testOldTerms;
        private AmendTermsAndConditionsEvent testAddNewEvent;
        private AmendTermsAndConditionsEvent testRemoveEvent;
        private AmendTermsAndConditionsEvent testReplaceEvent;

        protected override TermsAndConditionsManager createObject() 
            => new (testOrder);
        
        [TestInitialize] public override void TestInitialize() {
            var d = random<OrderData>();
            d.OrderType = random<bool>()? OrderType.SalesOrder: OrderType.PurchaseOrder;
            testOrder = OrderFactory.Create(d);
            base.TestInitialize();

            testSignature = new PartySignature(random<PartySignatureData>());
            testTerms = new TermsAndConditions(random<TermsAndConditionsData>());
            testOldTerms = new TermsAndConditions(random<TermsAndConditionsData>());

            testRemoveEvent = new AmendTermsAndConditionsEvent(
               testOrder, testSignature, null, testOldTerms);
            testAddNewEvent = new AmendTermsAndConditionsEvent(
                testOrder, testSignature, testTerms, null);
            testReplaceEvent = new AmendTermsAndConditionsEvent(
                testOrder, testSignature, testTerms, testOldTerms);
        }
        [TestMethod] public async Task TermsTest()
            => await testListAsync<TermsAndConditions, TermsAndConditionsData, ITermsAndConditionsRepo>(
                d => d.OrderId = testOrder.Id, d => new TermsAndConditions(d));
        [TestMethod] public async Task AmendTest() {
            await TermsTest();
            obj.Amend(null);
        }
        [TestMethod] public async Task DeleteTermsAndConditionsTest() {
            var e = testRemoveEvent;
            await TermsTest();
            var c = obj.Terms.Count;
            isFalse(obj.Amend(testRemoveEvent));

            addItem(testRemoveEvent.OldTermsAndConditions);
            isTrue(isListed(testRemoveEvent.oldTermsAndConditionsId));
            isTrue(obj.Amend(testRemoveEvent));

            areEqual(c, obj.Terms.Count);
            isFalse(isListed(testRemoveEvent.oldTermsAndConditionsId));
        }

        [TestMethod] public async Task AddTermsAndConditionsTest() {
            var e = testAddNewEvent;
            await TermsTest();
            var c = obj.Terms.Count;
            areEqual(c, obj.Terms.Count);
            isFalse(isListed(e.termsAndConditionsId));
            isTrue(obj.Amend(e));
            isFalse(obj.Amend(e));
            areEqual(c+1, obj.Terms.Count);
            isTrue(isListed(e.termsAndConditionsId));
        }
        [TestMethod] public async Task UpdateTermsAndConditionsTest() {
            var e = testReplaceEvent;
            await TermsTest();
            var c = obj.Terms.Count;
            var oldId = e.oldTermsAndConditionsId;
            var newId = e.termsAndConditionsId;

            isFalse(obj.Amend(e));
            isFalse(isListed(newId));
            isFalse(isListed(oldId));

            addItem(e.OldTermsAndConditions);
            isFalse(isListed(newId));
            isTrue(isListed(oldId));
            isTrue(obj.Amend(e));

            areEqual(c+1, obj.Terms.Count);
            isTrue(isListed(newId));
            isFalse(isListed(oldId));
        }
        [TestMethod] public void IsEventCorrectTest() { 
            isFalse(TermsAndConditionsManager.isEventCorrect(null));
            isTrue(TermsAndConditionsManager.isEventCorrect(testReplaceEvent));
            isTrue(TermsAndConditionsManager.isEventCorrect(testRemoveEvent));
            isTrue(TermsAndConditionsManager.isEventCorrect(testAddNewEvent));
            isFalse(TermsAndConditionsManager.isEventCorrect(
                new AmendTermsAndConditionsEvent(testOrder, testSignature)));
        }
        [TestMethod]
        public void IsOrderCorrectTest() {
            isFalse(obj.isOrderCorrect(null));
            isTrue(obj.isOrderCorrect(testReplaceEvent));
            isTrue(obj.isOrderCorrect(testRemoveEvent));
            isTrue(obj.isOrderCorrect(testAddNewEvent));
            isFalse(obj.isOrderCorrect(
                new AmendTermsAndConditionsEvent(null, testSignature, testTerms, testOldTerms)));
            isTrue(obj.isOrderCorrect(
                new AmendTermsAndConditionsEvent(obj.order, null, null, null)));
            isTrue(obj.isOrderCorrect(
                new AmendTermsAndConditionsEvent(testOrder, null, null, null)));
            isFalse(obj.isOrderCorrect(
                new AmendTermsAndConditionsEvent(
                    OrderFactory.Create(random<OrderData>()), null, null, null)));
        }
        [TestMethod]
        public void IsSignatureCorrectTest() {
            isFalse(OrderManager.isSignatureCorrect(null));
            isTrue(OrderManager.isSignatureCorrect(testReplaceEvent));
            isTrue(OrderManager.isSignatureCorrect(testRemoveEvent));
            isTrue(OrderManager.isSignatureCorrect(testAddNewEvent));
            isFalse(OrderManager.isSignatureCorrect(
                new AmendTermsAndConditionsEvent(testOrder, null, testTerms, testOldTerms)));
            isTrue(OrderManager.isSignatureCorrect(
                new AmendTermsAndConditionsEvent(
                    null, new PartySignature(random<PartySignatureData>()), null, null)));
        }

        [TestMethod]
        public async Task IsReplaceCorrectTest() {
            await TermsTest();
            areNotEqual(0, obj.Terms.Count);
            foreach (var t in obj.Terms) isTrue(obj.isReplaceCorrect(
                new AmendTermsAndConditionsEvent(testOrder, testSignature, testTerms, t)));
            isFalse(obj.isReplaceCorrect(testReplaceEvent));
            addItem(testReplaceEvent.OldTermsAndConditions);
            isTrue(obj.isReplaceCorrect(testReplaceEvent));
            addItem(testReplaceEvent.TermsAndConditions);
            isFalse(obj.isReplaceCorrect(testReplaceEvent));
            isFalse(obj.isReplaceCorrect(null));
        }
        [TestMethod] public async Task IsRemoveCorrectTest() {
            await TermsTest();
            areNotEqual(0, obj.Terms.Count);
            foreach (var t in obj.Terms) isTrue(obj.isRemoveCorrect(
                new AmendTermsAndConditionsEvent(testOrder, testSignature, null, t)));
            isFalse(obj.isRemoveCorrect(testRemoveEvent));
            addItem(testRemoveEvent.OldTermsAndConditions);
            isTrue(obj.isRemoveCorrect(testRemoveEvent));
            isFalse(obj.isRemoveCorrect(null));
        }
        [TestMethod]
        public async Task IsAddNewCorrectTest() {
            await TermsTest();
            areNotEqual(0, obj.Terms.Count);
            foreach (var t in obj.Terms) isFalse(obj.isAddNewCorrect(
                new AmendTermsAndConditionsEvent(testOrder, testSignature, t)));
            isTrue(obj.isAddNewCorrect(testAddNewEvent));
            addItem(testAddNewEvent.TermsAndConditions);
            isFalse(obj.isAddNewCorrect(testAddNewEvent));
            isFalse(obj.isAddNewCorrect(null));
        }
        [TestMethod]
        public async Task ExistsTest() {
            await TermsTest();
            areNotEqual(0, obj.Terms.Count);
            foreach( var t in obj.Terms) isTrue(obj.exists(t));
            var o = testReplaceEvent.OldTermsAndConditions;
            isFalse(o?.IsUnspecified?? true);
            isFalse(obj.exists(o));
            addItem(o);
            isTrue(obj.exists(o));
            isFalse(obj.exists(null));
        }
        private bool isListed(string id) => obj.Terms.FirstOrDefault(x => x.Id == id) != null;
        private static void addItem(TermsAndConditions t) => add<ITermsAndConditionsRepo, TermsAndConditions>(t);
    }
}
