using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Domain.Classifiers.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Classifiers.Orders {
    [TestClass] public class OrderTypeTests : SealedTests<OrderType, Classifier<OrderType>> {
        protected override OrderType createObject() => new (random<ClassifierData>());
        [TestMethod] public void TypeTest() => areEqual(ClassifierType.Order, obj.ClassifierType);
    }
}
