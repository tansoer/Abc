using System;
using System.Threading.Tasks;
using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Facade.Common;
using Microsoft.AspNetCore.Mvc;

namespace Abc.Pages.Common {

    public abstract class CrudPage<TR, TO, TV, TD> : CommonPage<TR, TO, TV, TD>
        where TR : class, IRepo<TO>
        where TO : IBaseEntity<TD>
        where TV : BaseView
        where TD : BaseData, new() {
        protected CrudPage(TR r) : base(r) { }
        [BindProperty] public TV Item { get; set; }
        public string ItemId => Item?.Id ?? string.Empty;
        public virtual void LoadDetails() { }
        protected internal async Task<bool> add() => await perform(db.AddAsync);
        protected internal async Task<bool> update() => await perform(db.UpdateAsync);
        protected internal async Task get(string id) => Item = toView(await db.GetAsync(id));
        protected internal async Task<bool> delete(string id) => await db.DeleteAsync(id);
        protected internal abstract TO toObject(TV v);
        protected internal abstract TV toView(TO o);
        protected internal async Task<bool> perform(Func<TO, Task> f) {
            if (!ModelState.IsValid) return false;
            await f(toObject(Item));
            return true;
        }
    }
}
