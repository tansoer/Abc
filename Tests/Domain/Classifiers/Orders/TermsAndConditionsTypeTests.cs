using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Domain.Classifiers.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Abc.Tests.Domain.Classifiers.Orders
{
    [TestClass]
    public class TermsAndConditionsTypeTests :SealedTests<TermsAndConditionsType, Classifier<TermsAndConditionsType>> {
        protected override TermsAndConditionsType createObject() => new(random<ClassifierData>());
        [TestMethod] public void TypeTest() => areEqual(ClassifierType.TermsAndConditions, obj.ClassifierType);
    }
}