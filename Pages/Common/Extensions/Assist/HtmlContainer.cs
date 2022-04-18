using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Html;

namespace Abc.Pages.Common.Extensions.Assist {
    public class HtmlContainer {
        internal List<object> strings;
        internal HtmlAssist assist;
        public HtmlContainer(string[] attributes = null, string containerType = null) {
            assist = new HtmlAssist();
            if (containerType is null) strings = new List<object>() {
                assist.StartContainer("div", attributes)
            };
            else {
                strings = new List<object>() {
                    assist.StartContainer(containerType, attributes)
                };
            }
        }
        public List<object> CloseHtmlContainer() {
            var idx = strings[0].ToString().IndexOf(' ');
            if (idx < 0) idx = strings[0].ToString().IndexOf('>');
            var startTag = strings[0].ToString().Remove(idx);
            var startTagCount = strings.Count(x => x.ToString().Contains(startTag));
            var endTagCount = strings.Count(x => x.ToString().Contains(modifyToEndTag(startTag)));
            var endTag = $"{modifyToEndTag(startTag)}>";
            for (int i = 0; i < startTagCount - endTagCount; i++) strings.Add(endTag);

            return strings;
        }
        public List<object> CloseHtmlContainer(string containerType) {
            strings.Add($"</{containerType}>");
            return strings;
        }
        private string modifyToEndTag(string tag) => $"</{tag[1..]}";

        public void AddContainer(string[] attributes, string containerType = null) {
            strings.Add(containerType is null
                ? assist.StartContainer("div", attributes)
                : assist.StartContainer(containerType, attributes));
        }
        public void AddContent(List<HtmlString> content) {
            foreach (var str in content) strings.Add(str);
        }
        public void AddContent(IHtmlContent str) => strings.Add(new HtmlString(str.ToString()));
        public void AddText(string str) => strings.Add(new HtmlString(str));
    }
}