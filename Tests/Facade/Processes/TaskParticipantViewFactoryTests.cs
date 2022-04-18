using Abc.Aids.Random;
using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Common;
using Abc.Facade.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Processes {
    [TestClass] public class TaskParticipantViewFactoryTests :SealedTests<TaskParticipantViewFactory, AbstractViewFactory<
        TaskParticipantData, TaskParticipant, TaskParticipantView>> {

        [TestMethod] public void CreateTest() { }

        [TestMethod] public void CreateObjectTest() {
            var view = GetRandom.ObjectOf<TaskParticipantView>();
            var data = new TaskParticipantViewFactory().Create(view).Data;

            allPropertiesAreEqual(view, data);
        }

        [TestMethod] public void CreateViewTest() {
            var data = GetRandom.ObjectOf<TaskParticipantData>();
            var view = new TaskParticipantViewFactory().Create(new TaskParticipant(data));

            allPropertiesAreEqual(view, data);
        }
    }
}
