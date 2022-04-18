﻿using System.Collections.Generic;
using System.Linq;
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abc.Pages.Common.Extensions {

    public static class LinkHtml {

        public static IHtmlContent Link(
            this IHtmlHelper htmlHelper, string text, params Link[] items) {
            var s = htmlStrings(text, items);

            return new HtmlContentBuilder(s);
        }
        internal static List<object> htmlStrings(string text, Link[] items) {
            var l = new List<object> {
                new HtmlString("<p>"),
                new HtmlString($"<a>{text}</a>")
            };

            l.AddRange(
                items.Select(item => new HtmlString($"<a> </a><a href=\"{item.Url}\">{item.DisplayName}</a>")));

            l.Add(new HtmlString("</p>"));

            return l;
        }

        public static IHtmlContent Link(
            this IHtmlHelper htmlHelper, params Link[] items) {
            var s = htmlStrings(items);

            return new HtmlContentBuilder(s);
        }

        internal static List<object> htmlStrings(Link[] items) {
            var l = new List<object>();

            l.AddRange(
                items.Select(item => new HtmlString($"<a href=\"{item.Url}\">{item.DisplayName}</a>")));

            return l;
        }
    }
}
