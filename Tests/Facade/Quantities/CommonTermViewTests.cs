using Abc.Facade.Common;
using Abc.Facade.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Quantities {

    [TestClass]
    public class CommonTermViewTests : AbstractTests<CommonTermView, BaseView> {
        private class testClass : CommonTermView { }
        protected override CommonTermView createObject() => new testClass();
        [TestMethod] public void PowerTest() => isProperty<double>();
        [TestMethod] public void TermIdTest() => isNullableProperty<string>("Term", true);
        [TestMethod] public void MasterIdTest() => isNullableProperty<string>("Metric", true);
    }
}
