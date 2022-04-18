using AngleSharp.Common;
using System.Net;
using System.Net.Http;
using AngleSharp.Html.Dom;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Soft {

    public abstract class BasePageTests : TestsHost {

        protected IHtmlDocument htmlDocument;
        protected string pageHtml;

        [TestMethod] public void CanRespondToQueryTest() => sendRequest(pageUrl());

        protected abstract string pageUrl();

        protected IHtmlDocument sendRequest(string url) {
            var page = client.GetPage(url);
            Assert.AreEqual(HttpStatusCode.OK, page.StatusCode);
            Assert.AreEqual(HtmlTag.Header, page.GetHeader());
            pageHtml = page.GetString();
            removeWhiteSpaceCharacters(ref pageHtml);
            return HtmlDoc.GetAsync(page).GetAwaiter().GetResult();
        }
        protected IHtmlDocument sendRequest(HttpRequestMessage m) {
            var page = client.GetPage(m);
            Assert.AreEqual(HttpStatusCode.OK, page.StatusCode);
            Assert.AreEqual(HtmlTag.Header, page.GetHeader());
            pageHtml = page.GetString();
            removeWhiteSpaceCharacters(ref pageHtml);
            return HtmlDoc.GetAsync(page).GetAwaiter().GetResult();
        }

        protected static T getContext<T>() where T : DbContext => host.GetContext<T>();

        protected static void removeWhiteSpaceCharacters(ref string s) {
            // do not touch this format
            const string c = @"
";
            s = s.Replace(c, string.Empty);
        }
    }

}