using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Pages.Common.Aids {
    [TestClass]
    public class ExpressionHelperTests :TestsBase {
        private ExpressionHelper obj;
        private Expression<Func<string>> strValue => () => s;
        private IEnumerable<SelectList> l;
        private Expression<Func<IEnumerable<SelectListItem>>> value => () => (IEnumerable<SelectListItem>)l;
        private string s;
        [TestInitialize] public void TestInitialize() {
            type = typeof(ExpressionHelper);
            s = random<string>();
            l = new List<SelectList>();
            obj = new ExpressionHelper(value, s, value, strValue);
        }
        [TestMethod] public void KeyTest() => isInstanceOfType<LambdaExpression>(obj.Key);
        [TestMethod] public void ValueTest() => isInstanceOfType <Expression<Func<IEnumerable<SelectListItem>>>> (obj.Value);
        [TestMethod] public void StrValueTest() => isInstanceOfType<Expression<Func<string>>>(obj.StrValue);
        [TestMethod] public void CaptionTest() => areEqual(s, obj.Caption);
    }
}
