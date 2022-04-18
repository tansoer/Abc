using System;
using System.Collections.Generic;
using Abc.Aids.Extensions;
using Abc.Pages.Common.Aids;
using Abc.Pages.Common.Consts;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abc.Pages.Common.Extensions {

    public static class NavButtonsHtml {

        public static IHtmlContent NavButtons(
            this IHtmlHelper h,
            Args a,
            int? totalPages) {
            if (h == null) throw new ArgumentNullException(nameof(h));

            var s = htmlStrings(a ?? new Args(), totalPages);

            return new HtmlContentBuilder(s);

        }

        internal static List<object> htmlStrings(Args a, int? totalPages) {
            var pageIndex = a.PageIndex < 1 ? 1 : a.PageIndex;
            a.Action = "Index";
            var l = new List<object>();
            addFirst(l, a, pageIndex);
            addSeparator(l);
            addPrevious(l, a, pageIndex);
            addSeparator(l);
            addPagesInfo(l, pageIndex, totalPages);
            addSeparator(l);
            addNext(l, a, pageIndex, totalPages);
            addSeparator(l);
            addLast(l, a, pageIndex, totalPages);

            return l;
        }

        internal static void addLast(ICollection<object> l, Args a, in int? pageIndex, in int? totalPages) {
            a.ControlId = "lastButton";
            a.PageIndex = totalPages;
            var b = htmlButton(a, Captions.Last, hasNext(pageIndex, totalPages));
            l.Add(b);
        }

        internal static string hasNext(in int? pageIndex, in int? totalPages) {
            return pageIndex >= totalPages ? "disabled" : string.Empty;
        }

        internal static void addNext(ICollection<object> l, Args a, in int? pageIndex, in int? totalPages) {
            a.ControlId = "nextButton";
            a.PageIndex = pageIndex + 1;
            var b = htmlButton(a, Captions.Next, hasNext(pageIndex, totalPages));
            l.Add(b);
        }

        internal static void addPrevious(ICollection<object> l, Args a, in int? pageIndex) {
            a.ControlId = "previousButton";
            a.PageIndex = pageIndex - 1;
            var b = htmlButton(a, Captions.Previous, hasPrevious(pageIndex));
            l.Add(b);
        }

        internal static string hasPrevious(in int? pageIndex) {
            return pageIndex <= 1 ? "disabled" : string.Empty;
        }

        internal static void addFirst(ICollection<object> l, Args a, in int? pageIndex) {
            a.ControlId = "firstButton";
            a.PageIndex = 1;
            var b = htmlButton(a, Captions.First, hasPrevious(pageIndex));
            l.Add(b);
        }

        internal static void addPagesInfo(ICollection<object> l, in int? pageIndex, in int? totalPages)
            => l.Add(new HtmlString($"<a id=\"pagesInfo\">{Messages.PagesOf.Format(pageIndex, totalPages)}</a>"));

        internal static void addSeparator(ICollection<object> l) {
            l.Add(new HtmlString("&nbsp;"));
        }

        internal static HtmlString htmlButton(Args a, string title, string disabled) {
            if (a is not null) {
                a.ItemId = null;
                a.Title = title;
                a.Handler = Actions.Index;
                a.Disabled = disabled;
            }

            var s = Href.Compose(a);

            return new HtmlString(s);
        }

    }

}
