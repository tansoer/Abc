using Abc.Data.Processes;
using Abc.Domain.Common;
using Abc.Facade.Processes;
using Abc.Infra.Parties;
using Abc.Infra.Rules;
using Abc.Pages.Processes;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Processes.Pages.Tasks {
    public abstract class TasksTests :BaseProcessTests<TaskView, TaskData> {
        protected List<SelectListItem> threads => createSelectList(db.Threads);
        protected List<SelectListItem> nextElements => createSelectList(db.Tasks);
        protected List<SelectListItem> previousElements => createSelectList(db.Tasks);
        protected List<SelectListItem> ruleContexts => getContext<RuleDb>().RuleContexts
            .Select(c => new SelectListItem(c.Name, c.Id)).ToList();
        protected List<SelectListItem> fromPartyAddresses => getContext<PartyDb>().PartyContacts
            .Select(c => new SelectListItem(c.Name, c.Id)).ToList();
        protected List<SelectListItem> toPartyAddresses => getContext<PartyDb>().PartyContacts
            .Select(c => new SelectListItem(c.Name, c.Id)).ToList();
        protected override string baseUrl() => ProcessUrls.Tasks;
        protected override TaskView toView(TaskData d)
            => new TaskViewFactory().Create(new Abc.Domain.Processes.Task(d));
        protected override void changeValuesExceptId(TaskData d) {
            var id = d.Id;
            d = random<TaskData>();
            d.Id = id;
        }
        protected override string getItemId(TaskData d) => d.Id;
        protected override void setItemId(TaskData d, string id) => d.Id = id;
        protected override void addRelatedItems(TaskData d) {
            if (d is null) return;
            addThread(d.ThreadId);
            addRuleContext(d.RuleContextId);
            addTask(d.NextElementId);
            addTask(d.PreviousElementId);
            addPartyContact(d.FromPartyAddressId);
            addPartyContact(d.ToPartyAddressId);
        }
        protected override void validateItems(TaskData d1, TaskData d2) {
            allPropertiesAreEqual(d1, d2,
                nameof(d1.RelationshipTypeId),
                nameof(d1.IsEscalation),
                nameof(d1.ConsumerEntityId),
                nameof(d1.ProviderEntityId));
        }
        protected override IEnumerable<Expression<Func<TaskView, object>>> indexPageColumns =>
            new Expression<Func<TaskView, object>>[] {
            x => x.Name,
            x => x.ThreadId,
            x => x.RuleContextId,
            x => x.NextElementId,
            x => x.PreviousElementId,
            x => x.FromPartyAddressId,
            x => x.ToPartyAddressId,
            x => x.ValidFrom,
            x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, x => x.Name);
            canView(item, x => x.Code);
            canView(item, x => x.Details);
            canView(item, x => x.ThreadId, Unspecified.String);
            canView(item, x => x.RuleContextId, Unspecified.String);
            canView(item, x => x.NextElementId, Unspecified.String);
            canView(item, x => x.PreviousElementId, Unspecified.String);
            canView(item, x => x.FromPartyAddressId, Unspecified.String);
            canView(item, x => x.ToPartyAddressId, Unspecified.String);
            canView(item, x => x.ValidFrom);
            canView(item, x => x.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, x => x.Name, true);
            canEdit(item, x => x.Code);
            canEdit(item, x => x.Details);
            canSelect(item, x => x.ThreadId, threads);
            canSelect(item, x => x.RuleContextId, ruleContexts);
            canSelect(item, x => x.NextElementId, nextElements);
            canSelect(item, x => x.PreviousElementId, previousElements);
            canSelect(item, x => x.FromPartyAddressId, fromPartyAddresses);
            canSelect(item, x => x.ToPartyAddressId, toPartyAddresses);
            canEdit(item, x => x.ValidFrom);
            canEdit(item, x => x.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, x => x.Name, true);
            canEdit(null, x => x.Code);
            canEdit(null, x => x.Details);
            canSelect(null, x => x.ThreadId, threads);
            canSelect(null, x => x.RuleContextId, ruleContexts);
            canSelect(null, x => x.NextElementId, nextElements);
            canSelect(null, x => x.PreviousElementId, previousElements);
            canSelect(null, x => x.FromPartyAddressId, fromPartyAddresses);
            canSelect(null, x => x.ToPartyAddressId, toPartyAddresses);
            canEdit(null, x => x.ValidFrom);
            canEdit(null, x => x.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(null, x => x.Id, true);
        }
    }
    [TestClass] public class CreatePageTests :TasksTests { }
    [TestClass] public class DeletePageTests :TasksTests { }
    [TestClass] public class DetailsPageTests :TasksTests { }
    [TestClass] public class EditPageTests :TasksTests { }
    [TestClass] public class IndexPageTests :TasksTests { }
}
