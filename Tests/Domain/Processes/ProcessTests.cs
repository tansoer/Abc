using Abc.Aids.Random;
using Abc.Data.Classifiers;
using Abc.Data.Processes;
using Abc.Data.Roles;
using Abc.Data.Rules;
using Abc.Domain.Classifiers;
using Abc.Domain.Common;
using Abc.Domain.Processes;
using Abc.Domain.Roles;
using Abc.Domain.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task = System.Threading.Tasks.Task;

namespace Abc.Tests.Domain.Processes {

    [TestClass] public class ProcessTests :SealedTests<Process, Entity<ProcessData>> {

        protected override Process createObject() => new(GetRandom.ObjectOf<ProcessData>());

        [TestMethod] public async Task TypeTest() {
            await testItemAsync<ProcessTypeData, ProcessType, IProcessTypesRepo>(
                obj.typeId, () => obj.Type.Data, d => new ProcessType(d));
            obj = createObject();
            Assert.IsNotNull(obj.Type);
            Assert.IsNotNull(obj.Type.Data);
        }

        [TestMethod] public async Task ManagerTest() {
            await testItemAsync<PartyRoleData, PartyRole, IPartyRolesRepo>(
                obj.managerId, () => obj.Manager.Data, d => new PartyRole(d));
            obj = createObject();
            Assert.IsNotNull(obj.Manager);
            Assert.IsNotNull(obj.Manager.Data);
        }

        [TestMethod] public async Task InitiatorTest() {
            await testItemAsync<PartyRoleData, PartyRole, IPartyRolesRepo>(
                obj.initiatorId, () => obj.Initiator.Data, d => new PartyRole(d));
            obj = createObject();
            Assert.IsNotNull(obj.Initiator);
            Assert.IsNotNull(obj.Initiator.Data);
        }

        [TestMethod] public async Task PriorityTest() {
            var d = GetRandom.ObjectOf<ClassifierData>();
            d.ClassifierType = ClassifierType.ProcessPriority;
            d.Id = obj.priorityId;
            await testItemAsync<ClassifierData, IClassifier, IClassifiersRepo>
                (d, () => obj.Priority.Data, ClassifierFactory.Create);
        }

        [TestMethod] public async Task ThreadsTest() =>
            await testListAsync<Thread, ThreadData, IThreadsRepo>(
                obj, nameof(obj.Threads),
                x => x.ProcessId = obj.Id,
                d => new Thread(d));

        [TestMethod] public async Task ContextTest() {
            await testItemAsync<RuleContextData, RuleContext, IRuleContextsRepo>(
                obj.contextId, () => obj.Context.Data, d => new RuleContext(d));
            obj = createObject();
            Assert.IsNotNull(obj.Context);
            Assert.IsNotNull(obj.Context.Data);
        }

        [TestMethod] public async Task AttributesTest() =>
            await testListAsync<AttributeValue, AttributeValueData, IAttributeValuesRepo>(
                obj, nameof(obj.Attributes),
                x => {
                    x.ProcessElementId = obj.Id;
                    x.Type = AttributeValueType.AttributeValue;
                },
                AttributeValuesFactory.Create);

    }
}