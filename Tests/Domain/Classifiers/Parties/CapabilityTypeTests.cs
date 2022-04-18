using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Domain.Classifiers.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Classifiers.Parties {

    [TestClass]
    public class CapabilityTypeTests 
        :SealedTests<CapabilityType, Classifier<CapabilityType>> {
        protected override CapabilityType createObject() => new CapabilityType(random<ClassifierData>());
        [TestMethod] public void TypeTest() => areEqual(ClassifierType.PartyCapability, obj.ClassifierType);
    }
}

