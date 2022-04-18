using Abc.Aids.Extensions;
using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Data.Quantities;
using Abc.Domain.Common;
using Abc.Domain.Parties.Attributes;
using Abc.Domain.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Parties.Attributes {

    [TestClass]
    public class BodyMetricQuantityTests
        : SealedTests<BodyMetricQuantity, BodyMetric<Quantity>> {

        protected override BodyMetricQuantity createObject() {
            var d = GetRandom.ObjectOf<BodyMetricData>();
            d.Value = Variable.ToString(GetRandom.Double(-1000000, 1000000));

            return new BodyMetricQuantity(d);
        }

        [TestMethod] public void unitIdTest() => isReadOnly(obj.Data.UnitId);

        [TestMethod] public async Task UnitTest() => await testItemAsync<UnitData, Unit, IUnitsRepo>(
            obj.unitId, () => obj.unit.Data, UnitFactory.Create);

        [TestMethod]
        public void ValueTest() {
            var d = GetRandom.ObjectOf<UnitData>();
            d.Id = obj.unitId;
            var r = GetRepo.Instance<IUnitsRepo>();
            r.Add(UnitFactory.Create(d));

            var actual = isReadOnly() as Quantity;
            Assert.IsNotNull(actual);
            Assert.AreEqual(obj.Data.Value, Variable.ToString(actual.Amount));
            Assert.IsNotNull(actual.Unit);
            allPropertiesAreEqual(d, actual.Unit.Data);
        }

        [TestMethod] public void ToStringTest() => areEqual(obj.Value.ToString(), obj.ToString());
    }
}