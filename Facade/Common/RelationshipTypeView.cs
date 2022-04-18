using System.ComponentModel;

namespace Abc.Facade.Common {
    public abstract class RelationshipTypeView :EntityTypeView {
        [DisplayName("Consumer type")] public string ConsumerTypeId { get; set; }
        [DisplayName("Provider type")] public string ProviderTypeId { get; set; }
    }
}
