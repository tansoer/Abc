using Abc.Infra.Processes;
using Abc.Infra.Processes.Initializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Abc.Tests.Infra.Processes.Initializers {
    [TestClass] public class ActionTypesInitializerTests :DbInitializerTests<ProcessDb> {
        public ActionTypesInitializerTests() {
            type = typeof(ActionTypesInitializer);
            db = new ProcessDb(options);
            ActionTypesInitializer.Initialize(db);
        }
        [TestMethod] public void InitializeTest() { }
        [TestMethod] public void ActionTypesTest() => areEqual(37, db.ActionTypes.Count());
    }
}