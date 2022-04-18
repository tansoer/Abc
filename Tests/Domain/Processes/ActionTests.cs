using Abc.Aids.Random;
using Abc.Data.Classifiers;
using Abc.Data.Parties;
using Abc.Data.Processes;
using Abc.Domain.Classifiers;
using Abc.Domain.Parties.Signatures;
using Abc.Domain.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Task = System.Threading.Tasks.Task;

namespace Abc.Tests.Domain.Processes {
    [TestClass] public class ActionTests :SealedTests<Action, ProcessElement<IActionsRepo, Action, ActionData>> {
        protected override Action createObject() =>
            new(GetRandom.ObjectOf<ActionData>());
        [TestMethod] public async Task TypeTest() 
            => await testItemAsync<ActionTypeData, ActionType, IActionTypesRepo>(
                obj.ActionTypeId, () => obj.Type.Data, d => new ActionType(d));
        [TestMethod] public async Task InitiatorSignatureTest() 
            => await testItemAsync<PartySignatureData, PartySignature, IPartySignaturesRepo>(
                obj.InitiatorSignatureId, () => obj.InitiatorSignature.Data, d => new PartySignature(d));
        [TestMethod] public async Task ActionApproversTest() 
            => await testListAsync<ActionApprover, ActionApproverData, IActionApproversRepo>(
                x => x.ActionId = obj.Id, d => new ActionApprover(d));
        [TestMethod] public async Task TaskTest()
            => await testItemAsync<TaskData, ITask, ITasksRepo>(
                obj.TaskId, () => obj.Task.Data, TaskFactory.Create);
        [TestMethod] public async Task ActionStatusTest() {
            var d = random<ClassifierData>();
            d.Id = obj.ActionStatusClassifierId;
            d.ClassifierType = ClassifierType.ActionStatus;
            await testItemAsync<ClassifierData, IClassifier, IClassifiersRepo>(
                d, () => obj.ActionStatus.Data, ClassifierFactory.Create);
        }
        [TestMethod] public void ApproversTest() =>
            testRelatedList<PartySignature, PartySignatureData, ActionApprover, IPartySignaturesRepo>(
                () => obj.Approvers,
                () => obj.ActionApprovers,
                (d, e) => {
                    d.Id = e.ApproverSignatureId;
                    return new PartySignature(d);
                }, ActionApproversTest, (x, r) => x.Id == r.ApproverSignatureId);
        [TestMethod] public async Task PossibleOutcomesTest() {
            await OutcomesTest();
            var count = obj.Outcomes.Where(x => x.IsPossibleOutcome).ToList().Count;
            areEqual(obj.PossibleOutcomes.Count, count);
        }
        [TestMethod] public async Task ActualOutcomesTest() {
            await OutcomesTest();
            var count = obj.Outcomes.Where(x => !x.IsPossibleOutcome).ToList().Count;
            areEqual(obj.ActualOutcomes.Count, count);
        }
        [TestMethod] public async Task OutcomesTest() =>
            await testListAsync<Outcome, OutcomeData, IOutcomesRepo>(
                x => x.ActionId = obj.Id, d => new Outcome(d));
        [TestMethod] public void ActionTypeIdTest() => isReadOnly(obj.Data.ActionTypeId);
        [TestMethod] public void TaskIdTest() => isReadOnly(obj.Data.TaskId);
        [TestMethod] public void InitiatorSignatureIdTest() => isReadOnly(obj.Data.InitiatorSignatureId);
        [TestMethod] public void ActionStatusClassifierIdTest() => isReadOnly(obj.Data.ActionStatusClassifierId);
        [TestMethod] public void ActionIdTest() => areEqual("ActionId", Action.actionId);
    }
}
