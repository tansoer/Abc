using Abc.Aids.Constants;
using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Abc.Pages.Common {
    public abstract class BasePageAids: PageModel {
        protected internal static ComponentArgs expandingEditorField(string propertyName) => new(propertyName);
        protected internal static ComponentArgs expandingEditorField(string propertyName, bool isHidden, object defaultValue = null)
            => new(propertyName, isHidden, null, defaultValue);
        protected internal static ComponentArgs expandingEditorField(string propertyName, IEnumerable<SelectListItem> selectList)
            => new(propertyName, false, selectList, null);
        protected internal static List<TView> loadDetails<TObj, TView>(
            IRepo<TObj> r, string filter, string value, Func<TObj, TView> toView)
            => r.Get(filter, value).Select(toView).ToList();
        protected internal static IEnumerable<SelectListItem> selectListByName<TRepo, TDomain, TData>(
            Func<TDomain, bool> condition = null,
            Func<TData, string> getName = null,
            Func<TData, string> getId = null)
            where TDomain : IEntity<TData>
            where TData : EntityBaseData, new()
            where TRepo: IRepo<TDomain>{
            string name(TData d) => (getName is null) ? d.Name : getName(d);
            string id(TData d) => (getId is null) ? d.Id : getId(d);
            var r = getRepo<TRepo>();
            var items = getList(r);
            //TODO Gunnar Kui osapoolel on mitu nime, siis teise nime puhul
            // saab siin paugu, kuna personi ID on sama
            var l = items is null
                ? new List<SelectListItem>()
                : condition is null ?
                    items.Select(m => new SelectListItem(name(m.Data), id(m.Data)))
                    .ToList() :
                    items.Where(condition)
                    .Select(m => new SelectListItem(name(m.Data), id(m.Data)))
                    .ToList();
            l.Insert(0, new SelectListItem(Word.Unspecified, null));
            return l;
        }
        private static List<TDomain> getList<TDomain>(IRepo<TDomain> r) {
            if (r is null) return new List<TDomain>();
            var pageIndex = r.PageIndex;
            r.PageIndex = -1;
            var items = r?.Get();
            r.PageIndex = pageIndex;
            return items;
        }
        protected internal static IEnumerable<SelectListItem> selectListById<TRepo, TDomain, TData>(
            Func<TDomain, bool> condition,
            Func<TDomain, string> getName)
            where TDomain : IEntity<TData>
            where TData : EntityBaseData, new()
            where TRepo: IRepo<TDomain>{
            string name(TDomain d) => (getName is null) ? d.Data.Id : getName(d);
            var r = getRepo<TRepo>();
            var items = getList(r);
            var l = items is null
                ? new List<SelectListItem>()
                : condition is null ?
                    items.Select(m => new SelectListItem(name(m), m.Data.Id))
                    .ToList() :
                    items.Where(condition)
                    .Select(m => new SelectListItem(name(m), m.Data.Id))
                    .ToList();
            l.Insert(0, new SelectListItem(Word.Unspecified, null));
            return l;
        }
        protected internal static string itemName(IEnumerable<SelectListItem> list, string id) {
            if (list is null) return Word.Unspecified;
            foreach (var m in list) if (m.Value == id) return m.Text;
            return Word.Unspecified;
        }
        protected internal static TRepo getRepo<TRepo>() => GetRepo.Instance<TRepo>();
    }
}

