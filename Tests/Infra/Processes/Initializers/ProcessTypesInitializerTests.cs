using Abc.Infra.Processes;
using Abc.Infra.Processes.Initializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Abc.Tests.Infra.Processes.Initializers {
    [TestClass] internal class ProcessTypesInitializerTests :DbInitializerTests<ProcessDb> {
        public ProcessTypesInitializerTests() {
            type = typeof(ProcessTypesInitializer);
            db = new ProcessDb(options);
            ProcessTypesInitializer.Initialize(db);
        }
        [TestMethod] public void InitializeTest() { }
        [TestMethod] public void ProcessTypesTest() => areEqual(30, db.ProcessTypes.Count());
    }
}