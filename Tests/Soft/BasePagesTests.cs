using Abc.Aids.Extensions;
using Abc.Data.Common;

namespace Abc.Tests.Soft {

    public abstract class BasePagesTests<TData> : BasePageTests where TData : BaseData {
        protected TData item;
        protected bool isCreatePage => pageName == "Create";
        protected bool isEditPage => pageName == "Edit";
        protected bool isDeletePage => pageName == "Delete";
        protected bool isDetailsPage => pageName == "Details";
        protected bool isIndexPage => pageName == "Index";
        protected string pageName => GetType().Name.SubstringBefore("PageTests");
        protected abstract string baseUrl();
        protected override string pageUrl() => baseUrl() + pageHandlerUrl(pageName);
        protected string indexPageUrl() => baseUrl() + pageHandlerUrl("Index");
        protected virtual string pageHandlerUrl(string s) {
            var h = s == "Select" ? "Index" : s;
            var u = $"/{h}?handler={h}";
            return s is "Index" or "Create" ? u : $"{u}&id={getItemId(item)}";
        }
        protected virtual string getItemId(TData d) => d.Id;
        protected virtual void setItemId(TData d, string id) => d.Id = id;
    }
}
