using Abc.Aids.Random;
using Abc.Data.Processes;
using Abc.Data.Rules;
using Abc.Domain.Processes;
using Abc.Domain.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task = System.Threading.Tasks.Task;

namespace Abc.Tests.Domain.Processes
{
    [TestClass] public class ThreadTypeTests :SealedTests<ThreadType,
            ProcessElementType<IThreadTypesRepo, ThreadType, ThreadTypeData>> {
        protected override ThreadType createObject() => new(GetRandom.ObjectOf<ThreadTypeData>());
        [TestMethod] public async Task ProcessTest() =>
            await testItemAsync<RuleSetData, IRuleSet, IRuleSetsRepo>(
                obj.ruleSetId, () => obj.Requirements.Data, d => new RuleSet(d));
        [TestMethod] public async Task PreviousTest() =>
            await testItemAsync<ThreadTypeData, ThreadType, IThreadTypesRepo>(
                obj.prevId, () => obj.Previous.Data, d => new ThreadType(d));
        [TestMethod] public async Task NextTest() =>
            await testItemAsync<ThreadTypeData, ThreadType, IThreadTypesRepo>(
                obj.nextId, () => obj.Next.Data, d => new ThreadType(d));
    }
}
