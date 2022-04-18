using Abc.Facade.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Abc.Facade.Quantities {
    public abstract class UnitAttributeView: BaseView {
        [Required][DisplayName("Unit")] public string UnitId { get; set; }
        [Required][DisplayName("System of units")] public string SystemOfUnitsId { get; set; }
    }
}
