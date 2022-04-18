using Abc.Aids.Methods;
using Abc.Data.Common;
using Abc.Domain.Common;

namespace Abc.Facade.Common {
    public abstract class AbstractViewFactory<TData, TObject, TView>
        where TData : BaseData, new()
        where TObject : IBaseEntity<TData>
        where TView : BaseView, new() {
        public virtual TObject Create(TView v) {
            var d = new TData();
            if (v is not null) Copy.Members(v, d);
            return toObject(d);
        }
        public virtual TView Create(TObject o) {
            var v = new TView();
            if (o is not null) Copy.Members(o.Data, v);
            return v;
        }
        protected internal abstract TObject toObject(TData d);
    }
}
