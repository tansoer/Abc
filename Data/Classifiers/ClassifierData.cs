using System;
using Abc.Data.Common;

namespace Abc.Data.Classifiers {
    public sealed class ClassifierData :EntityTypeData {
        public ClassifierData() : this(ClassifierType.Unspecified, null, null) { }
        public ClassifierData(ClassifierType type, string baseTypeId, string definition, string id = null, string name = null,
            string code = null) {
            ClassifierType = type;
            BaseTypeId = string.IsNullOrWhiteSpace(baseTypeId) ? null : baseTypeId;
            Details = string.IsNullOrWhiteSpace(definition) ? null : definition;
            Id = string.IsNullOrWhiteSpace(id) ? new Guid().ToString() : id;
            Code = string.IsNullOrWhiteSpace(code) ? id : code;
            Name = string.IsNullOrWhiteSpace(name) ? id : name;
        }
        public ClassifierType ClassifierType { get; set; }
    }
}
