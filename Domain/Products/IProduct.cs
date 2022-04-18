using Abc.Data.Products;
using Abc.Domain.Common;

namespace Abc.Domain.Products {

    public interface IProduct : IEntity<ProductData> {

        string TypeId { get; }
        ProductKind ProductKind { get; }
    }

}