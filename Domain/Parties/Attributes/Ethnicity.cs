using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;

namespace Abc.Domain.Parties.Attributes {

    public sealed class Ethnicity : Classifier<Ethnicity> {
        public Ethnicity() : this(null) { }
        public Ethnicity(ClassifierData d) : base(d) => data.ClassifierType = ClassifierType.PartyEthnicity;
    }
}
