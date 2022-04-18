using Abc.Data.Products;

namespace Abc.Domain.Products.Packets {

    public static class ProductInclusionRuleFactory {
        public static IProductInclusionRule Create(ProductInclusionRuleData d) {
            if (d is null) return error(null);

            return d.InclusionKind switch
            {
                ProductInclusionKind.Unspecified => error(d),
                ProductInclusionKind.Ordinal => new ProductInclusionRule(d),
                ProductInclusionKind.Conditional => new ProductInclusionRuleCondition(d),
                ProductInclusionKind.Detail => new ProductInclusionRuleDetail(d),
                _ => error(d)
            };
        }
        private static IProductInclusionRule error(ProductInclusionRuleData d) => new ProductInclusionError(d);
    }

}