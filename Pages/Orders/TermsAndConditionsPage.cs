using Abc.Data.Classifiers;
using Abc.Data.Orders;
using Abc.Domain.Classifiers;
using Abc.Domain.Orders;
using Abc.Domain.Orders.Terms;
using Abc.Facade.Orders;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Abc.Pages.Orders {
    public sealed class TermsAndConditionsPage :ViewPage<TermsAndConditionsPage, ITermsAndConditionsRepo, TermsAndConditions,
        TermsAndConditionsView, TermsAndConditionsData> {
        private IEnumerable<SelectListItem> termsAndConditionsTypes;
        private IEnumerable<SelectListItem> initializedOrders;
        public IEnumerable<SelectListItem> TermsAndConditionsTypes
            => termsAndConditionsTypes ??= selectListByName<IClassifiersRepo, IClassifier, ClassifierData>(x => x.IsTypeOf(ClassifierType.TermsAndConditions));
        public IEnumerable<SelectListItem> InitializedOrders
            => initializedOrders ??= selectListByName<IOrdersRepo, IOrder, OrderData>( x => x.IsInitialized());
        protected override string title => OrderTitles.TermsAndConditions;
        protected internal override string pageUrl => OrderUrls.TermsAndConditions;
        public TermsAndConditionsPage(ITermsAndConditionsRepo r) : base(r) {}
        protected internal override TermsAndConditions toObject(TermsAndConditionsView v) =>
            new TermsAndConditionsViewFactory().Create(v);
        protected internal override TermsAndConditionsView toView(TermsAndConditions o) =>
            new TermsAndConditionsViewFactory().Create(o);
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Timestamp)
        };
        protected override void tableColumns() {
            tableColumn(x => Item.Name);
            tableColumn(x => Item.Code);
            tableColumn(x => Item.Details);
            tableColumn(x => Item.TypeId);
            tableColumn(x => Item.OrderId);
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Name);
            valueViewer(x => Item.Code);
            valueViewer(x => Item.Details);
            valueViewer(x => Item.TypeId, () => TypeName(Item.TypeId));
            valueViewer(x => Item.OrderId, () => OrderName(Item.TypeId));
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.Name);
            valueEditor(x => Item.Code);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.TypeId, () => TermsAndConditionsTypes);
            valueEditor(x => Item.OrderId, () => InitializedOrders);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        public override IHtmlContent ValueFor(IHtmlHelper<TermsAndConditionsPage> html, int i) => i switch {
            3 => html.Raw(TypeName(Item?.TypeId)),
            4 => html.Raw(OrderName(Item?.OrderId)),
            _ => base.ValueFor(html, i)
        };
        public string TypeName(string id) => itemName(TermsAndConditionsTypes, id);
        public string OrderName(string id) => itemName(InitializedOrders, id);
        public override IActionResult OnGetCreate(
            string sortOrder, string searchString, int? pageIndex,
            string fixedFilter, string fixedValue, int? switchOfCreate) {
            Item = new TermsAndConditionsView();
            return base.OnGetCreate(sortOrder, searchString, pageIndex,
                fixedFilter, fixedValue, switchOfCreate);
        }
    }
}
