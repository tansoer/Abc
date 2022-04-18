using System.ComponentModel;

namespace Abc.Facade.Common {
    public abstract class EntityTypeView : EntityBaseView {
        [DisplayName("Base type")] public string BaseTypeId { get; set; }
    }
}
