using Abc.Aids.Random;
using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Domain.Parties.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Parties.Attributes {

    [TestClass]
    public class EthnicityTests :SealedTests<Ethnicity, Classifier<Ethnicity>> {
        protected override Ethnicity createObject() {
            var d = GetRandom.ObjectOf<ClassifierData>();
            d.ClassifierType = ClassifierType.PartyEthnicity;
            return ClassifierFactory.Create(d) as Ethnicity;
        }

    }
}
