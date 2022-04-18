using Abc.Aids.Random;
using Abc.Data.Processes;
using Abc.Domain.Common;
using Abc.Domain.Processes;
using Abc.Domain.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Task = System.Threading.Tasks.Task;

namespace Abc.Tests.Domain.Processes {
    [TestClass] public class TaskTypeTests :SealedTests<TaskType, PartyRelationshipBaseType<TaskTypeData>> {
        protected override TaskType createObject() => new(GetRandom.ObjectOf<TaskTypeData>());
        [TestMethod] public async Task BaseTypeTest() =>
            await testItemAsync<TaskTypeData, TaskType, ITaskTypesRepo>(
                obj.baseTypeId, () => obj.BaseType.Data, d => new TaskType(d));
        [TestMethod] public async Task ThreadTest() =>
            await testItemAsync<ThreadTypeData, ThreadType, IThreadTypesRepo>(
                obj.threadTypeId, () => obj.Thread.Data, d => new ThreadType(d));
        [TestMethod] public async Task NextTest() =>
            await testItemAsync<TaskTypeData, TaskType, ITaskTypesRepo>(
                obj.nextId, () => obj.Next.Data, d => new TaskType(d));
        [TestMethod] public async Task PreviousTest() =>
            await testItemAsync<TaskTypeData, TaskType, ITaskTypesRepo>(
                obj.prevId, () => obj.Previous.Data, d => new TaskType(d));
        [TestMethod] public void threadTypeIdTest() => isReadOnly(obj.Data.ThreadTypeId);
        [TestMethod] public void nextIdTest() => isReadOnly(obj.Data.NextElementId);
        [TestMethod] public void prevIdTest() => isReadOnly(obj.Data.PreviousElementId);
        [TestMethod] public void baseTypeIdTest() => isReadOnly(obj.Data.BaseTypeId);
        [TestMethod] public void IsSuitableTest() {
            IProcessElement e = new Process();
            isTrue(obj.IsSuitable(e));
        }
        [TestMethod] public void AttributesTest() {
            var count = GetRandom.UInt8(10, 30);
            var r = GetRepo.Instance<IAttributeTypesRepo>();

            for (var i = 0; i < count; i++) {
                var d = GetRandom.ObjectOf<AttributeTypeData>();
                if (i % 4 == 0) d.ElementTypeId = obj.Id;
                r.Add(new AttributeType(d));
            }

            var t = isReadOnly() as IReadOnlyList<AttributeType>;
            Assert.IsNotNull(t);
            Assert.AreEqual(Math.Ceiling(count / 4.0), t.Count);
        }
    }
}
