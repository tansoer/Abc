using Abc.Data.Products;
using Abc.Domain.Common;

namespace Abc.Domain.Products.Packets {

    public sealed class ProductInclusionRuleDetail : BaseProductInclusionRule {

        public ProductInclusionRuleDetail(ProductInclusionRuleData d = null) : base(d) { }

        public string MasterInclusionId => Data?.ProductInclusionId ?? Unspecified.String;

        public ProductInclusionRuleCondition MasterInclusion =>
            new GetFrom<IProductInclusionRulesRepo, IProductInclusionRule>().ById(MasterInclusionId) as ProductInclusionRuleCondition;
    }

}