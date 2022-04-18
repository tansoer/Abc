using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data {
    [TestClass]
    public class IsDataTested : AssemblyTests {
        protected override string assembly => "Abc.Data";

        protected override string nameSpace(string name) { return $"{assembly}.{name}"; }
    }
}
