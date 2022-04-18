using Abc.Data.Orders;
using Abc.Domain.Orders.Parties;
using System.Collections.Generic;
using System.Linq;

namespace Abc.Domain.Orders.Lines {
    public interface IOrderLinesManager: IOrderManager {
        public IReadOnlyList<OrderLine> ProductLines { get; }
        public IReadOnlyList<ChargeLine> ChargeLines { get; }
        bool Amend(AmendOrderLineEvent e);
        bool IsClosable();
        bool IsCancellable();
        bool IsProductLine(IOrderLine orderLine);
    }
    public sealed class OrderLinesManager :OrderManager, IOrderLinesManager {
        public OrderLinesManager(IOrder o) : base(o) { }
        public OrderLinesManager() { }
        internal static string orderIdInLine => nameOf<OrderLineData>(x => x.OrderId);
        internal IReadOnlyList<IOrderLine> orderLines => list<IOrderLinesRepo, IOrderLine>(orderIdInLine, order?.Id);
        public IReadOnlyList<OrderLine> ProductLines => list<OrderLine, IOrderLine>(orderLines);
        public IReadOnlyList<ChargeLine> ChargeLines => list<ChargeLine, IOrderLine>(orderLines);
        public void Add(IOrderLine l) {
            if (!isOrderLine(l)) add<IOrderLinesRepo, IOrderLine>(newOrderLine(l));
        }
        public void Remove(IOrderLine l) {
            if (isRemovable(l)) update<IOrderLinesRepo, IOrderLine>(l);
        }
        public void Add(ChargeLine forLine, TaxLine t) {
            if (!isOrderLine(forLine)) forLine.add(t);
        }
        public void Remove(ChargeLine forLine, TaxLine t) {
            if (isRemovable(forLine)) forLine.remove(t);
        }
        public void Add(OrderLine forLine, TaxLine t) {
            if (!isOrderLine(forLine)) forLine.add(t);
        }
        public void Remove(OrderLine forLine, TaxLine t) {
            if (isRemovable(forLine)) forLine.remove(t);
        }
        public void Add(OrderLine forLine, ChargeLine t) => throw new System.NotImplementedException();
        public void Add(OrderLine forLine, IPartyInOrder p) => throw new System.NotImplementedException();
        public void Remove(OrderLine forLine, ChargeLine t) => throw new System.NotImplementedException();
        public void Remove(OrderLine forLine, IPartyInOrder p) => throw new System.NotImplementedException();
        public bool IsClosable() => throw new System.NotImplementedException();
        public bool IsCancellable() => throw new System.NotImplementedException();
        internal bool isRemovable(IOrderLine l) => isOrderLine(l) && l.SetRemove();
        internal bool isOrderLine(IOrderLine l) 
            => !(orderLines.FirstOrDefault(x => x.Id == l.Id) is null);
        internal IOrderLine newOrderLine(IOrderLine l) {
            var d = l.Data;
            d.OrderId = order?.Id;
            return OrderLineFactory.Create(d);
        }
        public bool Amend(AmendOrderLineEvent e) => throw new System.NotImplementedException();

        public bool IsProductLine(IOrderLine l) 
            => ProductLines.FirstOrDefault(x => x.Id == l.Id) is not null;
    }
}