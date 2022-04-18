using Abc.Pages.Common.Extensions.Assist;
using Microsoft.AspNetCore.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Abc.Tests.Pages.Common.Extensions.Assist {
    [TestClass]
    public class HtmlContainerTests :TestsBase {
        private HtmlContainer obj;
        [TestInitialize]
        public void TestInitialize() {
            type = typeof(HtmlContainer);
            obj = new HtmlContainer();
        }
        [TestMethod]
        public void CanCreateTest() {
            areEqual(1, obj.strings.Count);
            areEqual("<div>", obj.strings[0].ToString());
        }
        [TestMethod]
        public void CanCreateParamsTest() {
            obj = new HtmlContainer(new[] { "A", "B", "C" }, "X");
            areEqual(1, obj.strings.Count);
            areEqual("<X A B C>", obj.strings[0].ToString());
        }
        [TestMethod]
        public void CanCreateNoTypeTest() {
            obj = new HtmlContainer(new[] { "A", "B", "C" });
            areEqual(1, obj.strings.Count);
            areEqual("<div A B C>", obj.strings[0].ToString());
        }
        [TestMethod]
        public void CloseHtmlContainerNoElementsTest() {
            obj.CloseHtmlContainer();
            areEqual(2, obj.strings.Count);
            areEqual("<div>", obj.strings[0].ToString());
            areEqual("</div>", obj.strings[1].ToString());
        }
        [TestMethod]
        public void CloseHtmlContainerHasDivTest() {
            obj = new HtmlContainer(new[] { "A", "B", "C" });
            obj.CloseHtmlContainer();
            areEqual(2, obj.strings.Count);
            areEqual("<div A B C>", obj.strings[0].ToString());
            areEqual("</div>", obj.strings[1].ToString());
        }
        [TestMethod]
        public void CloseHtmlContainerTest() {
            obj = new HtmlContainer(new[] { "A", "B", "C"}, "X");
            obj.CloseHtmlContainer();
            areEqual(2, obj.strings.Count);
            areEqual("<X A B C>", obj.strings[0].ToString());
            areEqual("</X>", obj.strings[1].ToString());
        }
        [TestMethod]
        public void CloseHtmlContainerTypeTest() {
            obj.CloseHtmlContainer("X");
            areEqual(2, obj.strings.Count);
            areEqual("<div>", obj.strings[0].ToString());
            areEqual("</X>", obj.strings[1].ToString());
        }
        [TestMethod]
        public void AddContainerTest() {
            obj.AddContainer(new[] { "A", "B", "C" }, "X");
            areEqual(2, obj.strings.Count);
            areEqual("<div>", obj.strings[0].ToString());
            areEqual("<X A B C>", obj.strings[1].ToString());
        }
        [TestMethod]
        public void AddContentListTest() {
            obj.AddContent(new List<HtmlString> { 
                new HtmlString("A"),
                new HtmlString("B"),
                new HtmlString("C"),
            });
            areEqual(4, obj.strings.Count);
            areEqual("<div>", obj.strings[0].ToString());
            areEqual("A", obj.strings[1].ToString());
            areEqual("B", obj.strings[2].ToString());
            areEqual("C", obj.strings[3].ToString());
        }
        [TestMethod]
        public void AddContentTest() {
            obj.AddContent(new HtmlString("ABC"));
            areEqual(2, obj.strings.Count);
            areEqual("<div>", obj.strings[0].ToString());
            areEqual("ABC", obj.strings[1].ToString());
        }
        [TestMethod]
        public void AddTextTest() {
            obj.AddText("ABC");
            areEqual(2, obj.strings.Count);
            areEqual("<div>", obj.strings[0].ToString());
            areEqual("ABC", obj.strings[1].ToString());
        }
    }
}
