using Abc.Data.Classifiers;

namespace Abc.Domain.Classifiers.Parties {
    public sealed class ContactUsageType :Classifier<ContactUsageType> {
        public ContactUsageType() : this(null) { }
        public ContactUsageType(ClassifierData d) : base(d) => data.ClassifierType = ClassifierType.ContactUsage;
    }
}
