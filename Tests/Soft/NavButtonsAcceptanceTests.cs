using System;
using Abc.Aids.Extensions;
using Abc.Pages.Common.Consts;
using AngleSharp.Html.Dom;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Soft {

    public delegate void ChangeHtmlString(ref string html);
    public class NavButtonsAcceptanceTests: TestsAssertions {

        private IHtmlAnchorElement firstBtn;
        private IHtmlAnchorElement prevBtn;
        private IHtmlAnchorElement nextBtn;
        private IHtmlAnchorElement lastBtn;
        private IHtmlAnchorElement pagesInfo;

        private readonly Action addToDatabase;
        private readonly Func<string> pageUrl;
        private readonly Func<int> getNumberOfPages;
        private readonly Func<string> baseUrl;
        private readonly Func<string, IHtmlDocument> sendRequest;
        private IHtmlDocument htmlDocument;
        private int numberOfPages => getNumberOfPages();
        public event ChangeHtmlString DoBeforeValidation;
        public NavButtonsAcceptanceTests(Action db,
            Func<string> getPageUrl,
            Func<string> getBaseUrl,
            Func<int> getNoOfPages,
            Func<string, IHtmlDocument> sendUrl) {
            addToDatabase = db;
            pageUrl = getPageUrl;
            baseUrl = getBaseUrl;
            getNumberOfPages = getNoOfPages;
            sendRequest = sendUrl;
        }

        public void DoTest() {

            addToDatabase();
            testNavButtons(pageUrl(), 1, 0, 2, numberOfPages);
            testNextButton();
            testPreviousButton();
            testFirstButton();
            testLastButton();
        }

        private void testLastButton() {
            testNavButtons(firstBtn.Href, 1, 0, 2, numberOfPages);

            for (var i = 2; i <= numberOfPages; i++) {
                Assert.IsNotNull(nextBtn);
                testNavButtons(nextBtn.Href, 1, i - 1, i + 1, numberOfPages);
                var nextNext = nextBtn;
                testNavButtons(lastBtn.Href, 1, numberOfPages - 1, numberOfPages + 1, numberOfPages);
                nextBtn = nextNext;
            }
        }

        private void testPreviousButton() {
            testNavButtons(lastBtn.Href, 1, numberOfPages - 1, numberOfPages + 1, numberOfPages);

            for (var i = numberOfPages - 1; i > 0; i--) {
                Assert.IsNotNull(prevBtn);
                testNavButtons(prevBtn.Href, 1, i - 1, i + 1, numberOfPages);
            }
        }

        private void testNextButton() {

            for (var i = 2; i <= numberOfPages; i++) {
                Assert.IsNotNull(nextBtn);
                testNavButtons(nextBtn.Href, 1, i - 1, i + 1, numberOfPages);
            }
        }

        private void testFirstButton() {
            for (var i = 2; i <= numberOfPages; i++) {
                Assert.IsNotNull(nextBtn);
                testNavButtons(nextBtn.Href, 1, i - 1, i + 1, numberOfPages);
                var nextNext = nextBtn;
                testNavButtons(firstBtn.Href, 1, 0, 2, numberOfPages);
                nextBtn = nextNext;
            }
        }

        protected void testNavButtons(string url, int firstIdx, int prevIdx, int nextIdx, int lastIdx) {
            htmlDocument = sendRequest(url);
            findNavLinks();
            testNavLinks(firstIdx, prevIdx, nextIdx, lastIdx);
        }


        private void testNavLinks(in int firstIdx, in int prevIdx, in int nextIdx, in int lastIdx) {
            var id = nextIdx - 1;
            var pages = Messages.PagesOf.Format(id, numberOfPages);
            var firstLink = navLink("First", firstIdx, id < 2);
            var prevLink = navLink("Previous", prevIdx, id < 2);
            var nextLink = navLink("Next", nextIdx, id > lastIdx - 1);
            var lastLink = navLink("Last", lastIdx, id > lastIdx - 1);
            if (DoBeforeValidation != null) {
                DoBeforeValidation(ref firstLink);
                DoBeforeValidation(ref prevLink);
                DoBeforeValidation(ref nextLink);
                DoBeforeValidation(ref lastLink);
            }
            areEqual(firstLink, firstBtn.OuterHtml);
            areEqual(prevLink, prevBtn.OuterHtml);
            areEqual(nextLink, nextBtn.OuterHtml);
            areEqual(lastLink, lastBtn.OuterHtml);
            areEqual(pages, pagesInfo.InnerHtml);
        }

        private void findNavLinks() {
            firstBtn = htmlDocument.FindAnchorElement("a[id='firstButton']");
            prevBtn = htmlDocument.FindAnchorElement("a[id='previousButton']");
            nextBtn = htmlDocument.FindAnchorElement("a[id='nextButton']");
            lastBtn = htmlDocument.FindAnchorElement("a[id='lastButton']");
            pagesInfo = htmlDocument.FindAnchorElement("a[id='pagesInfo']");
        }

        protected string navLink(string displayName, int? pageIndex = null, bool isDisabled = false) {
            var disabled = isDisabled ? "disabled" : string.Empty;
            var id = displayName.ToLower() + "Button";

            return
                $"<a id=\"{id}\" href=\"{baseUrl()}/Index?handler=Index&amp;pageIndex={pageIndex}\" class=\"btn btn-link {disabled}\">{displayName}</a>";
        }

    }

}