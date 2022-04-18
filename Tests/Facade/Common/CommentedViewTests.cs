using Abc.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Common {
    [TestClass]  public class CommentedViewTests : AbstractTests<CommentedView, DetailedView> {
        private class testClass :CommentedView { }
        protected override CommentedView createObject() => random<testClass>();
        [TestMethod] public void DetailsTest() => isNullableProperty<string>("Comments");
    }
}