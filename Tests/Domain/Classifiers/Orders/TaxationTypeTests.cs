using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Domain.Classifiers.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Classifiers.Orders {
    [TestClass]
    public class TaxationTypeTests :SealedTests<TaxationType, Classifier<TaxationType>> {
        protected override TaxationType createObject() => new(random<ClassifierData>());
        [TestMethod] public void TypeTest() => areEqual(ClassifierType.TaxationType, obj.ClassifierType);
    }
}
