using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Abc.Pages.Common.Extensions {
    public static class ItemHtmlExtension {
        public static IHtmlContent ShowItem<TPage>(this IHtmlHelper<TPage> h,
            IShowItem<TPage> page) {
            var s = ShowItemStrings(h, page);
            return new HtmlContentBuilder(s);
        }
        public static IHtmlContent EditItem<TPage>(this IHtmlHelper<TPage> h,
            IEditItem<TPage> page) {
            var s = EditItemStrings(h, page);
            return new HtmlContentBuilder(s);
        }

        internal static List<object> EditItemStrings<TPage>(this IHtmlHelper<TPage> h,
            IEditItem<TPage> page) {
            var l = new List<object>();
            for (int i = 0; i < page.Hidden.Count; i++) l.Add(page.HiddenFor(h, i));
            l.Add(new HtmlString("<dl class=\"row\">"));
            for (int i = 0; i < page.Editors.Count; i++) l.Add(page.EditorFor(h, i));
            l.Add(new HtmlString("</dl>"));
            return l;
        }
        internal static List<object> ShowItemStrings<TPage>(this IHtmlHelper<TPage> h,
            IShowItem<TPage> page) {
            var l = new List<object>();
            l.Add(new HtmlString("<dl class=\"row\">"));
            for (int i = 0; i < page.Viewers.Count; i++) l.Add(page.ViewerFor(h, i));
            l.Add(new HtmlString("</dl>"));
            return l;
        }
    }
    public interface IShowItem<TPage> {
        public List<ExpressionHelper> Viewers { get; }
        IHtmlContent ViewerFor(IHtmlHelper<TPage> h, int i);
    }
    public interface IEditItem<TPage> {
        public List<ExpressionHelper> Hidden { get; }
        public List<ExpressionHelper> Editors { get; }
        IHtmlContent EditorFor(IHtmlHelper<TPage> h, int i);
        IHtmlContent HiddenFor(IHtmlHelper<TPage> h, int i);
    }
}

