using Abc.Aids.Extensions;
using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Facade.Common;
using Abc.Pages.Common.Extensions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Abc.Pages.Common {
    public abstract class ViewsPage<TP, TR, TO, TV, TD, TE> : ViewPage<TP, TR, TO, TV, TD>,
        IButtonForCreateDropDown<TP, TE>
        where TP : PageModel
        where TR : class, IRepo<TO>
        where TO : IEntity<TD>
        where TV : EntityBaseView, new()
        where TD : EntityBaseData, new()
        where TE : Enum {

        protected ViewsPage(TR r) : base(r) => populateCreateButtonDropDownEntries();
        public int DropDownEntryCount => IndexCreateButtonDropDownEntries.Count;
        public List<TE> IndexCreateButtonDropDownEntries { get; } = new();
        protected void populateCreateButtonDropDownEntries() {
            var l = Enum.GetValues(typeof(TE)).Cast<TE>();
            foreach (var entry in l) addEntryToDropDown(entry);
        }
        protected void addEntryToDropDown(TE v) => IndexCreateButtonDropDownEntries.Add(v);
        public Uri CreateUri(TE type) => createUri(Convert.ToInt32(type));
        public Uri GetDropDownEntryUri(int i) => CreateUri(IndexCreateButtonDropDownEntries[i]);
        public string GetDropDownEntryName(int i) => IndexCreateButtonDropDownEntries[i].ToStr();
    }
}
