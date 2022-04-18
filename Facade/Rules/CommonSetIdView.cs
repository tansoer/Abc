using System.ComponentModel;
using Abc.Facade.Common;

namespace Abc.Facade.Rules {

    public abstract class CommonSetIdView : EntityBaseView {


        [DisplayName("Rule Set")]
        public string RuleSetId { get; set; }

    }

}
