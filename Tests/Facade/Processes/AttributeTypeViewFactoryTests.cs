using Abc.Aids.Random;
using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Common;
using Abc.Facade.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Processes {
    [TestClass] public class AttributeTypeViewFactoryTests :SealedTests<AttributeTypeViewFactory, AbstractViewFactory<
        AttributeTypeData, AttributeType, AttributeTypeView>> {

        [TestMethod] public void CreateTest() { }

        [TestMethod] public void CreateObjectTest() {
            var view = GetRandom.ObjectOf<AttributeTypeView>();
            var data = new AttributeTypeViewFactory().Create(view).Data;

            allPropertiesAreEqual(view, data);
        }

        [TestMethod] public void CreateViewTest() {
            var data = GetRandom.ObjectOf<AttributeTypeData>();
            var view = new AttributeTypeViewFactory().Create(new AttributeType(data));

            allPropertiesAreEqual(view, data);
        }
    }
}
