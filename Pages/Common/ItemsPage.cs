using Abc.Aids.Constants;
using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Facade.Common;
using Abc.Pages.Common.Aids;
using Abc.Pages.Common.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Abc.Pages.Common {
    public abstract class ItemsPage<TP, TR, TO, TV, TD> :FilteredPage<TP, TR, TO, TV, TD>
        where TP : PageModel
        where TR : class, IRepo<TO>
        where TO : IBaseEntity<TD>
        where TV : BaseView
        where TD : BaseData, new() {
        protected ItemsPage(TR r) : base(r) => tableColumns();
        public IList<TV> Items { get; protected set; }
        public List<ExpressionHelper> Columns { get; } = new();
        public List<ExpressionHelper> Viewers { get; } = new();
        public int ColumnsCount => Columns?.Count ?? -1;
        public int RowsCount => Items?.Count ?? -1;
        public void SetItem(int i) {
            Item = null;
            if (isCorrectIndex(i, Items)) Item = Items[i];
        }
        public virtual string NameFor(IHtmlHelper<TP> html, int i) {
            if (!isCorrectIndex(i, Columns)) return Word.Unspecified;
            dynamic exp = Columns[i].Key;
            return html.DisplayNameFor(exp);
        }
        public virtual IHtmlContent ValueFor(IHtmlHelper<TP> h, int i) {
            if (!isCorrectIndex(i, Columns)) return null;
            if (Columns.ElementAt(i).StrValue is null) {
                dynamic exp = Columns.ElementAt(i).Key;
                return valueFor(h, exp);
            } else {
                var exp = Columns[i].StrValue;
                return h.Raw(exp.Compile().Invoke());
            }
        }
        protected virtual void tableColumns() => createFields(Columns.Add, getFieldsForColumns());
        protected virtual void valueViewers() => createFields(Viewers.Add, getFieldsForViewers());
        protected ExpressionHelper field<TResult>(Expression<Func<TP, TResult>> e, string caption = null,
            Expression<Func<IEnumerable<SelectListItem>>> selectItems = null,
            Expression<Func<string>> stringValue = null) => new(e, caption, selectItems, stringValue);
        protected ExpressionHelper field<TResult>(Expression<Func<TP, TResult>> e,
            Expression<Func<IEnumerable<SelectListItem>>> selectItems,
            Expression<Func<string>> stringValue = null) => new(e, null, selectItems, stringValue);
        protected ExpressionHelper field<TResult>(Expression<Func<TP, TResult>> e,
            Expression<Func<string>> stringValue) => new(e, null, null, stringValue);
        protected void addField<TResult>(Expression<Func<TP, TResult>> e, string caption = null,
            Expression<Func<IEnumerable<SelectListItem>>> selectItems = null,
            Expression<Func<string>> stringValue = null)
            => Helpers.Add(new ExpressionHelper(e, caption, selectItems, stringValue));
        protected void addField<TResult>(Expression<Func<TP, TResult>> e,
            Expression<Func<IEnumerable<SelectListItem>>> selectItems,
            Expression<Func<string>> stringValue = null)
            => Helpers.Add(new ExpressionHelper(e, null, selectItems, stringValue));
        protected void addField<TResult>(Expression<Func<TP, TResult>> e,
            Expression<Func<string>> stringValue)
            => Helpers.Add(new ExpressionHelper(e, null, null, stringValue));
        protected void addFieldBefore<TResult>(ExpressionHelper toAdd, Expression<Func<TP, TResult>> before)
            => Helpers.Insert(index(before), toAdd);
        protected void addFieldAfter<TResult>(ExpressionHelper toAdd, Expression<Func<TP, TResult>> after)
            => Helpers.Insert(index(after) + 1, toAdd);
        protected void removeField<TResult>(Expression<Func<TP, TResult>> e)
            => Helpers.Remove(findHelper(e));
        protected ExpressionHelper findHelper<TResult>(Expression<Func<TP, TResult>> e)
            => Helpers.Find(x => Equals(x.Key.ToString(), e.ToString()));
        protected int index<TResult>(Expression<Func<TP, TResult>> e)
            => Helpers.IndexOf(findHelper(e));
        protected void tableColumn<TResult>(Expression<Func<TP, TResult>> e) => Columns.Add(new ExpressionHelper(e));
        protected void tableColumn<TResult>(Expression<Func<TP, TResult>> e, Expression<Func<string>> v) => Columns.Add(new ExpressionHelper(e, null, null, v));
        protected void valueViewer<TResult>(Expression<Func<TP, TResult>> e, Expression<Func<string>> v, string c = null) => Viewers.Add(new ExpressionHelper(e, c, null, v));
        protected void valueViewer<TResult>(Expression<Func<TP, TResult>> e, string c = null) => Viewers.Add(new ExpressionHelper(e, c));
        public virtual IHtmlContent ViewerFor(IHtmlHelper<TP> h, int i) {
            if (!isCorrectIndex(i, Viewers)) return null;
            dynamic exp = Viewers.ElementAt(i).Key;
            var val = Viewers.ElementAt(i).StrValue;
            var caption = Viewers.ElementAt(i).Caption;
            if (val is null) return viewerFor(h, exp, null);
            var v = val.Compile().Invoke();
            return viewerFor(h, exp, v, caption);
        }
        private static IHtmlContent viewerFor<TResult>(IHtmlHelper<TP> h, Expression<Func<TP, TResult>> f, string v, string caption = null)
            => (v is null) ? ((caption is null) ? h.Show(f) : h.ShowCaption(f, caption)) : ((caption is null) ? h.Show(f, v) : h.ShowCaption(f, caption, v));
        protected IHtmlContent valueFor<TResult>(IHtmlHelper<TP> h, Expression<Func<TP, TResult>> f)
                => h.DisplayFor(f);
        internal async Task<List<TV>> getList() {
            var l = await db.GetAsync();
            return l.Select(toView).ToList();
        }
    }
}
