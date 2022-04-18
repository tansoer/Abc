using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Domain.Classifiers.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Abc.Tests.Domain.Classifiers.Processes
{
    [TestClass]
    public class ActionStatusTests :SealedTests<ActionStatus, Classifier<ActionStatus>> {
        protected override ActionStatus createObject() => new (random<ClassifierData>());
        [TestMethod] public void TypeTest() => areEqual(ClassifierType.ActionStatus, obj.ClassifierType);
    }
}