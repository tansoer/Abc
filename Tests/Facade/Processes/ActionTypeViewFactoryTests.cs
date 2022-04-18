using Abc.Aids.Random;
using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Common;
using Abc.Facade.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Processes {
    [TestClass] public class ActionTypeViewFactoryTests :SealedTests<ActionTypeViewFactory, AbstractViewFactory<
        ActionTypeData, ActionType, ActionTypeView>> {

        [TestMethod] public void CreateTest() { }

        [TestMethod] public void CreateObjectTest() {
            var view = GetRandom.ObjectOf<ActionTypeView>();
            var data = new ActionTypeViewFactory().Create(view).Data;

            allPropertiesAreEqual(view, data);
        }

        [TestMethod] public void CreateViewTest() {
            var data = GetRandom.ObjectOf<ActionTypeData>();
            var view = new ActionTypeViewFactory().Create(new ActionType(data));

            allPropertiesAreEqual(view, data);
        }
    }
}
