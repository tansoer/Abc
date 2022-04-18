using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abc.Data.Quantities;
using Abc.Facade.Attributes;
using Abc.Facade.Common;

namespace Abc.Facade.Quantities {

    public sealed class MeasureView :MetricBaseView {
        [Required] [DisplayName("Type")] public MeasureType MeasureType { get; set; }
        [Required] [Unique(typeof(Domain.Quantities.IMeasuresRepo))] public new string Code { get; set; }
        [DisplayName("Formula")] public string Formula { get; set; }
    }
}
