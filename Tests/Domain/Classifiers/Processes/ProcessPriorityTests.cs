using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Domain.Classifiers.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Classifiers.Processes {

    [TestClass]
    public class ProcessPriorityTests :SealedTests<ProcessPriority, Classifier<ProcessPriority>> {
        protected override ProcessPriority createObject() => new (random<ClassifierData>());
        [TestMethod] public void TypeTest() => areEqual(ClassifierType.ProcessPriority, obj.ClassifierType);
    }
}