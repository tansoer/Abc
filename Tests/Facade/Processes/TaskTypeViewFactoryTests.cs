using Abc.Aids.Random;
using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Common;
using Abc.Facade.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Processes {
    [TestClass] public class TaskTypeViewFactoryTests :SealedTests<TaskTypeViewFactory, AbstractViewFactory<
        TaskTypeData, TaskType, TaskTypeView>> {

        [TestMethod] public void CreateTest() { }

        [TestMethod]
        public void CreateObjectTest() {
            var view = GetRandom.ObjectOf<TaskTypeView>();
            var data = new TaskTypeViewFactory().Create(view).Data;

            allPropertiesAreEqual(view, data);
        }

        [TestMethod]
        public void CreateViewTest() {
            var data = GetRandom.ObjectOf<TaskTypeData>();
            var view = new TaskTypeViewFactory().Create(new TaskType(data));

            allPropertiesAreEqual(view, data);
        }
    }
}
