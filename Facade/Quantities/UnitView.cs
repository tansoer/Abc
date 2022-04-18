using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abc.Data.Quantities;
using Abc.Facade.Attributes;
using Abc.Facade.Common;

namespace Abc.Facade.Quantities {
    public sealed class UnitView : MetricBaseView {
        [Required] [DisplayName("Measure")] public string MeasureId { get; set; }
        [Required] [DisplayName("Type")] public UnitType UnitType { get; set; }
        [Required] [Unique(typeof(Domain.Quantities.IUnitsRepo))][DisplayName("Code")] public new string Code { get; set; }
        [DisplayName("Formula")] public string Formula { get; set; }
    }
}
