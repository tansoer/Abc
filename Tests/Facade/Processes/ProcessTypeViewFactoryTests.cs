using Abc.Aids.Random;
using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Common;
using Abc.Facade.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Processes {
    [TestClass]
    public class ProcessTypeViewFactoryTests :SealedTests<ProcessTypeViewFactory, AbstractViewFactory<
        ProcessTypeData, ProcessType, ProcessTypeView>> {

        [TestMethod] public void CreateTest() { }

        [TestMethod]
        public void CreateObjectTest() {
            var view = GetRandom.ObjectOf<ProcessTypeView>();
            var data = new ProcessTypeViewFactory().Create(view).Data;

            allPropertiesAreEqual(view, data);
        }

        [TestMethod]
        public void CreateViewTest() {
            var data = GetRandom.ObjectOf<ProcessTypeData>();
            var view = new ProcessTypeViewFactory().Create(new ProcessType(data));

            allPropertiesAreEqual(view, data);
        }
    }
}
