using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Infra.Common;

namespace Abc.Infra.Products {

    public sealed class BatchesRepo : EntityRepo<Batch, BatchData>,
        IBatchesRepo {
        public BatchesRepo(ProductDb c = null) : base(c, c?.Batches) { }
    }
}