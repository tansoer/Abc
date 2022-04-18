using Abc.Data.Products;

namespace Abc.Domain.Products.Features {
    public sealed class Feature : FeatureValue<FeatureData> {
        public Feature() : this(null) { }
        public Feature(FeatureData d) : base(d) { }
        internal string productId => get(Data?.ProductId);
        public FeatureType FeatureType 
            => item<IFeatureTypesRepo, FeatureType>(featureTypeId);
        public bool IsValid() => FeatureType.IsValid(Value);
    }
}
