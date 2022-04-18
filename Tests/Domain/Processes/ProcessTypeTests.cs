using Abc.Aids.Random;
using Abc.Data.Processes;
using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Processes;
using Abc.Domain.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Task = System.Threading.Tasks.Task;

namespace Abc.Tests.Domain.Processes
{
    [TestClass] public class ProcessTypeTests :SealedTests<ProcessType, 
        EntityType<IProcessTypesRepo, ProcessType, ProcessTypeData>> {
        protected override ProcessType createObject() => new(GetRandom.ObjectOf<ProcessTypeData>());
        [TestMethod] public void IsSuitableTest() {
            IProcessElement e = new Process();
            isTrue(obj.IsSuitable(e));
        }
        [TestMethod] public void ruleSetIdTest() => isReadOnly(obj.Data.RuleSetId);
        [TestMethod] public async Task RequirementsTest() =>
            await testItemAsync<RuleSetData, IRuleSet, IRuleSetsRepo>(
                obj.ruleSetId, () => obj.Requirements.Data, d => new RuleSet(d));

        [TestMethod] public async Task BaseTypeTest() =>
            await testItemAsync<ProcessTypeData, ProcessType, IProcessTypesRepo>(
                obj.baseTypeId, () => obj.BaseType.Data, d => new ProcessType(d));
        [TestMethod] public void ThreadsTest() {
            var count = GetRandom.UInt8(10, 30);
            var r = GetRepo.Instance<IThreadTypesRepo>();

            for (var i = 0; i < count; i++) {
                var d = GetRandom.ObjectOf<ThreadTypeData>();
                if (i % 4 == 0) d.ProcessTypeId = obj.Id;
                r.Add(new ThreadType(d));
            }

            var t = isReadOnly() as IReadOnlyList<ThreadType>;
            Assert.IsNotNull(t);
            Assert.AreEqual(Math.Ceiling(count / 4.0), t.Count);
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