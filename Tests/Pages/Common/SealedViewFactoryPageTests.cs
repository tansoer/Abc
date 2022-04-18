using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Facade.Common;
using Abc.Pages.Common;

namespace Abc.Tests.Pages.Common {
    public abstract class SealedViewFactoryPageTests<TClass, TRepo, TObj, TView, TData, TFactory>
        :SealedPageTests<TClass, ViewFactoryPage<TClass, TRepo, TObj, TView, TData, TFactory>, TRepo, TObj, TView, TData>
        where TClass : ViewFactoryPage<TClass, TRepo, TObj, TView, TData, TFactory>
        where TRepo : class, IRepo<TObj>
        where TObj : BaseEntity<TData>
        where TData : BaseData, new()
        where TFactory : AbstractViewFactory<TData, TObj, TView>, new()
        where TView : BaseView, new() {
    }
}
