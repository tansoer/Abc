using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Domain.Classifiers.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Classifiers.Parties {

    [TestClass] public class NamePrefixTypeTests :SealedTests<NamePrefixType, Classifier<NamePrefixType>> {
        protected override NamePrefixType createObject() => new NamePrefixType(random<ClassifierData>());
        [TestMethod] public void TypeTest() => areEqual(ClassifierType.PersonNamePrefix, obj.ClassifierType);
    }
}