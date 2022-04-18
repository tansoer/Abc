using Abc.Data.Processes;
using Abc.Domain.Common;
using Abc.Facade.Processes;
using Abc.Infra.Classifiers;
using Abc.Infra.Roles;
using Abc.Infra.Rules;
using Abc.Pages.Processes;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Processes.Pages.CRM {
    public abstract class CrmTests :BaseProcessTests<ProcessView, ProcessData> {
        protected List<SelectListItem> tasks => createSelectList(db.Tasks);
        protected List<SelectListItem> processTypes => createSelectList(db.ProcessTypes);
        protected List<SelectListItem> ruleContexts => getContext<RuleDb>().RuleContexts
            .Select(c => new SelectListItem(c.Name, c.Id)).ToList();
        protected List<SelectListItem> managerPartyRoles => getContext<RoleDb>().Roles
            .Select(c => new SelectListItem(c.Name, c.Id)).ToList();
        protected List<SelectListItem> initiatorPartyRoles => getContext<RoleDb>().Roles
            .Select(c => new SelectListItem(c.Name, c.Id)).ToList();
        protected List<SelectListItem> priorityClassifiers => getContext<ClassifierDb>().Classifiers
            .Select(c => new SelectListItem(c.Name, c.Id)).ToList();
        protected override string baseUrl() => ProcessUrls.Processes;
        protected override ProcessView toView(ProcessData d)
            => new ProcessViewFactory().Create(new Abc.Domain.Processes.Process(d));
        protected override string baseClassName() => "Process";
        protected override string performTitleCorrection(string title) => "Process";
        protected override void changeValuesExceptId(ProcessData d) {
            var id = d.Id;
            d = random<ProcessData>();
            d.Id = id;
        }
        protected override string getItemId(ProcessData d) => d.Id;
        protected override void setItemId(ProcessData d, string id) => d.Id = id;
        protected override void addRelatedItems(ProcessData d) {
            if (d is null) return;
            addRuleContext(d.RuleContextId);
            addProcessType(d.ProcessTypeId);
            addPartyRole(d.ManagerPartyRoleId);
            addPartyRole(d.InitiatorPartyRoleId);
            addPriorityClassifier(d.PriorityClassifierId);
        }
        protected override IEnumerable<Expression<Func<ProcessView, object>>> indexPageColumns =>
            new Expression<Func<ProcessView, object>>[] {
            x => x.Name,
            x => x.RuleContextId,
            x => x.ProcessTypeId,
            x => x.ManagerPartyRoleId,
            x => x.InitiatorPartyRoleId,
            x => x.PriorityClassifierId,
            x => x.ValidFrom,
            x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, x => x.Name);
            canView(item, x => x.Code);
            canView(item, x => x.Details);
            canView(item, x => x.RuleContextId, Unspecified.String);
            canView(item, x => x.ProcessTypeId, Unspecified.String);
            canView(item, x => x.ManagerPartyRoleId, Unspecified.String);
            canView(item, x => x.InitiatorPartyRoleId, Unspecified.String);
            canView(item, x => x.PriorityClassifierId, Unspecified.String);
            canView(item, x => x.ValidFrom);
            canView(item, x => x.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, x => x.Name, true);
            canEdit(item, x => x.Code);
            canEdit(item, x => x.Details);
            canSelect(item, x => x.RuleContextId, ruleContexts);
            canSelect(item, x => x.ProcessTypeId, processTypes);
            canSelect(item, x => x.ManagerPartyRoleId, managerPartyRoles);
            canSelect(item, x => x.InitiatorPartyRoleId, initiatorPartyRoles);
            canSelect(item, x => x.PriorityClassifierId, priorityClassifiers);
            canEdit(item, x => x.ValidFrom);
            canEdit(item, x => x.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, x => x.Name, true);
            canEdit(null, x => x.Code);
            canEdit(null, x => x.Details);
            canSelect(null, x => x.RuleContextId, ruleContexts);
            canSelect(null, x => x.ProcessTypeId, processTypes);
            canSelect(null, x => x.ManagerPartyRoleId, managerPartyRoles);
            canSelect(null, x => x.InitiatorPartyRoleId, initiatorPartyRoles);
            canSelect(null, x => x.PriorityClassifierId, priorityClassifiers);
            canEdit(null, x => x.ValidFrom);
            canEdit(null, x => x.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(null, x => x.Id, true);
        }
    }
    [TestClass] public class CreatePageTests :CrmTests { }
    [TestClass] public class DeletePageTests :CrmTests { }
    [TestClass] public class DetailsPageTests :CrmTests { }
    [TestClass] public class EditPageTests :CrmTests { }
    [TestClass] public class IndexPageTests :CrmTests { }
}
