using System.Net.Http;
using Abc.Soft;

namespace Abc.Tests {
    public abstract class TestsHost :TestsData {
        protected static readonly TestHost<Startup> host;
        protected static readonly HttpClient client;
        static TestsHost() {
            host = new TestHost<Startup>();
            client = host.CreateClient();
        }
    }
}
