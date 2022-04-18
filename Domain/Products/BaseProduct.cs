using System;
using System.Collections.Generic;
using System.Linq;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Products.Features;
using Abc.Domain.Products.Packets;
using Abc.Domain.Products.Price;

namespace Abc.Domain.Products {

    public abstract class BaseProduct<TType> :Entity<ProductData>, IProduct where TType : IProductType {
        internal static string productId => nameOf<FeatureData>(x => x.ProductId);
        internal static string providerId => nameOf<ProductRelationshipData>(x => x.ProviderEntityId);
        internal static string consumerId => nameOf<ProductRelationshipData>(x => x.ConsumerEntityId);
        protected BaseProduct(ProductData d = null) :base(d) { }
        public abstract TType Type { get; }
        public ProductKind ProductKind => Data?.ProductKind ?? ProductKind.Unspecified;
        public string SerialNumber => get(Data?.Code);
        public ReservationStatus ReservationStatus => Data?.ReservationStatus ?? ReservationStatus.Unspecified;
        public string TypeId => get(Data?.ProductTypeId);
        protected internal IProductType type => item<IProductTypesRepo, IProductType>(TypeId);      
        public string BatchId => get(data?.BatchId);
        public Batch Batch => item<IBatchesRepo, Batch>(BatchId);
        public virtual IReadOnlyList<Feature> Features => list<IFeaturesRepo, Feature>(productId, Id);
        public IReadOnlyList<IPrice> RelatedPrices => list<IPricesRepo, IPrice>(productId, base.Id);
        public AppliedPrice Price => RelatedPrices.Count > 0
            ? RelatedPrices[0] as AppliedPrice ?? new PriceError()
            : new PriceError();
        public IReadOnlyList<ProductRelationship> RelationsWhereConsumer =>
            list<IProductRelationshipsRepo, ProductRelationship>(consumerId, Id);
        public IReadOnlyList<ProductRelationship> RelationsWhereProvider =>
            list<IProductRelationshipsRepo, ProductRelationship>(providerId, Id);
        public IReadOnlyList<ProductRelationship> Relations {
            get {
                var l = new List<ProductRelationship>();
                l.AddRange(RelationsWhereConsumer);
                l.AddRange(RelationsWhereProvider);
                return l.AsReadOnly();
            }
        }
        public IReadOnlyList<IProduct> ConsumerProducts =>
            RelationsWhereConsumer.Select(p => p.Provider).ToList().AsReadOnly();
        public IReadOnlyList<IProduct> ProviderProducts =>
            RelationsWhereProvider.Select(p => p.Consumer).ToList().AsReadOnly();
        public IReadOnlyList<IProduct> RelatedProducts {
            get {
                var l = new List<IProduct>();
                l.AddRange(ConsumerProducts);
                l.AddRange(ProviderProducts);

                return l.AsReadOnly();
            }
        }
        //TODO: methods to be implemented with reservation archetype
        public void Reserve(string reservationId) => throw new NotImplementedException();
        public void CancelReservation() => throw new NotImplementedException();
    }
}