using Abc.Aids.Extensions;
using Abc.Aids.Random;
using Abc.Data.Common;
using Abc.Data.Products;
using Abc.Domain.Products.Features;
using Abc.Facade.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Products {

    [TestClass]
    public class FeatureViewFactoryTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(FeatureViewFactory);
        //TODO Gunnar
        //[TestMethod] public void CreateTest() { }

        //[DataTestMethod]
        //[DataRow(ValueType.String)]
        //[DataRow(ValueType.Boolean)]
        //[DataRow(ValueType.DateTime)]
        //[DataRow(ValueType.Decimal)]
        //[DataRow(ValueType.Double)]
        //[DataRow(ValueType.Integer)]
        //[DataRow(ValueType.Money)]
        //[DataRow(ValueType.Quantity)]
        //[DataRow(ValueType.Error)]
        //[DataRow(ValueType.Unspecified)]
        //public void CreateObjectTest(ValueType t) {
        //    var view = GetRandom.ObjectOf<FeatureView>();
        //    view.ValueType = t;
        //    var data = new FeatureViewFactory().Create(view).Data;

        //    allPropertiesAreEqual(data, view, nameof(data.Value));
        //    Assert.AreEqual(data.Value.ValueType, view.ValueType);
        //    if (view.ValueType == ValueType.Money) Assert.AreEqual(data.Value.UnitOrCurrencyId, view.CurrencyId);
        //    else if (view.ValueType == ValueType.Quantity) Assert.AreEqual(data.Value.UnitOrCurrencyId, view.UnitId);
        //    else Assert.AreEqual(data.Value.UnitOrCurrencyId, null);

        //    if (view.ValueType is not ValueType.Money and not ValueType.Quantity and not ValueType.Unspecified) Assert.AreEqual(view.FeatureValue, data.Value.Value);
        //    else if (view.ValueType == ValueType.Money) Assert.AreEqual(Variable.ToString(view.DecimalValue), data.Value.Value);
        //    else if (view.ValueType == ValueType.Quantity) Assert.AreEqual(Variable.ToString(view.DoubleValue), data.Value.Value);
        //    else Assert.AreEqual(view.StringValue, data.Value.Value);
        //}

        //[DataTestMethod]
        //[DataRow(ValueType.String)]
        //[DataRow(ValueType.Boolean)]
        //[DataRow(ValueType.DateTime)]
        //[DataRow(ValueType.Decimal)]
        //[DataRow(ValueType.Double)]
        //[DataRow(ValueType.Integer)]
        //[DataRow(ValueType.Money)]
        //[DataRow(ValueType.Quantity)]
        //[DataRow(ValueType.Error)]
        //[DataRow(ValueType.Unspecified)]
        //public void CreateViewTest(ValueType t) {
        //    var data = GetRandom.ObjectOf<FeatureData>();
        //    data.Value = create(t);
        //    var view = new FeatureViewFactory().Create(new Feature(data));

        //    allPropertiesAreEqual(data, view, nameof(data.Value));

        //}
        //private static ValueData create(ValueType t) {
        //    var v = GetRandom.ObjectOf<FeatureView>();
        //    var d = new ValueData { ValueType = t };

        //    if (t == ValueType.Quantity) d.UnitOrCurrencyId = v.CurrencyId;
        //    else if (t == ValueType.Money) d.UnitOrCurrencyId = v.UnitId;

        //    if (t is not ValueType.Money and not ValueType.Quantity) d.Value = v.FeatureValue;
        //    else if (t == ValueType.Quantity) d.Value = Variable.ToString(v.DoubleValue);
        //    else d.Value = Variable.ToString(v.DecimalValue);

        //    return d;
        //}

    }

}