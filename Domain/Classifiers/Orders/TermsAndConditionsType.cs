using Abc.Data.Classifiers;
namespace Abc.Domain.Classifiers.Orders
{
    public sealed class TermsAndConditionsType :Classifier<TermsAndConditionsType> {
        public TermsAndConditionsType() : this(null) { }
        public TermsAndConditionsType(ClassifierData d) : base(d) => data.ClassifierType = ClassifierType.TermsAndConditions;
    }
}