using Abc.Aids.Reflection;
using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Facade.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq.Expressions;

namespace Abc.Pages.Common {
    public abstract class SortedPage<TP, TR, TO, TV, TD> :PagedPage<TP, TR, TO, TV, TD>
        where TP : PageModel
        where TR : class, IRepo<TO>
        where TO : IBaseEntity<TD>
        where TV : BaseView
        where TD : BaseData, new() {
        protected internal SortedPage(TR r) : base(r) { }
        public virtual Uri SortStringFor(int i) {
            if (!isCorrectIndex(i, Columns)) return null;
            dynamic exp = Columns[i].Key;
            return sortStringFor(exp);
        }
        internal Uri sortStringFor<T>(Expression<Func<TP, T>> e) {
            var name = GetMember.Name(e);
            var sortOrder = getSortOrder(name);
            return indexUri(sortOrder);
        }
    }
}
