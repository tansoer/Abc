using Abc.Aids.Calculator;
using Abc.Aids.Enums;
using Abc.Data.Products;
using Abc.Domain.Values;

namespace Abc.Domain.Products.Features {
    public sealed class PossibleFeatureValue :FeatureValue<PossibleFeatureValueData> {
        public PossibleFeatureValue() : this(null) { }
        public PossibleFeatureValue(PossibleFeatureValueData d = null) : base(d) { }
        public Relation Relation => Data?.Relation ?? Relation.IsEqual;
        internal bool IsValid(Feature f) => IsValid(f?.Value);
        internal bool IsValid(IValue v) => Relation switch {
            Relation.IsEqual => isEqual(v),
            Relation.IsNotEqual => isNotEqual(v),
            Relation.IsLess => isLess(v),
            Relation.IsGreater => isGreater(v),
            Relation.IsNotLess => isNotLess(v),
            Relation.IsNotGreater => isNotGreater(v),
            Relation.IsMuchLess => isMuchLess(v),
            Relation.IsMuchGreater => isMuchGreater(v),
            _ => isError(),
        };
        internal bool isMuchGreater(IValue v) => val(v?.IsGreater(Value));
        //TODO: Much needs defining
        internal bool isMuchLess(IValue v) => val(v?.IsLess(Value));
        internal bool isNotGreater(IValue v) => val(v?.IsGreater(Value).Not());
        internal bool isNotLess(IValue v) => val(v?.IsLess(Value).Not());
        internal bool isGreater(IValue v) => val(v?.IsGreater(Value));
        internal bool isLess(IValue v) => val(v?.IsLess(Value));
        internal bool isNotEqual(IValue v) => val(v?.IsEqual(Value).Not());
        internal bool isEqual(IValue v) => val(v?.IsEqual(Value));
        internal static bool val(IValue v) => (v as BooleanValue)?.Value ?? false;
        internal static bool isError() => false;
    }
}
