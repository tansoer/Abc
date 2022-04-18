using Abc.Aids.Random;
using Abc.Data.Processes;
using Abc.Data.Roles;
using Abc.Domain.Common;
using Abc.Domain.Processes;
using Abc.Domain.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task = System.Threading.Tasks.Task;

namespace Abc.Tests.Domain.Processes {
    [TestClass] public class TaskParticipantTests :SealedTests<TaskParticipant, Entity<TaskParticipantData>> {
        protected override TaskParticipant createObject() => new(GetRandom.ObjectOf<TaskParticipantData>());
        [TestMethod] public async Task PartyRoleTest() =>
            await testItemAsync<PartyRoleData, PartyRole, IPartyRolesRepo>(
                obj.partyRoleId, () => obj.PartyRole.Data, d => new PartyRole(d));
        [TestMethod] public void partyRoleIdTest() => isReadOnly(obj.Data.PartyRoleId);
        [TestMethod] public void taskIdTest() => isReadOnly(obj.Data.TaskId);

    }
}
