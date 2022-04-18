using Abc.Domain.Common;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abc.Tests.Soft {

    public static class HtmlTag {

        public static string Header => "text/html; charset=utf-8";
        public static string IndexForm(string url) => $"<form id=\"indexForm\" method=\"get\" action=\"{url}\">";
        public static string Edit<TView, TResult>(HtmlTagArgs<TView, TResult> a) {
            var value = HtmlHelper.Value(a.Value, true);
            var head = formGroup + label(a.DisplayName) + input;
            var data = dataVal;
            var isRequired = HtmlTag.required(a.DisplayName);
            var isNumber = number(a.DisplayName);
            var tail = item(a.Name, a.ParentName) + type(a.Type, value) + validation(a.Name, a.ParentName);
            var required = data + (a.IsNumber ? isNumber : string.Empty) + isRequired;
            var html = head + (a.IsRequired ? required : string.Empty) + tail;

            return html;
        }

        public static string Edit2<TView, TResult>(HtmlTagArgs<TView, TResult> a) {
            var value = HtmlHelper.Value(a.Value, true);
            var head = formGroup + label(a.DisplayName) + input;
            var data = dataVal;
            var isRequired = HtmlTag.required(a.DisplayName);
            var isNumber = number(a.DisplayName);
            var tail = item(a.Name, a.ParentName) + type2(a.Type, value) + validation(a.Name, a.ParentName);
            var required = data + (a.IsNumber ? isNumber : string.Empty) + isRequired;
            var html = head + (a.IsRequired ? required : string.Empty) + tail;

            return html;
        }

        public static string Hidden<TView, TResult>(HtmlTagArgs<TView, TResult> a) {
            var head = beginInput;
            var data = dataVal;
            var isRequired = HtmlTag.required(a.DisplayName);
            var isNumber = number(a.DisplayName);
            var tail = item(a.Name, a.ParentName) + type("hidden");
            var required = data + (a.IsNumber ? isNumber : string.Empty) + isRequired;
            var html = head + (a.IsRequired ? required : string.Empty) + tail;

            return html;
        }

        public static string Select<TView, TResult>(HtmlTagArgs<TView, TResult> a, List<SelectListItem> selectItems, bool isFirstSelected = false) {
            var head = formGroup + label(a.DisplayName) + beginSelect;
            var required = dataVal + HtmlTag.required(a.DisplayName);
            var tail = item(a.Name, a.ParentName) + beginOptions;
            tail += option(Unspecified.String, isFirstSelected);
            if (selectItems is not null) foreach (var item in selectItems) tail += option(item);
            tail += endSelect;
            tail += validation(a.Name, a.ParentName);

            var html = head + (a.IsRequired ? required : string.Empty) + tail;

            return html;
        }

        public static string Display<TView, TResult>(HtmlTagArgs<TView, TResult> a) {
            var v = HtmlHelper.Value(a.Value);
            var html = display(a.DisplayName, v);

            return html;
        }

        private static string formGroup => "<dd class=\"col-sm-2\">";
        private static string dataVal => " data-val=\"true\"";
        private static string beginSelect => "<select class=\"form-control\"";
        private static string beginOptions => @">";
        private static string option(string item = "Unspecified", bool selected = false) {
            var s = $"<option selected=\"selected\">{item}</option>";

            return selected ? s : $"<option>{item}</option>";
        }

        private static string option(SelectListItem item) {
            //Do not touch this format
            var s = $"<option value=\"{item.Value}\">{item.Text}</option>";

            return s;
        }
        //Do not touch this format
        private static string endSelect => @"</select>";
        private static string input => "<input class=\"form-control text-box single-line\"";

        private static string beginInput => "<input";

        private static string type(string type) => $" type=\"{type}\" ";
        private static string type(string type, string value) => $" type=\"{type}\" value=\"{value}\" />";

        private static string type2(string type, string value) => $" type=\"{type}\" value=\"{value}\">";

        private static string label(string displayName)
            => $"{displayName}</dd><dd class=\"col-sm-10\">";

        private static string display(string name, string value) =>
            $"<dd class=\"col-sm-2\">{name}</dd><dd class=\"col-sm-10\">{value}</dd>";

        private static string validation(string name, string parent) =>
            $"<span class=\"field-validation-valid text-danger\" data-valmsg-for=\"{parent}.{name}\" " +
            "data-valmsg-replace=\"true\"></span></dd>";

        private static string required(string name) => $" data-val-required=\"The {name} field is required.\"";

        private static string number(string name) => $" data-val-number=\"The field {name} must be a number.\"";

        private static string item(string name, string parent) => $" id=\"{parent}_{name}\" name=\"{parent}.{name}\"";

        public static string SelectEnum<TView, TResult>(HtmlTagArgs<TView, TResult> a, SelectList l) {
            var head = formGroup + label(a.DisplayName) + beginSelect;
            var required = dataVal + HtmlTag.required(a.DisplayName);
            var tail = item(a.Name, a.ParentName) + beginOptions;

            if (l is not null)
                foreach (var item in l) {
                    var selected = item.Text == a.Value.ToString();
                    tail += option(item.Text, selected);
                }
            tail += endSelect;
            tail += validation(a.Name, a.ParentName);

            var html = head + (a.IsRequired ? required : string.Empty) + tail;

            return html;
        }

        public static string CheckBox<TView>(HtmlTagArgs<TView, bool> a, bool currentValue) {
            var head = formGroup + label(a.DisplayName) + checkBoxInput(currentValue);
            var data = dataVal;
            var isRequired = required(a.DisplayName);
            var tail = item(a.Name, a.ParentName) + type("checkbox", "true");
            var hidden = checkBoxHiddenInput(a.Name, "false");
            var validationTail = validation(a.Name, a.ParentName);

            var html = head + data + isRequired + tail + hidden + validationTail;

            return html;
        }

        public static string DisplayCheckBox<TView>(HtmlTagArgs<TView, bool> a, bool currentValue) => displayCheckBox(a.DisplayName, currentValue);
        private static string checkBoxInput(bool isChecked) 
            => isChecked ? "<input checked=\"checked\" class=\"form-control check-box\"" : "<input class=\"form-control check-box\"";
        private static string checkBoxHiddenInput(string name, string value) => $"<input name=\"Item.{name}\" type=\"hidden\" value=\"{value}\" />";
        private static string displayCheckBox(string name, bool isChecked) =>
            isChecked ? 
                $"<dd class=\"col-sm-2\">{name}</dd><dd class=\"col-sm-10\"><input checked=\"checked\" class=\"check-box\" disabled=\"disabled\" type=\"checkbox\" /></dd>" :
                $"<dd class=\"col-sm-2\">{name}</dd><dd class=\"col-sm-10\"><input class=\"check-box\" disabled=\"disabled\" type=\"checkbox\" /></dd>";
    }

}
