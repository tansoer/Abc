using Abc.Data.Parties;

namespace Abc.Data.Rules {

    public sealed class RuleOverrideData : PartySignatureBaseData {

        public string RuleSetId { get; set; }

        public string RuleId { get; set; }

        public string RuleContextId { get; set; }

        public string VariableId { get; set; }

    }

}
