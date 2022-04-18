using System;
using Abc.Data.Common;

namespace Abc.Data.Products {

    public sealed class ProductData : EntityBaseData {
        public string ProductTypeId { get; set; }
        public ProductKind ProductKind { get; set; }
        public double Amount { get; set; }
        public string UnitId { get; set; }
        public string BatchId { get; set; }

        public DateTime? ScheduledFrom { get; set; }
        public DateTime? ScheduledTo { get; set; }
        public DateTime? DeliveredFrom { get; set; }
        public DateTime? DeliveredTo { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public ReservationStatus ReservationStatus { get; set; }
    }
}
