using Abc.Aids.Random;
using Abc.Data.Common;
using Abc.Domain.Orders;
using Abc.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Abc.Tests.Domain.Orders {
    [TestClass]
    public class ManagersFactoryTests: TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(ManagersFactory);

        [TestMethod] public void CreateTest() {
            var d = GetRandom.ObjectOf<ManagerData>();
            var o = ManagersFactory.Create(d);
            Assert.IsInstanceOfType(o, typeof(OrdersManager));
            allPropertiesAreEqual(d, o.Data);
        }
    }
}
