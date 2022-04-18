using Abc.Pages.Common.Extensions.Assist;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Common.Extensions.Assist {
    [TestClass]
    public class HtmlAssistTests: TestsBase {
        [TestInitialize] public void TestInitialize() {
            type = typeof (HtmlAssist);
        }
    }
}
