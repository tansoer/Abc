using System;
using Abc.Data.Common;

namespace Abc.Data.Products {

    public sealed class BatchData : EntityBaseData {

        public string ProductTypeId { get; set; }
        public string FirstSerialNumber { get; set; }
        public string LastSerialNumber { get; set; }
        public DateTime SellBy { get; set; }
        public DateTime UseBy { get; set; }
        public DateTime BestBefore { get; set; }
        public DateTime DateProduced { get; set; }

    }

}