using System;
using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Constants;
using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Facade.Common;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abc.Pages.Quantities {
    internal abstract class masterDetailsManager<TMasterView, TDetailData, TDetailObj, TDetailView, TDetailRepo, TFactory>
        where TFactory: AbstractViewFactory<TDetailData, TDetailObj, TDetailView>, new()
        where TDetailData : BaseData, new()
        where TDetailObj : IBaseEntity<TDetailData>
        where TDetailView : BaseView, new()
        where TMasterView : BaseView, new()
        where TDetailRepo : IRepo<TDetailObj>
    {
        protected internal readonly Func<TMasterView> getItem;
        protected internal readonly TDetailRepo repo;
        protected internal readonly string idField;
        protected internal List<TDetailView> details { get; set; }
        protected masterDetailsManager(Func<TMasterView> f, string foreinKey) {
            getItem = f;
            repo = GetRepo.Instance<TDetailRepo>();
            idField = foreinKey;
            details = new List<TDetailView>();
        }
        protected internal virtual void loadDetails() => 
            details = loadDetails(repo, idField, itemId, new TFactory().Create);
        protected internal void createDetails() {
            details.ForEach(x => updateMasterId(x, itemId));
            foreach (var x in details) repo.Add(detailViewToObj(x));
        }
        protected internal void deleteDetails(string id) => repo.Get().Where(x => isMasterDetail(x, id)).ToList().ForEach(x => repo.Delete(x.Id));
        protected abstract bool isMasterDetail(TDetailObj detail, string masterId);
        protected internal void editDetails() {
            var l = loadDetails(repo, idField, itemId, new TFactory().Create);
            foreach (var e in details) {
                if (!l.Any(x => x.Id == e.Id)) repo.Add(detailViewToObj(e));
                else updateDetail(ref l, e);
            }
            removeDeleted(ref l);
        }
        private void updateDetail(ref List<TDetailView> l, TDetailView v) {
            repo.Update(detailViewToObj(v));
            l.RemoveAll(x => x.Id == v.Id);
        }
        private void removeDeleted(ref List<TDetailView> l) => l.ForEach(x => repo.Delete(x.Id));
        protected internal abstract void updateMasterId(TDetailView x, string id);
        protected internal string itemId => getItem()?.Id?? Word.Unspecified;
        protected internal static TDetailObj detailViewToObj(TDetailView v) => new TFactory().Create(v);
        protected internal static ComponentArgs editorFor(string propertyName) => BasePageAids.expandingEditorField(propertyName);
        protected internal static ComponentArgs editorFor(string propertyName, bool isHidden, object defaultValue = null) 
            => BasePageAids.expandingEditorField(propertyName, isHidden, defaultValue);
        protected internal static ComponentArgs editorFor(string propertyName, IEnumerable<SelectListItem> selectList) 
            => BasePageAids.expandingEditorField(propertyName, selectList);
        protected internal static List<TDetailView> loadDetails(
            IRepo<TDetailObj> r, string filter, string value, Func<TDetailObj, TDetailView> toView)
            => BasePageAids.loadDetails(r, filter, value, toView);
        protected internal static TRepo getRepo<TRepo>() => BasePageAids.getRepo<TRepo>();
    }
}
