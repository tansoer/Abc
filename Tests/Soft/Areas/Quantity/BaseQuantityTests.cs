using Abc.Data.Common;
using Abc.Data.Quantities;
using Abc.Facade.Common;
using Abc.Infra.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Soft.Areas.Quantity {

    public abstract class BaseQuantityTests<TView, TData> :BaseAcceptanceTests<TView, TData, QuantityDb>
        where TData : BaseData where TView : BaseView {
        [TestCleanup] public override void TestCleanup() {
            clear(db.SystemsOfUnits);
            clear(db.UnitFactors);
            clear(db.CommonTerms);
            clear(db.Units);
            clear(db.Measures);
            base.TestCleanup();
        }
        protected void addUnit(string id) => addItem<UnitData>(x => x.Id = id);
        protected void addUnitFactor(string id) => addItem<UnitFactorData>(x => x.UnitId = id);
        protected void addTerms(string id) => addItem<CommonTermData>(x => x.MasterId = id);
        protected void addSystemOfUnits(string id) => addItem<SystemOfUnitsData>(x => x.Id = id);
        protected void addMeasure(string id) => addItem<MeasureData>(x => x.Id = id);
    }
}
