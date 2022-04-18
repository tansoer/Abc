using Abc.Aids.Random;
using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Common;
using Abc.Facade.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Processes {
    [TestClass] public class ActionViewFactoryTests :SealedTests<ActionViewFactory, AbstractViewFactory<
        ActionData, Action, ActionView>> {

        [TestMethod] public void CreateTest() { }

        [TestMethod] public void CreateObjectTest() {
            var view = GetRandom.ObjectOf<ActionView>();
            var data = new ActionViewFactory().Create(view).Data;

            allPropertiesAreEqual(view, data);
        }

        [TestMethod] public void CreateViewTest() {
            var data = GetRandom.ObjectOf<ActionData>();
            var view = new ActionViewFactory().Create(new Action(data));

            allPropertiesAreEqual(view, data);
        }
    }
}
