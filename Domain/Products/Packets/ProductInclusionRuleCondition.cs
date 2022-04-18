using System.Collections.Generic;
using Abc.Aids.Reflection;
using Abc.Data.Products;
using Abc.Domain.Common;

namespace Abc.Domain.Products.Packets {

    public sealed class ProductInclusionRuleCondition : BaseProductInclusionRule {

        private static string inclusionId => GetMember.Name<ProductInclusionRuleData>(x => x.ProductInclusionId);

        public ProductInclusionRuleCondition(ProductInclusionRuleData d = null) : base(d) { }

        public IReadOnlyList<IProductInclusionRule> ProductInclusionDetails =>
            new GetFrom<IProductInclusionRulesRepo, IProductInclusionRule>().ListBy(inclusionId, Id);

        public IReadOnlyList<ProductInclusionRuleDetail> DetailedInclusions {
            get {
                var l = new List<ProductInclusionRuleDetail>();

                foreach (var e in ProductInclusionDetails) {
                    if (e is ProductInclusionRuleDetail d) l.Add(d);
                }
                return l;

            }
        }

    }


}


