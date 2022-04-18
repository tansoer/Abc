using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Abc.Pages.Common.Extensions {

    public static class EditorHtml {

        public static IHtmlContent Editor<TModel, TResult>(
            this IHtmlHelper<TModel> h, Expression<Func<TModel, TResult>> e, Dictionary<string, object> attributes = null) {
            var n = h.DisplayNameFor(e);
            return Editor(h, e, n, attributes);
        }
        public static IHtmlContent Editor<TModel, TResult>(this IHtmlHelper<TModel> h,
            Expression<Func<TModel, TResult>> e, string displayName, Dictionary<string, object> attributes = null) {
            var s = htmlStrings(h, e, displayName, attributes);
            return new HtmlContentBuilder(s);
        }
        internal static bool isRequired<TModel, TResult>(Expression<Func<TModel, TResult>> e) {
            var m = e.Body as MemberExpression;
            return m?.Member.CustomAttributes.ToList()
                .Any(x => x.AttributeType == typeof(RequiredAttribute)) ?? false;
        }
        internal static Dictionary<string, object> convert(object content) {
            var props = content.GetType().GetProperties();
            var pairDictionary = props.ToDictionary(x => x.Name, x => x.GetValue(content, null));
            return pairDictionary;
        }
        internal static Dictionary<string, object> createAtributes<TModel, TResult>(Expression<Func<TModel, TResult>> e, string displayName) {
            var d = convert(new { @class = "form-control" });
            if (isRequired(e)) {
                d.Add("data-val", "true");
                d.Add("data-val-required", $"The {displayName} field is required.");
            }
            return d;
        }
        internal static List<object> htmlStrings<TModel, TResult>(IHtmlHelper<TModel> h,
            Expression<Func<TModel, TResult>> e, string displayName, Dictionary<string, object> attributes = null) {
            attributes ??= createAtributes(e, displayName);
            var editorFor = h.EditorFor(e, new { htmlAttributes = attributes });
            return htmlStrings(h, e, editorFor, displayName);
        }
        internal static List<object> htmlStrings<TModel, TResult>(IHtmlHelper<TModel> h,
            Expression<Func<TModel, TResult>> e, IHtmlContent editorFor, string displayName) => new() {
                new HtmlString("<dd class=\"col-sm-2\">"),
                h.Raw(displayName),
                new HtmlString("</dd>"),
                new HtmlString("<dd class=\"col-sm-10\">"),
                editorFor,
                h.ValidationMessageFor(e, "", new { @class = "text-danger" }),
                new HtmlString("</dd>")
            };
    }
    public static class EmailForHtml {

        public static IHtmlContent EmailFor<TModel, TResult>(
            this IHtmlHelper<TModel> h, Expression<Func<TModel, TResult>> e) {
            var n = h.DisplayNameFor(e);
            return EmailFor(h, e, n);
        }
        public static IHtmlContent EmailFor<TModel, TResult>(this IHtmlHelper<TModel> h,
            Expression<Func<TModel, TResult>> e, string displayName) {
            var d = EditorHtml.createAtributes(e, displayName);
            d.Add("type", "email");
            return h.Editor(e, displayName, d);
        }
    }
    public static class WebForHtml {

        public static IHtmlContent WebFor<TModel, TResult>(
            this IHtmlHelper<TModel> h, Expression<Func<TModel, TResult>> e) {
            var n = h.DisplayNameFor(e);
            return WebFor(h, e, n);
        }
        public static IHtmlContent WebFor<TModel, TResult>(this IHtmlHelper<TModel> h,
            Expression<Func<TModel, TResult>> e, string displayName) {
            var d = EditorHtml.createAtributes(e, displayName);
            d.Add("type", "url");
            return h.Editor(e, displayName, d);
        }
    }
    public static class PhoneForHtml {

        public static IHtmlContent PhoneFor<TModel, TResult>(
            this IHtmlHelper<TModel> h, Expression<Func<TModel, TResult>> e, byte minLength, byte maxLength = 0) {
            var n = h.DisplayNameFor(e);
            return PhoneFor(h, e, n, minLength, maxLength);
        }
        public static IHtmlContent PhoneFor<TModel, TResult>(this IHtmlHelper<TModel> h,
            Expression<Func<TModel, TResult>> e, string displayName, byte minLength, byte maxLength=0) {
            if (maxLength == 0) maxLength = minLength; 
            var d = EditorHtml.createAtributes(e, displayName);
            d.Add("type", "number");
            d.Add("step", "1");
            d.Add("minlength", $"{minLength}");
            d.Add("maxlength", $"{maxLength}");
            return h.Editor(e, displayName, d);
        }
    }
    public static class NumberForHtml {
        public static IHtmlContent NumberFor<TModel, TResult>(
            this IHtmlHelper<TModel> h, Expression<Func<TModel, TResult>> e, int min = 0, int max = int.MaxValue) {
            var n = h.DisplayNameFor(e);
            return NumberFor(h, e, n, min, max);
        }
        public static IHtmlContent NumberFor<TModel, TResult>(this IHtmlHelper<TModel> h,
            Expression<Func<TModel, TResult>> e, string displayName, int min = 0, int max = int.MaxValue) {
            var d = EditorHtml.createAtributes(e, displayName);
            d.Add("type", "number");
            d.Add("min", $"{min}");
            d.Add("max", $"{max}");
            return h.Editor(e, displayName, d);
        }
    }
}
