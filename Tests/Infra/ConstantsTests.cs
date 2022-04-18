using Abc.Infra;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra {

    public class ConstantsTests : TestsBase {

        [TestInitialize] public virtual void TestInitialize() => type = typeof(Constants);

        [TestMethod] public void DefaultPageSizeTest() => Assert.AreEqual(5, Constants.DefaultPageSize);

    }

}

