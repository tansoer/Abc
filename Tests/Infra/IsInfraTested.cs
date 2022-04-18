using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra {

    [TestClass]
    public class IsInfraTested : AssemblyTests {

        protected override string assembly => "Abc.Infra";

        protected override string nameSpace(string name) => $"{assembly}.{name}";


    }

}
