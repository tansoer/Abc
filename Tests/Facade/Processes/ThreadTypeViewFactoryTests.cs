using Abc.Aids.Random;
using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Common;
using Abc.Facade.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Processes {
    [TestClass] public class ThreadTypeViewFactoryTests :SealedTests<ThreadTypeViewFactory, AbstractViewFactory<
        ThreadTypeData, ThreadType, ThreadTypeView>> {

        [TestMethod] public void CreateTest() { }

        [TestMethod] public void CreateObjectTest() {
            var view = GetRandom.ObjectOf<ThreadTypeView>();
            var data = new ThreadTypeViewFactory().Create(view).Data;

            allPropertiesAreEqual(view, data);
        }

        [TestMethod] public void CreateViewTest() {
            var data = GetRandom.ObjectOf<ThreadTypeData>();
            var view = new ThreadTypeViewFactory().Create(new ThreadType(data));

            allPropertiesAreEqual(view, data);
        }
    }
}
