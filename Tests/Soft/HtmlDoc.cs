// (c) https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-3.1

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Io;
using HttpMethod = System.Net.Http.HttpMethod;

namespace Abc.Tests.Soft {

    public static class HtmlDoc {
        public static async Task<IHtmlDocument> GetAsync(HttpResponseMessage response) {
            var content = await response.Content.ReadAsStringAsync();
            var document = await BrowsingContext.New()
                .OpenAsync(responseFactory, CancellationToken.None);
            return (IHtmlDocument) document;
            void responseFactory(VirtualResponse htmlResponse) {
                htmlResponse
                    .Address(response.RequestMessage.RequestUri)
                    .Status(response.StatusCode);

                mapHeaders(response.Headers);
                mapHeaders(response.Content.Headers);

                htmlResponse.Content(content);

                void mapHeaders(HttpHeaders headers) {
                    foreach (var header in headers) {
                        foreach (var value in header.Value) { htmlResponse.Header(header.Key, value); }
                    }
                }
            }
        }
        private const string action = "formaction";
        public static HttpRequestMessage ComposeRequestMessage<TData>(
            IHtmlFormElement form, IHtmlElement submitButton, TData item) {
            if (item is not null) foreach (var p in typeof(TData).GetProperties()) {
                    var val = p.GetValue(item);
                    var v = HtmlHelper.Value(val, true);
                    var id = $"Item_{p.Name}";
                    var element = form.Elements.GetElementById(id);
                    if (element is null) continue;
                    if (element is IHtmlSelectElement s) s.Value = v;
                    else if (element is IHtmlInputElement i) {
                        if (p.PropertyType == typeof(bool)) i.IsChecked = v == "true";
                        else if (i.Type == "datetime-local") i.Value =v + "T00:00:00.000";
                        else i.Value = v;
                    }
                }
            var submit = form.GetSubmission(submitButton);
            var target = (Uri) submit.Target;
            if (submitButton.HasAttribute(action)) {
                var formaction = submitButton.GetAttribute(action);
                target = new Uri(formaction, UriKind.Relative);
            }
            var message = new HttpRequestMessage(new HttpMethod(submit.Method.ToString()), target) {
                Content = new StreamContent(submit.Body)
            };
            foreach (var (key, value) in submit.Headers) {
                message.Headers.TryAddWithoutValidation(key, value);
                message.Content.Headers.TryAddWithoutValidation(key, value);
            }
            return message;
        }
        public static HttpRequestMessage ComposeRequestMessage<TData, TRelatedData>(
        IHtmlFormElement form, IHtmlElement submitButton, TData item, TRelatedData relatedItem,
        string relatedItemName) {
            if (item is not null) foreach (var p in typeof(TData).GetProperties()) {
                    var v = HtmlHelper.Value(p.GetValue(item), true);
                    var id = $"Item_{p.Name}";
                    var element = form.Elements.GetElementById(id);
                    if (element is null) continue;
                    if (element is IHtmlSelectElement s) s.Value = v;
                    else if (element is IHtmlInputElement i) i.Value = v;
            }
            if (relatedItem is not null)
                foreach (var p in typeof(TRelatedData).GetProperties()) {
                    var v = HtmlHelper.Value(p.GetValue(relatedItem), true);
                    var id = $"{relatedItemName}_{p.Name}";
                    var element = form.Elements.GetElementById(id);

                    if (element is null) continue;
                    if (element is IHtmlSelectElement s) s.Value = v;
                    else if (element is IHtmlInputElement i) i.Value = v;
                }
            var submit = form.GetSubmission(submitButton);
            var target = (Uri)submit.Target;
            if (submitButton.HasAttribute(action)) {
                var formaction = submitButton.GetAttribute(action);
                target = new Uri(formaction, UriKind.Relative);
            }
            var message = new HttpRequestMessage(new HttpMethod(submit.Method.ToString()), target) {
                Content = new StreamContent(submit.Body)
            };
            foreach (var (key, value) in submit.Headers) {
                message.Headers.TryAddWithoutValidation(key, value);
                message.Content.Headers.TryAddWithoutValidation(key, value);
            }
            return message;
        }
        public static HttpRequestMessage ComposeRequestMessage(IHtmlFormElement form,
            IHtmlElement submitButton) {
            return ComposeRequestMessage<object>(form, submitButton, null);
        }
        public static IHtmlAnchorElement FindAnchorElement(this IHtmlDocument doc, string tag)
            => doc.FindElement(tag) as IHtmlAnchorElement;
        public static IHtmlFormElement FindFormElement(this IHtmlDocument doc, string tag)
            => doc.FindElement(tag) as IHtmlFormElement;
        public static IHtmlInputElement FindInputElement(this IHtmlDocument doc, string tag)
            => doc.FindElement(tag) as IHtmlInputElement;
        public static IHtmlElement FindElement(this IHtmlDocument doc, string tag)
            => (IHtmlElement) doc.QuerySelector(tag);
    }
}
