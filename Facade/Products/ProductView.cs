using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abc.Data.Products;
using Abc.Facade.Common;

namespace Abc.Facade.Products {

    public sealed class ProductView : EntityBaseView {

        [DisplayName("Product Type")]
        public string ProductTypeId { get; set; }

        public new string Name { get; set; }

        [Required]
        [DisplayName("Serial Number")]
        public new string Code { get; set; }

        [DisplayName("Product Kind")]
        public ProductKind ProductKind { get; set; }

        [DisplayName("Amount")]
        public double Amount { get; set; }

        [DisplayName("Unit")]
        public string UnitId { get; set; }

        [DisplayName("Batch")]
        public string BatchId { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Scheduled From")]
        public DateTime? ScheduledFrom { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Scheduled To")]
        public DateTime? ScheduledTo { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Delivered From")]
        public DateTime? DeliveredFrom { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Delivered To")]
        public DateTime? DeliveredTo { get; set; }

        [DisplayName("Delivery Status")]
        public DeliveryStatus DeliveryStatus { get; set; }

        [DisplayName("Reservation Status")]
        public ReservationStatus ReservationStatus { get; set; }

    }

}
