using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Domain.Parties.Names;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Parties.Names {
    [TestClass] public class NameUsageTypeTests : SealedTests<NameUsageType, Classifier<NameUsageType>> {
        protected override NameUsageType createObject() => new NameUsageType(random<ClassifierData>());
        [TestMethod] public void TypeTest() => areEqual(ClassifierType.PartyNameUsage, obj.ClassifierType);
    }
}