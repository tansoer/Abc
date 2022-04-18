using Abc.Aids.Random;
using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Common;
using Abc.Facade.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Processes {
    [TestClass] public class OutcomeApproverViewFactoryTests :SealedTests<OutcomeApproverViewFactory, AbstractViewFactory<
        OutcomeApproverData, OutcomeApprover, OutcomeApproverView>> {

        [TestMethod] public void CreateTest() { }

        [TestMethod] public void CreateObjectTest() {
            var view = GetRandom.ObjectOf<OutcomeApproverView>();
            var data = new OutcomeApproverViewFactory().Create(view).Data;

            allPropertiesAreEqual(view, data);
        }

        [TestMethod] public void CreateViewTest() {
            var data = GetRandom.ObjectOf<OutcomeApproverData>();
            var view = new OutcomeApproverViewFactory().Create(new OutcomeApprover(data));

            allPropertiesAreEqual(view, data);
        }
    }
}
