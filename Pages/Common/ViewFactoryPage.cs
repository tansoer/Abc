using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Facade.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Abc.Pages.Common {
    public abstract class ViewFactoryPage<TP, TR, TO, TV, TD, TF>
      :ViewPage<TP, TR, TO, TV, TD>
        where TP : PageModel
        where TR : class, IRepo<TO>
        where TO : IBaseEntity<TD>
        where TV : BaseView, new()
        where TD : BaseData, new()
        where TF : AbstractViewFactory<TD, TO, TV>, new() {
        protected ViewFactoryPage(TR r) : base(r) { }
        protected internal override TO toObject(TV v) => new TF().Create(v);
        protected internal override TV toView(TO o) => new TF().Create(o);
    }
}