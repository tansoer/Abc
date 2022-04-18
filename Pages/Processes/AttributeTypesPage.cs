using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Facade.Processes;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using System.Collections.Generic;

namespace Abc.Pages.Processes {
    public sealed class AttributeTypesPage :ViewFactoryPage<AttributeTypesPage, IAttributeTypesRepo,
        AttributeType, AttributeTypeView, AttributeTypeData, AttributeTypeViewFactory> {
        protected override string title => ProcessTitles.AttributeTypes;
        public AttributeTypesPage(IAttributeTypesRepo r) : base(r) { }
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id)
        };
        protected override void tableColumns() {
            tableColumn(x => Item.Name);
            tableColumn(x => Item.ElementTypeId);
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Name);
            valueViewer(x => Item.Code);
            valueViewer(x => Item.Details);
            valueViewer(x => Item.ElementTypeId);
            valueViewer(x => Item.IsMandatory);
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.Name);
            valueEditor(x => Item.Code);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.ElementTypeId);
            valueEditor(x => Item.IsMandatory);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => ProcessUrls.AttributeTypes;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) 
            => Item = new();
    }
}
