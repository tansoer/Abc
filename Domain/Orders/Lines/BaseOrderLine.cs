using Abc.Data.Orders;
using Abc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Abc.Domain.Orders.Lines {
    public interface IOrderLine :IEntity<OrderLineData> {
        public bool SetRemove();
        bool IsTypeOf(OrderLineType t);
        public IOrder Order { get; }
        public string OrderId { get; }
        public string OrderLineId { get; }
    }
    public interface IOrderLinesRepo :IRepo<IOrderLine> { }

    public abstract class BaseOrderLine: Entity<OrderLineData>, IOrderLine {
        private static string orderLineIdField => nameOf<OrderLineData>(x => x.OrderLineId);
        internal IOrder order;
        protected BaseOrderLine() : this(null) { }
        protected BaseOrderLine(OrderLineData d) : base(d) { }
        public string OrderId => get(Data?.OrderId);
        public string OrderLineId => get(Data?.OrderLineId);
        public IOrder Order => order??item<IOrdersRepo, IOrder>(OrderId);
        public bool SetRemove() {
            data.ValidTo = DateTime.Now;
            return true;
        }
        public bool IsTypeOf(OrderLineType t) => t switch {
            OrderLineType.Unspecified => (this is UnspecifiedLine) || Data.OrderLineType == t, 
            OrderLineType.ProductLine => (this is OrderLine) || Data.OrderLineType == t, 
            OrderLineType.ChargeLine => (this is ChargeLine) || Data.OrderLineType == t, 
            OrderLineType.TaxLine => (this is TaxLine) || Data.OrderLineType == t, 
            OrderLineType.DispatchLine => (this is DispatchLine) || Data.OrderLineType == t, 
            OrderLineType.ReceiptLine => (this is ReceiptLine) || Data.OrderLineType == t,
            _ => false
        };
        protected internal decimal amount => get(Data?.Amount);
        protected internal string currencyId => get(Data?.CurrencyId);
        protected internal IReadOnlyList<IOrderLine> relatedLines
            => list<IOrderLinesRepo, IOrderLine>(orderLineIdField, Id);
        protected internal static bool isListed<T>(IReadOnlyList<T> l, string id) where T : IEntity
            => l.FirstOrDefault(x => x.Id == id) is not null;
        protected internal OrderLineData newLine(IOrderLine l, OrderLineType t) {
            var d = l.Data;
            d.OrderId = OrderId;
            d.OrderLineId = Id;
            d.OrderLineType = t;
            return d;
        }
    }
}