using Abc.Data.Processes;
using Abc.Domain.Common;
using Abc.Domain.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task = System.Threading.Tasks.Task;

namespace Abc.Tests.Domain.Processes
{
    [TestClass]
    public class ProcessElementBaseTests
        :AbstractTests<ProcessElementBase<IOutcomesRepo, Outcome, OutcomeData>,
            Entity<OutcomeData>> {

        private class testClass :ProcessElementBase<IOutcomesRepo, Outcome, OutcomeData> {
            public testClass(OutcomeData d) : base(d) { }
        }

        protected override ProcessElementBase<IOutcomesRepo, Outcome, OutcomeData> createObject()
            => new testClass(random<OutcomeData>());

        [TestMethod]
        public async Task NextTest()
            => await testItemAsync<OutcomeData, Outcome, IOutcomesRepo>(
                obj.nextId, () => obj.Next.Data, d => new Outcome(d));

        [TestMethod]
        public async Task PreviousTest()
            => await testItemAsync<OutcomeData, Outcome, IOutcomesRepo>(
                obj.prevId, () => obj.Previous.Data, d => new Outcome(d));
    }
}