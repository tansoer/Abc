using Abc.Aids.Random;
using Abc.Data.Processes;
using Abc.Domain.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task = System.Threading.Tasks.Task;

namespace Abc.Tests.Domain.Processes {
    [TestClass]
    public class ActionTypeTests :SealedTests<ActionType, ProcessElementType<IActionTypesRepo, ActionType, ActionTypeData>> {
        protected override ActionType createObject() =>
            new (GetRandom.ObjectOf<ActionTypeData>());

        [TestMethod]
        public async Task BaseTypeTest() {
            await testItemAsync<ActionTypeData, ActionType, IActionTypesRepo>(
                obj.baseTypeId, () => obj.BaseType.Data, d => new ActionType(d));
            obj = createObject();
            Assert.IsNotNull(obj.BaseType);
            Assert.IsNotNull(obj.BaseType.Data);
        }

        [TestMethod]
        public async Task TaskTest() {
            await testItemAsync<TaskTypeData, TaskType, ITaskTypesRepo>(
                obj.taskId, () => obj.Task.Data, d => new TaskType(d));
            obj = createObject();
            Assert.IsNotNull(obj.Task);
            Assert.IsNotNull(obj.Task.Data);
        }
    }
}