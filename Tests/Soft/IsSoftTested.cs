using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Soft {

    [TestClass]
    public class IsSoftTested :AssemblyTests {

        protected override string assembly => "Abc.Soft";

        protected override string nameSpace(string name) {
            return $"{assembly}.Areas.{name}.Pages";
        }
        protected override void isAllTested(string assembly, string namespaceName = null) {

        }
    }
}
