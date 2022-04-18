using System;
using Abc.Aids.Extensions;
using Abc.Aids.Random;
using Abc.Data.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValueType = Abc.Data.Common.ValueType;

namespace Abc.Tests.Data.Common {

    [TestClass]
    public class ValueDataTests : SealedTests<ValueData, object> {

        [TestMethod] public void UnitOrCurrencyIdTest() => isNullable<string>();

        [TestMethod] public void ValueTest() => isNullable<string>();

        [TestMethod] public void ValueTypeTest() => isProperty<ValueType>();

        [TestMethod] public void ValidFromTest() => isNullable<DateTime?>();
        [TestMethod] public void ValidToTest() => isNullable<DateTime?>();

        internal static string correctValue(ValueType t) {
            switch (t) {
                case ValueType.Quantity:
                case ValueType.Double: return Variable.ToString(GetRandom.Double());
                case ValueType.Money:
                case ValueType.Decimal: return Variable.ToString(GetRandom.Decimal());
                case ValueType.Boolean: return Variable.ToString(rndBool);
                case ValueType.Integer: return Variable.ToString(GetRandom.Int32());
                case ValueType.DateTime: return Variable.ToString(rndDt);
                case ValueType.Unspecified:
                case ValueType.Error:
                case ValueType.String:
                    return rndStr;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }

}