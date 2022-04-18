using Abc.Infra.Processes;
using Abc.Infra.Processes.Initializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Abc.Tests.Infra.Processes.Initializers {
    [TestClass] public class ThreadTypesInitializerTests :DbInitializerTests<ProcessDb> {
        public ThreadTypesInitializerTests() {
            type = typeof(ThreadTypesInitializer);
            db = new ProcessDb(options);
            ThreadTypesInitializer.Initialize(db);
        }
        [TestMethod] public void InitializeTest() { }
        [TestMethod] public void ThreadTypesTest() => areEqual(4, db.ThreadTypes.Count());
    }
}