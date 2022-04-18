using Abc.Domain.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Products {

    [TestClass]
    public class ServiceTypeTests : SealedTests<ServiceType, BaseProductType> {

        [TestMethod] public void PeriodOfOperationFromTest() => isReadOnly(obj.Data.PeriodOfOperationFrom);

        [TestMethod] public void PeriodOfOperationToTest() => isReadOnly(obj.Data.PeriodOfOperationTo);

    }

}