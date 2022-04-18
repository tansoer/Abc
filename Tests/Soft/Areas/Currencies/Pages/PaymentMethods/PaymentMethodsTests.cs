using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Domain.Common;
using Abc.Domain.Currencies;
using Abc.Facade.Currencies;
using Abc.Pages.Currencies;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Currencies.Pages.PaymentMethods {
    public abstract class PaymentMethodsTests :BaseCurrenciesTests<PaymentMethodView, PaymentMethodData> {

        protected PaymentMethodType? paymentMethodType;
        protected List<SelectListItem> currencies => createSelectList(db.Currencies);
        protected List<SelectListItem> cardAssociations => createSelectList(db.CardAssociations);
        protected override string baseUrl() => MoneyUrls.PaymentMethods;
        protected override void addRelatedItems(PaymentMethodData d) {
            if (d is null) return;
            addCurrency(d.CurrencyId);
            addCardAssociation(d.CardAssociationId);
            correctPaymentMethod(d);
        }
        private void correctPaymentMethod(PaymentMethodData d) {
            if (d is null) return;
            switch (d.PaymentMethodType)
            {
                case PaymentMethodType.DebitCard or PaymentMethodType.LoyaltyCard:
                    d.BankName = null;
                    d.Payee = null;
                    break;
                case PaymentMethodType.CreditCard:
                    d.BankName = null;
                    d.Payee = null;
                    break;
                case PaymentMethodType.BankAccount:
                    d.CardOrCheckNumber = null;
                    d.CardAssociationId = null;
                    d.CurrencyId = null;
                    d.Payee = null;
                    break;
                case PaymentMethodType.Check:
                    d.CardAssociationId = null;
                    d.CurrencyId = null;
                    break;
                default:
                    d.IssueOrBankIdNumber = null;
                    d.BillingOrBankAddress = null;
                    d.CardOrCheckNumber = null;
                    d.BankName = null;
                    d.CurrencyId = null;
                    d.Payee = null;
                    d.CardAssociationId = null;
                    d.Code = null;
                    break;
            }
        }
        protected override void changeValuesExceptId(PaymentMethodData d) {
            var id = d.Id;
            d = random<PaymentMethodData>();
            d.Id = id;
        }
        protected override PaymentMethodData correctOriginal(PaymentMethodData oldItem, PaymentMethodData newItem) {
            oldItem.PaymentMethodType = newItem.PaymentMethodType;
            update<IPaymentMethodsRepo, PaymentMethod>(PaymentMethodFactory.Create(oldItem));
            return oldItem;
        }
        protected override string getItemId(PaymentMethodData d) => d.Id;
        protected override void setItemId(PaymentMethodData d, string id) => d.Id = id;
        protected override PaymentMethodData randomItem() {
            var d = base.randomItem();
            d.PaymentMethodType = paymentMethodType ?? d.PaymentMethodType;
            d.DailyLimit = Math.Round(GetRandom.Decimal(-1000, 1000), 2);
            d.CreditLimit = Math.Round(GetRandom.Decimal(-1000, 1000), 2);
            return d;
        }
        protected override PaymentMethodView toView(PaymentMethodData d) {
            var o = PaymentMethodFactory.Create(d);
            var v = new PaymentMethodViewFactory().Create(o);
            return v;
        }
        protected override string pageUrl() {
            var url = base.pageUrl();
            if (paymentMethodType is null) return url;
            var typeIdx = Convert.ToInt32(paymentMethodType);
            url += $"&switchOfCreate={typeIdx}";
            return url;
        }
        protected override IEnumerable<Expression<Func<PaymentMethodView, object>>> indexPageColumns =>
            new Expression<Func<PaymentMethodView, object>>[] {
                x => x.PaymentMethodType,
                x => x.Name,
                x => x.ValidFrom,
                x => x.ValidTo,
            };
        protected override void dataInDetailsPage() {
            PaymentMethodView v = toView(item);
            canView(v, x => x.Id);
            switch (v.PaymentMethodType) {
                case PaymentMethodType.Check:
                    canView(v, x => x.Name);
                    canView(v, x => x.Code);
                    canView(v, x => x.CardOrCheckNumber);
                    canView(v, x => x.Payee);
                    canView(v, x => x.BankName);
                    canView(v, x => x.BillingOrBankAddress);
                    canView(v, x => x.IssueOrBankIdNumber);
                    break;
                case PaymentMethodType.CreditCard:
                    canView(v, x => x.CardAssociationId, Unspecified.String);
                    canView(v, x => x.CurrencyId, Unspecified.String);
                    canView(v, x => x.Name);
                    canView(v, x => x.CardOrCheckNumber);
                    canView(v, x => x.BillingOrBankAddress);
                    canView(v, x => x.Code);
                    canView(v, x => x.IssueOrBankIdNumber);
                    canView(v, x => x.DailyLimit);
                    canView(v, x => x.CreditLimit);
                    break;
                case PaymentMethodType.DebitCard or PaymentMethodType.LoyaltyCard:
                    canView(v, x => x.CardAssociationId, Unspecified.String);
                    canView(v, x => x.CurrencyId, Unspecified.String);
                    canView(v, x => x.Name);
                    canView(v, x => x.CardOrCheckNumber);
                    canView(v, x => x.BillingOrBankAddress);
                    canView(v, x => x.Code);
                    canView(v, x => x.IssueOrBankIdNumber);
                    canView(v, x => x.DailyLimit);
                    break;
                case PaymentMethodType.BankAccount:
                    canView(v, x => x.Name);
                    canView(v, x => x.Code);
                    canView(v, x => x.BankName);
                    canView(v, x => x.BillingOrBankAddress);
                    canView(v, x => x.IssueOrBankIdNumber);
                    break;
                default:
                    canView(v, x => x.Name);
                    break;
            }

            canView(v, x => x.Details);
            canView(v, x => x.ValidFrom);
            canView(v, x => x.ValidTo);
        }
        protected override void dataInEditPage() {
            PaymentMethodView v = toView(item);
            switch (v.PaymentMethodType) {
                case PaymentMethodType.Check:
                    canEdit(v, x => x.Name, "Account name", true);
                    canEdit(v, x => x.Code, "Account number");
                    canEdit(v, x => x.CardOrCheckNumber, "Check number");
                    canEdit(v, x => x.Payee);
                    canEdit(v, x => x.BankName);
                    canEdit(v, x => x.BillingOrBankAddress, "Bank address");
                    canEdit(v, x => x.IssueOrBankIdNumber, "Bank identification number");
                    break;
                case PaymentMethodType.CreditCard:
                    canSelect(v, x => x.CardAssociationId, cardAssociations);
                    canSelect(v, x => x.CurrencyId, currencies);
                    canEdit(v, x => x.Name, "Name on card", true);
                    canEdit(v, x => x.CardOrCheckNumber, "Card number");
                    canEdit(v, x => x.BillingOrBankAddress, "Billing address");
                    canEdit(v, x => x.Code, "Card verification code");
                    canEdit(v, x => x.IssueOrBankIdNumber, "Issue number");
                    canEdit(v, x => x.DailyLimit, true, true, 0.00.ToString("0.00"));
                    canEdit(v, x => x.CreditLimit, true, true, 0.00.ToString("0.00"));
                    break;
                case PaymentMethodType.DebitCard or PaymentMethodType.LoyaltyCard:
                    canSelect(v, x => x.CardAssociationId, cardAssociations);
                    canSelect(v, x => x.CurrencyId, currencies);
                    canEdit(v, x => x.Name, "Name on card", true);
                    canEdit(v, x => x.CardOrCheckNumber, "Card number");
                    canEdit(v, x => x.BillingOrBankAddress, "Billing address");
                    canEdit(v, x => x.Code, "Card verification code");
                    canEdit(v, x => x.IssueOrBankIdNumber, "Issue number");
                    canEdit(v, x => x.DailyLimit, true, true, 0.00.ToString("0.00"));
                    break;
                case PaymentMethodType.BankAccount:
                    canEdit(v, x => x.Name, "Account name", true);
                    canEdit(v, x => x.Code, "Account number");
                    canEdit(v, x => x.BankName);
                    canEdit(v, x => x.BillingOrBankAddress, "Bank address");
                    canEdit(v, x => x.IssueOrBankIdNumber, "Bank identification number");
                    break;
                default:
                    canEdit(v, x => x.Name, true);
                    break;
            }

            canEdit(v, x => x.Details);
            canEdit(v, x => x.ValidFrom);
            canEdit(v, x => x.ValidTo);
        }
        protected override void dataInCreatePage() {
            switch (paymentMethodType) {
                case PaymentMethodType.Check:
                    canEdit(null, x => x.Name, "Account name", true);
                    canEdit(null, x => x.Code, "Account number");
                    canEdit(null, x => x.CardOrCheckNumber, "Check number");
                    canEdit(null, x => x.Payee);
                    canEdit(null, x => x.BankName);
                    canEdit(null, x => x.BillingOrBankAddress, "Bank address");
                    canEdit(null, x => x.IssueOrBankIdNumber, "Bank identification number");
                    break;
                case PaymentMethodType.CreditCard:
                    canSelect(null, x => x.CardAssociationId, cardAssociations);
                    canSelect(null, x => x.CurrencyId, currencies);
                    canEdit(null, x => x.Name, "Name on card", true);
                    canEdit(null, x => x.CardOrCheckNumber, "Card number");
                    canEdit(null, x => x.BillingOrBankAddress, "Billing address");
                    canEdit(null, x => x.Code, "Card verification code");
                    canEdit(null, x => x.IssueOrBankIdNumber, "Issue number");
                    canEdit(null, x => x.DailyLimit, true, true, 0.00.ToString("0.00"));
                    canEdit(null, x => x.CreditLimit, true, true, 0.00.ToString("0.00"));
                    break;
                case PaymentMethodType.DebitCard or PaymentMethodType.LoyaltyCard:
                    canSelect(null, x => x.CardAssociationId, cardAssociations);
                    canSelect(null, x => x.CurrencyId, currencies);
                    canEdit(null, x => x.Name, "Name on card", true);
                    canEdit(null, x => x.CardOrCheckNumber, "Card number");
                    canEdit(null, x => x.BillingOrBankAddress, "Billing address");
                    canEdit(null, x => x.Code, "Card verification code");
                    canEdit(null, x => x.IssueOrBankIdNumber, "Issue number");
                    canEdit(null, x => x.DailyLimit, true, true, 0.00.ToString("0.00"));
                    break;
                case PaymentMethodType.BankAccount:
                    canEdit(null, x => x.Name, "Account name", true);
                    canEdit(null, x => x.Code, "Account number");
                    canEdit(null, x => x.BankName);
                    canEdit(null, x => x.BillingOrBankAddress, "Bank address");
                    canEdit(null, x => x.IssueOrBankIdNumber, "Bank identification number");
                    break;
                default:
                    canEdit(null, x => x.Name, true);
                    break;
            }

            canEdit(null, x => x.Details);
            canEdit(null, x => x.ValidFrom);
            canEdit(null, x => x.ValidTo);
        }
        protected override void validateItems(PaymentMethodData d1, PaymentMethodData d2) {
            if (d1.PaymentMethodType is PaymentMethodType.CreditCard) base.validateItems(d1, d2);
            if (d1.PaymentMethodType is PaymentMethodType.DebitCard or PaymentMethodType.LoyaltyCard) allPropertiesAreEqual(d1, d2, nameof(d1.CreditLimit));
            else allPropertiesAreEqual(d1, d2, nameof(d1.DailyLimit), nameof(d1.CreditLimit));
        }
        [DataTestMethod]
        [DataRow(PaymentMethodType.Cash)]
        [DataRow(PaymentMethodType.Check)]
        [DataRow(PaymentMethodType.DebitCard)]
        [DataRow(PaymentMethodType.CreditCard)]
        [DataRow(PaymentMethodType.LoyaltyCard)]
        [DataRow(PaymentMethodType.BankAccount)]
        [DataRow(PaymentMethodType.Unspecified)]
        public void CanDisplayDataTest(PaymentMethodType t) {
            paymentMethodType = t;
            CanDisplayDataTest();
        }
        protected override void dataHiddenInPage() {
            hasHidden(item, x => x.Id, true);
            HiddenInputs.Clear();
        }
    }
    [TestClass] public class CreatePageTests :PaymentMethodsTests {
        [DataTestMethod]
        [DataRow(PaymentMethodType.Cash)]
        [DataRow(PaymentMethodType.Check)]
        [DataRow(PaymentMethodType.DebitCard)]
        [DataRow(PaymentMethodType.CreditCard)]
        [DataRow(PaymentMethodType.LoyaltyCard)]
        [DataRow(PaymentMethodType.BankAccount)]
        [DataRow(PaymentMethodType.Unspecified)]
        public void CanClickCreateButtonTest(PaymentMethodType t) {
            paymentMethodType = t;
            canClickCreateButtonTest();
        }
    }
    [TestClass] public class DeletePageTests :PaymentMethodsTests {
        [DataTestMethod]
        [DataRow(PaymentMethodType.Cash)]
        [DataRow(PaymentMethodType.Check)]
        [DataRow(PaymentMethodType.DebitCard)]
        [DataRow(PaymentMethodType.CreditCard)]
        [DataRow(PaymentMethodType.LoyaltyCard)]
        [DataRow(PaymentMethodType.BankAccount)]
        [DataRow(PaymentMethodType.Unspecified)]
        public void CanClickDeleteButtonTest(PaymentMethodType t) {
            paymentMethodType = t;
            canClickDeleteButtonTest();
        }
    }
    [TestClass] public class DetailsPageTests :PaymentMethodsTests {
        [DataTestMethod]
        [DataRow(PaymentMethodType.Cash)]
        [DataRow(PaymentMethodType.Check)]
        [DataRow(PaymentMethodType.DebitCard)]
        [DataRow(PaymentMethodType.CreditCard)]
        [DataRow(PaymentMethodType.LoyaltyCard)]
        [DataRow(PaymentMethodType.BankAccount)]
        [DataRow(PaymentMethodType.Unspecified)]
        public void CanClickEditButtonInDetailsTest(PaymentMethodType t) {
            paymentMethodType = t;
            canClickEditButtonInDetailsTest();
        }
    }
    [TestClass] public class EditPageTests :PaymentMethodsTests {
        [DataTestMethod]
        [DataRow(PaymentMethodType.Cash)]
        [DataRow(PaymentMethodType.Check)]
        [DataRow(PaymentMethodType.DebitCard)]
        [DataRow(PaymentMethodType.CreditCard)]
        [DataRow(PaymentMethodType.LoyaltyCard)]
        [DataRow(PaymentMethodType.BankAccount)]
        [DataRow(PaymentMethodType.Unspecified)]
        public void CanClickEditButtonTest(PaymentMethodType t) {
            paymentMethodType = t;
            canClickEditButtonTest();
        }
    }
    [TestClass] public class IndexPageTests :PaymentMethodsTests { }
}
