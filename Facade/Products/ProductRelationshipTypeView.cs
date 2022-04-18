using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Products {
    public sealed class ProductRelationshipTypeView: EntityTypeView {
        [DisplayName("Consumer Type")]
        public string ConsumerTypeId { get; set; }
        [DisplayName("Provider Type")]
        public string ProviderTypeId { get; set; }
    }
}
