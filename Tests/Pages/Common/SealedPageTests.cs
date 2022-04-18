using Abc.Aids.Random;
using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Facade.Common;
using Abc.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Pages.Common {

    public abstract class SealedPageTests<TClass, TBaseClass, TRepo, TObj, TView, TData>
        : SealedTests<TClass, TBaseClass>
        where TClass : ViewPage<TClass, TRepo, TObj, TView, TData>
        where TRepo : class, IRepo<TObj>
        where TObj : IBaseEntity<TData>
        where TData : BaseData, new()
        where TView : BaseView, new() {

        protected string sortOrder;
        protected string searchString;
        protected byte pageIndex;
        protected string fixedFilter;
        protected string fixedValue;
        protected byte createSwitch;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            sortOrder = rndStr;
            searchString = rndStr;
            pageIndex = GetRandom.UInt8();
            fixedFilter = rndStr;
            fixedValue = rndStr;
            createSwitch = GetRandom.UInt8(0, 10);
        }
        [TestMethod]
        public virtual void ItemIdTest() {
            var item = random<TView>();
            obj.Item = item;
            areEqual(item.Id, obj.ItemId);
            obj.Item = null;
            areEqual(string.Empty, obj.ItemId);
        }
        [TestMethod] public void TitleTest() => areEqual(pageTitle, obj.Title);
        protected abstract string pageTitle { get; }
        [TestMethod] public void PageUrlTest() => areEqual(pageUrl, obj.PageUrl.ToString());
        protected abstract string pageUrl { get; }
        [TestMethod] public virtual void ToObjectTest() {
            var v = GetRandom.ObjectOf<TView>();
            var o = obj.toObject(v);
            validateData(v, o.Data);
        }

        protected virtual void validateData(TView v, TData d) 
            => allPropertiesAreEqual(v, d);

        protected virtual void validateView(TData d, TView v)
            => allPropertiesAreEqual(d, v);

        [TestMethod] public virtual void ToViewTest() {
            var d = GetRandom.ObjectOf<TData>();
            var o = toObject(d);
            var view = obj.toView(o);
            validateView(d, view);
        }
        protected abstract TObj toObject(TData d);
        protected void testPageProperties(string selId = null, int? pageIdx = null) {
            areEqual(selId, obj.SelectedId);
            areEqual(fixedFilter, obj.FixedFilter);
            areEqual(fixedValue, obj.FixedValue);
            areEqual(searchString, obj.SearchString);
            areEqual(pageIdx ?? pageIndex, obj.PageIndex);
            areEqual(sortOrder, obj.SortOrder);
        }
    }
}