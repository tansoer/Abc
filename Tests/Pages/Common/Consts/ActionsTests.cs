using Abc.Pages.Common.Consts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Common.Consts {
    [TestClass]
    public class ActionsTests : TestsBase {

        [TestInitialize] public virtual void TestInitialize() => type = typeof(Actions);
        [TestMethod] public void EditTest() => Assert.AreEqual("Edit", Actions.Edit);
        [TestMethod] public void DetailsTest() => Assert.AreEqual("Details", Actions.Details);
        [TestMethod] public void DeleteTest() => Assert.AreEqual("Delete", Actions.Delete);
        [TestMethod] public void IndexTest() => Assert.AreEqual("Index", Actions.Index);
        [TestMethod] public void CreateTest() => Assert.AreEqual("Create", Actions.Create);
        [TestMethod] public void SelectTest() => Assert.AreEqual("Select", Actions.Select);

    }
}
