using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Data.Orders;
using Abc.Domain.Currencies;
using Abc.Domain.Orders;
using Abc.Domain.Orders.Lines;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Orders.Lines {
    [TestClass]
    public class TaxLineTests :SealedTests<TaxLine, BaseOrderLine> {
        protected override TaxLine createObject() {
            var d = random<OrderLineData>();
            d.OrderLineType = OrderLineType.TaxLine;
            return OrderLineFactory.Create(d) as TaxLine;
        }
        [TestMethod]
        public async Task CalculateTaxTest() {
            var c = new Currency(random<CurrencyData>());
            var m = new Money(GetRandom.Decimal(100, 1000), c);
            var d = random<SalesTaxPolicyData>();
            d.TaxationRate = GetRandom.Decimal(0.01M, 0.99M);
            d.Id = obj.SalesTaxPolicyId;
            await addAsync<ISalesTaxPoliciesRepo, SalesTaxPolicy>(new SalesTaxPolicy(d));
            var t = obj.CalculateTax(m);
            areEqual(m.Currency.Id, t.Currency.Id);
            var e = m.Amount * d.TaxationRate;
            areEqual(e, t.Amount);
        }
        [TestMethod] public void SalesTaxPolicyIdTest() => isReadOnly(obj.Data.SalesTaxPolicyId);

        [TestMethod] public async Task SalesTaxPolicyTest()
            => await testItemAsync<SalesTaxPolicyData, SalesTaxPolicy, ISalesTaxPoliciesRepo>
            (obj.SalesTaxPolicyId, () => obj.SalesTaxPolicy.Data, d => new SalesTaxPolicy(d));
    }
}