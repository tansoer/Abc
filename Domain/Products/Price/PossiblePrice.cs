using Abc.Data.Products;
using Abc.Domain.Rules;

namespace Abc.Domain.Products.Price {

    public sealed class PossiblePrice : BasePrice {

        public PossiblePrice(PriceData d = null) : base(d) { }

        public string ProductTypeId => get(Data?.ProductTypeId);

        public string RuleSetId => get(Data?.RuleSetId);

        public IProductType ProductType => item<IProductTypesRepo, IProductType>(ProductTypeId);

        public IRuleSet RuleSet =>item<IRuleSetsRepo, IRuleSet>(RuleSetId);

        public bool IsValid(RuleContext c, params RuleOverride[] overrides)
            => (RuleSet.Evaluate(c, overrides) as BooleanVariable)?.Value ?? false;

    }
}