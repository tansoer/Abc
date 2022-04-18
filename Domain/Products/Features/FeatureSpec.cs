using Abc.Aids.Calculator;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Values;

namespace Abc.Domain.Products.Features {
    public sealed class FeatureSpec : Entity<FeatureSpecData> {
        public FeatureSpec() : this(null) { }
        public FeatureSpec(FeatureSpecData d) : base(d) { }
        public IValue Value => ValueFactory.Create(Data?.Value);
    }
}
