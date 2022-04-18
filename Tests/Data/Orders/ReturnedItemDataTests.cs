using Abc.Data.Common;
using Abc.Data.Orders;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Orders {
    [TestClass]
    public class ReturnedItemDataTests :SealedTests<ReturnedItemData, EntityBaseData> {
        [TestMethod] public void OrderEventIdTest() => isNullable<string>();
        [TestMethod] public void ProductIdTest() => isNullable<string>();
    }
}