using Abc.Aids.Random;
using Abc.Facade.Quantities;
using Abc.Pages.Common.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Common.Extensions {

    [TestClass]
    public class ShowHtmlTests : TestsBase {

        [TestInitialize] public virtual void TestInitialize() => type = typeof(ShowHtml);

        [TestMethod]
        public void ShowTest() {
            var obj = new mockHtmlHelper<UnitView>().Show(x => x.MeasureId);
            Assert.IsInstanceOfType(obj, typeof(HtmlContentBuilder));
        }

        [TestMethod]
        public void ShowCaptionTest() {
            var obj = new mockHtmlHelper<UnitView>().ShowCaption(x => x.MeasureId, rndStr);
            Assert.IsInstanceOfType(obj, typeof(HtmlContentBuilder));
        }
    }
}