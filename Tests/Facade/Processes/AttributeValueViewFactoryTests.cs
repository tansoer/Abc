using Abc.Aids.Random;
using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Common;
using Abc.Facade.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Processes {
    [TestClass]
    public class AttributeValueViewFactoryTests :SealedTests<AttributeValueViewFactory, AbstractViewFactory<
        AttributeValueData, AttributeValue, AttributeValueView>> {

        [TestMethod] public void CreateTest() { }

        private static string[] specificProperties
            => new[] {
                nameof(AttributeValueView.UnitId),
                nameof(AttributeValueView.CurrencyId),
                nameof(AttributeValueView.ValueType),
                nameof(AttributeValueView.StringValue),
                nameof(AttributeValueView.IntegerValue),
                nameof(AttributeValueView.BooleanValue),
                nameof(AttributeValueView.DateTimeValue),
                nameof(AttributeValueView.DoubleValue),
                nameof(AttributeValueView.DecimalValue),
                nameof(AttributeValueView.Value)
            };

        [TestMethod]
        public void CreateObjectTest() {
            var view = GetRandom.ObjectOf<AttributeValueView>();
            var data = new AttributeValueViewFactory().Create(view).Data;

            allPropertiesAreEqual(view, data, specificProperties);
        }

        [TestMethod]
        public void CreateViewTest() {
            var data = GetRandom.ObjectOf<AttributeValueData>();
            var view = new AttributeValueViewFactory().Create(new Attribute(data));

            allPropertiesAreEqual(view, data, specificProperties);
        }
    }
}