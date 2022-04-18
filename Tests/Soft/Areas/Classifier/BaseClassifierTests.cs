using Abc.Data.Classifiers;
using Abc.Facade.Classifiers;
using Abc.Infra.Classifiers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Classifier {

    public abstract class BaseClassifierTests
        :BaseAcceptanceTests<ClassifierView, ClassifierData, ClassifierDb> {
        protected override void doOnCreated(ClassifierDb c) => clearAll(c);

        protected override ClassifierData randomItem() {
            var d = base.randomItem();
            d.ClassifierType = classifierType ?? random<ClassifierType>();
            return d;
        }

        protected List<SelectListItem> baseTypes;
        private int? classifValue => (int)classifierType;

        protected virtual ClassifierType? classifierType => null;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            baseTypes = new List<SelectListItem>();

            foreach (var c in db.Classifiers) {
                if (isThisType(c))
                    baseTypes.Add(new SelectListItem(c.Name, c.Id));
            }
        }

        private bool isThisType(ClassifierData c) => classifierType is null || c?.ClassifierType == classifierType;

        [TestCleanup]
        public override void TestCleanup() {
            base.TestCleanup();
            clearAll(db);
        }
        protected void clearAll(ClassifierDb c) => clear(c.Classifiers);
        protected override void changeValuesExceptId(ClassifierData d) {
            var id = d.Id;
            d = random<ClassifierData>();
            d.ClassifierType = classifierType ?? ClassifierType.Unspecified;
            d.Id = id;
        }

        protected override void addRelatedItems(ClassifierData d) {
            if (d is null) return;
            addClassifier(d.BaseTypeId, classifierType ?? ClassifierType.Unspecified);
        }

        protected override string getItemId(ClassifierData d) => d.Id;

        protected override void setItemId(ClassifierData d, string id) => d.Id = id;

        protected override string baseClassName() => nameof(Classifier);

        protected override void isCorrectPageName() {
            var n = base.baseClassName().ToLower();
            var title = pageTitle().ToLower();
            isTrue(n.StartsWith(title));
        }

        protected override IEnumerable<Expression<Func<ClassifierView, object>>> indexPageColumns =>
            new Expression<Func<ClassifierView, object>>[] {
                x => x.Name,
                x => x.BaseTypeId,
                x => x.Code,
                x => x.Details,
                x => x.ValidFrom,
                x => x.ValidTo
            };

        protected override void dataInDetailsPage() {
            canView(item, m => m.Name);
            canView(item, m => m.Code);
            canView(item, m => m.Details);
            canView(item, m => m.BaseTypeId, "Unspecified");
            canView(item, m => m.ValidFrom);
            canView(item, m => m.ValidTo);
        }

        protected override void dataInEditPage() {
            canEdit(item, m => m.Name, true);
            canEdit(item, m => m.Code);
            canEdit(item, m => m.Details);
            canSelect(item, m => m.BaseTypeId, getBaseTypes(item.Id));
            canEdit(item, m => m.ValidFrom);
            canEdit(item, m => m.ValidTo);
            for(var i = HiddenInputs.Count-1; i >= 0; i--) {
                var s = HiddenInputs[i];
                if (s.Contains(item.ClassifierType.ToString()) ||
                    s.Contains(item.Id.ToString()))
                    HiddenInputs.RemoveAt(i);
            }
        }

        private List<SelectListItem> getBaseTypes(string id) {
            var list = baseTypes;
            var l = new List<SelectListItem>();
            foreach (var e in list) {
                if (e.Value == id) continue;
                l.Add(e);
            }
            return l;
        }

        protected override void dataInCreatePage() {
            item.Code = getCodeValue();
            canEdit(null, m => m.Name, true);
            canEdit(item, m => m.Code);
            canEdit(null, m => m.Details);
            canSelect(null, m => m.BaseTypeId, baseTypes);
            canEdit(null, m => m.ValidFrom);
            canEdit(null, m => m.ValidTo);
            for (var i = HiddenInputs.Count - 1; i >= 0; i--) {
                var s = HiddenInputs[i];
                if (s.Contains(item.ClassifierType.ToString()) ||
                    s.Contains("name=\"Item.Id\""))
                    HiddenInputs.RemoveAt(i);
            }
        }

        private string getCodeValue() {
            var c = db.Classifiers.Where(x => x.ClassifierType == classifierType).ToList().Count + 1;
            return c.ToString();
        }
        protected override void addButtonsFixedFilterAndValue(ref string html) {
            if (classifierType is null) return;
            html = html.Replace(";pageIndex=", $";fixedFilter=ClassifierType&amp;fixedValue={classifValue}&amp;pageIndex=");
        }
        protected override void addHeadersFixedFilterAndValue(ref string html) {
            if (classifierType is null) return;
            html = html.Replace("fixedFilter=&amp;fixedValue=\"", $"fixedFilter=ClassifierType&amp;fixedValue={classifValue}\"");
        }
    }
}
