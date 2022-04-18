using Abc.Data.Currencies;
using Abc.Data.Orders;
using Abc.Domain.Currencies;
using Abc.Domain.Orders.Lines;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Orders.Lines {
    [TestClass]
    public class ChargeLineTests :SealedTests<ChargeLine, TaxableLine> {
        protected override ChargeLine createObject() {
            var d = random<OrderLineData>();
            d.OrderLineType = OrderLineType.ChargeLine;
            return OrderLineFactory.Create(d) as ChargeLine;
        }
        [TestMethod] public async Task AmountTest() {
            isReadOnly();
            var d = random<CurrencyData>();
            d.Id = obj.currencyId;
            await addAsync<ICurrencyRepo, Currency>(new Currency(d));
            var p = obj.Amount;
            areEqual(obj.amount, p.Amount);
            areEqual(obj.currencyId, p.Currency.Id);
        }
        [TestMethod]
        public async Task ProductLineTest() {
            var d = random<OrderLineData>();
            d.Id = obj.OrderLineId;
            d.OrderLineType = OrderLineType.ProductLine;
            await testItemAsync<OrderLineData, IOrderLine, IOrderLinesRepo>(
                d, () => obj.ProductLine.Data, d => OrderLineFactory.Create(d) as OrderLine);
        }
    }
}