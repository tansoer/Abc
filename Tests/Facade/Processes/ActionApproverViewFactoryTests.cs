using Abc.Aids.Random;
using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Common;
using Abc.Facade.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Processes {
    [TestClass] public class ActionApproverViewFactoryTests :SealedTests<ActionApproverViewFactory, AbstractViewFactory<
        ActionApproverData, ActionApprover, ActionApproverView>> {

        [TestMethod] public void CreateTest() { }

        [TestMethod] public void CreateObjectTest() {
            var view = GetRandom.ObjectOf<ActionApproverView>();
            var data = new ActionApproverViewFactory().Create(view).Data;

            allPropertiesAreEqual(view, data);
        }

        [TestMethod] public void CreateViewTest() {
            var data = GetRandom.ObjectOf<ActionApproverData>();
            var view = new ActionApproverViewFactory().Create(new ActionApprover(data));

            allPropertiesAreEqual(view, data);
        }
    }
}
