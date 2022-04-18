using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;

namespace Abc.Domain.Parties.Names {

    public sealed class NameUsageType : Classifier<NameUsageType> {
        public NameUsageType() : this(null) { }
        public NameUsageType(ClassifierData d = null) : base(d) => data.ClassifierType = ClassifierType.PartyNameUsage;
    }
}