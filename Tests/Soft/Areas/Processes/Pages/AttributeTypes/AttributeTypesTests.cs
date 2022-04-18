using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Processes;
using Abc.Pages.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Processes.Pages.AttributeTypes {
    public abstract class AttributeTypesTests :BaseProcessTests<AttributeTypeView, AttributeTypeData> {
        protected override string baseUrl() => ProcessUrls.AttributeTypes;
        protected override void changeValuesExceptId(AttributeTypeData d) {
            var id = d.Id;
            d = random<AttributeTypeData>();
            d.Id = id;
        }
        protected override string getItemId(AttributeTypeData d) => d.Id;
        protected override void setItemId(AttributeTypeData d, string id) => d.Id = id;
        protected override IEnumerable<Expression<Func<AttributeTypeView, object>>> indexPageColumns =>
            new Expression<Func<AttributeTypeView, object>>[] {
            x => x.Name,
            x => x.ElementTypeId,
            x => x.ValidFrom,
            x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, x => x.Name);
            canView(item, x => x.Code);
            canView(item, x => x.Details);
            canView(item, x => x.ElementTypeId);
            canView(item, x => x.ValidFrom);
            canView(item, x => x.ValidTo);
        }
        protected override AttributeTypeView toView(AttributeTypeData d) => new AttributeTypeViewFactory().Create(new AttributeType(d));
        protected override void dataInEditPage() {
            AttributeTypeView v = toView(item);
            canEdit(v, x => x.Name, true);
            canEdit(v, x => x.Code);
            canEdit(v, x => x.Details);
            canEdit(v, x => x.ElementTypeId);
            canClickCheckBox(v, x => x.IsMandatory, true, true);
            canEdit(v, x => x.ValidFrom);
            canEdit(v, x => x.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, x => x.Name, true);
            canEdit(null, x => x.Code);
            canEdit(null, x => x.Details);
            canEdit(null, x => x.ElementTypeId);
            canClickCheckBox(null, x => x.IsMandatory, true, true);
            canEdit(null, x => x.ValidFrom);
            canEdit(null, x => x.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(item, x => x.Id, true);
            HiddenInputs.Clear();
        }
    }
    [TestClass] public class CreatePageTests :AttributeTypesTests { }
    [TestClass] public class DeletePageTests :AttributeTypesTests { }
    [TestClass] public class DetailsPageTests :AttributeTypesTests { }
    [TestClass] public class EditPageTests :AttributeTypesTests { }
    [TestClass] public class IndexPageTests :AttributeTypesTests { }
}
