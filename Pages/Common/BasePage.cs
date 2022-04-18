using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Abc.Aids.Reflection;
using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Pages.Common.Consts;

namespace Abc.Pages.Common {

    public abstract class BasePage<TR, TO, TD> : BasePageAids, IPageUrl
        where TR : class, IRepo<TO> where TO : IBaseEntity<TD> where TD : BaseData, new() {
        protected TR db { get; }
        protected internal BasePage(TR r) => db = r;
        public string SortOrder { get => db.SortOrder; set => db.SortOrder = value; }
        public string SearchString { get => db.SearchString; set => db.SearchString = value; }
        public string CurrentFilter { get => db.CurrentFilter; set => db.CurrentFilter = value; }
        public string FixedValue { get => db.FixedValue; set => db.FixedValue = value; }
        public string FixedFilter { get => db.FixedFilter; set => db.FixedFilter = value; }
        public string[] FixedValues { get => db.FixedValues; set => db.FixedValues = value; }
        public int PageIndex { get => db.PageIndex; set => db.PageIndex = value; }
        public bool HasFixedFilter => !string.IsNullOrWhiteSpace(FixedFilter);
        protected internal abstract string pageUrl { get; }
        public Uri PageUrl => new(pageUrl, UriKind.Relative);
        public Uri CreateUrl => createUrl();
        public Uri IndexUrl => indexUrl();

        public Uri SortUrl(Expression<Func<TD, object>> e) => indexUri(getSortOrder(GetMember.Name(e)));
        protected internal Uri createUrl() => toUri(createHandlerUrl + attributes);
        protected internal string attributes => pageIndexUrl() + sortOrderUrl() + filters;
        protected internal string filters => searchStringUrl() + currentFilterUrl() + fixedFilters;
        protected internal string fixedFilters => fixedFilterUrl() + fixedValueUrl();

        protected internal virtual string getSortOrder(string name) {
            if (string.IsNullOrEmpty(SortOrder)) return name;
            if (!SortOrder.StartsWith(name, StringComparison.InvariantCulture)) return name;
            if (SortOrder.EndsWith("_desc", StringComparison.InvariantCulture)) return name;
            return name + "_desc";
        }

        protected internal Uri indexUrl() => toUri(indexHandlerUrl+ fixedFilters);
        protected internal Uri indexUri(string sortOrder) => toUri(indexHandlerUrl + sortOrderUrl(sortOrder) + filters);
        protected internal Uri createUri(int i) => toUri(createUrl() + switchUrl(i));
        protected internal static Uri toUri(string s) => new(s, UriKind.Relative);

        protected internal string indexHandlerUrl => handlerUrl(Actions.Index);
        protected internal string createHandlerUrl => handlerUrl(Actions.Create);
        protected internal string editHandlerUrl => handlerUrl(Actions.Edit);
        protected internal string detailsHandlerUrl => handlerUrl(Actions.Details);
        protected internal string deleteHandlerUrl => handlerUrl(Actions.Delete);
        protected internal string selectHandlerUrl() => handlerUrl(Actions.Select);
        protected internal string handlerUrl(string handler) => $"{PageUrl}/{handler}?handler={handler}";

        protected internal string pageIndexUrl(int i = int.MinValue) => $"&pageIndex={ pageIndex(i)}";
        protected internal string sortOrderUrl(string s = null) => $"&sortOrder={s ?? SortOrder}";
        protected internal string searchStringUrl(string s = null) => $"&searchString={s ?? SearchString}";
        protected internal string fixedFilterUrl(string s = null) => $"&fixedFilter={s ?? FixedFilter}";
        protected internal string fixedValueUrl(string s = null) => $"&fixedValue={s??FixedValue}";
        protected internal string currentFilterUrl(string s = null) => $"&currentFilter={s??CurrentFilter}";
        protected internal string switchUrl(int i) => $"&switchOfCreate={i}";
        protected internal int pageIndex(int i) => (i == int.MinValue) ? PageIndex : i;
        protected static bool isCorrectIndex<T>(int i, IEnumerable<T> l) => i >= 0 && i < l?.Count();
    }
    public interface IPageUrl {
        Uri PageUrl { get; }
    }

}

