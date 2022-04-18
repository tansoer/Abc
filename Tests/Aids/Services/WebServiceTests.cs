using System.Net;
using System.Net.Http;
using Abc.Aids.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Services {

    [TestClass]
    public class WebServiceTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(WebService);

        [TestMethod]
        public void LoadTest() {
            const string source1 = "https://docs.microsoft.com/";
            const string source2 = "http://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";
            const string source3 = "http://www.example.com/";

            var webpage = new HttpClient();

            Assert.AreEqual(webpage.GetStringAsync(source1).GetAwaiter().GetResult(), WebService.Load(source1));
            Assert.AreEqual(webpage.GetStringAsync(source2).GetAwaiter().GetResult(), WebService.Load(source2));
            Assert.AreEqual(webpage.GetStringAsync(source3).GetAwaiter().GetResult(), WebService.Load(source3));
        }

    }

}