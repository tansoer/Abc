using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade {

    [TestClass]
    public class IsFacadeTested : AssemblyTests {

        protected override string assembly => "Abc.Facade";

        protected override string nameSpace(string name) => $"{assembly}.{name}";


    }

}
