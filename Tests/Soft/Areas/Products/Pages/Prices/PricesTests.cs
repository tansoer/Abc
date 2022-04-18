using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Data.Orders;
using Abc.Data.Products;
using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Currencies;
using Abc.Domain.Orders.Discounts;
using Abc.Domain.Products.Price;
using Abc.Domain.Rules;
using Abc.Facade.Products;
using Abc.Infra.Currencies;
using Abc.Infra.Orders;
using Abc.Infra.Rules;
using Abc.Pages.Products;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Products.Pages.Prices {
    public abstract class PricesTests :BaseProductTests<PriceView, PriceData> {
        private MoneyDb moneyDb;
        private OrderDb orderDb;
        private RuleDb ruleDb;
        protected PriceKind? priceKind;
        protected List<SelectListItem> prices => createSelectList(db.Prices);
        protected List<SelectListItem> currencies => createSelectList(moneyDb.Currencies);
        protected List<SelectListItem> discounts => createSelectList(orderDb.Discounts);
        protected List<SelectListItem> products => createSelectList(db.Products);
        protected List<SelectListItem> productTypes => createSelectList(db.ProductTypes);
        protected List<SelectListItem> ruleSets => createSelectList(ruleDb.RuleSets);
        protected List<SelectListItem> ruleOverrides => createSelectList(ruleDb.RuleOverrides);
        [TestInitialize] public override void TestInitialize() {
            moneyDb = getContext<MoneyDb>();
            orderDb = getContext<OrderDb>();
            ruleDb = getContext<RuleDb>();
            base.TestInitialize();
        }
        protected override string baseUrl() => ProductUrls.Prices;
        protected override void addRelatedItems(PriceData d) {
            if (d is null) return;
            addPrice(d.PriceId);
            addPrice(d.PossiblePriceId);
            addCurrency(d.CurrencyId);
            addDiscount(d.DiscountId);
            addProduct(d.ProductId);
            addProductType(d.ProductTypeId);
            addRuleSet(d.RuleSetId);
            addRuleOverride(d.RuleOverrideId);
            setNullValues(d);
        }
        private void setNullValues(PriceData d) {
            switch (item.Kind) {
                case PriceKind.Possible:
                    d.ProductId = null;
                    d.RuleOverrideId = null;
                    d.PossiblePriceId = null;
                    d.PriceId = null;
                    d.DiscountId = null;
                    break;
                case PriceKind.Agreed:
                    d.ProductTypeId = null;
                    d.RuleSetId = null;
                    d.PriceId = null;
                    d.DiscountId = null;
                    break;
                case PriceKind.Arbitrary:
                    d.ProductTypeId = null;
                    d.RuleSetId = null;
                    d.RuleOverrideId = null;
                    d.PossiblePriceId = null;
                    d.DiscountId = null;
                    break;
                case PriceKind.Discount:
                    d.ProductTypeId = null;
                    d.RuleSetId = null;
                    d.ProductId = null;
                    d.RuleOverrideId = null;
                    d.PossiblePriceId = null;
                    break;
                default:
                    d.ProductTypeId = null;
                    d.RuleSetId = null;
                    d.ProductId = null;
                    d.RuleOverrideId = null;
                    d.PossiblePriceId = null;
                    d.PriceId = null;
                    d.DiscountId = null;
                    break;
            }
        }
        private static void addCurrency(string id) {
            var d = random<CurrencyData>();
            d.Id = id;
            add<ICurrencyRepo, Currency>(new Currency(d));
        }
        private static void addDiscount(string id) {
            var d = random<DiscountData>();
            d.Id = id;
            d.DiscountType = DiscountsType.Monetary;
            add<IDiscountsRepo, IDiscount>(DiscountFactory.Create(d));
        }
        private static void addRuleSet(string id) {
            var d = random<RuleSetData>();
            d.Id = id;
            add<IRuleSetsRepo, IRuleSet>(new RuleSet(d));
        }
        private static void addRuleOverride(string id) {
            var d = random<RuleOverrideData>();
            d.Id = id;
            add<IRuleOverridesRepo, RuleOverride>(new RuleOverride(d));
        }
        protected override void changeValuesExceptId(PriceData d) {
            var id = d.Id;
            d = random<PriceData>();
            d.Id = id;
        }
        protected override PriceData correctOriginal(PriceData oldItem, PriceData newItem) {
            oldItem.Kind = newItem.Kind;
            update<IPricesRepo, IPrice>(PriceFactory.Create(oldItem));
            return oldItem;
        }
        protected override string getItemId(PriceData d) => d.Id;
        protected override void setItemId(PriceData d, string id) => d.Id = id;
        protected override PriceData randomItem() {
            var d = base.randomItem();
            d.Kind = priceKind ?? d.Kind;
            d.Amount = Math.Round(GetRandom.Decimal(-1000, 1000), 2);
            return d;
        }
        protected override PriceView toView(PriceData d) {
            var o = PriceFactory.Create(d);
            var v = new PriceViewFactory().Create(o);
            return v;
        }
        protected override string pageUrl() {
            var url = base.pageUrl();
            if (priceKind is null) return url;
            var typeIdx = Convert.ToInt32(priceKind);
            url += $"&switchOfCreate={typeIdx}";
            return url;
        }
        protected override IEnumerable<Expression<Func<PriceView, object>>> indexPageColumns =>
            new Expression<Func<PriceView, object>>[] {
                x => x.Kind,
                x => x.Amount,
                x => x.CurrencyId,
                x => x.ValidFrom,
                x => x.ValidTo,
            };
        protected override void dataInDetailsPage() {
            var v = toView(item) as PriceView;
            canView(v, x => x.Id);
            canView(v, x => x.Name);
            canView(v, x => x.Code);
            canView(v, x => x.Amount);
            canView(v, x => x.CurrencyId, Unspecified.String);
            switch (item.Kind) {
                case PriceKind.Possible:
                    canView(v, x => x.ProductTypeId, Unspecified.String);
                    canView(v, x => x.RuleSetId, Unspecified.String);
                    break;
                case PriceKind.Agreed:
                    canView(v, x => x.ProductId, Unspecified.String);
                    canView(v, x => x.RuleOverrideId, Unspecified.String);
                    canView(v, x => x.PossiblePriceId, Unspecified.String);
                    break;
                case PriceKind.Arbitrary:
                    canView(v, x => x.ProductId, Unspecified.String);
                    canView(v, x => x.PriceId, Unspecified.String);
                    break;
                case PriceKind.Discount:
                    canView(v, x => x.DiscountId, Unspecified.String);
                    canView(v, x => x.PriceId, Unspecified.String);
                    break;
            }
            canView(v, x => x.Details);
            canView(v, x => x.ValidFrom);
            canView(v, x => x.ValidTo);
        }
        protected override void dataInEditPage() {
            var v = toView(item) as PriceView;
            canEdit(v, x => x.Name, true);
            canEdit(v, x => x.Code);
            canEdit(v, x => x.Amount, true, true, 0.00.ToString("0.00"));
            canSelect(v, x => x.CurrencyId, currencies);
            switch (item.Kind) {
                case PriceKind.Possible:
                    canSelect(v, x => x.ProductTypeId, productTypes);
                    canSelect(v, x => x.RuleSetId, ruleSets);
                    break;
                case PriceKind.Agreed:
                    canSelect(v, x => x.ProductId, products);
                    canSelect(v, x => x.PossiblePriceId, prices);
                    canSelect(v, x => x.RuleOverrideId, ruleOverrides);
                    break;
                case PriceKind.Arbitrary:
                    canSelect(v, x => x.ProductId, products);
                    canSelect(v, x => x.PriceId, prices);
                    break;
                case PriceKind.Discount:
                    canSelect(v, x => x.DiscountId, discounts);
                    canSelect(v, x => x.PriceId, prices);
                    break;
            }
            canEdit(v, x => x.Details);
            canEdit(v, x => x.ValidFrom);
            canEdit(v, x => x.ValidTo);
        }
        protected override void dataInCreatePage() {
            canEdit(null, x => x.Name, true);
            canEdit(null, x => x.Code);
            canEdit(null, x => x.Amount, true, true, 0.00.ToString("0.00"));
            canSelect(null, x => x.CurrencyId, currencies);
            switch (priceKind) {
                case PriceKind.Possible:
                    canSelect(null, x => x.ProductTypeId, productTypes);
                    canSelect(null, x => x.RuleSetId, ruleSets);
                    break;
                case PriceKind.Agreed:
                    canSelect(null, x => x.ProductId, products);
                    canSelect(null, x => x.PossiblePriceId, prices);
                    canSelect(null, x => x.RuleOverrideId, ruleOverrides);
                    break;
                case PriceKind.Arbitrary:
                    canSelect(null, x => x.ProductId, products);
                    canSelect(null, x => x.PriceId, prices);
                    break;
                case PriceKind.Discount:
                    canSelect(null, x => x.DiscountId, discounts);
                    canSelect(null, x => x.PriceId, prices);
                    break;
            }
            canEdit(null, x => x.Details);
            canEdit(null, x => x.ValidFrom);
            canEdit(null, x => x.ValidTo);
        }
        [DataTestMethod]
        [DataRow(PriceKind.Unspecified)]
        [DataRow(PriceKind.Possible)]
        [DataRow(PriceKind.Agreed)]
        [DataRow(PriceKind.Arbitrary)]
        [DataRow(PriceKind.Discount)]
        public void CanDisplayDataTest(PriceKind t) {
            priceKind = t;
            CanDisplayDataTest();
        }
        protected override void dataHiddenInPage() {
            hasHidden(item, x => x.Id, true);
            HiddenInputs.Clear();
        }
    }
    [TestClass] public class CreatePageTests :PricesTests {
        [DataTestMethod]
        [DataRow(PriceKind.Unspecified)]
        [DataRow(PriceKind.Possible)]
        [DataRow(PriceKind.Agreed)]
        [DataRow(PriceKind.Arbitrary)]
        [DataRow(PriceKind.Discount)]
        public void CanClickCreateButtonTest(PriceKind t) {
            priceKind = t;
            canClickCreateButtonTest();
        }
    }
    [TestClass] public class DeletePageTests :PricesTests {
        [DataTestMethod]
        [DataRow(PriceKind.Unspecified)]
        [DataRow(PriceKind.Possible)]
        [DataRow(PriceKind.Agreed)]
        [DataRow(PriceKind.Arbitrary)]
        [DataRow(PriceKind.Discount)]
        public void CanClickDeleteButtonTest(PriceKind t) {
            priceKind = t;
            canClickDeleteButtonTest();
        }
    }
    [TestClass] public class DetailsPageTests :PricesTests {
        [DataTestMethod]
        [DataRow(PriceKind.Unspecified)]
        [DataRow(PriceKind.Possible)]
        [DataRow(PriceKind.Agreed)]
        [DataRow(PriceKind.Arbitrary)]
        [DataRow(PriceKind.Discount)]
        public void CanClickEditButtonInDetailsTest(PriceKind t) {
            priceKind = t;
            canClickEditButtonInDetailsTest();
        }
    }
    [TestClass] public class EditPageTests :PricesTests {
        [DataTestMethod]
        [DataRow(PriceKind.Unspecified)]
        [DataRow(PriceKind.Possible)]
        [DataRow(PriceKind.Agreed)]
        [DataRow(PriceKind.Arbitrary)]
        [DataRow(PriceKind.Discount)]
        public void CanClickEditButtonTest(PriceKind t) {
            priceKind = t;
            canClickEditButtonTest();
        }
    }
    [TestClass] public class IndexPageTests :PricesTests { }
}
