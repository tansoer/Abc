using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abc.Pages.Common.Extensions {

    public static class ShowHtml {
        public static IHtmlContent Show<TModel, TResult>(
            this IHtmlHelper<TModel> h,
            Expression<Func<TModel, TResult>> e) {
            var label = h.DisplayNameFor(e);
            var value = getValue(h, e);
            return Show(h, label, value);
        }
        private static string getValue<TModel, TResult>(IHtmlHelper<TModel> h, Expression<Func<TModel, TResult>> e) {
            var value = h.DisplayFor(e);
            var writer = new System.IO.StringWriter();
            value.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }
        public static IHtmlContent Show<TModel, TResult>(this IHtmlHelper<TModel> h,  
            Expression<Func<TModel, TResult>> e, string value) {
            var label = h.DisplayNameFor(e);
            return Show(h, label, value);
        }
        public static IHtmlContent ShowCaption<TModel, TResult>(this IHtmlHelper<TModel> h,
            Expression<Func<TModel, TResult>> e, string label, string value = null) {
            value ??= getValue(h, e);
            return Show(h, label, value);
        }
        public static IHtmlContent Show<TModel>( this IHtmlHelper<TModel> h,
           string label, string value) {
            if (h == null) throw new ArgumentNullException(nameof(h));
            var s = htmlStrings(h, label, value);
            return new HtmlContentBuilder(s);
        }
        internal static List<object> htmlStrings<TModel>(
            IHtmlHelper<TModel> h, string label, string value) {
            return new List<object> {
                new HtmlString("<dd class=\"col-sm-2\">"),
                h.Raw(label),
                new HtmlString("</dd>"),
                new HtmlString("<dd class=\"col-sm-10\">"),
                h.Raw(value),
                new HtmlString("</dd>")
            };
        }
    }
}
