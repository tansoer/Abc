using System.Collections.Generic;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Quantities;

namespace Abc.Domain.Products {

    public abstract class QuantifiedProductType : BaseProductType {

        protected QuantifiedProductType(ProductTypeData d = null) : base(d) { }

        public string UnitId => Data?.UnitId ?? Unspecified.String;

        public Unit Unit => new GetFrom<IUnitsRepo, Unit>().ById(UnitId) ?? new UnspecifiedUnit();

        public Measure Measure => Unit?.Measure ?? new UnspecifiedMeasure();

        public IReadOnlyList<Unit> PossibleUnits => Measure?.Units;

        public double Amount => Data?.Amount ?? Unspecified.Double;

        public Quantity QuantityOnHand => new Quantity(Amount, UnitId);

    }

}