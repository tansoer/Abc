using Abc.Data.Parties;
using Abc.Data.Processes;
using Abc.Domain.Common;
using Abc.Domain.Parties.Signatures;
using Abc.Domain.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task = System.Threading.Tasks.Task;

namespace Abc.Tests.Domain.Processes {

    [TestClass]
    public class OutcomeApproverTests :SealedTests<OutcomeApprover, Entity<OutcomeApproverData>> {
        protected override OutcomeApprover createObject() => new(random<OutcomeApproverData>());
        [TestMethod] public async Task ApproverTest() =>
            await testItemAsync<PartySignatureData, PartySignature, IPartySignaturesRepo>(
                obj.Data.ApproverSignatureId, () => obj.Approver.Data,
                d => new PartySignature(d));
        [TestMethod] public async Task OutcomeTest() =>
            await testItemAsync<OutcomeData, Outcome, IOutcomesRepo>(
                obj.Data.OutcomeId, () => obj.Outcome.Data,
                d => new Outcome(d));
        [TestMethod] public void OutcomeIdTest() => isReadOnly(obj.Data.OutcomeId);
        [TestMethod] public void ApproverIdTest() => isReadOnly(obj.Data.ApproverSignatureId);
    }
}