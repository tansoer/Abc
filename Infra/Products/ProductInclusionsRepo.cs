using Abc.Data.Products;
using Abc.Domain.Products.Packets;
using Abc.Infra.Common;

namespace Abc.Infra.Products {

    public sealed class ProductInclusionsRepo :
        PagedRepo<IProductInclusionRule, ProductInclusionRuleData>,
        IProductInclusionRulesRepo {
        public ProductInclusionsRepo(ProductDb c = null) : base(c, c?.ProductInclusions) { }
        protected internal override IProductInclusionRule toDomainObject(ProductInclusionRuleData d) =>
            ProductInclusionRuleFactory.Create(d);
    }
}