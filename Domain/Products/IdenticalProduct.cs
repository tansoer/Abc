using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Quantities;

namespace Abc.Domain.Products {

    public sealed class IdenticalProduct : PoleomorphProduct<IdenticalProductType> {

        public IdenticalProduct(ProductData d = null) : base(d) { }

        public override IdenticalProductType Type => type as IdenticalProductType;

        public string UnitId => Data?.UnitId ?? Unspecified.String;

        public Unit Unit => new GetFrom<IUnitsRepo, Unit>().ById(UnitId);

        public double Amount => Data?.Amount ?? Unspecified.Double;

        public Quantity Quantity => new Quantity(Amount, UnitId);

    }

}