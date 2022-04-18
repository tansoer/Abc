using Abc.Aids.Random;
using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Common {
    public abstract class ViewFactoryTests<TClass, TData, TObj, TView> :
        SealedTests<TClass, AbstractViewFactory<TData, TObj, TView>>
        where TClass : AbstractViewFactory<TData, TObj, TView>, new()
        where TData : BaseData, new()
        where TView : BaseView, new()
        where TObj : IBaseEntity<TData> {

        [TestMethod]
        public virtual void CreateTest() {
            var d = createData();
            var o = dataToObject(d);
            var v = objectToView(o);
            evaluateView(o.Data, v);
        }

        [TestMethod]
        public virtual void CreateObjectTest() {
            var v = createView();
            var o = viewToObject(v);
            evaluateData(v, o.Data);
        }

        protected virtual void evaluateView(TData d, TView v) => allPropertiesAreEqual(d, v);

        protected virtual void evaluateData(TView v, TData d) 
            => allPropertiesAreEqual(v, d);

        protected virtual TView objectToView(TObj o) => new TClass().Create(o);
        protected virtual TObj viewToObject(TView v) => new TClass().Create(v);

        protected virtual TObj dataToObject(TData d) => new TClass().toObject(d);

        protected virtual TData createData() => GetRandom.ObjectOf<TData>();
        protected virtual TView createView() => GetRandom.ObjectOf<TView>();

    }
}
