using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Domain.Parties.Identifiers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Parties.Identifiers {

    [TestClass] public class IdentifierTypeTests : SealedTests<IdentifierType, Classifier<IdentifierType>> {

        protected override IdentifierType createObject() => new IdentifierType(random<ClassifierData>());
        [TestMethod] public void TypeTest() => areEqual(ClassifierType.RegisteredIdentifier, obj.ClassifierType);
    }
}
