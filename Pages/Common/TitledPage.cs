using System;
using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Constants;
using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Facade.Common;
using Abc.Pages.Common.Aids;
using Abc.Pages.Common.Consts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abc.Pages.Common {

    public abstract class TitledPage<TP, TR, TO, TV, TD> : SortedPage<TP, TR, TO, TV, TD>
        where TP : PageModel where TR : class, IRepo<TO> where TO : IBaseEntity<TD>
        where TV : BaseView where TD : BaseData, new() {
        protected internal TitledPage(TR r) : base(r) { }
        protected abstract string title { get; }
        public string Title => title;

        public string SubTitle => FixedValue is null ? string.Empty : $"{subtitle}";
        protected internal virtual string subtitle => string.Empty;
        public virtual bool IsMasterDetail() => SubTitle != string.Empty;
        public string MasterUri() => masterUri() ?? "javascript:history.go(-1)";
        public string masterUri() {
            var c = masterPage();
            if (c == null) return null;
            var args = new Args(c.PageUrl, getName(), FixedFilter, FixedValue,
                SortOrder, SearchString, PageIndex);
            var url = Href.Compose(args, Actions.Index, Captions.Edit);
            url = url.Replace("<a href=\"", string.Empty);
            url = url.Replace($"\">{Captions.Edit}</a>", string.Empty);
            return url;
        }

        private string getName() 
            => (Item as EntityBaseView)?.Name ?? Item?.Id ?? Word.Unspecified;

        protected internal virtual IPageUrl masterPage() => null;
        protected internal string composedFixedFilter(string fixedFilter)
            => $"{typeof(TP).Name}.{fixedFilter}";
    }
}
