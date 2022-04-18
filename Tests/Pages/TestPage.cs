using System;
using Abc.Pages.Common;
using Abc.Tests.Aids;
using Abc.Tests.Data;
using Abc.Tests.Domain;
using Abc.Tests.Facade;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abc.Tests.Pages {
    public class TestPage : ViewsPage<TestPage, ITestRepo,
        TestObject, TestView, TestData, TestType> {
        public TestPage(ITestRepo r) : base(r) { }
        protected internal override TestObject toObject(TestView v) => new TestViewFactory().Create(v);
        protected internal override TestView toView(TestObject o) => new TestViewFactory().Create(o);
        protected internal override string pageUrl => "/Tests";
        protected override void tableColumns() {
            tableColumn(x => x.Item.Id);
            tableColumn(x => x.Item.Name);
            tableColumn(x => x.Item.Code);
            tableColumn(x => x.Item.Details);
            tableColumn(x => x.Item.ValidFrom);
            tableColumn(x => x.Item.ValidTo);
            tableColumn(x => x.Item.Type);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Name);
            valueViewer(x => Item.Code);
            valueViewer(x => Item.Details);
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
            valueViewer(x => Item.Type);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.Name);
            valueEditor(x => Item.Code);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
            valueEditor(x => Item.Type, () => new SelectList(Enum.GetNames(typeof(TestType))));
        }
        internal string subTitle { get; set; } = string.Empty;
        protected override string title => "Test page";
        protected internal override string subtitle => subTitle;
    }
}
