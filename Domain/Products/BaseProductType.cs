using System.Collections.Generic;
using System.Linq;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Products.Features;
using Abc.Domain.Products.Packets;
using Abc.Domain.Products.Price;

namespace Abc.Domain.Products {
    public abstract class BaseProductType : Entity<ProductTypeData>, IProductType {
        internal static string productTypeId => nameOf<FeatureTypeData>(x => x.ProductTypeId);
        internal static string providerId => nameOf<ProductRelationshipType>(x => x.ProviderTypeId);
        internal static string consumerId => nameOf<ProductRelationshipType>(x => x.ConsumerTypeId);
        protected BaseProductType(ProductTypeData d = null) : base(d) { }
        public string AlternativeCodes => get(Data?.AlternativeCodes);
        public bool IsBaseType => get(Data?.IsBaseType);
        public string Description => Details;
        public ProductKind ProductKind => Data?.ProductKind ?? ProductKind.Unspecified;
        public string BaseTypeId => get(Data?.BaseTypeId);
        public IProductType BaseType => item<IProductTypesRepo, IProductType>(BaseTypeId);
        public IReadOnlyList<FeatureType> Features => list<IFeatureTypesRepo, FeatureType>(productTypeId, Id);
        public IReadOnlyList<IPrice> RelatedPrices => list<IPricesRepo, IPrice>(productTypeId, base.Id);
        public virtual IReadOnlyList<PossiblePrice> PossiblePrices => 
            RelatedPrices.OfType<PossiblePrice>().ToList().AsReadOnly();
        public IReadOnlyList<ProductRelationshipType> WhereConsumer => list<IProductRelationshipTypesRepo, ProductRelationshipType>(consumerId, Id);
        public IReadOnlyList<ProductRelationshipType> WhereProvider => list<IProductRelationshipTypesRepo, ProductRelationshipType>(providerId, Id);
        public IReadOnlyList<ProductRelationshipType> Relations {
            get {
                var l = new List<ProductRelationshipType>();
                l.AddRange(WhereConsumer);
                l.AddRange(WhereProvider);
                return l.AsReadOnly();
            }
        }
        public IReadOnlyList<IProductType> ConsumerProducts => list(WhereConsumer, p => p.Provider);
        public IReadOnlyList<IProductType> ProviderProducts => list(WhereProvider, p => p.Consumer);
        public IReadOnlyList<IProductType> RelatedProducts {
            get {
                var l = new List<IProductType>();
                l.AddRange(ConsumerProducts);
                l.AddRange(ProviderProducts);
                return l.AsReadOnly();
            }
        }
        
    }
}