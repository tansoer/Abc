using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Rules;

namespace Abc.Domain.Products.Price {

    public sealed class AgreedPrice : AppliedPrice {
        public AgreedPrice(PriceData d = null) : base(d) { }
        public string RuleOverrideId => Data?.RuleOverrideId ?? Unspecified.String;
        public string PossiblePriceId => Data?.PossiblePriceId ?? Unspecified.String;
        public RuleOverride RuleOverride =>
            new GetFrom<IRuleOverridesRepo, RuleOverride>().ById(RuleOverrideId);
        public PossiblePrice PossiblePrice =>
            (new GetFrom<IPricesRepo, IPrice>().ById(PossiblePriceId) as PossiblePrice) ?? new PossiblePrice();

    }

}
