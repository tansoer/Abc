using Abc.Aids.Random;
using Abc.Data.Processes;
using Abc.Domain.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Processes {
    [TestClass] public class TaskRoutingTests :SealedTests<TaskRouting, TaskBase> {
        protected override TaskRouting createObject() => new(GetRandom.ObjectOf<TaskData>());
        [TestMethod] public void IsEscalatingTest() => isProperty<bool>();
    }
}
