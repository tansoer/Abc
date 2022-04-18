using Abc.Data.Orders;
using Abc.Domain.Classifiers;
using Abc.Domain.Classifiers.Orders;
using Abc.Domain.Common;


namespace Abc.Domain.Orders.Terms {

    public interface ITermsAndConditionsRepo :IRepo<TermsAndConditions> { }

    public sealed class TermsAndConditions :Entity<TermsAndConditionsData> {
        public TermsAndConditions() : this(null) { }
        public TermsAndConditions(TermsAndConditionsData d) : base(d) { }

        internal string typeId => get(Data?.TypeId);
        internal string orderId => get(Data?.OrderId);

        public IOrder Order => item<IOrdersRepo, IOrder>(orderId);

        public TermsAndConditionsType Type
            => item<IClassifiersRepo, IClassifier>(typeId) as TermsAndConditionsType;

        public bool IsTypeOf(TermsAndConditionsType t) =>
            (!(Type?.IsUnspecified ?? true)) && (!(t?.IsUnspecified ?? true)) && Type?.Id == t?.Id;

        public bool IsCorrect() => !IsUnspecified;
        protected internal override bool isUnspecified() => 
            arePropertiesEqual(data, new TermsAndConditionsData(), 
                nameof(Data.Id), nameof(Data.ValidFrom), nameof(Data.ValidTo), nameof(Data.OrderId)) ;
    }
}