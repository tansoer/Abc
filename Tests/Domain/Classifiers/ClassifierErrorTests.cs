using Abc.Aids.Random;
using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Classifiers {
    [TestClass]
    public class ClassifierErrorTests :SealedTests<ClassifierError, Classifier<ClassifierError>> {
        protected override ClassifierError createObject() {
            var d = GetRandom.ObjectOf<ClassifierData>();
            d.ClassifierType = ClassifierType.Unspecified;
            return ClassifierFactory.Create(d) as ClassifierError;
        }
    }
}