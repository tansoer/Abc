using Abc.Domain.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Rules {

    [TestClass]
    public class OperatorTests : SealedTests<Operator, RuleElement> {

        [TestMethod] public void OperationTest() => isReadOnly(obj.Data.Operation);

        [TestMethod] public void NameTest() => isReadOnly(obj.Operation.ToString());

    }

}
