using System.ComponentModel;
using Abc.Facade.Common;

namespace Abc.Facade.Parties {

    public sealed class BodyMetricTypeView : EntityBaseView {

        [DisplayName("Rule Set")]
        public string RuleSetId { get; set; }
        [DisplayName("Base Type")]
        public string BaseTypeId { get; set; }

    }

}
