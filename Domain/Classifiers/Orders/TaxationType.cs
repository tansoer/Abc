using Abc.Data.Classifiers;

namespace Abc.Domain.Classifiers.Orders {
    public sealed class TaxationType :Classifier<TaxationType> {
        public TaxationType() : this(null) { }
        public TaxationType(ClassifierData d) : base(d) => data.ClassifierType = ClassifierType.TaxationType;
    }
}
