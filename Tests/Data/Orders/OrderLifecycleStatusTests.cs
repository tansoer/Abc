using Abc.Aids.Reflection;
using Abc.Data.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Orders {
    [TestClass] public class OrderLifecycleStatusTests :TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(OrderLifecycleStatus);
        [TestMethod] public void CountTest() => areEqual(6, GetEnum.Count<OrderLifecycleStatus>());
        [TestMethod] public void UnspecifiedTest() => areEqual(0, (int)OrderLifecycleStatus.Unspecified);
        [TestMethod] public void InitializedTest() => areEqual(1, (int)OrderLifecycleStatus.Initialized);
        [TestMethod] public void OpenTest() => areEqual(2, (int)OrderLifecycleStatus.Open);
        [TestMethod] public void ClosedTest() => areEqual(3, (int)OrderLifecycleStatus.Closed);
        [TestMethod] public void CancellingTest() => areEqual(8, (int)OrderLifecycleStatus.Cancelling);
        [TestMethod] public void CancelledTest() => areEqual(9, (int)OrderLifecycleStatus.Cancelled);
    }
}