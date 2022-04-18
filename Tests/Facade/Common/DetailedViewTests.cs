using Abc.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Common {
    [TestClass]
    public class DetailedViewTests :
        AbstractTests<DetailedView, BaseView> {
        private class testClass :DetailedView { }
        protected override DetailedView createObject() => random<testClass>();
        [TestMethod] public void DetailsTest() => isNullableProperty<string>("Details");
    }
}