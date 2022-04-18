using Abc.Domain.Orders;
using Abc.Domain.Orders.Lines;
using Abc.Domain.Orders.Parties;
using System.Collections.Generic;

namespace Abc.Tests.Domain.Orders {

    public sealed class MockPartiesManager :MockOrderManager, IOrderPartiesManager {
        
        public MockPartiesManager(IOrder o) : base(o) { }
        public IReadOnlyList<OrderLineReceiver> LineReceivers => registerMethod<IReadOnlyList<OrderLineReceiver>>();
        public Purchaser Purchaser => registerMethod<Purchaser>();
        public OrderInitiator OrderInitiator => registerMethod<OrderInitiator>();
        public PaymentReceiver PaymentReceiver => registerMethod<PaymentReceiver>();
        public OrderReceiver DeliveryReceiver => registerMethod<OrderReceiver>();
        public Vendor Vendor => registerMethod<Vendor>();
        public SalesAgent SalesAgent => registerMethod<SalesAgent>();
        public bool Amend(AmendPartySummaryEvent e) => registerMethod<bool>(e);
        public OrderLineReceiver LineReceiver(OrderLine l) => registerMethod<OrderLineReceiver>(l);
    }
}