using System.Collections.Generic;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Products.Features;

namespace Abc.Domain.Products {

    public interface IProductType : IEntity<ProductTypeData> {

        IReadOnlyList<FeatureType> Features { get; }
        public ProductKind ProductKind { get; }
        public bool IsBaseType { get; }
    }

}