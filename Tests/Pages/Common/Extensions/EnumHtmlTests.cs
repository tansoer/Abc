using Abc.Aids.Enums;
using Abc.Facade.Common;
using Abc.Pages.Common.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Common.Extensions {
    [TestClass] public class EnumHtmlTests : TestsBase {
        private class testClass : EntityBaseView {
            public IsoGender Gender { get; set; }
        }
        [TestInitialize] public virtual void TestInitialize() => type = typeof(EnumHtml);
        [TestMethod] public void EnumEditorTest() {
            var obj = new mockHtmlHelper<testClass>().EnumEditor(x => x.Gender);
            Assert.IsInstanceOfType(obj, typeof(HtmlContentBuilder));
        }
    }
}