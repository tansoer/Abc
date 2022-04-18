using Abc.Aids.Random;
using Abc.Aids.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Services {
    [TestClass] public class RowClassMapperTests :SealedTests<RowClassMapper, object> {
        private string name;
        private object value;
        private RowClassMapperType mapperType;

        private class testClass {
            public string Name { get; set; }
        }
        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            name = rndStr;
            value = GetRandom.AnyValue();
            mapperType = GetRandom.EnumOf<RowClassMapperType>();
        }
        [TestMethod] public void NameTest() => isNullable<string>();
        [TestMethod] public void ValueTest() => isNullable<object>();
        [TestMethod] public void ValueTypeTest() => isProperty<RowClassMapperType>();
        [TestMethod] public void FormatStrTest() => isNullable<string>();
        [TestMethod] public void CreateEmptyTest() {
            obj = new RowClassMapper();
            validate(null, null, RowClassMapperType.Value, null);
        }
        [TestMethod] public void CreateParamsTest() {
            obj = new RowClassMapper(name, value, mapperType);
            validate(name, value, mapperType, null);
        }
        [TestMethod] public void CreateTest() {
            obj = RowClassMapper.Create<testClass>(x => x.Name, value, mapperType);
            validate(nameof(testClass.Name), value, mapperType, null);
        }
        private void validate(string n, object v, RowClassMapperType t, string f) {
            areEqual(n, obj.Name, nameof(obj.Name));
            areEqual(v, obj.Value, nameof(obj.Value));
            areEqual(t, obj.ValueType, nameof(obj.ValueType));
            areEqual(f, obj.FormatStr, nameof(obj.FormatStr));
        }
    }
}
