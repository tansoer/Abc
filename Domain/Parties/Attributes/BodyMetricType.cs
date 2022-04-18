using Abc.Data.Parties;
using Abc.Domain.Common;
using Abc.Domain.Rules;

namespace Abc.Domain.Parties.Attributes {

    public sealed class BodyMetricType : Entity<BodyMetricTypeData> {
        //TODO Saab Viia klassifikaatoriks lisades 
        // klassifikaatori datale RuleSetId
        public BodyMetricType() : this(null) { }
        public BodyMetricType(BodyMetricTypeData d) : base(d) { }
        public string BaseTypeId => get(Data?.BaseTypeId);
        public string RuleSetId => get(Data?.RuleSetId);
        public BodyMetricType BaseType => item<IBodyMetricTypesRepo, BodyMetricType>(BaseTypeId);
        public IRuleSet RuleSet => item<IRuleSetsRepo, IRuleSet>(RuleSetId);

    }

}