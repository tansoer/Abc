using Abc.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Common {
    [TestClass]
    public class QuantityBaseViewTests :AbstractTests<QuantityBaseView, EntityBaseView> {
        private class testClass :QuantityBaseView { }
        [TestMethod] public void CodeTest() => isNullable<string>();
        protected override QuantityBaseView createObject() => new testClass();
        [TestMethod] public void DetailsTest() => isNullableProperty<string>("Definition");
    }
}
