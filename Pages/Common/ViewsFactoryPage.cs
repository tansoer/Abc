using Abc.Aids.Extensions;
using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Facade.Common;
using Abc.Pages.Common.Extensions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Abc.Pages.Common
{
    public abstract class ViewsFactoryPage<TP, TR, TO, TV, TD, TF, TE> : ViewFactoryPage<TP, TR, TO, TV, TD, TF>,
        IButtonForCreateDropDown<TP, TE>
        where TP : PageModel
        where TR : class, IRepo<TO>
        where TO : IEntity<TD>
        where TV : EntityBaseView, new()
        where TD : EntityBaseData, new()
        where TF : AbstractViewFactory<TD, TO, TV>, new()
        where TE : Enum {

        protected ViewsFactoryPage(TR r) : base(r) => populateCreateButtonDropDownEntries();
        public int DropDownEntryCount => IndexCreateButtonDropDownEntries.Count;
        public List<TE> IndexCreateButtonDropDownEntries { get; } = new();
        protected virtual void populateCreateButtonDropDownEntries() {
            var l = Enum.GetValues(typeof(TE)).Cast<TE>();
            var except = removeFromCreateDropdown();
            foreach (var entry in l) if (!except.Contains(entry)) addEntryToDropDown(entry);
        }
        protected virtual List<TE> removeFromCreateDropdown() => new();
        protected void addEntryToDropDown(TE v) => IndexCreateButtonDropDownEntries.Add(v);
        public Uri CreateUri(TE type) => createUri(Convert.ToInt32(type));
        public Uri GetDropDownEntryUri(int i) => CreateUri(IndexCreateButtonDropDownEntries[i]);
        public string GetDropDownEntryName(int i) => IndexCreateButtonDropDownEntries[i].ToStr();
    }
}