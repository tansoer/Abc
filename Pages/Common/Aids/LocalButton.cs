namespace Abc.Pages.Common.Aids {
    public sealed class LocalButton :IButtonHelper {
        public LocalButton(string caption, string action = null) {
            SetFields(caption, action);
        }

        public string Caption { get; private set; }
        public string Action { get; private set; }

        public string GetUrlString(Args a) => Href.Compose(a, Action, Caption);
        internal void SetFields(string caption, string action) {
            Caption = caption;
            if (action == null) { Action = caption; } 
            else { Action = action; }
        }

    }
}
