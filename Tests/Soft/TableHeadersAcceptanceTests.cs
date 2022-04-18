using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Abc.Aids.Extensions;
using Abc.Aids.Reflection;
using AngleSharp.Html.Dom;

namespace Abc.Tests.Soft {
    public class TableHeadersAcceptanceTests<TView>: TestsAssertions {

        private readonly Action addToDatabase;
        private readonly Func<string> pageUrl;
        private readonly Func<string> baseUrl;
        private readonly Func<string, IHtmlDocument> sendRequest;
        private IHtmlDocument htmlDocument;
        private readonly IEnumerable<Expression<Func<TView, object>>> indexPageColumns;

        public event ChangeHtmlString DoBeforeValidation;

        public TableHeadersAcceptanceTests(Action db,
            Func<string> getPageUrl,
            Func<string> getBaseUrl,
            Func<string, IHtmlDocument> sendUrl,
            IEnumerable<Expression<Func<TView, object>>> columns
            ) {
            addToDatabase = db;
            pageUrl = getPageUrl;
            baseUrl = getBaseUrl;
            sendRequest = sendUrl;
            indexPageColumns = columns;
        }

        public void DoTest() {

            addToDatabase();

            foreach (var e in indexPageColumns) { testClickHeader(e); }
        }

        private void testClickHeader(Expression<Func<TView, object>> e) {
            var isDesc = false;
            var name = GetMember.Name(e);
            var displayName = GetMember.DisplayName(e);
            var id = displayName.ToLowerCase().RemoveSpaces() + "Column";
            htmlDocument = sendRequest(pageUrl());

            for (var i = 0; i < 3; i++) {
                var link = htmlDocument.FindAnchorElement($"a[id='{id}']");
                isNotNull(link);
                var sortOrder = name;
                sortOrder = isDesc ? sortOrder + "_desc" : sortOrder;
                var expected = $"<a id=\"{id}\" href=\"{baseUrl()}/Index?handler=Index" +
                               $"&amp;sortOrder={sortOrder}&amp;" +
                               "searchString=&amp;" +
                               "currentFilter=&amp;" +
                               "fixedFilter=&amp;" +
                               "fixedValue=\">" +
                               $"<span style=\"font-weight:normal\">{displayName}</span></a>";

                DoBeforeValidation?.Invoke(ref expected);

                areEqual(expected, link.OuterHtml);
                isDesc = !isDesc;
                htmlDocument = sendRequest(link.Href);
            }
        }

    }
}
