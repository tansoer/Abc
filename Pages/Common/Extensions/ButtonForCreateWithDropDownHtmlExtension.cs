using Abc.Pages.Common.Consts;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Abc.Pages.Common.Extensions.Assist;

namespace Abc.Pages.Common.Extensions {
    public static class ButtonForCreateWithDropDownHtmlExtension {
        public static IHtmlContent ShowCreateDropDown<TPage, TEnum>(
            this IHtmlHelper<TPage> h,
            IButtonForCreateDropDown<TPage, TEnum> page) {
            var s = HtmlStrings(page);
            return new HtmlContentBuilder(s);
        }
        internal static List<object> HtmlStrings<TPage, 
            TEnum>(IButtonForCreateDropDown<TPage, TEnum> page) {
            var l = new List<object> {
                new HtmlString("<div class=\"dropdown\">"),
                new HtmlString($"<button class=\"btn btn-link\" "),
                new HtmlString($"id=\"dropdownMenuButton\"  "),
                new HtmlString($"data-toggle=\"dropdown\"> "),
                new HtmlString($"{Captions.Create} "),
                new HtmlString($"</button>"),
                new HtmlString($"<div class=\"dropdown-menu\" "),
                new HtmlString($"aria-labelledby=\"dropdownMenuButton\">")
            };
            for (int i = 0; i < page.DropDownEntryCount; i++) {
                l.Add(new HtmlString(GetDropDownRow(i, page)));
            }
            l.Add(new HtmlString("</div>"));
            l.Add(new HtmlString("</div>"));
            return l;
        }
        internal static List<object> HtmlStrings2<TPage, TEnum>(IButtonForCreateDropDown<TPage, 
            TEnum> page, IHtmlHelper<TPage> h) {
            var a = new HtmlAssist();
            var container = new HtmlContainer(new [] {a.ClassAttribute("dropdown") });
            container.AddContent(a.SelfClosingTag("button", Captions.Create, 
                new []{ a.ClassAttribute("btn btn-link"),
                    a.IdAttribute("dropdownMenuButton"),
                    a.DataToggleAttribute("dropdown")}));
            container.AddContainer(new[] { 
                a.ClassAttribute("dropdown-menu"), 
                a.Attribute("aria-labelledby", 
                "dropdownMenuButton") });
            container.AddContent(GetDropDownRows(page, a));
            return container.CloseHtmlContainer();
        }
        internal static string GetDropDownRow<TPage, TEnum>(int i, 
            IButtonForCreateDropDown<TPage, TEnum> page) {
            var s = "<a class=\"dropdown-item\" " +
                $"href=\"{page.GetDropDownEntryUri(i)}\">" +
                $"{page.GetDropDownEntryName(i)}</a>";
            return s;
        }
        internal static List<HtmlString> GetDropDownRows<TPage, 
            TEnum>(IButtonForCreateDropDown<TPage, TEnum> page, HtmlAssist a) {
            var rows = new List<HtmlString>();
            for (int i = 0; i < page.DropDownEntryCount; i++) 
                rows.Add(GetDropDownRow2(i, page, a));
            return rows;
        }
        internal static HtmlString GetDropDownRow2<TPage, TEnum>(int i, 
            IButtonForCreateDropDown<TPage, TEnum> page, HtmlAssist a) {
            var s = a.SelfClosingTag("a", page.GetDropDownEntryName(i),
                new[] {a.ClassAttribute("dropdown-item"),
                    a.HrefAttribute(page.GetDropDownEntryUri(i).ToString())});
            return s;
        }
    }
}
