using Abc.Data.Orders;
using Abc.Domain.Orders.Delivery;
using System;
using System.Collections.Generic;

namespace Abc.Domain.Orders.Lines {
    public abstract class DeliveryOrderLine :BaseOrderLine, IDeliveryOrderLine {
        protected internal DeliveryOrderLine() : this(null) { }
        protected internal DeliveryOrderLine(OrderLineData d) : base(d) { }

        public OrderLine ProductLine => item<IOrderLinesRepo, IOrderLine>(OrderLineId) as OrderLine;

        public virtual void RejectProductInstances(string reason, string orderLineId, params string[] productIDs) {
            if (orderLineId is null) return;
            if (ProductLine?.Id != orderLineId) return;
            foreach (var productId in productIDs) {
                if (ProductLine?.Product?.Id != productId) continue;
                var i = new OrderLineItemData {
                    OrderLineId = Id,
                    ProductId = productId,
                    ValidFrom = DateTime.Now,
                    Code = nameof(RejectedItem),
                    Name = nameof(DeliveryOrderLine),
                    Details = reason
                };
            }
        }

        private static string orderLineIdInRejectItem
             => nameOf<OrderLineItemData>(x => x.OrderLineId);
        public IReadOnlyList<IOrderLineItem> RejectedItems =>
            list<IOrderLineItemsRepo, IOrderLineItem>(orderLineIdInRejectItem, Id);

        public int NumberRejected => RejectedItems?.Count ?? 0;
        public int NumberOfProducts => ProductLine?.NumberOfProducts ?? 0 - NumberRejected;
    }

    public interface IDeliveryOrderLine: IOrderLine {
        public void RejectProductInstances(string reason, string orderLineId, params string[] productIDs);
    }
}