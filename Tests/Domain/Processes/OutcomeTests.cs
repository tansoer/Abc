using Abc.Aids.Random;
using Abc.Data.Processes;
using Abc.Domain.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task = System.Threading.Tasks.Task;

namespace Abc.Tests.Domain.Processes {
    [TestClass]
    public class OutcomeTests :SealedTests<Outcome, ProcessElement<IOutcomesRepo, Outcome, OutcomeData>> {
        protected override Outcome createObject() => new(GetRandom.ObjectOf<OutcomeData>());

        [TestMethod]
        public async Task TypeTest() {
            await testItemAsync<OutcomeTypeData, OutcomeType, IOutcomeTypesRepo>(
                obj.typeId, () => obj.Type.Data, d => new OutcomeType(d));
            obj = createObject();
            Assert.IsNotNull(obj.Type);
            Assert.IsNotNull(obj.Type.Data);
        }

        [TestMethod]
        public async Task ActionTest() {
            await testItemAsync<ActionData, Action, IActionsRepo>(
                obj.actionId, () => obj.Action.Data, d => new Action(d));
            obj = createObject();
            Assert.IsNotNull(obj.Action);
            Assert.IsNotNull(obj.Action.Data);
        }

        [TestMethod]
        public async Task ApproversTest() =>
            await testListAsync<OutcomeApprover, OutcomeApproverData, IOutcomeApproversRepo>(
                obj, nameof(obj.approvers),
                x => x.OutcomeId = obj.Id,
                d => new OutcomeApprover(d));

        [TestMethod] public void IsPossibleOutcomeTest() => isReadOnly(obj.Data.IsPossibleOutcome);

    }
}