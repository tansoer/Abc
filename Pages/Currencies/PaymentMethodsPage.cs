using Abc.Aids.Methods;
using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Facade.Currencies;
using Abc.Pages.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Abc.Pages.Currencies {
    public sealed class PaymentMethodsPage : ViewsPage<PaymentMethodsPage, IPaymentMethodsRepo, 
        PaymentMethod, PaymentMethodView, PaymentMethodData, PaymentMethodType> {
        protected override string title => MoneyTitles.PaymentMethods;
        public PaymentMethodsPage(IPaymentMethodsRepo r) :base(r) {}
        private IEnumerable<SelectListItem> currencies;
        public IEnumerable<SelectListItem> Currencies => currencies ??= selectListByName<ICurrencyRepo, Currency, CurrencyData>();
        private IEnumerable<SelectListItem> cardAssociations;
        public IEnumerable<SelectListItem> CardAssociations => cardAssociations ??= selectListByName<ICardAssociationsRepo, CardAssociation, CardAssociationData>();
        protected override void tableColumns() {
            tableColumn(x => Item.PaymentMethodType);
            tableColumn(x => Item.Name);
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Id);
            switch (Item?.PaymentMethodType)
            {
                case PaymentMethodType.Check:
                    valueViewer(x => Item.Name, "Account name");
                    valueViewer(x => Item.Code, "Account number");
                    valueViewer(x => Item.CardOrCheckNumber, "Check number");
                    valueViewer(x => Item.Payee);
                    valueViewer(x => Item.BankName);
                    valueViewer(x => Item.BillingOrBankAddress, "Bank address");
                    valueViewer(x => Item.IssueOrBankIdNumber, "Bank identification number");
                    break;
                case PaymentMethodType.CreditCard:
                    valueViewer(x => Item.CardAssociationId, () => CardAssociationName(Item.CardAssociationId));
                    valueViewer(x => Item.CurrencyId, () => CurrencyName(Item.CurrencyId));
                    valueViewer(x => Item.Name, "Name on card");
                    valueViewer(x => Item.CardOrCheckNumber, "Card number");
                    valueViewer(x => Item.BillingOrBankAddress, "Billing address");
                    valueViewer(x => Item.Code, "Card verification code");
                    valueViewer(x => Item.IssueOrBankIdNumber, "Issue number");
                    valueViewer(x => Item.DailyLimit);
                    valueViewer(x => Item.CreditLimit);
                    break;
                case PaymentMethodType.DebitCard or PaymentMethodType.LoyaltyCard:
                    valueViewer(x => Item.CardAssociationId, () => CardAssociationName(Item.CardAssociationId));
                    valueViewer(x => Item.CurrencyId, () => CurrencyName(Item.CurrencyId));
                    valueViewer(x => Item.Name, "Name on card");
                    valueViewer(x => Item.CardOrCheckNumber, "Card number");
                    valueViewer(x => Item.BillingOrBankAddress, "Billing address");
                    valueViewer(x => Item.Code, "Card verification code");
                    valueViewer(x => Item.IssueOrBankIdNumber, "Issue number");
                    valueViewer(x => Item.DailyLimit);
                    break;
                case PaymentMethodType.BankAccount:
                    valueViewer(x => Item.Name, "Account name");
                    valueViewer(x => Item.Code, "Account number");
                    valueViewer(x => Item.BankName);
                    valueViewer(x => Item.BillingOrBankAddress, "Bank address");
                    valueViewer(x => Item.IssueOrBankIdNumber, "Bank identification number");
                    break;
                default:
                    valueViewer(x => Item.Name);
                    break;
            }

            valueViewer(x => Item.Details);
            valueViewer(x => Item.ValidFrom); 
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            switch (Item?.PaymentMethodType) {
                case PaymentMethodType.Check:
                    valueEditor(x => Item.Name, "Account name");
                    valueEditor(x => Item.Code, "Account number");
                    valueEditor(x => Item.CardOrCheckNumber, "Check number");
                    valueEditor(x => Item.Payee);
                    valueEditor(x => Item.BankName);
                    valueEditor(x => Item.BillingOrBankAddress, "Bank address");
                    valueEditor(x => Item.IssueOrBankIdNumber, "Bank identification number");
                    break;
                case PaymentMethodType.CreditCard:
                    valueEditor(x => Item.CardAssociationId, () => CardAssociations);
                    valueEditor(x => Item.CurrencyId, () => Currencies);
                    valueEditor(x => Item.Name, "Name on card");
                    valueEditor(x => Item.CardOrCheckNumber, "Card number");
                    valueEditor(x => Item.BillingOrBankAddress, "Billing address");
                    valueEditor(x => Item.Code, "Card verification code");
                    valueEditor(x => Item.IssueOrBankIdNumber, "Issue number");
                    valueEditor(x => Item.DailyLimit);
                    valueEditor(x => Item.CreditLimit);
                    break;
                case PaymentMethodType.DebitCard or PaymentMethodType.LoyaltyCard:
                    valueEditor(x => Item.CardAssociationId, () => CardAssociations);
                    valueEditor(x => Item.CurrencyId, () => Currencies);
                    valueEditor(x => Item.Name, "Name on card");
                    valueEditor(x => Item.CardOrCheckNumber, "Card number");
                    valueEditor(x => Item.BillingOrBankAddress, "Billing address");
                    valueEditor(x => Item.Code, "Card verification code");
                    valueEditor(x => Item.IssueOrBankIdNumber, "Issue number");
                    valueEditor(x => Item.DailyLimit);
                    break;
                case PaymentMethodType.BankAccount:
                    valueEditor(x => Item.Name, "Account name");
                    valueEditor(x => Item.Code, "Account number");
                    valueEditor(x => Item.BankName);
                    valueEditor(x => Item.BillingOrBankAddress, "Bank address");
                    valueEditor(x => Item.IssueOrBankIdNumber, "Bank identification number");
                    break;
                default:
                    valueEditor(x => Item.Name);
                    break;
            }
            valueEditor(x => Item.Details);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => MoneyUrls.PaymentMethods;
        protected internal override PaymentMethod toObject(PaymentMethodView v) =>
            new PaymentMethodViewFactory().Create(v);
        protected internal override PaymentMethodView toView(PaymentMethod o) =>
            new PaymentMethodViewFactory().Create(o);
        public string CardAssociationName(string id) => itemName(CardAssociations, id);
        public string CurrencyName(string id) => itemName(Currencies, id);
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) {
            Item = new();
            Item.PaymentMethodType = Safe.Run(() => (PaymentMethodType)(switchOfCreate ?? 1000), PaymentMethodType.Unspecified);
        }
    }
}