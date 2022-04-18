using Abc.Domain.Orders.Payments;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Orders.Payments {
    internal class MockOrderPaymentsManager :MockInterface, IInternalOrderPaymentsManager {
    }
 
    [TestClass] public class orderPaymentsManagerTests :TestsHost {
        [TestInitialize] public void TestInitialize() {
            type = typeof(orderPaymentsManager);
        }
    }
}
