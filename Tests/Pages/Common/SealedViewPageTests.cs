using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Facade.Common;
using Abc.Pages.Common;

namespace Abc.Tests.Pages.Common {
    public abstract class SealedViewPageTests<TClass, TRepo, TObj, TView, TData>
        : SealedPageTests<TClass, ViewPage<TClass, TRepo, TObj, TView, TData>, TRepo, TObj, TView, TData>
        where TClass : ViewPage<TClass, TRepo, TObj, TView, TData>
        where TRepo : class, IRepo<TObj>
        where TObj : IBaseEntity<TData>
        where TData : BaseData, new()
        where TView : BaseView, new() {
    }
}
