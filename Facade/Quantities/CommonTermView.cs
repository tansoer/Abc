using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abc.Facade.Common;

namespace Abc.Facade.Quantities {

    public abstract class CommonTermView : BaseView {
        public double Power { get; set; }
        [Required][DisplayName("Term")] public string TermId { get; set; }
        [Required][DisplayName("Metric")] public string MasterId { get; set; }
    }
}
