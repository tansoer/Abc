using System;
using System.Linq.Expressions;
using Abc.Aids.Reflection;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Abc.Tests.Soft {

    public class HtmlTagArgs<TView, TResult> {

        public HtmlTagArgs(object obj, Expression<Func<TView, TResult>> e, in bool isRequired = false,
            in bool isNumber = false, object defaultValue = null) {
            IsRequired = isRequired;
            IsNumber = isNumber;
            DisplayName = GetMember.DisplayName(e);
            Name = GetMember.Name(e);
            var memberExp = e.Body as MemberExpression;
            var member = memberExp?.Expression as MemberExpression;
            ParentName = member?.Member.Name ?? "Item";
            var p = obj?.GetType().GetProperty(Name);
            Type = HtmlHelper.Type<TView>(Name);
            Value = p?.GetValue(obj) ?? defaultValue;
        }


        public bool IsRequired { get; set; }
        public bool IsNumber { get; set; }
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string ParentName { get; set; }
        public object Value { get; set; }

    }

}
