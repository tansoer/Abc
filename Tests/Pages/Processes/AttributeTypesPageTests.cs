using Abc.Aids.Random;
using Abc.Aids.Reflection;
using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Processes;
using Abc.Pages.Processes;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Processes {
    [TestClass]
    public class AttributeTypesPageTests :SealedViewFactoryPageTests<AttributeTypesPage, 
        IAttributeTypesRepo, AttributeType, AttributeTypeView, AttributeTypeData, AttributeTypeViewFactory> {
        protected override string pageTitle => ProcessTitles.AttributeTypes;
        protected override string pageUrl => ProcessUrls.AttributeTypes;
        protected override AttributeType toObject(AttributeTypeData d) => new(d);
        private class AttributeTypeRepo :mockRepo<AttributeType, AttributeTypeData>, IAttributeTypesRepo { };
        private AttributeTypeRepo attributeType;
        private AttributeTypeData attributeTypeData;
        protected override AttributeTypesPage createObject() {
            attributeType = new AttributeTypeRepo();
            attributeTypeData = GetRandom.ObjectOf<AttributeTypeData>();
            addRandomAttributeTypeInfo();
            return new AttributeTypesPage(attributeType);
        }

        private void addRandomAttributeTypeInfo() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? attributeTypeData : GetRandom.ObjectOf<AttributeTypeData>();
                attributeType.Add(new AttributeType(d));
            }
        }

        [TestMethod]
        public override void ToObjectTest() {
            var view = GetRandom.ObjectOf<AttributeTypeView>();
            var o = obj.toObject(view);
            allPropertiesAreEqual(view, o.Data, GetMember.Name<AttributeTypeView>(x => x.ElementTypeId),
                GetMember.Name<AttributeTypeView>(x => x.IsMandatory));
        }

        [TestMethod]
        public override void ToViewTest() {
            var d = GetRandom.ObjectOf<AttributeTypeData>();
            var view = obj.toView(toObject(d));
            allPropertiesAreEqual(view, d, GetMember.Name<AttributeTypeView>(x => x.ElementTypeId),
                GetMember.Name<AttributeTypeView>(x => x.IsMandatory));
        }

    }
}