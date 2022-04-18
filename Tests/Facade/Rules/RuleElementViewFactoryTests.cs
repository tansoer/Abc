using System;
using Abc.Aids.Enums;
using Abc.Aids.Extensions;
using Abc.Aids.Random;
using Abc.Data.Rules;
using Abc.Facade.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Rules {

    [TestClass]
    public class RuleElementViewFactoryTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(RuleElementViewFactory);

        [TestMethod]
        public void CreateTest() {
            foreach (var t in (RuleElementType[]) Enum.GetValues(typeof(RuleElementType))) {
                var view = GetRandom.ObjectOf<RuleElementView>();
                view.RuleElementType = t;
                var o = RuleElementViewFactory.Create(view);
                view = RuleElementViewFactory.Create(o);
                allPropertiesAreEqual(o.Data, view
                    , nameof(o.Data.UnitOrCurrencyId)
                    , nameof(o.Data.ActivityId)
                    , nameof(o.Data.Value));
                Assert.IsTrue(validateValues(o.Data, view));
                Assert.IsTrue(validateUnits(o.Data, view));
            }
        }
        private static bool validateValues(RuleElementData d, RuleElementView v) {
            return d.RuleElementType switch
            {
                RuleElementType.Boolean => d.Value == Variable.ToString(v.BooleanValue),
                RuleElementType.String => d.Value == v.StringValue,
                RuleElementType.Integer => d.Value == Variable.ToString(v.IntegerValue),
                RuleElementType.Decimal => d.Value == Variable.ToString(v.DecimalValue),
                RuleElementType.Double => d.Value == Variable.ToString(v.DoubleValue),
                RuleElementType.DateTime => d.Value == Variable.ToString(v.DateTimeValue),
                RuleElementType.Quantity => d.Value == Variable.ToString(v.DoubleValue),
                RuleElementType.Money => d.Value == Variable.ToString(v.DecimalValue),
                RuleElementType.Error => d.Value == v.StringValue,
                _ => true
            };
        }
        private static bool validateUnits(RuleElementData d, RuleElementView v) {
            return d.RuleElementType switch
            {
                RuleElementType.Quantity => d.UnitOrCurrencyId == v.UnitId,
                RuleElementType.Money => d.UnitOrCurrencyId == v.CurrencyId,
                _ => true
            };
        }

    }

}