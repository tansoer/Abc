using Abc.Infra.Processes;
using Abc.Infra.Processes.Initializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Abc.Tests.Infra.Processes.Initializers {
    [TestClass] internal class OutcomeTypesInitializerTests :DbInitializerTests<ProcessDb> {
        public OutcomeTypesInitializerTests() {
            type = typeof(OutcomeTypesInitializer);
            db = new ProcessDb(options);
            OutcomeTypesInitializer.Initialize(db);
        }
        [TestMethod] public void InitializeTest() { }
        [TestMethod] public void OutcomeTypesTest() => areEqual(30, db.OutcomeTypes.Count());
    }
}