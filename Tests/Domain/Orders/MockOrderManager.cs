using Abc.Domain.Orders;

namespace Abc.Tests.Domain.Orders {
    public abstract class MockOrderManager :MockInterface, IOrderManager {
        internal readonly IOrder order;
        public MockOrderManager(IOrder o) => order = o;
    }

}