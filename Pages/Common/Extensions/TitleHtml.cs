using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abc.Pages.Common.Extensions {
    public static class TitleHtml {

        public static IHtmlContent Title(
            this IHtmlHelper h, string title) {
            if (h == null) throw new ArgumentNullException(nameof(h));
            h.ViewData["Title"] = title;
            var s = htmlStrings(title);
            return new HtmlContentBuilder(s);
        }

        internal static List<object> htmlStrings(string title) {
            return new List<object> {
                new HtmlString("<h1>"),
                new HtmlString(title),
                new HtmlString("</h1>")
            };
        }

    }
}
