using Abc.Data.Classifiers;
using Abc.Data.Parties;
using Abc.Data.Processes;
using Abc.Domain.Classifiers;
using Abc.Domain.Parties.Signatures;
using Abc.Domain.Processes;
using Abc.Facade.Processes;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Action = Abc.Domain.Processes.Action;

namespace Abc.Pages.Processes {
    public sealed class ActionsPage :ViewFactoryPage<ActionsPage, IActionsRepo,
        Action, ActionView, ActionData, ActionViewFactory> {
        protected override string title => ProcessTitles.Actions;
        public ActionsPage(IActionsRepo r) :base(r) {}
        private IEnumerable<SelectListItem> types;
        private IEnumerable<SelectListItem> tasks;
        private IEnumerable<SelectListItem> signatures;
        private IEnumerable<SelectListItem> classifiers;
        public IEnumerable<SelectListItem> ActionTypes => types ??= selectListByName<IActionTypesRepo, ActionType, ActionTypeData>();
        public IEnumerable<SelectListItem> Tasks => tasks ??= selectListByName<ITasksRepo, ITask, TaskData>();
        public IEnumerable<SelectListItem> InitiatorSignatures => signatures ??= selectListByName<IPartySignaturesRepo, PartySignature, PartySignatureData>();
        public IEnumerable<SelectListItem> ActionStatusClassifiers 
            => classifiers ??= selectListByName<IClassifiersRepo, IClassifier, ClassifierData>(x => x.IsTypeOf(ClassifierType.ActionStatus));
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id)
        };
        protected override void tableColumns() {
            tableColumn(x => Item.Name);
            tableColumn(x => Item.ActionTypeId);
            tableColumn(x => Item.TaskId);
            tableColumn(x => Item.InitiatorSignatureId);
            tableColumn(x => Item.ActionStatusClassifierId);
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Name);
            valueViewer(x => Item.Code);
            valueViewer(x => Item.Details);
            valueViewer(x => Item.ActionTypeId, () => ActionTypeName(Item.ActionTypeId));
            valueViewer(x => Item.TaskId, () => TaskName(Item.TaskId));
            valueViewer(x => Item.InitiatorSignatureId, () => InitiatorSignatureName(Item.InitiatorSignatureId));
            valueViewer(x => Item.ActionStatusClassifierId, () => ActionTypeName(Item.ActionStatusClassifierId));
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.Name);
            valueEditor(x => Item.Code);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.ActionTypeId, () => ActionTypes);
            valueEditor(x => Item.TaskId, () => Tasks);
            valueEditor(x => Item.InitiatorSignatureId, () => InitiatorSignatures);
            valueEditor(x => Item.ActionStatusClassifierId, () => ActionStatusClassifiers);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => ProcessUrls.Actions;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) 
            => Item = new();
        public string ActionTypeName(string id) => itemName(ActionTypes, id);
        public string TaskName(string id) => itemName(Tasks, id);
        public string InitiatorSignatureName(string id) => itemName(InitiatorSignatures, id);
        public string ActionStatusClassifierName(string id) => itemName(ActionStatusClassifiers, id);

    }
}
