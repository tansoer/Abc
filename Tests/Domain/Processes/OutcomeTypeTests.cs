using Abc.Data.Processes;
using Abc.Domain.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task = System.Threading.Tasks.Task;

namespace Abc.Tests.Domain.Processes {
    [TestClass] public class OutcomeTypeTests :SealedTests<OutcomeType, ProcessElementType<IOutcomeTypesRepo, OutcomeType, OutcomeTypeData>> {
        protected override OutcomeType createObject() => new(random<OutcomeTypeData>());
        [TestMethod] public async Task ActionTypeTest() {
            await testItemAsync<ActionTypeData, ActionType, IActionTypesRepo>(
                obj.Data.ActionTypeId,
                () => obj.ActionType.Data,
                d => new ActionType(d));
        }
        [TestMethod] public void ActionTypeIdTest() => isReadOnly(obj.Data.ActionTypeId);
    }
}