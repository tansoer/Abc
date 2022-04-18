using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Infra.Common;

namespace Abc.Infra.Products {

    public sealed class BatchCheckedByRepo :EntityRepo<BatchCheckedBy, BatchCheckedByData>,
        IBatchCheckedByRepo {
        public BatchCheckedByRepo(ProductDb c = null) : base(c, c?.BatchCheckedByParties) { }
    }
}