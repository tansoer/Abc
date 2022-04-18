using Abc.Data.Processes;
using Abc.Domain.Common;
using Abc.Facade.Processes;
using Abc.Infra.Rules;
using Abc.Pages.Processes;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Processes.Pages.ProcessTypes {
    public abstract class ProcessTypesTests :BaseProcessTests<ProcessTypeView, ProcessTypeData> {
        protected List<SelectListItem> ruleSets => getContext<RuleDb>().RuleSets
            .Select(c => new SelectListItem(c.Name, c.Id)).ToList();
        protected override string baseUrl() => ProcessUrls.ProcessTypes;
        protected override ProcessTypeView toView(ProcessTypeData d)
            => new ProcessTypeViewFactory().Create(new Abc.Domain.Processes.ProcessType(d));
        protected override void changeValuesExceptId(ProcessTypeData d) {
            var id = d.Id;
            d = random<ProcessTypeData>();
            d.Id = id;
        }
        protected override string getItemId(ProcessTypeData d) => d.Id;
        protected override void setItemId(ProcessTypeData d, string id) => d.Id = id;
        protected override void addRelatedItems(ProcessTypeData d) {
            if (d is null) return;
            addRuleSet(d.RuleSetId);
        }
        protected override void validateItems(ProcessTypeData d1, ProcessTypeData d2) {
            allPropertiesAreEqual(d1, d2,
                nameof(d1.BaseTypeId));
        }
        protected override IEnumerable<Expression<Func<ProcessTypeView, object>>> indexPageColumns =>
            new Expression<Func<ProcessTypeView, object>>[] {
            x => x.Name,
            x => x.ValidFrom,
            x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, x => x.Name);
            canView(item, x => x.Code);
            canView(item, x => x.Details);
            canView(item, x => x.RuleSetId, Unspecified.String);
            canView(item, x => x.ValidFrom);
            canView(item, x => x.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, x => x.Name, true);
            canEdit(item, x => x.Code);
            canEdit(item, x => x.Details);
            canSelect(item, x => x.RuleSetId, ruleSets);
            canEdit(item, x => x.ValidFrom);
            canEdit(item, x => x.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, x => x.Name, true);
            canEdit(null, x => x.Code);
            canEdit(null, x => x.Details);
            canSelect(null, x => x.RuleSetId, ruleSets);
            canEdit(null, x => x.ValidFrom);
            canEdit(null, x => x.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(null, x => x.Id, true);
        }
    }
    [TestClass] public class CreatePageTests :ProcessTypesTests { }
    [TestClass] public class DeletePageTests :ProcessTypesTests { }
    [TestClass] public class DetailsPageTests :ProcessTypesTests { }
    [TestClass] public class EditPageTests :ProcessTypesTests { }
    [TestClass] public class IndexPageTests :ProcessTypesTests { }
}
