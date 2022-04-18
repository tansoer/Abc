using System;
using System.Collections.Generic;
using Abc.Pages.Common.Aids;
using Abc.Pages.Common.Consts;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abc.Pages.Common.Extensions {


    public static class RowHtml {

        public static IHtmlContent Row(
            this IHtmlHelper h,
            bool hasSelect,
            Uri pageUrl,
            string itemId,
            params IHtmlContent[] values) {
            if (h == null) throw new ArgumentNullException(nameof(h));

            var a = new Args(pageUrl, itemId);

            return row(hasSelect, a, values);
        }

        public static IHtmlContent Row(
            this IHtmlHelper h,
            bool hasSelect,
            Uri pageUrl,
            string itemId,
            string fixedFilter,
            string fixedValue,
            params IHtmlContent[] values) {

            if (h == null) throw new ArgumentNullException(nameof(h));

            var a = new Args(pageUrl, itemId, fixedFilter, fixedValue);

            return row(hasSelect, a, values);
        }


        public static IHtmlContent Row(
            this IHtmlHelper h,
            bool hasSelect,
            Uri pageUrl,
            string itemId,
            string sortOrder,
            string searchString,
            int pageIndex,
            string fixedFilter,
            string fixedValue,
            params IHtmlContent[] values) {

            if (h == null) throw new ArgumentNullException(nameof(h));

            var a = new Args(pageUrl, itemId, fixedFilter, fixedValue, sortOrder, searchString, pageIndex);

            return row(hasSelect, a, values);
        }

        public static IHtmlContent Row<TPage>(
            this IHtmlHelper h,
            IIndexTable<TPage> page,
            params IHtmlContent[] values) {

            if (h == null) throw new ArgumentNullException(nameof(h));

            var a = new Args(page.PageUrl, page.ItemId, page.FixedFilter,
                page.FixedValue, page.SortOrder, page.SearchString, page.PageIndex);

            return row(page, a, values);
        }


        internal static IHtmlContent row<TPage>(IIndexTable<TPage> page,
            Args a, params IHtmlContent[] values) {
            var s = htmlStrings(page, a, values);

            return new HtmlContentBuilder(s);
        }

        internal static List<object> htmlStrings<TPage>(IIndexTable<TPage> page,
            Args a, params IHtmlContent[] values) {
            var list = new List<object>();
            foreach (var value in values) addValue(list, value);
            list.Add(new HtmlString("<td>"));

            addButtons(page, a, list);

            list.Add(new HtmlString("</td>"));

            return list;
        }

        internal static IHtmlContent row(bool hasSelect, Args a, params IHtmlContent[] values) {
            var s = htmlStrings(hasSelect, a, values);

            return new HtmlContentBuilder(s);
        }

        internal static List<object> htmlStrings(bool hasSelect, Args a, params IHtmlContent[] values) {
            var list = new List<object>();
            foreach (var value in values) addValue(list, value);
            list.Add(new HtmlString("<td>"));

            if (hasSelect) {
                list.Add(new HtmlString(Href.Compose(a, Actions.Index, Captions.Select)));
                list.Add(new HtmlString("&nbsp;"));
            }

            list.Add(new HtmlString(Href.Compose(a, Actions.Edit, Captions.Edit)));
            list.Add(new HtmlString("&nbsp;"));
            list.Add(new HtmlString(Href.Compose(a, Actions.Details, Captions.Details)));
            list.Add(new HtmlString("&nbsp;"));
            list.Add(new HtmlString(Href.Compose(a, Actions.Delete, Captions.Delete)));
            list.Add(new HtmlString("</td>"));

            return list;
        }

        internal static void addValue(List<object> htmlStrings, IHtmlContent value) {
            if (htmlStrings is null) return;
            if (value is null) return;
            htmlStrings.Add(new HtmlString("<td>"));
            htmlStrings.Add(value);
            htmlStrings.Add(new HtmlString("</td>"));
        }

        internal static void addButtons<TPage>(IIndexTable<TPage> page,
            Args a, List<object> htmlStrings) {

            for (int i = 0; i < page.ButtonsCount; i++) {
                var b = page.GetButton(i);
                htmlStrings.Add(new HtmlString(b.GetUrlString(a)));
                if (i + 1 != page.ButtonsCount) htmlStrings.Add(new HtmlString("&nbsp;"));
            }
        }
    }
}
