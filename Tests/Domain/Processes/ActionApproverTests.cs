using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Data.Processes;
using Abc.Domain.Common;
using Abc.Domain.Parties.Signatures;
using Abc.Domain.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task = System.Threading.Tasks.Task;
namespace Abc.Tests.Domain.Processes {

    [TestClass]
    public class ActionApproverTests :SealedTests<ActionApprover, Entity<ActionApproverData>> {
        protected override ActionApprover createObject() =>
            new (GetRandom.ObjectOf<ActionApproverData>());
        [TestMethod] public async Task ApproverTest() =>
            await testItemAsync<PartySignatureData, PartySignature, IPartySignaturesRepo>(
                obj.ApproverSignatureId, () => obj.Approver.Data, d => new PartySignature(d));
        [TestMethod] public async Task ActionTest() =>
            await testItemAsync<ActionData, Action, IActionsRepo>(
                obj.ActionId, () => obj.Action.Data, d => new Action(d));
        [TestMethod] public void ActionIdTest() => isReadOnly(obj.Data.ActionId);
        [TestMethod] public void ApproverSignatureIdTest() => isReadOnly(obj.Data.ApproverSignatureId);
    }
}
