using Abc.Aids.Enums;
using System;
using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Processes;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Abc.Pages.Processes {
    public sealed class AttributeValuesPage :ViewsPage<AttributeValuesPage, IAttributeValuesRepo,
        AttributeValue, AttributeValueView, AttributeValueData, AttributeValueType> {
        protected override string title => ProcessTitles.AttributeValues;
        internal readonly IAttributeTypesRepo attributeTypesRepo;
        public IEnumerable<SelectListItem> Equalities { get; }
        public AttributeValuesPage(IAttributeValuesRepo r, IAttributeTypesRepo types) : base(r) {
            attributeTypesRepo = types;
            Equalities = Enum.GetValues(typeof(Relation))
                .Cast<Relation>()
                .Select(p => new SelectListItem() {
                    Text = p.ToString(),
                    Value = ((int)p).ToString()
                })
                .ToList();
        }
        private IEnumerable<SelectListItem> types;
        public IEnumerable<SelectListItem> AttributeTypes => types ??= selectListByName<IAttributeTypesRepo, AttributeType, AttributeTypeData>();
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id),
            field(x => Item.Type)
        };
        protected override void tableColumns() {
            tableColumn(x => Item.Name);
            tableColumn(x => Item.Type);
            tableColumn(x => Item.ProcessElementId);
            tableColumn(x => Item.AttributeTypeId);
            tableColumn(x => Item.Equality);
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Name);
            valueViewer(x => Item.Code);
            valueViewer(x => Item.Details);
            valueViewer(x => Item.ProcessElementId);
            valueViewer(x => Item.AttributeTypeId, () => AttributeTypeName(Item.AttributeTypeId));
            valueViewer(x => Item.Equality);
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.Name);
            valueEditor(x => Item.Code);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.ProcessElementId);
            valueEditor(x => Item.AttributeTypeId, () => AttributeTypes);
            valueEditor(x => Item.Equality, () => Equalities);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        public AttributeValueType Type { get; internal set; }
        protected internal override string pageUrl => ProcessUrls.AttributeValues;
        protected internal override AttributeValue toObject(AttributeValueView v)
            => new AttributeValueViewFactory().Create(v);
        protected internal override AttributeValueView toView(AttributeValue o) {
            Type = o.Data.Type;
            return new AttributeValueViewFactory().Create(o);
        }
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) {
            Item = new AttributeValueView {
                Type = Type
            };
        }
        public string AttributeTypeName(string id) => itemName(AttributeTypes, id);
    }
}