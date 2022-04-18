using Abc.Aids.Methods;
using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Facade.Common;

namespace Abc.Facade.Products {

    public sealed class BatchViewFactory :AbstractViewFactory<BatchData, Batch, BatchView> {
        protected internal override Batch toObject(BatchData d) => new (d);

        public override BatchView Create(Batch o) {
            var v = base.Create(o);
            v.ProductsCount = o.ProductsCount;
            return v;
        }
    }

}
