using Abc.Aids.Random;
using Abc.Data.Processes;
using Abc.Domain.Common;
using Abc.Domain.Processes;
using Abc.Facade.Processes;
using Abc.Pages.Common;
using Abc.Pages.Processes;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Attribute = Abc.Domain.Processes.Attribute;

namespace Abc.Tests.Pages.Processes {
    [TestClass]
    public class AttributeValuesPageTests :SealedViewsPageTests<
        AttributeValuesPage,
        IAttributeValuesRepo,
        AttributeValue,
        AttributeValueView,
        AttributeValueData,
        AttributeValueType> {

        private AttributeValueData data;
        private AttributeTypeData attributeTypeData;

        protected override Type getBaseClass() => typeof(ViewsPage<AttributeValuesPage, IAttributeValuesRepo, AttributeValue,
            AttributeValueView, AttributeValueData, AttributeValueType>);

        protected override string pageTitle => ProcessTitles.AttributeValues;
        protected override string pageUrl => ProcessUrls.AttributeValues;

        protected override AttributeValue toObject(AttributeValueData d) => new Attribute(d);

        private class testRepo :mockRepo<AttributeValue, AttributeValueData>, IAttributeValuesRepo { }
        private class attributeTypesRepo :mockRepo<AttributeType, AttributeTypeData>, IAttributeTypesRepo { }

        private testRepo repo;
        private attributeTypesRepo attributeTypes;

        protected override AttributeValuesPage createObject() {
            initializeRepos();
            setRandomData();
            addRandomDataToRepos();
            return new AttributeValuesPage(repo, attributeTypes);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(repo, attributeTypes);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }

        private void initializeRepos() {
            repo = new();
            attributeTypes = new();
        }

        private void setRandomData() {
            data = GetRandom.ObjectOf<AttributeValueData>();
            attributeTypeData = GetRandom.ObjectOf<AttributeTypeData>();
        }

        private void addRandomDataToRepos() {
            addRandomAttributeValues();
            addRandomAttributeTypes();
        }

        private void addRandomAttributeValues() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? data : GetRandom.ObjectOf<AttributeValueData>();
                repo.Add(new Attribute(d));
            }
        }

        private void addRandomAttributeTypes() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? attributeTypeData : GetRandom.ObjectOf<AttributeTypeData>();
                attributeTypes.Add(new AttributeType(d));
            }
        }

        [TestMethod] public void AttributeTypesTest() {
            var list = attributeTypes.Get();
            Assert.AreEqual(list.Count + 1, obj.AttributeTypes.Count());
        }

        [TestMethod] public void EqualitiesTest() => isReadOnly(obj.Equalities);

        [TestMethod] public void TypeTest() => isProperty<AttributeValueType>();

        [TestMethod] public void AttributeTypeNameTest() {
            var name = obj.AttributeTypeName(attributeTypeData.Id);
            Assert.AreEqual(attributeTypeData.Name, name);
        }

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

        [TestMethod] public override void ToObjectTest() {
            var view = GetRandom.ObjectOf<AttributeValueView>();
            var o = obj.toObject(view);
            allPropertiesAreEqual(view, o.Data, specificProperties);
        }
        [TestMethod] public override void ToViewTest() {
            var d = GetRandom.ObjectOf<AttributeValueData>();
            var view = obj.toView(toObject(d));
            allPropertiesAreEqual(view, d, specificProperties);
        }
    }
}