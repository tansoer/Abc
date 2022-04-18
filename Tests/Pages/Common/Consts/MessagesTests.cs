using Abc.Pages.Common.Consts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Common.Consts {

    [TestClass]
    public class MessagesTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(Messages);

        [TestMethod]
        public void PagesOfTest() =>
            Assert.AreEqual("Page {0} of {1}", Messages.PagesOf);

    }

}
