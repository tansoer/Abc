using Abc.Aids.Random;
using Abc.Data.Orders;
using Abc.Data.Parties;
using Abc.Domain.Orders.Lines;
using Abc.Domain.Parties.Signatures;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Orders.Lines {
    [TestClass] public class TaxableLineTests :AbstractTests<TaxableLine, BaseOrderLine> {
        private OrderLineData data;
        private TaxLine taxLine;
        private OrderEventData eventData;
        private PartySignature signature;
        private static string[] specificProperties
            => new[] {
                nameof(OrderLineData.OrderId), nameof(OrderLineData.OrderLineId),
                nameof(OrderLineData.OrderLineType)
            };
        private class testClass :TaxableLine {
            public testClass(OrderLineData d) :base(d) { }
        }
        protected override TaxableLine createObject() => new testClass(random<OrderLineData>());
        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            createOrderLineData();
            taxLine = new TaxLine(data);
            createOrderEventData();
            signature = getSignature(eventData.AuthorizedPartySignatureId);
        }
        private void createOrderEventData() {
            eventData = random<OrderEventData>();
            eventData.OrderEventType = OrderEventType.AmendOrderLineEvent;
            eventData.IsProcessed = false;
        }
        private void createOrderLineData() {
            data = random<OrderLineData>();
            data.OrderLineType =
                (data.OrderLineType == OrderLineType.TaxLine)
                    ? OrderLineType.Unspecified
                    : data.OrderLineType;
        }
        [TestMethod] public async Task RelatedLinesTest()
            => await testListAsync<IOrderLine, OrderLineData, IOrderLinesRepo>(
                x => {
                    x.OrderLineId = obj.Id;
                    x.OrderId = obj.OrderId;
                    x.OrderLineType = random<bool>() ? x.OrderLineType : OrderLineType.TaxLine;
                }, OrderLineFactory.Create, true);
        [TestMethod] public async Task TaxLinesTest() {
            await RelatedLinesTest();
            var lines = obj.relatedLines;
            var taxLines = obj.TaxLines;
            foreach (var l in lines) {
                if (l is not TaxLine x) continue;
                var y = taxLines.FirstOrDefault(z => z.Id == x.Id) ?? new TaxLine();
                allPropertiesAreEqual(x.Data, y.Data);
            }
        }
        [TestMethod] public async Task AddTest() {
            await RelatedLinesTest();
            addNewTest(x => obj.add(x));
        }
        [TestMethod] public async Task AddExistingTest() {
            await RelatedLinesTest();
            addExistingTest(x => obj.add(x));
        }
        [TestMethod] public async Task RemoveTest() {
            await RelatedLinesTest();
            removeExistingTest(x => obj.remove(x));
        }
        [TestMethod] public async Task RemoveNewTest() {
            await RelatedLinesTest();
            removeNewTest(x => obj.remove(x));
        }
        [TestMethod] public void AmendLineTest() { }
        [TestMethod] public async Task AmendAddNewLineTest() {
            await RelatedLinesTest();
            addNewTest(x => obj.AmendLine(new AmendOrderLineEvent(obj.OrderId, signature, x)));
        }
        [TestMethod] public async Task AmendAddExistingLineTest() {
            await RelatedLinesTest();
            addExistingTest(x => obj.AmendLine(new AmendOrderLineEvent(obj.OrderId, signature, x)));
        }
        [TestMethod] public async Task AmendRemoveNewLineTest() {
            await RelatedLinesTest();
            removeNewTest(x => obj.AmendLine(new AmendOrderLineEvent(obj.OrderId, signature, null, x)));
        }
        [TestMethod] public async Task AmendRemoveExistingLineTest() {
            await RelatedLinesTest();
            removeExistingTest(x => obj.AmendLine(new AmendOrderLineEvent(obj.OrderId, signature, null, x)));
        }
        [TestMethod] public async Task AmendReplaceNotExistingLineTest() {
            await RelatedLinesTest();
            var c = obj.TaxLines.Count;
            var d = random<OrderLineData>();
            d.OrderLineType = OrderLineType.TaxLine;
            isFalse(obj.AmendLine(
                new AmendOrderLineEvent(obj.OrderId, signature, taxLine, new TaxLine(d))));
            areEqual(c, obj.TaxLines.Count);
        }
        [TestMethod] public async Task AmendReplaceExistingLineTest() {
            await RelatedLinesTest();
            var c = obj.TaxLines.Count;
            var idx = GetRandom.Int32(0, c - 1);
            var i = obj.TaxLines[idx];
            var timeBefore = DateTime.Now;
            isTrue(obj.AmendLine(new AmendOrderLineEvent(obj.OrderId, signature, taxLine, i)));
            var timeAfter = DateTime.Now;
            areEqual(c+1, obj.TaxLines.Count);
            var y = obj.TaxLines.FirstOrDefault(z => z.Id == i.Id) ?? new TaxLine();
            allPropertiesAreEqual(y.Data, i.Data, nameof(OrderLineData.ValidTo));
            isTrue(y.ValidTo >= timeBefore);
            isTrue(y.ValidTo <= timeAfter);
            y = obj.TaxLines.FirstOrDefault(z => z.Id == taxLine.Id) ?? new TaxLine();
            allPropertiesAreEqual(y.Data, taxLine.Data, specificProperties);
        }
        [TestMethod] public void AmendIsProcessedLineTest() {
            eventData.IsProcessed = true;
            isFalse(obj.AmendLine(new AmendOrderLineEvent(eventData)));
        }
        [TestMethod] public void AmendNoSignatureLineTest() {
            eventData.AuthorizedPartySignatureId = random<string>();
            isFalse(obj.AmendLine(new AmendOrderLineEvent(eventData)));
        }
        [TestMethod] public void AmendNotACorrectTypeLineTest() {
            var d = random<OrderLineData>();
            var notTaxLine = random<bool>() ? d: data;
            while (notTaxLine.OrderLineType == OrderLineType.TaxLine)
                notTaxLine.OrderLineType = random<OrderLineType>();
            isFalse(obj.AmendLine(new AmendOrderLineEvent(obj.OrderId, signature, 
                OrderLineFactory.Create(data), OrderLineFactory.Create(d))));
        }
        private void removeExistingTest(Func<TaxLine, bool> remove) {
            var c = obj.TaxLines.Count;
            var idx = GetRandom.Int32(0, c - 1);
            var i = obj.TaxLines[idx];
            var timeBefore = DateTime.Now;
            isTrue(remove(i));
            var timeAfter = DateTime.Now;
            areEqual(c, obj.TaxLines.Count);
            var y = obj.TaxLines.FirstOrDefault(z => z.Id == i.Id) ?? new TaxLine();
            allPropertiesAreEqual(y.Data, i.Data, nameof(OrderLineData.ValidTo));
            isTrue(y.ValidTo >= timeBefore);
            isTrue(y.ValidTo <= timeAfter);
        }
        private void removeNewTest(Func<TaxLine, bool> remove) {
            var c = obj.TaxLines.Count;
            isFalse(remove(taxLine));
            areEqual(c, obj.TaxLines.Count);
        }
        private void addExistingTest(Func<TaxLine, bool> add) {
            var c = obj.TaxLines.Count;
            var idx = GetRandom.Int32(0, c - 1);
            var i = obj.TaxLines[idx];
            isFalse(add(i));
            areEqual(c, obj.TaxLines.Count);
            var y = obj.TaxLines.FirstOrDefault(z => z.Id == i.Id) ?? new TaxLine();
            allPropertiesAreEqual(y.Data, i.Data);
        }
        private void addNewTest(Func<TaxLine, bool> add) {
            var c = obj.TaxLines.Count;
            isTrue(add(taxLine));
            areEqual(c + 1, obj.TaxLines.Count);
            var y = obj.TaxLines.FirstOrDefault(z => z.Id == taxLine.Id) ?? new TaxLine();
            allPropertiesAreEqual(y.Data, taxLine.Data, specificProperties);
        }
        [TestMethod] public async Task IsTaxLineTest() {
            await RelatedLinesTest();
            isFalse(obj.isTaxLine(taxLine));
            var c = obj.TaxLines.Count;
            var idx = GetRandom.Int32(0, c - 1);
            isTrue(obj.isTaxLine(obj.TaxLines[idx]));
        }
        [TestMethod] public async Task NewTaxLineTest() {
            await RelatedLinesTest();
            areNotEqual(obj.OrderId, taxLine.OrderId);
            areNotEqual(obj.Id, taxLine.OrderLineId);
            areNotEqual(OrderLineType.TaxLine, taxLine.Data.OrderLineType);
            var l = obj.newTaxLine(taxLine);
            areEqual(obj.OrderId, l.OrderId);
            areEqual(obj.Id, l.OrderLineId);
            areEqual(OrderLineType.TaxLine, l.Data.OrderLineType);
        }
    }
}
