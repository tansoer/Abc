using System;
using Abc.Aids.Calculator;
using Abc.Aids.Extensions;
using Abc.Aids.Random;
using Abc.Data.Common;
using Abc.Domain.Values;
using Abc.Facade.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValueType = Abc.Data.Common.ValueType;

namespace Abc.Tests.Facade.Products {
    [TestClass] public class FeatureSpecViewValueFactoryTests : TestsBase {
        private FeatureSpecView view;
        private ValueData data;
        [TestInitialize] public void TestInitialize() {
            type = typeof(FeatureSpecViewValueFactory);
            view = random<FeatureSpecView>();
            data = random<ValueData>();
        }
        [TestMethod] public void CreateTest() { }
        [TestMethod] public void CreateObjectTest() {
            var v = FeatureSpecViewValueFactory.Create(view);
            test(v, view.ValueType);
            var d = (v as BaseCommonValue)?.Data;
            test(d, view);
        }
        [TestMethod] public void QuantityViewTest() => test<QuantityValue>(quantity, view.DoubleValue, view.UnitId);
        [TestMethod] public void MoneyViewTest() => test<MoneyValue>(money, view.DecimalValue, view.CurrencyId);
        [TestMethod] public void IntegerViewTest() => test<IntegerValue>(integer, view.IntegerValue);
        [TestMethod] public void DoubleViewTest() => test<DoubleValue>(doubl, view.DoubleValue);
        [TestMethod] public void DecimalViewTest() => test<DecimalValue>(decim, view.DecimalValue);
        [TestMethod] public void DateTimeViewTest() => test<DateTimeValue>(dateTime, view.DateTimeValue);
        [TestMethod] public void ErrorViewTest() => test<ErrorValue>(error, view.StringValue);
        [TestMethod] public void StringViewTest() => test<StringValue>(str, view.StringValue);
        [TestMethod] public void BooleanViewTest() => test<BooleanValue>(boolean, view.BooleanValue);
        [DataRow(ValueType.String)]
        [DataRow(ValueType.Boolean)]
        [DataRow(ValueType.DateTime)]
        [DataRow(ValueType.Decimal)]
        [DataRow(ValueType.Double)]
        [DataRow(ValueType.Integer)]
        [DataRow(ValueType.Money)]
        [DataRow(ValueType.Quantity)]
        [DataRow(ValueType.Error)]
        [DataRow(ValueType.Unspecified)]
        [TestMethod] public void CreateViewTest(ValueType t) {
            data.ValueType = t;
            correct(data);
            view = new FeatureSpecView();
            var v = FeatureSpecViewValueFactory.Create(view, ValueFactory.Create(data));
            testView(v, data);
        }
        private static void correct(ValueData d) {
            if (isUnspecified(d)) d.ValueType = error;
            if (! isMoney(d) && !isQuantity(d)) d.UnitOrCurrencyId = null;
            if (isBoolean(d)) d.Value = toStr(rndBool);
            else if (isInteger(d)) d.Value = toStr(GetRandom.Int32());
            else if (isDouble(d)) d.Value = toStr(GetRandom.Double(-10000, 10000));
            else if (isDecimal(d)) d.Value = toStr(GetRandom.Decimal(-10000, 10000));
            else if (isMoney(d)) d.Value = toStr(GetRandom.Decimal(0, 10000));
            else if (isQuantity(d)) d.Value = toStr(GetRandom.Double(-10000, 10000));
            else if (isDateTime(d)) d.Value = toStr(GetRandom.DateTime(DateTime.Now.AddYears(-10), DateTime.Now.AddYears(10)));
        }
        [TestMethod] public void QuantityValueTest() => test(quantity, random<double>(), rndStr);
        [TestMethod] public void MoneyValueTest() => test(money, random<decimal>(), rndStr);
        [TestMethod] public void DateTimeValueTest() => test(dateTime, parse<DateTime>(Variable.ToString(rndDt)));
        [TestMethod] public void DecimalValueTest() => test(decim, random<decimal>());
        [TestMethod] public void DoubleValueTest() => test(doubl, random<double>());
        [TestMethod] public void IntegerValueTest() => test(integer, random<int>());
        [TestMethod] public void ErrorValueTest() => test(error, rndStr);
        [TestMethod] public void StringValueTest() => test(str, rndStr);
        [TestMethod] public void BooleanValueTest() => test(boolean, rndBool);
        private void test(ValueType t, object x, string id = null) {
            view = new FeatureSpecView();
            data.ValueType = t;
            data.Value = toStr(x);
            data.UnitOrCurrencyId = id;
            var v = FeatureSpecViewValueFactory.Create(view, ValueFactory.Create(data));
            isNotNull(v);
            test(v, t, x, id);
        }
        private void test<T>(ValueType t, object x, string id = null) where T : BaseCommonValue {
            view.ValueType = t;
            var o = (T)FeatureSpecViewValueFactory.Create(view);
            isNotNull(o);
            areEqual(o.Data.UnitOrCurrencyId, id);
            areEqual(o.Data.Value, Variable.ToString(x));
            areEqual(o.Data.ValueType, t);
        }
        private static void test(FeatureSpecView s, ValueType t, object x, string id) {
            areEqual(s.ValueType, t);
            testId(s, id);
            test(s, x);
        }
        private static void testId(FeatureSpecView s, string id) {
            areEqual(s.UnitId, s.ValueType == quantity ? id : null);
            areEqual(s.CurrencyId, s.ValueType == money ? id : null);
        }
        private static void test(FeatureSpecView s, object o) {
            if (isBoolean(s)) areEqual(s.BooleanValue, o);
            else if (isInteger(s)) areEqual(s.IntegerValue, o);
            else if (isDouble(s)) areEqual(s.DoubleValue, o);
            else if (isDecimal(s)) areEqual(s.DecimalValue, o);
            else if (isDateTime(s)) areEqual(s.DateTimeValue, o);
            else if (isMoney(s)) areEqual(s.DecimalValue, o);
            else if (isQuantity(s)) areEqual(s.DoubleValue, o);
            else Assert.AreEqual(s.StringValue, o);
        }
        private static void testView(FeatureSpecView s, ValueData d) {
            areEqual(s.ValueType, d.ValueType);
            areEqual(s.UnitId, isQuantity(s) ? d.UnitOrCurrencyId : null);
            areEqual(s.CurrencyId, isMoney(s) ? d.UnitOrCurrencyId : null);
            areEqual(s.BooleanValue, s.ValueType == boolean && parse<bool>(d.Value));
            areEqual(s.IntegerValue, s.ValueType == integer ? parse<int>(d.Value) : default);
            areEqual(s.DecimalValue, isDecimal(s) || isMoney(s) ? parse<decimal>(d.Value) : default);
            areEqual(s.DoubleValue, isDouble(s) || isQuantity(s) ? parse<double>(d.Value) : default);
            areEqual(s.DateTimeValue, isDateTime(s) ? parse<DateTime>(d.Value) : default);
            areEqual(s.StringValue, isString(s) || isError(s) ? d.Value : default);
        }
        private static void test(IValue v, ValueType t) {
            if (t == boolean) isInstanceOfType(v, typeof(BooleanValue));
            else if (t == str) isInstanceOfType(v, typeof(StringValue));
            else if (t == integer) isInstanceOfType(v, typeof(IntegerValue));
            else if (t == doubl) isInstanceOfType(v, typeof(DoubleValue));
            else if (t == decim) isInstanceOfType(v, typeof(DecimalValue));
            else if (t == dateTime) isInstanceOfType(v, typeof(DateTimeValue));
            else if (t == quantity) isInstanceOfType(v, typeof(QuantityValue));
            else if (t == money) isInstanceOfType(v, typeof(MoneyValue));
            else isInstanceOfType(v, typeof(ErrorValue));
        }
        private static void test(ValueData d, FeatureSpecView s) {
            if (isBoolean(s)) test(d, s.BooleanValue);
            else if (isString(s)) test(d, s.StringValue);
            else if (isInteger(s)) test(d, s.IntegerValue);
            else if (isDouble(s)) test(d, s.DoubleValue);
            else if (isDecimal(s)) test(d, s.DecimalValue);
            else if (isDateTime(s)) test(d, s.DateTimeValue);
            else if (isQuantity(s)) test(d, s.DoubleValue, s.UnitId);
            else if (isMoney(s)) test(d, s.DecimalValue, s.CurrencyId);
            else test(d, s.StringValue);
        }
        private static void test(ValueData d, object v, string id = null) {
            areEqual(d.Value, toStr(v));
            areEqual(d.UnitOrCurrencyId, id);
        }
        private static readonly ValueType money = ValueType.Money;
        private static readonly ValueType quantity = ValueType.Quantity;
        private static readonly ValueType unspecified = ValueType.Unspecified;
        private static readonly ValueType integer = ValueType.Integer;
        private static readonly ValueType doubl = ValueType.Double;
        private static readonly ValueType decim = ValueType.Decimal;
        private static readonly ValueType dateTime = ValueType.DateTime;
        private static readonly ValueType error = ValueType.Error;
        private static readonly ValueType str = ValueType.String;
        private static readonly ValueType boolean = ValueType.Boolean;
        private static bool isMoney(FeatureSpecView s) => s.ValueType == money;
        private static bool isQuantity(FeatureSpecView s) => s.ValueType == quantity;
        private static bool isInteger(FeatureSpecView s) => s.ValueType == integer;
        private static bool isDouble(FeatureSpecView s) => s.ValueType == doubl;
        private static bool isDecimal(FeatureSpecView s) => s.ValueType == decim;
        private static bool isDateTime(FeatureSpecView s) => s.ValueType == dateTime;
        private static bool isError(FeatureSpecView s) => s.ValueType == error;
        private static bool isString(FeatureSpecView s) => s.ValueType == str;
        private static bool isBoolean(FeatureSpecView s) => s.ValueType == boolean;
        private static bool isMoney(ValueData d) => d.ValueType == money;
        private static bool isUnspecified(ValueData d) => d.ValueType == unspecified;
        private static bool isQuantity(ValueData d) => d.ValueType == quantity;
        private static bool isInteger(ValueData d) => d.ValueType == integer;
        private static bool isDouble(ValueData d) => d.ValueType == doubl;
        private static bool isDecimal(ValueData d) => d.ValueType == decim;
        private static bool isDateTime(ValueData d) => d.ValueType == dateTime;
        private static bool isError(ValueData d) => d.ValueType == error;
        private static bool isString(ValueData d) => d.ValueType == str;
        private static bool isBoolean(ValueData d) => d.ValueType == boolean;
        private static T parse<T>(string s) => Variable.TryParse<T>(s);
        private static string toStr<T>(T value) => Variable.ToString<T>(value);
    }
}