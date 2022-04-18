using Abc.Data.Products;

namespace Abc.Domain.Products.Packets {

    public sealed class ProductInclusionError : BaseProductInclusionRule {

        public ProductInclusionError(ProductInclusionRuleData d = null) : base(d) { }

    }

}