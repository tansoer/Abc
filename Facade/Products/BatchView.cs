using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abc.Facade.Common;

namespace Abc.Facade.Products {
    public sealed class BatchView : EntityBaseView {
        [DisplayName("Product Type")]
        public string ProductTypeId { get; set; }
        [DisplayName("First Serial Number")]
        public string FirstSerialNumber { get; set; }
        [DisplayName("Last Serial Number")]
        public string LastSerialNumber { get; set; }
        [DisplayName("Products Count")]
        public int ProductsCount { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Sell By Date")]
        public DateTime? SellBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Use By Date")]
        public DateTime? UseBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Best Before Date")]
        public DateTime? BestBefore { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Date Produced")]
        public DateTime? DateProduced { get; set; }
    }
}
