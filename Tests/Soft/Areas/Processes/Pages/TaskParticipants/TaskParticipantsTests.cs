using Abc.Data.Processes;
using Abc.Domain.Common;
using Abc.Facade.Processes;
using Abc.Infra.Roles;
using Abc.Pages.Processes;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Processes.Pages.TaskParticipants {
    public abstract class TaskParticipantsTests :BaseProcessTests<TaskParticipantView, TaskParticipantData> {
        protected List<SelectListItem> partyRoles => getContext<RoleDb>().Roles
            .Select(c => new SelectListItem(c.Name, c.Id)).ToList();
        protected List<SelectListItem> tasks => createSelectList(db.Tasks);
        protected override string baseUrl() => ProcessUrls.TaskParticipants;
        protected override TaskParticipantView toView(TaskParticipantData d)
            => new TaskParticipantViewFactory().Create(new Abc.Domain.Processes.TaskParticipant(d));
        protected override void changeValuesExceptId(TaskParticipantData d) {
            var id = d.Id;
            d = random<TaskParticipantData>();
            d.Id = id;
        }
        protected override string getItemId(TaskParticipantData d) => d.Id;
        protected override void setItemId(TaskParticipantData d, string id) => d.Id = id;
        protected override void addRelatedItems(TaskParticipantData d) {
            if (d is null) return;
            addPartyRole(d.PartyRoleId);
            addTask(d.TaskId);
        }
        protected override IEnumerable<Expression<Func<TaskParticipantView, object>>> indexPageColumns =>
            new Expression<Func<TaskParticipantView, object>>[] {
            x => x.Name,
            x => x.PartyRoleId,
            x => x.TaskId,
            x => x.ValidFrom,
            x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, x => x.Name);
            canView(item, x => x.Code);
            canView(item, x => x.Details);
            canView(item, x => x.PartyRoleId, Unspecified.String);
            canView(item, x => x.TaskId, Unspecified.String);
            canView(item, x => x.ValidFrom);
            canView(item, x => x.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, x => x.Name, true);
            canEdit(item, x => x.Code);
            canEdit(item, x => x.Details);
            canSelect(item, x => x.PartyRoleId, partyRoles);
            canSelect(item, x => x.TaskId, tasks);
            canEdit(item, x => x.ValidFrom);
            canEdit(item, x => x.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, x => x.Name, true);
            canEdit(null, x => x.Code);
            canEdit(null, x => x.Details);
            canSelect(null, x => x.PartyRoleId, partyRoles);
            canSelect(null, x => x.TaskId, tasks);
            canEdit(null, x => x.ValidFrom);
            canEdit(null, x => x.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(null, x => x.Id, true);
        }
    }
    [TestClass] public class CreatePageTests :TaskParticipantsTests { }
    [TestClass] public class DeletePageTests :TaskParticipantsTests { }
    [TestClass] public class DetailsPageTests :TaskParticipantsTests { }
    [TestClass] public class EditPageTests :TaskParticipantsTests { }
    [TestClass] public class IndexPageTests :TaskParticipantsTests { }
}
