using Abc.Aids.Random;
using Abc.Data.Common;
using Abc.Domain.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Common {

    [TestClass]
    public class ValueTests : AbstractTests<Value<ValueData>, Archetype> {
        private class testClass : Value<ValueData> {
            public testClass(ValueData d = null) : base(d) { }
        }
        protected override Value<ValueData> createObject() => 
            new testClass(GetRandom.ObjectOf<ValueData>());
        [TestMethod] public void DataTest() => isReadOnly();
        [TestMethod] public void IsUnspecifiedTest() {
            Assert.IsTrue(new testClass().IsUnspecified);
            Assert.IsFalse(new testClass(GetRandom.ObjectOf<ValueData>()).IsUnspecified);
        }
        [TestMethod] public void CanSetDataTest() {
            var d = GetRandom.ObjectOf<ValueData>();
            obj = new testClass(d);
            Assert.AreNotSame(d, obj.Data);
            allPropertiesAreEqual(d, obj.Data);
        }
        [TestMethod] public void CanSetNullDataTest() {
            obj = new testClass();
            Assert.IsNotNull(obj.Data);
            Assert.IsTrue(obj.IsUnspecified);
        }
        [TestMethod] public void CantChangeDataElementsTest() {
            obj = new testClass(GetRandom.ObjectOf<ValueData>());
            var d = obj.Data;
            obj.Data.UnitOrCurrencyId = rndStr;
            obj.Data.ValueType = GetRandom.EnumOf<ValueType>();
            obj.Data.Value = rndStr;
            allPropertiesAreEqual(d, obj.Data);
        }
        [TestMethod] public void SetDataTest() {
            var data = GetRandom.ObjectOf<ValueData>();
            testSettingRandomData(data);
            tryOverwritingExistingData(data);
            testSettingNullData();
        }
        private void testSettingRandomData(ValueData data) {
            obj = new testClass();
            notEqualProperties(data, obj.Data);
            obj.SetData(data);
            allPropertiesAreEqual(data, obj.Data);
        }
        private void tryOverwritingExistingData(ValueData data) {
            var newData = GetRandom.ObjectOf<ValueData>();
            obj.SetData(newData);
            notEqualProperties(newData, obj.Data);
            allPropertiesAreEqual(data, obj.Data);
        }
        private void testSettingNullData() {
            obj = new testClass();
            obj.SetData(null);
            allPropertiesAreEqual(new ValueData(), obj.Data, nameof(obj.Data.UnitOrCurrencyId));
        }
    }
}
