using Abc.Data.Orders;
using Abc.Domain.Common;
using Abc.Domain.Currencies;
using Abc.Domain.Rules;
using System;

namespace Abc.Domain.Orders.Discounts {
    public interface IDiscountType: IEntity<DiscountTypeData> {
        public IRuleSet Eligibility { get; }
        public Discount GetDiscount(RuleOverride o);
        public Discount GetDiscount(RuleContext c);
        public Discount GetDiscount(RuleContext c, params RuleOverride[] overrides);
    }
    public interface IDiscountTypesRepo :IRepo<IDiscountType> {
    }
    public sealed class DiscountType 
        :EntityType<IDiscountTypesRepo, IDiscountType, DiscountTypeData>
        ,IDiscountType {

        internal IRuleSet eligibility;

        public DiscountType() : this(null) { }
        public DiscountType(DiscountTypeData d) : base(d) { }

        public IRuleSet Eligibility => eligibility??item<IRuleSetsRepo, IRuleSet>(eligibilityRuleSetId);

        public Discount GetDiscount(RuleOverride o) => isApplicable(o) ? hasDiscount() : noDiscount();
        public Discount GetDiscount(RuleContext c) => isApplicable(c) ? hasDiscount() : noDiscount();
        public Discount GetDiscount(RuleContext c, params RuleOverride[] overrides)
            => isApplicable(c, overrides) ? hasDiscount() : noDiscount();

        internal decimal amount => get(Data?.Amount);
        internal Currency currency => item<ICurrencyRepo, Currency>(currencyId);
        internal string currencyId => get(Data?.CurrencyId);
        internal string eligibilityRuleSetId => get(Data?.EligibilityRuleSetId);

        internal DiscountData createData(DiscountsType t) => new() {
            DiscountTypeId = Id,
            ValidFrom = DateTime.Now,
            Name = Name,
            Details = Details,
            Code = Code,
            DiscountType = t,
            CurrencyId = (t is DiscountsType.Monetary) ? currencyId : null,
            Amount = (t is not DiscountsType.Unspecified) ? amount : 0
        };
        internal Discount hasDiscount() => currency.IsUnspecified ? persentageDiscount() : monetaryDiscount();
        internal MonetaryDiscount monetaryDiscount()
            => DiscountFactory.Create(createData(DiscountsType.Monetary)) as MonetaryDiscount;
        internal PercentageDiscount persentageDiscount()
            => DiscountFactory.Create(createData(DiscountsType.Percentage)) as PercentageDiscount;
        internal UnspecifiedDiscount noDiscount()
            => DiscountFactory.Create(createData(DiscountsType.Unspecified)) as UnspecifiedDiscount;
        internal bool isApplicable(RuleOverride o) => isApplicable(Eligibility?.Evaluate(o));
        internal bool isApplicable(RuleContext c) => isApplicable(Eligibility?.Evaluate(c));
        internal bool isApplicable(RuleContext c, params RuleOverride[] overrides)
            => isApplicable(Eligibility?.Evaluate(c, overrides));
        internal static bool isApplicable(IVariable v) => (v as BooleanVariable)?.Value ?? false;
    }

}

