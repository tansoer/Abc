using Abc.Data.Orders;
using Abc.Domain.Classifiers.Orders;
using Abc.Domain.Classifiers;
using Abc.Domain.Common;

namespace Abc.Domain.Orders {
    public interface ISalesTaxPoliciesRepo :IRepo<SalesTaxPolicy> { }

    public sealed class SalesTaxPolicy :Entity<SalesTaxPolicyData> {
        public SalesTaxPolicy() : this(null) { }
        public SalesTaxPolicy(SalesTaxPolicyData d) : base(d) { }
        public decimal TaxationRate => get(Data?.TaxationRate);
        public string TaxationTypeId => get(Data?.TaxationTypeId);
        public TaxationType TaxationType => item<IClassifiersRepo, IClassifier>(TaxationTypeId) as TaxationType;
    }
}

