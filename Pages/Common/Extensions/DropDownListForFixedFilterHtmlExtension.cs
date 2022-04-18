using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Abc.Pages.Common.Extensions.Assist;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abc.Pages.Common.Extensions
{
    public static class DropDownListForFixedFilterHtmlExtension {

        public static IHtmlContent ShowFixedFilterDropDown<TModel, TResult>(this IHtmlHelper<TModel> h,
            Expression<Func<TModel, TResult>> e, IEnumerable<SelectListItem> items, IIndexTable<TModel> page) {
            var s = HtmlStrings(h, e, items, page);
            return new HtmlContentBuilder(s);
        }

        internal static List<object> HtmlStrings<TModel, TResult>(IHtmlHelper<TModel> h, Expression<Func<TModel, TResult>> e, IEnumerable<SelectListItem> items, IIndexTable<TModel> page) {

            var l = new List<object>();

            l.Add(new HtmlString("<form id=\"indexForm\" asp-page=\"./Index\" method=\"get\">"));
            l.Add(new HtmlString($"<input type=\"hidden\" name=\"SearchString\" value=\"{page.SearchString}\" />"));
            l.Add(new HtmlString($"<input type=\"hidden\" name=\"FixedFilter\" value=ClassificatorType />"));
            l.Add(new HtmlString("<input type=\"hidden\" name=\"Handler\" value=\"Index\" />"));

            l.Add(new HtmlString("<div class=\"form-inline\">"));
            l.Add(new HtmlString("<p>"));
            l.Add(new HtmlString("Select Type:  "));
            l.Add(new HtmlString("&nbsp"));
            l.Add(h.DropDownListFor(e,
                items, "Select Filter",
                new { @class="form-control", @name="FixedValue", @value="@Model.FixedValue", @onchange="submit()" }));
            l.Add(new HtmlString("</p>"));
            l.Add(new HtmlString("</div>"));
            l.Add(new HtmlString("</form>"));

            return l;
        }

        internal static List<object> HtmlStrings2<TModel, TResult>(IHtmlHelper<TModel> h, Expression<Func<TModel, TResult>> e, IEnumerable<SelectListItem> items, IIndexTable<TModel> page) {
            var a = new HtmlAssist();
            var c = new HtmlContainer(new []{a.IdAttribute("indexForm"), a.AspPageAttribute("./Index"), a.MethodAttribute("get")}, "form");
            c.AddContent(a.Input("SearchString", page.SearchString));
            c.AddContent(a.Input("FixedFilter", "ClassificatorType"));
            c.AddContent(a.Input("Handler", "Index"));
            c.AddContainer(new []{a.ClassAttribute("form-inline")});
            c.AddContainer(null, "p");
            c.AddText("Select Type:  ");
            c.AddText("&nbsp");
            c.AddContent(h.DropDownListFor(e, items, "Select Filter",
                new { @class = "form-control", @name = "FixedValue", @value = "@Model.FixedValue", @onchange = "submit()" }));
            c.CloseHtmlContainer("p");
            c.CloseHtmlContainer("div");
            return c.CloseHtmlContainer("form");
        }
    }
}