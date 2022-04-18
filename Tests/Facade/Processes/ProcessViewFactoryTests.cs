using Abc.Aids.Random;
using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Common;
using Abc.Facade.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Processes {
    [TestClass]
    public class ProcessViewFactoryTests :SealedTests<ProcessViewFactory, AbstractViewFactory<
        ProcessData, Process, ProcessView>> {

        [TestMethod] public void CreateTest() { }

        [TestMethod]
        public void CreateObjectTest() {
            var view = GetRandom.ObjectOf<ProcessView>();
            var data = new ProcessViewFactory().Create(view).Data;

            allPropertiesAreEqual(view, data);
        }

        [TestMethod]
        public void CreateViewTest() {
            var data = GetRandom.ObjectOf<ProcessData>();
            var view = new ProcessViewFactory().Create(new Process(data));

            allPropertiesAreEqual(view, data);
        }
    }
}
