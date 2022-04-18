using System.Collections.Generic;
using Abc.Facade.Quantities;
using Abc.Pages.Common.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Common.Extensions {

    [TestClass]
    public class DropDownHtmlTests : TestsBase {

        private readonly List<SelectListItem> items = new List<SelectListItem> { new SelectListItem("text", "value") };

        [TestInitialize] public virtual void TestInitialize() => type = typeof(DropDownHtml);

        [TestMethod]
        public void DropDownTest() {
            var obj = new mockHtmlHelper<UnitView>().DropDown(x => x.MeasureId, items);
            Assert.IsInstanceOfType(obj, typeof(HtmlContentBuilder));
        }
    }

}
