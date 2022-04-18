using Abc.Data.Common;
using Abc.Data.Products;
using Abc.Domain.Products.Features;
using Abc.Facade.Products;
using Abc.Tests.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValueType = Abc.Data.Common.ValueType;
using System;

namespace Abc.Tests.Facade.Products {
    [TestClass] public class FeatureSpecViewFactoryTests :ViewFactoryTests<FeatureSpecViewFactory,
        FeatureSpecData, FeatureSpec, FeatureSpecView> {
        [DataRow(ValueType.Unspecified)]
        [DataRow(ValueType.Boolean)]
        [DataRow(ValueType.String)]
        [DataRow(ValueType.Integer)]
        [DataRow(ValueType.Decimal)]
        [DataRow(ValueType.Double)]
        [DataRow(ValueType.DateTime)]
        [DataRow(ValueType.Quantity)]
        [DataRow(ValueType.Money)]
        [DataRow(ValueType.Error)]
        [TestMethod]
        public void CreateTest(ValueType t) {
            var d = createData();
            d.Value = random<ValueData>();
            d.Value.ValueType = t;
            d.Value.Value = rndVal(t);
            d.Value.UnitOrCurrencyId = isMoneyOrQuantity(t) ? d.Value.UnitOrCurrencyId : null;
            var o = dataToObject(d);
            var v = objectToView(o);
            evaluateView(o.Data, v);
        }
        protected override void evaluateData(FeatureSpecView v, FeatureSpecData d) {
            evaluateView(d, v);
        }
        protected override void evaluateView(FeatureSpecData d, FeatureSpecView v) {
            allPropertiesAreEqual(d, v, nameof(d.Value));
            if (!isMoneyOrQuantity(d.Value.ValueType))
                areEqual(d.Value.Value, v.ToString());
            else
                areEqual($"{d.Value.Value} {d.Value.UnitOrCurrencyId}", v.ToString());
        }
        private static bool isMoneyOrQuantity(ValueType t) => t is ValueType.Money or ValueType.Quantity;
        private static string rndVal(ValueType t) => t switch {
            ValueType.Unspecified => random<string>(),
            ValueType.Boolean => random<bool>().ToString(),
            ValueType.String => random<string>(),
            ValueType.Integer => random<int>().ToString(),
            ValueType.Decimal => random<decimal>().ToString(),
            ValueType.Double => random<double>().ToString(),
            ValueType.DateTime => random<DateTime>().ToString(),
            ValueType.Quantity => random<double>().ToString(),
            ValueType.Money => random<decimal>().ToString(),
            ValueType.Error => random<string>(),
            _ => random<string>()
        };
        [DataRow(ValueType.Unspecified)]
        [DataRow(ValueType.Boolean)]
        [DataRow(ValueType.String)]
        [DataRow(ValueType.Integer)]
        [DataRow(ValueType.Decimal)]
        [DataRow(ValueType.Double)]
        [DataRow(ValueType.DateTime)]
        [DataRow(ValueType.Quantity)]
        [DataRow(ValueType.Money)]
        [DataRow(ValueType.Error)]
        [TestMethod]
        public void CreateObjectTest(ValueType t) {
            var v = createView();
            v.ValueType = t;
            var o = viewToObject(v);
            evaluateData(v, o.Data);
        }
    }
}