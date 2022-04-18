using Abc.Data.Parties;
using Abc.Data.Products;
using Abc.Domain.Parties.Signatures;
using Abc.Domain.Products;
using Abc.Facade.Products;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Abc.Pages.Products {
    public sealed class BatchCheckedByPage :ViewFactoryPage<BatchCheckedByPage, IBatchCheckedByRepo,
        BatchCheckedBy, BatchCheckedByView, BatchCheckedByData, BatchCheckedByViewFactory> {
        protected override string title => ProductTitles.BatchCheckedBy;
        public BatchCheckedByPage(IBatchCheckedByRepo r) :base(r) {}
        private IEnumerable<SelectListItem> batches;
        public IEnumerable<SelectListItem> Batches => batches ??= selectListByName<IBatchesRepo, Batch, BatchData>();
        private IEnumerable<SelectListItem> partySignatures;
        public IEnumerable<SelectListItem> PartySignatures => partySignatures ??= selectListByName<IPartySignaturesRepo, PartySignature, PartySignatureData>();
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id),
        };
        protected override void tableColumns() {
            tableColumn(x => Item.BatchId, () => BatchName(Item.BatchId));
            tableColumn(x => Item.PartySignatureId, () => PartySignatureName(Item.PartySignatureId));
            tableColumn(x => Item.CheckedBy);
            tableColumn(x => Item.Address);
            tableColumn(x => Item.EmailAddress);
            tableColumn(x => Item.PhoneNumber);
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Id);
            valueViewer(x => Item.Name);
            valueViewer(x => Item.Code);
            valueViewer(x => Item.BatchId, () => BatchName(Item.BatchId));
            valueViewer(x => Item.PartySignatureId, () => PartySignatureName(Item.PartySignatureId));
            valueViewer(x => Item.CheckedBy);
            valueViewer(x => Item.Address);
            valueViewer(x => Item.EmailAddress);
            valueViewer(x => Item.PhoneNumber);
            valueViewer(x => Item.Details);
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.Name);
            valueEditor(x => Item.Code);
            valueEditor(x => Item.BatchId, () => Batches);
            valueEditor(x => Item.PartySignatureId, () => PartySignatures);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => ProductUrls.BatchCheckedBy;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) {
            Item = new();
            if (HasFixedFilter) Item.BatchId = fixedValue;
        } 
        public string BatchName(string id) => itemName(Batches, id);
        public string PartySignatureName(string id) => itemName(PartySignatures, id);
        protected internal override string subtitle => subTitle(removeId(FixedFilter), BatchName(FixedValue));
        private string subTitle(string item, string name) => HasFixedFilter ? $"For {item} ({name})" : string.Empty;
        private static string removeId(string idField) => idField?.Replace("Id", string.Empty) ?? string.Empty;
    }
}
