using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Facade.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Abc.Pages.Common {
    public abstract class FilteredPage<TP, TR, TO, TV, TD> :EditorsPage<TP, TR, TO, TV, TD>
        where TP : PageModel
        where TR : class, IRepo<TO>
        where TO : IBaseEntity<TD>
        where TV : BaseView
        where TD : BaseData, new() {
        protected FilteredPage(TR r) : base(r) { }
    }
}
