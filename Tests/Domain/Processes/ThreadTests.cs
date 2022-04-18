using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Data.Processes;
using Abc.Domain.Parties.Signatures;
using Abc.Domain.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task = System.Threading.Tasks.Task;

namespace Abc.Tests.Domain.Processes {
    [TestClass] public class ThreadTests :SealedTests<Thread, ProcessElement<IThreadsRepo, Thread, ThreadData>> {
        protected override Thread createObject() => new(GetRandom.ObjectOf<ThreadData>());
        [TestMethod] public async Task TypeTest() {
            await testItemAsync<ThreadTypeData, ThreadType, IThreadTypesRepo>(
                obj.typeId, () => obj.Type.Data, d => new ThreadType(d));
        }
        [TestMethod] public async Task TerminatorTest() {
            await testItemAsync<PartySignatureData, PartySignature, IPartySignaturesRepo>(
                obj.terminatorId, () => obj.Terminator.Data, d => new PartySignature(d));
        }
        [TestMethod] public async Task ProcessTest() {
            await testItemAsync<ProcessData, Process, IProcessesRepo>(
                obj.processId, () => obj.Process.Data, d => new Process(d));
        }
        [TestMethod] public async Task TasksTest() {
            await testListAsync<ITask, TaskData, ITasksRepo>(
                x => {
                    x.ThreadId = obj.Id;
                }, TaskFactory.Create, false);
        }
        [TestMethod] public async Task NextTest() {
            await testItemAsync<ThreadData, Thread, IThreadsRepo>(
                obj.nextId, () => obj.Next.Data, d => new Thread(d));
            obj = createObject();
            Assert.IsNotNull(obj.Next);
            Assert.IsNotNull(obj.Next.Data);
        }
        [TestMethod] public async Task PreviousTest() {
            await testItemAsync<ThreadData, Thread, IThreadsRepo>(
                obj.prevId, () => obj.Previous.Data, d => new Thread(d));
            obj = createObject();
            Assert.IsNotNull(obj.Previous);
            Assert.IsNotNull(obj.Previous.Data);
        }
    }
}
