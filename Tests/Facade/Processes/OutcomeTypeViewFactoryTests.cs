using Abc.Aids.Random;
using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Common;
using Abc.Facade.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Processes {
    [TestClass] public class OutcomeTypeViewFactoryTests :SealedTests<OutcomeTypeViewFactory, AbstractViewFactory<
        OutcomeTypeData, OutcomeType, OutcomeTypeView>> {

        [TestMethod] public void CreateTest() { }

        [TestMethod]
        public void CreateObjectTest() {
            var view = GetRandom.ObjectOf<OutcomeTypeView>();
            var data = new OutcomeTypeViewFactory().Create(view).Data;

            allPropertiesAreEqual(view, data);
        }

        [TestMethod]
        public void CreateViewTest() {
            var data = GetRandom.ObjectOf<OutcomeTypeData>();
            var view = new OutcomeTypeViewFactory().Create(new OutcomeType(data));

            allPropertiesAreEqual(view, data);
        }
    }
}
