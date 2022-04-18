using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Pages.Common.Aids {
    public struct ExpressionHelper {
        public ExpressionHelper(LambdaExpression e, string caption = null, 
            Expression<Func<IEnumerable<SelectListItem>>> selectItems = null, 
            Expression<Func<string>> stringValue = null) {
            Key = e;
            Caption = caption;
            Value = e.Body.Type.IsEnum ? () => new SelectList(Enum.GetNames(e.Body.Type)) : selectItems;
            StrValue = stringValue;
        }
        public LambdaExpression Key;
        public Expression<Func<IEnumerable<SelectListItem>>> Value;
        public Expression<Func<string>> StrValue;
        public string Caption;

        private Expression<Func<IEnumerable<SelectListItem>>> getSelectList() {
            return null;
        }
    }
}
