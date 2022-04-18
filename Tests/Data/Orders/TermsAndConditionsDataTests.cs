using Abc.Data.Common;
using Abc.Data.Orders;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Orders {
    [TestClass]
    public class TermsAndConditionsDataTests :SealedTests<TermsAndConditionsData, EntityBaseData> {
        [TestMethod] public void TypeIdTest() => isNullable<string>();
        [TestMethod] public void OrderIdTest() => isNullable<string>();
    }
}