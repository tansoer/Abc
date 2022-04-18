using Abc.Aids.Random;
using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Common;
using Abc.Facade.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Processes {
    [TestClass] public class OutcomeViewFactoryTests :SealedTests<OutcomeViewFactory, AbstractViewFactory<
        OutcomeData, Outcome, OutcomeView>> {

        [TestMethod] public void CreateTest() { }

        [TestMethod]
        public void CreateObjectTest() {
            var view = GetRandom.ObjectOf<OutcomeView>();
            var data = new OutcomeViewFactory().Create(view).Data;

            allPropertiesAreEqual(view, data);
        }

        [TestMethod]
        public void CreateViewTest() {
            var data = GetRandom.ObjectOf<OutcomeData>();
            var view = new OutcomeViewFactory().Create(new Outcome(data));

            allPropertiesAreEqual(view, data);
        }
    }
}