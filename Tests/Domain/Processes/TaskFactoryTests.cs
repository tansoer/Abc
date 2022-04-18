using Abc.Aids.Random;
using Abc.Data.Processes;
using Abc.Domain.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Processes {
    [TestClass]
    public class TaskFactoryTests :TestsBase {
        [TestInitialize]
        public void TestInitialize() => type = typeof(TaskFactory);
        [TestMethod] public void CreateTest() { }
        [TestMethod] public void CreateTaskRoutingTest() {
            var d = GetRandom.ObjectOf<TaskData>();
            d.IsEscalation = true;
            var o = TaskFactory.Create(d) as TaskRouting;
            Assert.IsNotNull(o);
            allPropertiesAreEqual(d, o.Data);
        }
        [TestMethod] public void CreateTaskTest() {
            var d = GetRandom.ObjectOf<TaskData>();
            d.IsEscalation = false;
            var o = TaskFactory.Create(d) as Task;
            Assert.IsNotNull(o);
            allPropertiesAreEqual(d, o.Data);
        }
    }
}