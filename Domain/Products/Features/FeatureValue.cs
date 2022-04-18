using Abc.Aids.Calculator;
using Abc.Data.Common;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Values;

namespace Abc.Domain.Products.Features {

    public abstract class FeatureValue<TData> : Entity<TData> where TData : FeatureValueData, new() {
        protected FeatureValue() : this(null) { }
        protected FeatureValue(TData d) : base(d) { }
        internal protected string featureTypeId => get(Data?.FeatureTypeId);
        internal protected string valueSpecId => get(Data?.ValueSpecId);
        public FeatureType FeatureType => item<IFeatureTypesRepo, FeatureType>(featureTypeId);
        public FeatureSpec ValueSpec => item<IFeatureSpecsRepo, FeatureSpec>(valueSpecId);
        public IValue Value => ValueSpec?.Value ?? ValueFactory.Create(new ValueData());
    }
}
