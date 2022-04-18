using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Domain.Classifiers.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Classifiers.Parties {

    [TestClass]
    public class ContactUsageTypeTests 
        :SealedTests<ContactUsageType, Classifier<ContactUsageType>> {

        protected override ContactUsageType createObject() => new ContactUsageType(random<ClassifierData>());
        [TestMethod] public void TypeTest() => areEqual(ClassifierType.ContactUsage, obj.ClassifierType);
    }
}

