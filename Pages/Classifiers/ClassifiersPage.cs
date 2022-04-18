using Abc.Pages.Common;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Extensions;
using Abc.Facade.Classifiers;
using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;

namespace Abc.Pages.Classifiers {
    public sealed class ClassifiersPage :
        ViewsPage<ClassifiersPage, IClassifiersRepo, IClassifier,
        ClassifierView, ClassifierData, ClassifierType> {
        protected override string title => ClassifierTitles.Classifiers;
        public ClassifiersPage(IClassifiersRepo r) : base(r) {
        }
        internal static SelectList getTypeSelectList() {
            var types = Enum.GetValues<ClassifierType>().OrderBy(x => x.ToStr()).ToList();
            var l = new List<string>();
            foreach (var t in types) l.Add(t.ToStr());
            return new SelectList(l);
        }
        protected internal override string pageUrl => ClassifierUrls.Classifiers;
        protected internal override IClassifier toObject(ClassifierView view) => new ClassifierViewFactory().Create(view);
        protected internal override ClassifierView toView(IClassifier obj) => new ClassifierViewFactory().Create(obj);
        private IEnumerable<SelectListItem> baseTypes;
        private IEnumerable<SelectListItem> types;
        public IEnumerable<SelectListItem> BaseTypes => baseTypes ??= selectListByName<IClassifiersRepo, IClassifier, ClassifierData>();
        public IEnumerable<SelectListItem> Types => types ??= getTypeSelectList();
        public override IActionResult OnGetCreate(
            string sortOrder, string searchString, int? pageIndex,
            string fixedFilter, string fixedValue, int? switchOfCreate) {
            baseTypes = selectListByName<IClassifiersRepo, IClassifier, ClassifierData>( x => x.Data.ClassifierType == classificatorType(switchOfCreate ?? 0));
            createItem(switchOfCreate ?? 0);
            return base.OnGetCreate(sortOrder, searchString, pageIndex,
                fixedFilter, fixedValue, switchOfCreate);
        }
        private void createItem(int switchOfCreate) {
            Item = new() {
                Id = Guid.NewGuid().ToString(),
                ClassifierType = classificatorType(switchOfCreate)
            };
        }
        internal static ClassifierType classificatorType(int i) =>
            Enum.IsDefined(typeof(ClassifierType), i)
            ? (ClassifierType)i
            : ClassifierType.Unspecified;
        protected override void tableColumns() {
            tableColumn(x => Item.ClassifierType);
            tableColumn(x => Item.BaseTypeId);
            tableColumn(x => Item.Name);
            tableColumn(x => Item.Code);
            tableColumn(x => Item.Details);
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        public override IHtmlContent ValueFor(IHtmlHelper<ClassifiersPage> html, int i) => i switch {
            0 => html.Raw(Item?.ClassifierType.ToStr()),
            1 => html.Raw(BaseTypeName(Item?.BaseTypeId)),
            _ => base.ValueFor(html, i)
        };
        private object BaseTypeName(string id) => itemName(BaseTypes, id);
    }
}

