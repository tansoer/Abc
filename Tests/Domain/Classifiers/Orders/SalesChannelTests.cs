using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Domain.Classifiers.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Abc.Tests.Domain.Classifiers.Orders
{
    [TestClass]
    public class SalesChannelTests :SealedTests<SalesChannel, Classifier<SalesChannel>> {
        protected override SalesChannel createObject() => new (random<ClassifierData>());
        [TestMethod] public void TypeTest() => areEqual(ClassifierType.SalesChannel, obj.ClassifierType);
    }
}