
using System;

namespace Abc.Pages.Common.Aids {

    public static class Href {

        public static string Compose(Args a, string handler, string title) {
            if (a is null) return Compose(null);
            a.Handler = handler;
            a.Action = handler;
            a.Title = title;

            return Compose(a);
        }

        public static string Compose(Args args) {
            var s = addPage(args?.PageUrl, args?.ControlId)
                + addAction(args?.Action)
                + addHandler(args?.Handler)
                + addItemId(args?.ItemId)
                + addFixedFilter(args?.FixedFilter)
                + addFixedValue(args?.FixedValue)
                + addSearchString(args?.SearchString)
                + addCurrentFilter(args?.CurrentFilter)
                + addSortOrder(args?.SortOrder)
                + addPageIndex(args?.PageIndex);
            return remove(s) + addClass(args?.Disabled) + addTitle(args?.Title);
        }

        public static string ComposeForeign(Args a, Uri foreignUrl, string handler, string caption,
            string fixedFilter) {
            if (a is null) return Compose(null);

            a.PageUrl = foreignUrl;
            a.Handler = handler;
            a.Action = handler;
            a.Title = caption;
            a.FixedFilter = fixedFilter;
            a.FixedValue = a.ItemId;
            a.ItemId = null;

            return Compose(a);
        }

        internal static string addPage(Uri s, string id = null) =>
            s is null ? "<a href=\"#" :
            id is null ? $"<a href=\"{s}" :
            $"<a id=\"{id}\" href=\"{s}";

        internal static string addAction(string s) => s is null ? "?" : $"/{s}?";

        internal static string addHandler(string s) => s is null ? string.Empty : $"handler={s}";

        internal static string addItemId(string s) => s is null ? string.Empty : $"&id={s}";

        internal static string addFixedFilter(string s) => s is null ? string.Empty : $"&fixedFilter={s}";

        internal static string addFixedValue(string s) => s is null ? string.Empty : $"&fixedValue={s}";

        internal static string addSortOrder(string s) => s is null ? string.Empty : $"&sortOrder={s}";

        internal static string addSearchString(string s) => s is null ? string.Empty : $"&searchString={s}";

        internal static string addCurrentFilter(string s) => s is null ? string.Empty : $"&currentFilter={s}";

        internal static string addPageIndex(int? s) => s is null ? string.Empty : $"&pageIndex={s}";

        internal static string addClass(string s) => s is null ? string.Empty : $"\" class=\"btn btn-link {s}";

        internal static string addTitle(string s) {
            return s is null ? "\"></a>" : $"\">{s}</a>";
        }

        internal static string remove(string s) {
            if (s is null) return null;
            if (s.EndsWith("?", StringComparison.InvariantCulture)) s = s[..^1];

            return s;
        }

    }

}
