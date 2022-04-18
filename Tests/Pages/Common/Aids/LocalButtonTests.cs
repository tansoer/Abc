using Abc.Aids.Random;
using Abc.Pages.Common.Aids;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Common.Aids {
    [TestClass]
    public class LocalButtonTests :SealedTests<LocalButton, object> {
        private string caption;
        private string action;
        protected override LocalButton createObject() {
            caption = rndStr;
            action = rndStr;
            return new LocalButton(caption, action);
        }

        [TestMethod] public void ActionTest() => Assert.AreEqual(action, obj.Action);
        [TestMethod] public void CaptionTest() => Assert.AreEqual(caption, obj.Caption);

        [TestMethod]
        public void GetUrlStringTest() {
            var a = GetRandom.ObjectOf<Args>();
            var actual = obj.GetUrlString(a);
            var expected = Href.Compose(a, action, caption);
            Assert.AreEqual(expected, actual);
        }
    }
}
