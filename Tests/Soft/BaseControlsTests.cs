using Abc.Aids.Methods;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Abc.Aids.Reflection;
using Abc.Data.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Abc.Facade.Common;

namespace Abc.Tests.Soft {

    public abstract class BaseControlsTests<TView, TData, TContext>
        : BaseDataTests<TData, TContext> where TContext : DbContext where TData : BaseData where TView: BaseView{
        protected void canView<TResult>(object o, Expression<Func<TView, TResult>> e, string value = null) {
            var a = new HtmlTagArgs<TView, TResult>(o, e);
            a.Value = value ?? a.Value;
            var expected = HtmlTag.Display(a);
            if (pageHtml.Contains(expected)) return;
            expected = expected.Replace("+", "&#x2B;");
            expected = expected.Replace("∞", "&#x221E;");
            if (pageHtml.Contains(expected)) return;
            Assert.AreEqual(expected, pageHtml);
        }
        protected void canViewCheckBox(object o, Expression<Func<TView, bool>> e) {
            var a = new HtmlTagArgs<TView, bool>(o, e);
            bool currentValue = Safe.Run(() => e.Compile().Invoke((TView)o), false);

            var expected = HtmlTag.DisplayCheckBox(a, currentValue);

            if (pageHtml.Contains(expected)) return;
            Assert.AreEqual(expected, pageHtml);
        }
        protected void canEdit<TResult>(object o, Expression<Func<TView, TResult>> e,
            bool isRequired = false, bool isNumber = false, object defaultValue = null) {
            var a = new HtmlTagArgs<TView, TResult>(o, e, isRequired, isNumber, defaultValue);
            var expected = HtmlTag.Edit2(a);
            if (Editors.Remove(expected)) return;
            if (expected.Contains("type=\"date\" value=\""))
                expected = expected.Replace("type=\"date\" value=\"",
                    "type=\"datetime-local\" value=\"");
            if (Editors.Remove(expected)) return;
            expected = toNet6CompatibleEditHtml(expected);
            if (Editors.Remove(expected)) return;
            if (expected.Contains("type=\"datetime-local\" value=\""))
                expected = expected.Replace("type=\"datetime-local\" value=\"",
                    "type=\"date\" value=\"");
            if (Editors.Remove(expected)) return;
            Assert.AreEqual(expected, getUntestedInputs(Editors));
        }
        protected void canEdit<TResult>(object o, Expression<Func<TView, TResult>> e,
            string displayName, bool isRequired = false) {
            var a = new HtmlTagArgs<TView, TResult>(o, e, isRequired) {
                DisplayName = displayName
            };
            var expected = HtmlTag.Edit(a);
            if (expected.Contains(" /")) expected = expected.Replace(" /", "");
            if (Editors.Remove(expected)) return;
            expected = toNet6CompatibleEditHtml(expected);
            if (Editors.Remove(expected)) return;
            Assert.AreEqual(expected, getUntestedInputs(Editors));
        }
        private static string getUntestedInputs(List<string> inputs) {
            var result = "";
            foreach (var input in inputs) { 
                result += input + "\n";
            }
            return result;
        }
        protected void hasHidden<TResult>(object o, Expression<Func<TView, TResult>> e,
            bool isRequired = false, bool isNumber = false, string displayName = null) {
            var a = new HtmlTagArgs<TView, TResult>(o, e, isRequired, isNumber);
            if (displayName != null) a.DisplayName = displayName;
            var expected = HtmlTag.Hidden(a);
            var i = HiddenInputs.FindIndex(x => x.StartsWith(expected));
            if (i != -1) {
                HiddenInputs.RemoveAt(i);
                return;
            }
            expected = toHiddenFirstHtml(expected);
            i = HiddenInputs.FindIndex(x => x.StartsWith(expected));
            if (i != -1) {
                HiddenInputs.RemoveAt(i);
                return;
            }
            expected = toNet6CompatibleEditHtml(expected);
            i = HiddenInputs.FindIndex(x => x.StartsWith(expected));
            if (i != -1) {
                HiddenInputs.RemoveAt(i);
                return;
            }
            Assert.AreEqual(expected, getUntestedInputs(HiddenInputs));
        }
        private static string toHiddenFirstHtml(string expected) {
            var s = expected;
            s = s.Replace(" type=\"hidden\" ", "");
            s = s.Replace("<input", "<input type=\"hidden\"");
            return s;
        }
        private static string toNet6CompatibleEditHtml(string expected) 
            => expected?.Replace("field-validation-valid text-danger", "text-danger field-validation-valid");
        protected void canSelect<TResult>(object o, Expression<Func<TView, TResult>> e,
            List<SelectListItem> selectItems, bool isRequired = false, bool isFirstSelected = false) {
            var a = new HtmlTagArgs<TView, TResult>(o, e, isRequired);
            var expected = HtmlTag.Select(a, selectItems, isFirstSelected);
            for (int i = 0; i < Editors.Count; i++)
                if (Editors[i].Contains('\n')) Editors[i] = Editors[i].Replace("\n", "");
            if (Editors.Remove(expected)) return;
            expected = toNet6CompatibleEditHtml(expected);
            if (Editors.Remove(expected)) return;
            Assert.AreEqual(expected, getUntestedInputs(Editors));
        }
        protected void canSelect(object o, Expression<Func<TView, object>> e, bool isRequired) =>
            canSelect(o, e, null, isRequired);
        protected void canSelect(object o, Expression<Func<TView, object>> e) => canSelect(o, e, null);
        protected void canSelectEnum<TResult>(object o, Expression<Func<TView, TResult>> e, bool isRequired = false,
            object defaultValue = null) {
            var a = new HtmlTagArgs<TView, TResult>(o, e, isRequired, false, defaultValue);
            var n = GetMember.Name(e);
            var p = typeof(TView).GetProperty(n);
            var t = p?.PropertyType;
            Assert.IsNotNull(t);
            var l = new SelectList(Enum.GetNames(t));

            var expected = HtmlTag.SelectEnum(a, l);
            for (int i = 0; i < Editors.Count; i++)
                if (Editors[i].Contains('\n')) Editors[i] = Editors[i].Replace("\n", "");
            if (validateCanSelectEnum(expected)) return;
            expected = expected.Replace(" selected=\"selected\"", "");
            if (validateCanSelectEnum(expected)) return;
            Assert.AreEqual(expected, getUntestedInputs(Editors));
        }
        private bool validateCanSelectEnum(string expected) {
            if (Editors.Remove(expected)) return true;
            expected = toNet6CompatibleEditHtml(expected);
            if (Editors.Remove(expected)) return true;
            return false;
        }
        protected void canClickCheckBox(object o, Expression<Func<TView, bool>> e,
            bool isRequired = false, object defaultValue = null) {
            var a = new HtmlTagArgs<TView, bool>(o, e, isRequired, false, defaultValue);
            bool currentValue = Safe.Run(() => e.Compile().Invoke((TView) o), false);
            var expected = HtmlTag.CheckBox(a, currentValue);

            if (expected.Contains(" /")) expected = expected.Replace(" /", "");

            if (Editors.Remove(expected)) return;
            expected = toNet6CompatibleEditHtml(expected);
            if (Editors.Remove(expected)) return;
            Assert.AreEqual(expected, getUntestedInputs(Editors));
        }
        protected override void dataHiddenInPage() => hasHidden(item, x => x.Timestamp, true);
    }
}
