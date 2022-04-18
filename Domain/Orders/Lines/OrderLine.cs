using Abc.Data.Orders;
using Abc.Data.Parties;
using Abc.Domain.Currencies;
using Abc.Domain.Orders.Parties;
using Abc.Domain.Products;
using System;
using System.Collections.Generic;

namespace Abc.Domain.Orders.Lines {

    public sealed class OrderLine :TaxableLine {
        internal bool hasDeliveryReceiver => DeliveryReceiver is not null;
        internal static string orderLineIdField => nameOf<PartySummaryData>(x => x.OrderLineId);
        public OrderLine() :this(null) { }
        public OrderLine(OrderLineData d) :base(d) { }
        public IReadOnlyList<ChargeLine> ChargeLines => list<ChargeLine, IOrderLine>(relatedLines);
        public IProductType ProductType => item<IProductTypesRepo, IProductType>(productTypeId);
        public IProduct Product => item<IProductsRepo, IProduct>(productId);
        internal string productTypeId => get(Data?.ProductTypeId);
        internal string productId => get(Data?.ProductId);
        public int NumberOfProducts => get(Data?.NumberOfProducts);
        public DateTime ExpectedDelivery => get(Data?.ExpectedDelivery);
        public Money UnitPrice => new(amount, currencyId);
        public OrderLineReceiver DeliveryReceiver => Order.LineReceiver(this);

        public OrderLine Clone => MemberwiseClone() as OrderLine;
    }
}