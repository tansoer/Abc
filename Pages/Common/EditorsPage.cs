using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Facade.Common;
using Abc.Pages.Common.Aids;
using Abc.Pages.Common.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Abc.Pages.Common {
    public abstract class EditorsPage<TP, TR, TO, TV, TD> : CrudPage<TR, TO, TV, TD>
        where TP : PageModel where TR : class, IRepo<TO> where TO : IBaseEntity<TD>
        where TV : BaseView where TD : BaseData, new() {
        protected EditorsPage(TR r) :base(r) => addFields();
        public List<ExpressionHelper> Hidden => hiddenInputs();
        public List<ExpressionHelper> Editors { get; } = new();
        public List<ExpressionHelper> Helpers { get; set; } = new();
        public virtual IHtmlContent HiddenFor(IHtmlHelper<TP> h, int i) => hiddenFor(h, i);
        public virtual IHtmlContent EditorFor(IHtmlHelper<TP> h, int i) => editorFor(h, i);
        protected virtual void valueEditors() => createFields(Editors.Add, getFieldsForEditors());
        protected virtual List<ExpressionHelper> getFieldsForColumns() => Helpers;
        protected virtual List<ExpressionHelper> getFieldsForViewers() {
            fieldsForViewers();
            return Helpers;
        }
        protected virtual List<ExpressionHelper> getFieldsForEditors() {
            fieldsForEditors();
            return Helpers;
        }
        protected virtual void fieldsForViewers() { }
        protected virtual void fieldsForEditors() { }
        protected virtual void addFields() {}
        protected IHtmlContent dropDownFor<TResult>(IHtmlHelper<TP> h, Expression<Func<TP, TResult>> f,
           IEnumerable<SelectListItem> items, string v) => (v is null) ? h.DropDown(f, items) : h.DropDown(f, items, v);
        protected IHtmlContent editorFor<TResult>(IHtmlHelper<TP> h, Expression<Func<TP, TResult>> f, string v)
                => (v is null) ? h.Editor(f) : h.Editor(f, v);
        protected IHtmlContent numberEditorFor(IHtmlHelper<TP> h, int idx, int min = 0, int max = int.MaxValue) {
            if (!isCorrectIndex(idx, Editors)) return null;
            dynamic exp = Editors.ElementAt(idx).Key;
            var caption = Editors.ElementAt(idx).Caption;
            return numberEditorFor(h, exp, caption, min, max);
        }
        private static IHtmlContent numberEditorFor<TResult>(IHtmlHelper<TP> h, Expression<Func<TP, TResult>> f,
            string v, int min = 0, int max = int.MaxValue)
                    => (v is null) ? h.NumberFor(f, min, max) : h.NumberFor(f, v, min, max);
        protected IHtmlContent phoneNoEditorFor(IHtmlHelper<TP> h, int idx, byte minLength, byte maxLength = 0) {
            if (!isCorrectIndex(idx, Editors)) return null;
            dynamic exp = Editors.ElementAt(idx).Key;
            var caption = Editors.ElementAt(idx).Caption;
            return phoneNoEditorFor(h, exp, caption, minLength, maxLength);
        }
        private static IHtmlContent phoneNoEditorFor<TResult>(IHtmlHelper<TP> h, 
            Expression<Func<TP, TResult>> f, string displayName, byte minLength, byte maxLength)
                    => (displayName is null)
            ? h.PhoneFor(f, minLength, maxLength)
            : h.PhoneFor(f, displayName, minLength, maxLength);
        protected IHtmlContent webAdrEditorFor(IHtmlHelper<TP> h, int idx) {
            if (!isCorrectIndex(idx, Editors)) return null;
            dynamic exp = Editors.ElementAt(idx).Key;
            var caption = Editors.ElementAt(idx).Caption;
            return webAdrEditorFor(h, exp, caption);
        }
        private static IHtmlContent webAdrEditorFor<TResult>(IHtmlHelper<TP> h, 
            Expression<Func<TP, TResult>> f, string v)
                    => (v is null) ? h.WebFor(f) : h.WebFor(f, v);
        protected IHtmlContent emailAdrEditorFor(IHtmlHelper<TP> h, int idx) {
            if (!isCorrectIndex(idx, Editors)) return null;
            dynamic exp = Editors.ElementAt(idx).Key;
            var caption = Editors.ElementAt(idx).Caption;
            return emailAdrEditorFor(h, exp, caption);
        }
        private static IHtmlContent emailAdrEditorFor<TResult>(IHtmlHelper<TP> h, 
            Expression<Func<TP, TResult>> f, string v)
                    => (v is null) ? h.EmailFor(f) : h.EmailFor(f, v);
        protected void valueEditor<TResult>(Expression<Func<TP, TResult>> e, 
            Expression<Func<IEnumerable<SelectListItem>>> l, string v = null) =>
           Editors.Add(new ExpressionHelper(e, v, l));
        protected void valueEditor<TResult>(Expression<Func<TP, TResult>> e, string v = null) =>
            Editors.Add(new ExpressionHelper(e, v));
        protected virtual List<ExpressionHelper> hiddenInputs() { return new List<ExpressionHelper>();}
        protected IHtmlContent hiddenFor(IHtmlHelper<TP> h, int i) {
            if (!isCorrectIndex(i, Hidden)) return null;
            dynamic exp = Hidden.ElementAt(i).Key;
            return h.HiddenFor(exp, null);
        }
        protected IHtmlContent editorFor(IHtmlHelper<TP> h, int i) {
            if (!isCorrectIndex(i, Editors)) return null;
            dynamic exp = Editors.ElementAt(i).Key;
            var val = Editors.ElementAt(i).Value;
            var caption = Editors.ElementAt(i).Caption;
            if (val is null) return editorFor(h, exp, caption);
            var v = val.Compile().Invoke();
            return dropDownFor(h, exp, v, caption);
        }
        protected void createFields(Action<ExpressionHelper> function, List<ExpressionHelper> expressions = null) { 
            foreach (var e in expressions) { function(e); }
        }
    }
}
