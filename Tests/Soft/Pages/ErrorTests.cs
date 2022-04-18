using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Soft.Pages {

    [TestClass]
    public class ErrorTests : BasePageTests {

        protected override string pageUrl() => "/Error";

        [TestMethod] public override void IsTested() { }

        [TestMethod] public override void IsSpecifiedClassTested() { }

    }

}
