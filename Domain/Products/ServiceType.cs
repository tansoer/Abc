using System;
using Abc.Data.Products;
using Abc.Domain.Common;

namespace Abc.Domain.Products {

    public sealed class ServiceType : BaseProductType {

        public ServiceType(ProductTypeData d = null) : base(d) { }

        public DateTime PeriodOfOperationFrom => Data?.PeriodOfOperationFrom ?? Unspecified.ValidFromDate;

        public DateTime PeriodOfOperationTo => Data?.PeriodOfOperationTo ?? Unspecified.ValidToDate;

    }

}