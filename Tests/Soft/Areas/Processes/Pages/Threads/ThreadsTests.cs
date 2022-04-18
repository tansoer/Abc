using Abc.Data.Processes;
using Abc.Domain.Common;
using Abc.Facade.Processes;
using Abc.Infra.Parties;
using Abc.Pages.Processes;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Processes.Pages.Threads {
    public abstract class ThreadsTests :BaseProcessTests<ThreadView, ThreadData> {
        protected List<SelectListItem> threadTypes => createSelectList(db.ThreadTypes);
        protected List<SelectListItem> processes => createSelectList(db.Processes);
        protected List<SelectListItem> terminatorSignatures => getContext<PartyDb>().PartySignatures
            .Select(c => new SelectListItem(c.Name, c.Id)).ToList();
        protected override string baseUrl() => ProcessUrls.Threads;
        protected override ThreadView toView(ThreadData d)
            => new ThreadViewFactory().Create(new Abc.Domain.Processes.Thread(d));
        protected override void changeValuesExceptId(ThreadData d) {
            var id = d.Id;
            d = random<ThreadData>();
            d.Id = id;
        }
        protected override string getItemId(ThreadData d) => d.Id;
        protected override void setItemId(ThreadData d, string id) => d.Id = id;
        protected override void addRelatedItems(ThreadData d) {
            if (d is null) return;
            addThreadType(d.ThreadTypeId);
            addProcess(d.ProcessId);
            addPartySignature(d.TerminatorSignatureId);
        }
        protected override void validateItems(ThreadData d1, ThreadData d2) {
            allPropertiesAreEqual(d1, d2,
                nameof(d1.RuleContextId),
                nameof(d1.NextElementId),
                nameof(d1.PreviousElementId));
        }
        protected override IEnumerable<Expression<Func<ThreadView, object>>> indexPageColumns =>
            new Expression<Func<ThreadView, object>>[] {
            x => x.Name,
            x => x.ThreadTypeId,
            x => x.ProcessId,
            x => x.TerminatorSignatureId,
            x => x.ValidFrom,
            x => x.ValidTo
            };
        protected override void dataInDetailsPage() {
            canView(item, x => x.Name);
            canView(item, x => x.Code);
            canView(item, x => x.Details);
            canView(item, x => x.ThreadTypeId, Unspecified.String);
            canView(item, x => x.ProcessId, Unspecified.String);
            canView(item, x => x.TerminatorSignatureId, Unspecified.String);
            canView(item, x => x.ValidFrom);
            canView(item, x => x.ValidTo);
        }
        protected override void dataInEditPage() {
            canEdit(item, x => x.Name, true);
            canEdit(item, x => x.Code);
            canEdit(item, x => x.Details);
            canSelect(item, x => x.ThreadTypeId, threadTypes);
            canSelect(item, x => x.ProcessId, processes);
            canSelect(item, x => x.TerminatorSignatureId, terminatorSignatures);
            canEdit(item, x => x.ValidFrom);
            canEdit(item, x => x.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, x => x.Name, true);
            canEdit(null, x => x.Code);
            canEdit(null, x => x.Details);
            canSelect(null, x => x.ThreadTypeId, threadTypes);
            canSelect(null, x => x.ProcessId, processes);
            canSelect(null, x => x.TerminatorSignatureId, terminatorSignatures);
            canEdit(null, x => x.ValidFrom);
            canEdit(null, x => x.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(null, x => x.Id, true);
        }
    }
    [TestClass] public class CreatePageTests :ThreadsTests { }
    [TestClass] public class DeletePageTests :ThreadsTests { }
    [TestClass] public class DetailsPageTests :ThreadsTests { }
    [TestClass] public class EditPageTests :ThreadsTests { }
    [TestClass] public class IndexPageTests :ThreadsTests { }
}
