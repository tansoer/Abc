using Abc.Aids.Random;
using Abc.Data.Orders;
using Abc.Domain.Orders;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Orders {

    [TestClass]
    public class UnspecifiedOrderTests :SealedTests<UnspecifiedOrder, Order> {

        protected override UnspecifiedOrder createObject() => new UnspecifiedOrder(GetRandom.ObjectOf<OrderData>());
    }
}