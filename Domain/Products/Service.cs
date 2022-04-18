using System;
using Abc.Data.Products;

namespace Abc.Domain.Products {

    public sealed class Service : BaseProduct<ServiceType> {
        public Service(ProductData d = null) : base(d) { }
        public override ServiceType Type => type as ServiceType;
        public DateTime DeliveredFrom => get(Data?.DeliveredFrom);
        public DateTime DeliveredTo => get(Data?.DeliveredTo);
        public DateTime ScheduledFrom => get(Data?.ScheduledFrom);
        public DateTime ScheduledTo => get(Data?.ScheduledTo);
        public DeliveryStatus DeliveryStatus => Data?.DeliveryStatus ?? DeliveryStatus.Unspecified;
    }
}