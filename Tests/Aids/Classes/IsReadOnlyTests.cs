using Abc.Aids.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Classes {
    [TestClass]
    public class IsReadOnlyTests : TestsBase {
        private class testClass {
            public string A;
            public readonly string B = "";
            public testClass() { E = ""; }
            public string C { get; set; }
            public string D { get; } = "";
            public string E { get; private set; }
        }
        private testClass o;
        [TestInitialize]
        public void TestInitialize() {
            type = typeof(IsReadOnly);
            o = new testClass { A = "", C = "" };
        }
        [TestCleanup]
        public override void TestCleanup() {
            Assert.IsNotNull(o.A);
            Assert.IsNotNull(o.B);
            Assert.IsNotNull(o.C);
            Assert.IsNotNull(o.D);
            Assert.IsNotNull(o.E);
            base.TestCleanup();
        }
    }
}

