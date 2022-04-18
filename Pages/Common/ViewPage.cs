using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Facade.Common;
using Abc.Pages.Common.Extensions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Abc.Pages.Common {
    public abstract class ViewPage<TP, TR, TO, TV, TD> :TitledPage<TP, TR, TO, TV, TD>
        , IIndexTable<TP>, IEditItem<TP>, IShowItem<TP>
        where TP : PageModel
        where TR : class, IRepo<TO>
        where TO : IBaseEntity<TD>
        where TV : BaseView, new()
        where TD : BaseData, new() {
        protected ViewPage(TR r) : base(r) {}
        protected virtual void doAfterOnPostCreate() { }
        protected virtual void doAfterOnPostDelete(string id) { }
        protected virtual void doAfterOnPostEdit() { }
        protected virtual void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) { }
        protected virtual void doBeforeOnGetDelete(string id) { }
        protected virtual void doBeforeOnGetDetails(string id) { }
        protected virtual void doBeforeOnGetEdit(string id) { }
        protected virtual void doAfterOnGetEdit() { }
        protected virtual void doBeforeOnPostCreate() { }
        protected virtual void doBeforeOnPostDelete(string id) { }
        protected virtual void doBeforeOnPostEdit() { }
        public virtual IActionResult OnGetCreate(
            string sortOrder, string searchString, int? pageIndex,
            string fixedFilter, string fixedValue, int? switchOfCreate) {
            setPageValues(sortOrder, searchString, pageIndex, fixedFilter, fixedValue);
            doBeforeOnGetCreate(fixedFilter, fixedValue, switchOfCreate);
            valueEditors();
            return Page();
        }
        public virtual async Task<IActionResult> OnGetDeleteAsync(
            string id, string sortOrder, string searchString,
            int? pageIndex, string fixedFilter, string fixedValue) {
            setPageValues(sortOrder, searchString, pageIndex, fixedFilter, fixedValue);
            doBeforeOnGetDelete(id);
            await get(id);
            valueViewers();
            return Page();
        }
        public virtual async Task<IActionResult> OnGetDetailsAsync(
            string id, string sortOrder, string searchString,
            int pageIndex, string fixedFilter, string fixedValue) {
            setPageValues(sortOrder, searchString, pageIndex, fixedFilter, fixedValue);
            doBeforeOnGetDetails(id);
            await get(id);
            valueViewers();
            return Page();
        }
        public virtual async Task<IActionResult> OnGetEditAsync(
            string id, string sortOrder, string searchString,
            int pageIndex, string fixedFilter, string fixedValue) {
            setPageValues(sortOrder, searchString, pageIndex, fixedFilter, fixedValue);
            doBeforeOnGetEdit(id);
            await get(id);
            doAfterOnGetEdit();
            valueEditors();
            return Page();
        }
        public virtual async Task<IActionResult> OnGetIndexAsync(string sortOrder,
        string id, string currentFilter, string searchString, 
        int? pageIndex, string fixedFilter, string fixedValue, string[] fixedValues = null) {
            SelectedId = id;
            await getList(sortOrder, currentFilter, searchString, pageIndex,
                fixedFilter, fixedValue, fixedValues);
            return Page();
        }
        public virtual async Task<IActionResult> OnPostCreateAsync(
            string sortOrder, string searchString,
            int? pageIndex, string fixedFilter, string fixedValue) {
            setPageValues(sortOrder, searchString, pageIndex, fixedFilter, fixedValue);
            doBeforeOnPostCreate();
            reValidateModel();
            if (!await add()) {
                valueEditors();
                return Page();
            }
            doAfterOnPostCreate();
            return Redirect(IndexUrl.ToString());
        }

        private void reValidateModel() {
            if (ModelState.IsValid) return;
            var name = nameof(Item);
            ModelState.ClearValidationState(name);
            TryValidateModel(Item, name);
        }

        public virtual async Task<IActionResult> OnPostDeleteAsync(
            string id, string sortOrder, string searchString,
            int pageIndex, string fixedFilter, string fixedValue) {
            setPageValues(sortOrder, searchString, pageIndex, fixedFilter, fixedValue);
            doBeforeOnPostDelete(id);
            if (!await delete(id)) {
                valueEditors();
                return Page();
            }
            doAfterOnPostDelete(id); 
            return Redirect(IndexUrl.ToString());
        }
        public virtual async Task<IActionResult> OnPostEditAsync(
            string sortOrder, string searchString, 
            int pageIndex, string fixedFilter, string fixedValue) {
            setPageValues(sortOrder, searchString, pageIndex, fixedFilter, fixedValue);
            doBeforeOnPostEdit();
            if (!await update()) {
                valueEditors();
                return Page();
            }
            doAfterOnPostEdit();
            return Redirect(IndexUrl.ToString());
        }
    }
}
