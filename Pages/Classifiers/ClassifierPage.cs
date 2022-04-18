using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Facade.Classifiers;
using Abc.Pages.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abc.Pages.Classifiers {
    public abstract class ClassifierPage<TPage> :ViewFactoryPage<TPage, IClassifiersRepo, IClassifier,
        ClassifierView, ClassifierData, ClassifierViewFactory> where TPage : PageModel {
        protected abstract ClassifierType classifierType { get; }
        private IEnumerable<SelectListItem> baseTypes;
        public IEnumerable<SelectListItem> BaseTypes => baseTypes ??= selectListByName<IClassifiersRepo, IClassifier, ClassifierData>(x => x.IsTypeOf(classifierType));
        public ClassifierPage(IClassifiersRepo r) : base(r) { }
        protected override void addFields() {
            addField(x => Item.Name);
            addField(x => Item.Code);
            addField(x => Item.Details);
            addField(x => Item.BaseTypeId, () => getBaseTypes(Item.Id), () => BaseTypeName(Item.BaseTypeId));
            addField(x => Item.ValidFrom);
            addField(x => Item.ValidTo);
        }
        private IEnumerable<SelectListItem> getBaseTypes(string id) {
            var types = BaseTypes;
            var l = new List<SelectListItem>();
            foreach( var t in types) {
                if (t.Value == id) continue;
                l.Add(t);
            }
            return l;
        }
        public string BaseTypeName(string id) => itemName(BaseTypes, id);
        private void createItem(int count) {
            Item = new() {
                Id = Guid.NewGuid().ToString(),
                ClassifierType = classifierType,
                Code = count.ToString()
            };
        }
        public override async Task<IActionResult> OnGetIndexAsync(string sortOrder,
           string id, string currentFilter, string searchString,
           int? pageIndex, string fixedFilter, string fixedValue, string[] fixedValues = null) {
            fixedFilter = nameof(ClassifierData.ClassifierType);
            fixedValue = ((int)classifierType).ToString();
            return await base.OnGetIndexAsync(sortOrder, id, currentFilter, searchString,
                pageIndex, fixedFilter, fixedValue);
        }
        public override IActionResult OnGetCreate(
            string sortOrder, string searchString, int? pageIndex,
            string fixedFilter, string fixedValue, int? switchOfCreate) {
            var count = BaseTypes.Count();
            createItem(count);
            return base.OnGetCreate(sortOrder, searchString, pageIndex,
                fixedFilter, fixedValue, switchOfCreate);
        }
    }
}