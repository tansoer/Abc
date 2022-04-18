using Abc.Data.Classifiers;

namespace Abc.Domain.Classifiers.Parties {
    public sealed class CapabilityType :Classifier<CapabilityType> {
        public CapabilityType() : this(null) { }
        public CapabilityType(ClassifierData d) : base(d) => data.ClassifierType = ClassifierType.PartyCapability;
    }
}