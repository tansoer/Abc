using Abc.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Common {
    [TestClass]
    public class MetricBaseViewTests :
        AbstractTests<MetricBaseView, QuantityBaseView> {
        private class testClass :MetricBaseView { }
        protected override MetricBaseView createObject() => random<testClass>();
    }
}
