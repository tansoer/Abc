using Abc.Facade.Quantities;
using Abc.Pages.Common.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Common.Extensions {

    [TestClass]
    public class EditorHtmlTests : TestsBase {

        [TestInitialize] public virtual void TestInitialize() => type = typeof(EditorHtml);

        [TestMethod]
        public void EditorTest() {
            var obj = new mockHtmlHelper<UnitView>().Editor(x => x.MeasureId);
            isInstanceOfType<HtmlContentBuilder>(obj);
        }
    }
}