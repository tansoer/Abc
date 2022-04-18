using Abc.Facade.Common;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Orders {
    [TestClass]
    public class TermsAndConditionsViewTests :SealedTests<TermsAndConditionsView, EntityBaseView> {
        [TestMethod] public void TypeIdTest() => isNullableProperty<string>("Terms and conditions type");
        [TestMethod] public void OrderIdTest() => isNullableProperty<string>("Order");
    }
}