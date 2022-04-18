using Abc.Facade.Common;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Orders {
    [TestClass]
    public class ReturnedItemViewTests: SealedTests<ReturnedItemView, EntityBaseView> {
        [TestMethod] public void OrderEventIdTest() => isNullableProperty<string>("Order event");
        [TestMethod] public void ProductIdTest() => isNullableProperty<string>("Product");
    }
}