using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages {

    [TestClass]
    public class IsPagesTested : AssemblyTests {

        protected override string assembly => "Abc.Pages";

        protected override string nameSpace(string name) {
            return $"{assembly}.{name}";
        }

    }

}
