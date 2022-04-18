using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Facade.Common;
using Abc.Pages.Common;
using System;

namespace Abc.Tests.Pages.Common
{
    public abstract class SealedViewsFactoryPageTests<TClass, TRepo, TObj, TView, TData, TFactory, TType> 
        :SealedPageTests<TClass, ViewsFactoryPage<TClass, TRepo, TObj, TView, TData, TFactory, TType>, TRepo, TObj, TView, TData>
        where TClass : ViewPage<TClass, TRepo, TObj, TView, TData>
        where TRepo : class, IRepo<TObj>
        where TObj : IEntity<TData>
        where TData : EntityBaseData, new()
        where TView : EntityBaseView, new()
        where TFactory : AbstractViewFactory<TData, TObj, TView>, new()
        where TType : System.Enum {
        protected override Type getBaseClass() =>
            typeof(ViewsFactoryPage<TClass, TRepo, TObj, TView, TData, TFactory, TType>);
    }
}