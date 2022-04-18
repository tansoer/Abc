using System;

namespace Abc.Pages.Common.Aids {
    public sealed class ForeignButton :IButtonHelper {
        public ForeignButton(string caption, string action, Uri url, string fixedFilter) {
            Caption = caption;
            Action = action;
            ForeignUrl = url;
            FixedFilter = fixedFilter;
        }
        public string Caption { get; private set; }
        public string Action { get; private set; }
        public Uri ForeignUrl { get; private set; }
        public string FixedFilter { get; private set; }

        public string GetUrlString(Args a) => Href.ComposeForeign(a, ForeignUrl, Action, Caption, FixedFilter);
    }
}
