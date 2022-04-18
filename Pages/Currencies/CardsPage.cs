using System.Collections.Generic;
using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Facade.Currencies;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;

namespace Abc.Pages.Currencies {

    public sealed class CardsPage : ViewPage<CardsPage, ICardAssociationsRepo, CardAssociation, CardAssociationView,
        CardAssociationData> {
        protected override string title => MoneyTitles.Cards;
        public CardsPage(ICardAssociationsRepo r) : base(r) { }
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Timestamp)
        };
        protected override void tableColumns() {
            tableColumn(x => x.Item.Id);
            tableColumn(x => x.Item.Name);
            tableColumn(x => x.Item.Code);
            tableColumn(x => x.Item.Details);
            tableColumn(x => x.Item.ValidFrom);
            tableColumn(x => x.Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Id);
            valueViewer(x => Item.Name);
            valueViewer(x => Item.Code);
            valueViewer(x => Item.Details);
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.Id);
            valueEditor(x => Item.Name);
            valueEditor(x => Item.Code);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => MoneyUrls.Cards;
        protected internal override CardAssociation toObject(CardAssociationView v) =>
            new CardAssociationViewFactory().Create(v);
        protected internal override CardAssociationView toView(CardAssociation o) =>
            new CardAssociationViewFactory().Create(o);
    }
}

