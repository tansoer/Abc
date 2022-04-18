using System.ComponentModel;

namespace Abc.Facade.Common {
    public abstract class RelationshipView :EntityBaseView {
        [DisplayName("Relationship type")] public string RelationshipTypeId { get; set; }
        [DisplayName("Consumer")] public string ConsumerEntityId { get; set; }
        [DisplayName("Provider")] public string ProviderEntityId { get; set; }
    }
}
