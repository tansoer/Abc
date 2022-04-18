using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Domain.Parties.Identifiers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Parties.Identifiers {

    [TestClass] public class AuthorityTypeTests : SealedTests<AuthorityType, Classifier<AuthorityType>> {
        protected override AuthorityType createObject() => new AuthorityType(random<ClassifierData>());
        [TestMethod] public void TypeTest() => areEqual(ClassifierType.RegistrationAuthority, obj.ClassifierType);
    }
}