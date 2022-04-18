using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Abc.Pages.Common.Extensions.Assist;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abc.Pages.Common.Extensions {
    public static class DropDownHtml {
        public static IHtmlContent DropDown<TModel, TResult>( this IHtmlHelper<TModel> h,
            Expression<Func<TModel, TResult>> e, IEnumerable<SelectListItem> items, string caption = null) {
            var label = caption ?? h.DisplayNameFor(e);
            var s = htmlStrings(h, e, label, items);
            return new HtmlContentBuilder(s);
        }
        internal static List<object> htmlStrings<TModel, TResult>(IHtmlHelper<TModel> h,
            Expression<Func<TModel, TResult>> e, string label, IEnumerable<SelectListItem> items) {
            return new List<object> {
                new HtmlString("<dd class=\"col-sm-2\">"),
                h.Raw(label),
                new HtmlString("</dd>"),
                new HtmlString("<dd class=\"col-sm-10\">"),
                h.DropDownListFor(e, items, new {@class = "form-control"}),
                h.ValidationMessageFor(e, "", new {@class = "text-danger"}),
                new HtmlString("</dd>")
            };
        }

        internal static List<object> htmlStrings2<TModel, TResult>(IHtmlHelper<TModel> h,
            Expression<Func<TModel, TResult>> e, string label, IEnumerable<SelectListItem> items) {
            var a = new HtmlAssist();
            var c = new HtmlContainer(new []{ a.ClassAttribute("col-sm-2"), "dd"});
            c.AddContent(h.Raw(label));
            c.CloseHtmlContainer();
            c.AddContainer(new []{ a.ClassAttribute("col-sm-10")}, "dd");
            c.AddContent(h.DropDownListFor(e, items, new { @class = "form-control" }));
            c.AddContent(h.ValidationMessageFor(e, "", new { @class = "text-danger" }));
            return c.CloseHtmlContainer();
        }
    }
}
