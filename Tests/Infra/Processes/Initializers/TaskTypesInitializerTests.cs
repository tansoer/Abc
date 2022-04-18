using Abc.Infra.Processes;
using Abc.Infra.Processes.Initializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Abc.Tests.Infra.Processes.Initializers {
    [TestClass] public class TaskTypesInitializerTests :DbInitializerTests<ProcessDb> {
        public TaskTypesInitializerTests() {
            type = typeof(TaskTypesInitializer);
            db = new ProcessDb(options);
            TaskTypesInitializer.Initialize(db);
        }
        [TestMethod] public void InitializeTest() { }
        [TestMethod] public void TaskTypesTest() => areEqual(22, db.TaskTypes.Count());
    }
}