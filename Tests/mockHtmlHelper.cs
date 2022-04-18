using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Abc.Aids.Reflection;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Abc.Tests {

    internal class mockHtmlHelper<TModel> : IHtmlHelper<TModel> {
        public IHtmlContent ActionLink(string linkText, string actionName, string controllerName, string protocol,
            string hostname,
            string fragment, object routeValues, object htmlAttributes) => new mockHtmlContent("ActionLink");
        public IHtmlContent AntiForgeryToken() => new mockHtmlContent("AntiForgeryToken");
        public MvcForm BeginForm(string actionName, string controllerName, object routeValues, FormMethod method,
            bool? antiforgery,
            object htmlAttributes) => null;
        public MvcForm BeginRouteForm(string routeName, object routeValues, FormMethod method, bool? antiforgery,
            object htmlAttributes) => null;

        public IHtmlContent CheckBox(string expression, bool? isChecked, object htmlAttributes)
            => new mockHtmlContent("CheckBox");

        public IHtmlContent Display(string expression, string templateName, string htmlFieldName,
            object additionalViewData)
            => new mockHtmlContent("Display");

        public string DisplayName(string expression) => "DisplayName";

        public string DisplayText(string expression) => "DisplayText";

        public IHtmlContent DropDownList(
            string expression, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes)
            => new mockHtmlContent("DropDownList");

        public IHtmlContent Editor(string expression, string templateName, string htmlFieldName,
            object additionalViewData)
            => new mockHtmlContent("Editor");

        string IHtmlHelper<TModel>.Encode(object value) => null;

        string IHtmlHelper<TModel>.Encode(string value) => null;

        public IHtmlContent HiddenFor<TResult>(Expression<Func<TModel, TResult>> expression, object htmlAttributes)
            => new mockHtmlContent("HiddenFor");

        public string IdFor<TResult>(Expression<Func<TModel, TResult>> expression) => "IdFor";

        public IHtmlContent LabelFor<TResult>(Expression<Func<TModel, TResult>> e,
            string labelText, object htmlAttributes)
            => new mockHtmlContent($"LabelFor{GetMember.Name(e)}");

        public IHtmlContent ListBoxFor<TResult>(
            Expression<Func<TModel, TResult>> e, IEnumerable<SelectListItem> selectList, object htmlAttributes)
            => new mockHtmlContent("ListBoxFor");

        public string NameFor<TResult>(Expression<Func<TModel, TResult>> expression)
            => "NameFor";

        public IHtmlContent PasswordFor<TResult>(Expression<Func<TModel, TResult>> e, object htmlAttributes)
            => new mockHtmlContent("PasswordFor");

        public IHtmlContent RadioButtonFor<TResult>(
            Expression<Func<TModel, TResult>> e, object value, object htmlAttributes)
            => new mockHtmlContent("RadioButtonFor");

        IHtmlContent IHtmlHelper<TModel>.Raw(object value) => null;

        IHtmlContent IHtmlHelper<TModel>.Raw(string value) => null;

        public IHtmlContent TextAreaFor<TResult>(Expression<Func<TModel, TResult>> e, int rows, int columns,
            object htmlAttributes)
            => new mockHtmlContent("TextAreaFor");

        public IHtmlContent TextBoxFor<TResult>(
            Expression<Func<TModel, TResult>> e, string format, object htmlAttributes)
            => new mockHtmlContent("TextBoxFor");

        public IHtmlContent ValidationMessageFor<TResult>(
            Expression<Func<TModel, TResult>> e, string message, object htmlAttributes, string tag)
            => new mockHtmlContent("ValidationMessageFor");

        public string ValueFor<TResult>(Expression<Func<TModel, TResult>> e, string format)
            => "ValueFor";

        public ViewDataDictionary<TModel> ViewData { get; } = new ViewDataDictionary<TModel>(new EmptyModelMetadataProvider(), new ModelStateDictionary());

        public IHtmlContent CheckBoxFor(Expression<Func<TModel, bool>> expression, object htmlAttributes)
            => new mockHtmlContent("CheckBoxFor");

        public IHtmlContent DisplayFor<TResult>(Expression<Func<TModel, TResult>> expression, string templateName,
            string htmlFieldName,
            object additionalViewData)
            => new mockHtmlContent($"DisplayFor({expression})");

        public string DisplayNameForInnerType<TModelItem, TResult>(Expression<Func<TModelItem, TResult>> expression)
            => "DisplayNameForInnerType";

        public string DisplayNameFor<TResult>(Expression<Func<TModel, TResult>> expression)
            => $"DisplayNameFor({expression})";


        public string DisplayTextFor<TResult>(Expression<Func<TModel, TResult>> expression)
            => "DisplayTextFor";

        public IHtmlContent DropDownListFor<TResult>(Expression<Func<TModel, TResult>> expression,
            IEnumerable<SelectListItem> selectList, string optionLabel,
            object htmlAttributes)
            => new mockHtmlContent("DropDownListFor");

        public IHtmlContent EditorFor<TResult>(Expression<Func<TModel, TResult>> expression, string templateName,
            string htmlFieldName,
            object additionalViewData) => new mockHtmlContent("EditorFor");

        string IHtmlHelper.Encode(object value) => null;

        string IHtmlHelper.Encode(string value) => null;

        public void EndForm() { }

        public string FormatValue(object value, string format) => "FormatValue";

        public string GenerateIdFromName(string fullName) => "GenerateIdFromName";

        public IEnumerable<SelectListItem> GetEnumSelectList(Type enumType) => null;

        public IEnumerable<SelectListItem> GetEnumSelectList<TEnum>() where TEnum : struct => null;

        public IHtmlContent Hidden(string expression, object value, object htmlAttributes)
            => new mockHtmlContent("Hidden");

        public string Id(string expression) => "Id";

        public IHtmlContent Label(string expression, string labelText, object htmlAttributes)
            => new mockHtmlContent("Label");

        public IHtmlContent ListBox(string expression, IEnumerable<SelectListItem> selectList, object htmlAttributes)
            => new mockHtmlContent("ListBox");

        public string Name(string expression) => "Name";

        public async Task<IHtmlContent>
            PartialAsync(string partialViewName, object model, ViewDataDictionary viewData) {
            await Task.CompletedTask;

            return new mockHtmlContent("PartialAsync");
        }

        public IHtmlContent Password(string expression, object value, object htmlAttributes)
            => new mockHtmlContent("Password");

        public IHtmlContent RadioButton(string expression, object value, bool? isChecked, object htmlAttributes)
            => new mockHtmlContent("RadioButton");

        IHtmlContent IHtmlHelper.Raw(object value) => new mockHtmlContent("RawValue");

        IHtmlContent IHtmlHelper.Raw(string value) => new mockHtmlContent("RawString");

        public async Task RenderPartialAsync(string partialViewName, object model, ViewDataDictionary viewData) {
            await Task.CompletedTask;
        }

        public IHtmlContent RouteLink(string linkText, string routeName, string protocol, string hostName,
            string fragment,
            object routeValues, object htmlAttributes)
            => new mockHtmlContent("RouteLink");

        public IHtmlContent TextArea(string expression, string value, int rows, int columns, object htmlAttributes)
            => new mockHtmlContent("TextArea");

        public IHtmlContent TextBox(string expression, object value, string format, object htmlAttributes)
            => new mockHtmlContent("TextBox");

        public IHtmlContent ValidationMessage(string expression, string message, object htmlAttributes, string tag)
            => new mockHtmlContent("ValidationMessage");

        public IHtmlContent ValidationSummary(bool excludePropertyErrors, string message, object htmlAttributes,
            string tag)
            => new mockHtmlContent("ValidationSummary");

        public string Value(string expression, string format)
            => "Value";

        public Html5DateRenderingMode Html5DateRenderingMode { get; set; }
        public string IdAttributeDotReplacement { get; } = null;
        public IModelMetadataProvider MetadataProvider { get; } = null;
        public ITempDataDictionary TempData { get; } = null;
        public UrlEncoder UrlEncoder { get; } = null;
        public dynamic ViewBag { get; } = null;
        public ViewContext ViewContext { get; } = null;
        ViewDataDictionary IHtmlHelper.ViewData => ViewData;

    }

}