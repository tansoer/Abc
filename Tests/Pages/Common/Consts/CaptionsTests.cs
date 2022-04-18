using Abc.Pages.Common.Consts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Common.Consts {

    [TestClass]
    public class CaptionsTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(Captions);

        [TestMethod]
        public void BackToListTest() =>
            Assert.AreEqual("Back to full list", Captions.BackToList);

        [TestMethod]
        public void CreateTest() =>
            Assert.AreEqual("Create new", Captions.Create);

        [TestMethod]
        public void DeleteTest() =>
            Assert.AreEqual("Delete", Captions.Delete);

        [TestMethod]
        public void DetailsTest() =>
            Assert.AreEqual("Details", Captions.Details);

        [TestMethod]
        public void EditTest() =>
            Assert.AreEqual("Edit", Captions.Edit);

        [TestMethod]
        public void SelectTest() =>
            Assert.AreEqual("Select", Captions.Select);
        [TestMethod]
        public void FirstTest() =>
            Assert.AreEqual("First", Captions.First);
        [TestMethod]
        public void LastTest() =>
            Assert.AreEqual("Last", Captions.Last);
        [TestMethod]
        public void NextTest() =>
            Assert.AreEqual("Next", Captions.Next);
        [TestMethod]
        public void PreviousTest() =>
            Assert.AreEqual("Previous", Captions.Previous);

    }

}

