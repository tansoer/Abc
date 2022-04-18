using Abc.Aids.Random;
using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Common;
using Abc.Facade.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Processes {
    [TestClass]
    public class TaskViewFactoryTests :SealedTests<TaskViewFactory, 
        AbstractViewFactory<TaskData, ITask, TaskView>> {

        [TestMethod] public void CreateTest() { }

        [TestMethod]
        public void CreateViewTest() {
            var d = GetRandom.ObjectOf<TaskData>();
            var v = obj.Create(TaskFactory.Create(d));
            allPropertiesAreEqual(d, v, nameof(v.Name));
            Assert.AreEqual(TaskFactory.Create(d).GetName(), v.Name);
        }
    }
}
