using System;
using Abc.Data.Common;

namespace Abc.Data.Parties {
    public sealed class BodyMetricTypeData : EntityTypeData {
        public BodyMetricTypeData() : this(null, null, null) { }
        public BodyMetricTypeData(string baseTypeId, string ruleSetId, string definition, string id = null, string name = null,
            string code = null) {
            BaseTypeId = string.IsNullOrWhiteSpace(baseTypeId) ? null : baseTypeId;
            RuleSetId = string.IsNullOrWhiteSpace(ruleSetId) ? null : ruleSetId;
            Details = string.IsNullOrWhiteSpace(definition) ? null : definition;
            Id = string.IsNullOrWhiteSpace(id) ? new Guid().ToString() : id;
            Code = string.IsNullOrWhiteSpace(code) ? id : code;
            Name = string.IsNullOrWhiteSpace(name) ? id : name;
        }
        public string RuleSetId { get; set; }
    }
}