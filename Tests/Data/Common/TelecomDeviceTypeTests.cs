using Abc.Aids.Reflection;
using Abc.Data.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Common {

    [TestClass]
    public class TelecomDeviceTypeTests : TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(TelecomDeviceType);
        [TestMethod] public void CountTest() => Assert.AreEqual(5, GetEnum.Count<TelecomDeviceType>());
        [TestMethod] public void NotKnownTest() => areEqual(0, (int) TelecomDeviceType.NotKnown);
        [TestMethod] public void PhoneTest() => areEqual(1, (int) TelecomDeviceType.Phone);
        [TestMethod] public void FaxTest() => areEqual(2, (int) TelecomDeviceType.Fax);
        [TestMethod] public void MobileTest() => areEqual(3, (int) TelecomDeviceType.Mobile);
        [TestMethod] public void PagerTest() => areEqual(4, (int) TelecomDeviceType.Pager);
    }
}


