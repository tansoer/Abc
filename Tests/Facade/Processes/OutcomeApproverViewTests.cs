using Abc.Facade.Common;
using Abc.Facade.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Processes {
    [TestClass] public class OutcomeApproverViewTests :SealedTests<OutcomeApproverView, EntityBaseView> {
        [TestMethod] public void OutcomeIdTest() => isNullableProperty<string>("Outcome");
        [TestMethod] public void ApproverSignatureIdTest() => isNullableProperty<string>("Approver Signature");
    }
}