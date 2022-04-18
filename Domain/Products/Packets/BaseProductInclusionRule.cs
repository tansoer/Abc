using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Quantities;

namespace Abc.Domain.Products.Packets {

    public abstract class BaseProductInclusionRule : Entity<ProductInclusionRuleData>, IProductInclusionRule {

        protected BaseProductInclusionRule(ProductInclusionRuleData d = null) : base(d) { }

        public ProductInclusionKind InclusionKind => Data?.InclusionKind ?? ProductInclusionKind.Unspecified;

        public double MinimumAmount => Data?.MinimumAmount ?? Unspecified.Double;

        public double MaximumAmount => Data?.MaximumAmount ?? Unspecified.Double;

        public string UnitId => Data?.UnitId ?? Unspecified.String;

        public Unit Unit =>
            new GetFrom<IUnitsRepo, Unit>().ById(UnitId);

        public string ProductSetId => Data?.ProductSetId ?? Unspecified.String;

        public string PackageTypeId => Data?.PackageTypeId ?? Unspecified.String;

        public ProductSet ProductSet =>
            new GetFrom<IProductSetsRepo, ProductSet>().ById(ProductSetId);

        public Quantity Minimum => new Quantity(MinimumAmount, Unit);

        public Quantity Maximum => new Quantity(MaximumAmount, Unit);

        public override string ToString() => $"Select from {Minimum} to {Maximum} from product set {ProductSet}";
    }

}