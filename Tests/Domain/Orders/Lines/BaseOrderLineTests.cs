using Abc.Aids.Methods;
using Abc.Aids.Random;
using Abc.Data.Orders;
using Abc.Domain.Common;
using Abc.Domain.Orders;
using Abc.Domain.Orders.Lines;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Orders.Lines {
    [TestClass] public class BaseOrderLineTests: AbstractTests<BaseOrderLine, Entity<OrderLineData>> {
        private class testClass :BaseOrderLine {
            public testClass(OrderLineData d) : base(d) { }
        }
        protected override BaseOrderLine createObject() => new testClass(random<OrderLineData>());

        [TestMethod] public void SetRemoveTest() {
            var d = random<OrderLineData>();
            d.ValidTo = null;
            var dCopy = new OrderLineData(); 
            Copy.Members(d, dCopy);
            var o = new testClass(d);
            areEqual(Unspecified.ValidToDate, o.ValidTo);
            var from = DateTime.Now;
            o.SetRemove();
            var to = DateTime.Now;
            isTrue(o.ValidTo >= from);
            isTrue(o.ValidTo <= to);
            allPropertiesAreEqual(o.Data, dCopy, "ValidTo");
        }
        [TestMethod] public void OrderIdTest() => isReadOnly(obj.Data.OrderId);
        [TestMethod] public async Task OrderTest() => 
            await testItemAsync<OrderData, IOrder, IOrdersRepo>(
                obj.OrderId, ()=> obj.Order.Data, d => OrderFactory.Create(d));
        [TestMethod] public void AmountTest() => isReadOnly(obj.Data?.Amount, true);
        [TestMethod] public void CurrencyIdTest() => isReadOnly(obj.Data?.CurrencyId, true);
        [TestMethod] public void OrderLineIdTest() => isReadOnly(obj.Data?.OrderLineId);
        [TestMethod] public async Task RelatedLinesTest()
            => await testListAsync<IOrderLine, OrderLineData, IOrderLinesRepo>(
                x => {
                    x.OrderLineId = obj.Id;
                    x.OrderId = obj.OrderId;
                }, OrderLineFactory.Create, true);
        [TestMethod] public async Task IsListedTest() {
            await RelatedLinesTest();
            isFalse(BaseOrderLine.isListed(obj.relatedLines, random<string>()));
            var i = GetRandom.Int32(0, obj.relatedLines.Count - 1);
            var o = obj.relatedLines[i];
            isTrue(BaseOrderLine.isListed(obj.relatedLines, o.Id));
        }
        [TestMethod] public void NewLineTest() {
            var d = random<OrderLineData>();
            var x = OrderLineFactory.Create(d);
            var t = random<OrderLineType>();
            while (t == d.OrderLineType) t = random<OrderLineType>();
            var y = obj.newLine(x, t);
            areEqual(obj.OrderId, y.OrderId);
            areEqual(obj.Id, y.OrderLineId);
            areEqual(t, y.OrderLineType);
            allPropertiesAreEqual(d, y, 
                nameof(y.OrderId), 
                nameof(y.OrderLineId),
                nameof(y.Id),
                nameof(y.OrderLineType));
        }
        [TestMethod] public void IsTypeOfTest() {
            var t = obj.Data.OrderLineType;
            isTrue(obj.IsTypeOf(t));
            while (t == obj.Data.OrderLineType) t = random<OrderLineType>();
            isFalse(obj.IsTypeOf(t));
        }
    }
}
