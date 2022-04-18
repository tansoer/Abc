using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Products {
    public sealed class ProductRelationshipView :EntityBaseView {
        [DisplayName("Relationship Type")]
        public string RelationshipTypeId { get; set; }
        [DisplayName("Consumer Entity")]
        public string ConsumerEntityId { get; set; }
        [DisplayName("Provider Entity")]
        public string ProviderEntityId { get; set; }
    }
}
