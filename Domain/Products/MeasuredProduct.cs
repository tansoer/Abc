using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Quantities;

namespace Abc.Domain.Products {

    public sealed class MeasuredProduct : BaseProduct<MeasuredProductType> {

        public MeasuredProduct(ProductData d = null) : base(d) { }

        public override MeasuredProductType Type => type as MeasuredProductType;

        public string UnitId => Data?.UnitId ?? Unspecified.String;

        public Unit Unit => new GetFrom<IUnitsRepo, Unit>().ById(UnitId);

        public double Amount => Data?.Amount ?? Unspecified.Double;

        public Quantity Quantity => new Quantity(Amount, UnitId);
    }

}