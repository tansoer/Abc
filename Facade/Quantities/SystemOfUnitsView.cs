using Abc.Domain.Quantities;
using Abc.Facade.Attributes;
using Abc.Facade.Common;
using System.ComponentModel.DataAnnotations;

namespace Abc.Facade.Quantities {

    public sealed class SystemOfUnitsView :QuantityBaseView {
        [Required] [Unique(typeof(ISystemsOfUnitsRepo))] public new string Code { get; set; }
    }
}
