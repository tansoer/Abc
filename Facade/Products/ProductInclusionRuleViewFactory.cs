using Abc.Data.Products;
using Abc.Domain.Products.Packets;
using Abc.Facade.Common;

namespace Abc.Facade.Products {
    public sealed class ProductInclusionRuleViewFactory :AbstractViewFactory<
        ProductInclusionRuleData, IProductInclusionRule, ProductInclusionRuleView> {
        protected internal override IProductInclusionRule toObject(ProductInclusionRuleData d)
            => ProductInclusionRuleFactory.Create(d);
    }
}
