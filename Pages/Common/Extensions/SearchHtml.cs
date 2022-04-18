﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abc.Pages.Common.Extensions {
    public static class SearchHtml {
        internal const string backToFullList = "Back to full list";

        public static IHtmlContent Search(
            this IHtmlHelper h,
            string filter,
            string linkToFullList,
            string text = backToFullList) {

            var s = htmlStrings(filter, linkToFullList, text);

            return new HtmlContentBuilder(s);
        }

        internal static List<object> htmlStrings(
            string filter,
            string linkToFullList,
            string text) {

            var htmlStrings = new List<object> {
                new HtmlString("<form asp-action=\"./Index\" method=\"get\">"),
                new HtmlString("<div class=\"form-inline col-md-6\">"),
                new HtmlString("Find by:"),
                new HtmlString("&nbsp;"),
                new HtmlString($"<input class=\"form-control\" type=\"text\" name=\"SearchString\" value=\"{filter}\" />"),
                new HtmlString("&nbsp;"),
                new HtmlString("<input type=\"submit\" value=\"Search\" class=\"btn btn-default\" />"),
                new HtmlString("&nbsp;"),
                new HtmlString($"<a href=\"{linkToFullList}\">{text}</a>"),
                new HtmlString("</div>"),
                new HtmlString("</form>")
            };
            return htmlStrings;
        }
    }
}
