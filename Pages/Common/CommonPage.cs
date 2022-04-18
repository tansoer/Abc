using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Facade.Common;

namespace Abc.Pages.Common {
    public abstract class CommonPage<TR, TO, TV, TD> :ColumnsPage<TR, TO, TV,TD>
        where TR : class, IRepo<TO>
        where TO : IBaseEntity<TD>
        where TV : BaseView
        where TD : BaseData, new() {
        protected CommonPage(TR r) : base(r) { }
    }
}
