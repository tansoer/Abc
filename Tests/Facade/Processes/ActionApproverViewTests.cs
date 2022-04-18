using Abc.Facade.Common;
using Abc.Facade.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Processes {
    [TestClass] public class ActionApproverViewTests :SealedTests<ActionApproverView, EntityBaseView> {
        [TestMethod] public void ActionIdTest() => isNullableProperty<string>("Action");
        [TestMethod] public void ApproverSignatureIdTest() => isNullableProperty<string>("Approver Signature");
    }
}