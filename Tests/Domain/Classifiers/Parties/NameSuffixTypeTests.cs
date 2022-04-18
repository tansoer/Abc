using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Domain.Classifiers.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Classifiers.Parties {
    [TestClass] public class NameSuffixTypeTests :SealedTests<NameSuffixType, Classifier<NameSuffixType>> {
        protected override NameSuffixType createObject() => new NameSuffixType(random<ClassifierData>());
        [TestMethod] public void TypeTest() => areEqual(ClassifierType.PersonNameSuffix, obj.ClassifierType);
    }
}