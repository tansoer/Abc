using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Calculator;
using Abc.Data.Products;
using Abc.Domain.Common;
using ValueType = Abc.Data.Common.ValueType;

namespace Abc.Domain.Products.Features {
    public sealed class FeatureType : Entity<FeatureTypeData> {
        internal static string typeId 
            => nameOf<PossibleFeatureValueData>(x => x.FeatureTypeId);
        public FeatureType() : this(null) { }
        public FeatureType(FeatureTypeData d) : base(d) { }
        internal string productTypeId => get(Data?.ProductTypeId);
        public IProductType ProductType => item<IProductTypesRepo, IProductType>(productTypeId);
        public bool IsMandatory => get(Data?.IsMandatory);
        public bool MustSatisfyAll => get(Data?.MustSatisfyAll);
        public ValueType ValueType => Data?.ValueType ?? ValueType.Unspecified;
        public IReadOnlyList<PossibleFeatureValue> PossibleValues 
            => list<IPossibleFeatureValuesRepo, PossibleFeatureValue>(typeId, Id);
        public int NumericCode => get(Data?.NumericCode);
        public bool IsValid(Feature f) => IsValid(f?.Value);
        public bool IsValid(IValue v) => MustSatisfyAll
            ? PossibleValues.All(x => x.IsValid(v))
            : PossibleValues.Any(x => x.IsValid(v));
    }
}
