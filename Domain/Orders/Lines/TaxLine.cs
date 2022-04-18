using Abc.Data.Orders;
using Abc.Domain.Currencies;

namespace Abc.Domain.Orders.Lines {
    public sealed class TaxLine :BaseOrderLine{
        public TaxLine() : this(null) { }
        public TaxLine(OrderLineData d) : base(d) { }
        public string SalesTaxPolicyId => get(Data?.SalesTaxPolicyId);
        public SalesTaxPolicy SalesTaxPolicy 
            => item<ISalesTaxPoliciesRepo, SalesTaxPolicy>(SalesTaxPolicyId);
        public Money CalculateTax(Money m) => m.Multiply(SalesTaxPolicy.TaxationRate);
    }
}