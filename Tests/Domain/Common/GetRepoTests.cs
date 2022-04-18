using Abc.Domain.Common;
using Abc.Domain.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Common {

    [TestClass]
    public class GetRepoTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(GetRepo);

        [TestMethod] public void SetServiceProviderTest() => Assert.IsNotNull(GetRepo.services);
        [TestMethod] public void InstanceTest() => Assert.IsNotNull(GetRepo.Instance<IMeasuresRepo>());

    }

}
