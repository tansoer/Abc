using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Facade.Common;
using Abc.Pages.Common;

namespace Abc.Tests.Pages.Common {

    public abstract class SealedViewsPageTests<TClass, TRepo, TObj, TView, TData, TType>
        : SealedPageTests<TClass, ViewsPage<TClass, TRepo, TObj, TView, TData, TType>, TRepo, TObj, TView, TData>
        where TClass : ViewPage<TClass, TRepo, TObj, TView, TData>
        where TRepo : class, IRepo<TObj>
        where TObj : IEntity<TData>
        where TData : EntityBaseData, new()
        where TView : EntityBaseView, new()
        where TType : System.Enum{
    }
}