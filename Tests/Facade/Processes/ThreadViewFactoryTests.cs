using Abc.Aids.Random;
using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Common;
using Abc.Facade.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Processes {
    [TestClass] public class ThreadViewFactoryTests :SealedTests<ThreadViewFactory, AbstractViewFactory<
        ThreadData, Thread, ThreadView>> {

        [TestMethod] public void CreateTest() { }

        [TestMethod] public void CreateObjectTest() {
            var view = GetRandom.ObjectOf<ThreadView>();
            var data = new ThreadViewFactory().Create(view).Data;

            allPropertiesAreEqual(view, data);
        }

        [TestMethod] public void CreateViewTest() {
            var data = GetRandom.ObjectOf<ThreadData>();
            var view = new ThreadViewFactory().Create(new Thread(data));

            allPropertiesAreEqual(view, data);
        }
    }
}
