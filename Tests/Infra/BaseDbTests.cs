using Abc.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra {
    [TestClass] public class BaseDbTests :TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(BaseDb<testClass>);

        private class testClass: BaseDb<testClass> {
            public testClass(DbContextOptions<testClass> o) : base(o) { }
        }
    }
}
