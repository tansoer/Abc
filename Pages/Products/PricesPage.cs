using Abc.Aids.Methods;
using Abc.Aids.Reflection;
using Abc.Data.Currencies;
using Abc.Data.Orders;
using Abc.Data.Products;
using Abc.Data.Rules;
using Abc.Domain.Currencies;
using Abc.Domain.Orders.Discounts;
using Abc.Domain.Products;
using Abc.Domain.Products.Price;
using Abc.Domain.Rules;
using Abc.Facade.Products;
using Abc.Pages.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Abc.Pages.Products {
    public sealed class PricesPage :ViewsPage<PricesPage, IPricesRepo, IPrice, PriceView, PriceData, PriceKind> {
        protected override string title => ProductTitles.Prices;
        public PricesPage(IPricesRepo r) :base(r) {}
        private IEnumerable<SelectListItem> prices;
        public IEnumerable<SelectListItem> Prices => prices ??= selectListByName<IPricesRepo, IPrice, PriceData>();
        private IEnumerable<SelectListItem> discounts;
        public IEnumerable<SelectListItem> Discounts => discounts ??= selectListByName<IDiscountsRepo, IDiscount, DiscountData>();
        private IEnumerable<SelectListItem> currencies;
        public IEnumerable<SelectListItem> Currencies => currencies ??= selectListByName<ICurrencyRepo, Currency, CurrencyData>();
        private IEnumerable<SelectListItem> products;
        public IEnumerable<SelectListItem> Products => products ??= selectListByName<IProductsRepo, IProduct, ProductData>();
        private IEnumerable<SelectListItem> productTypes;
        public IEnumerable<SelectListItem> ProductTypes => productTypes ??= selectListByName<IProductTypesRepo, IProductType, ProductTypeData>();
        private IEnumerable<SelectListItem> ruleSets;
        public IEnumerable<SelectListItem> RuleSets => ruleSets ??= selectListByName<IRuleSetsRepo, IRuleSet, RuleSetData>();
        private IEnumerable<SelectListItem> ruleOverrides;
        public IEnumerable<SelectListItem> RuleOverrides => ruleOverrides ??= selectListByName<IRuleOverridesRepo, RuleOverride, RuleOverrideData>();
        protected override void tableColumns() {
            tableColumn(x => Item.Kind);
            tableColumn(x => Item.Amount);
            tableColumn(x => Item.CurrencyId, () => CurrencyName(Item.CurrencyId));
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Id);
            valueViewer(x => Item.Name);
            valueViewer(x => Item.Code);
            valueViewer(x => Item.Kind);
            valueViewer(x => Item.Amount);
            valueViewer(x => Item.CurrencyId, () => CurrencyName(Item.CurrencyId));

            switch (Item.Kind) {
                case PriceKind.Possible:
                    valueViewer(x => Item.ProductTypeId, () => ProductTypeName(Item.ProductTypeId));
                    valueViewer(x => Item.RuleSetId, () => RuleSetName(Item.RuleSetId));
                    break;
                case PriceKind.Agreed:
                    valueViewer(x => Item.ProductId, () => ProductName(Item.ProductId));
                    valueViewer(x => Item.RuleOverrideId, () => RuleOverrideName(Item.RuleOverrideId));
                    valueViewer(x => Item.PossiblePriceId, () => PriceName(Item.PossiblePriceId));
                    break;
                case PriceKind.Arbitrary:
                    valueViewer(x => Item.ProductId, () => ProductName(Item.ProductId));
                    valueViewer(x => Item.PriceId, () => PriceName(Item.PriceId));
                    break;
                case PriceKind.Discount:
                    valueViewer(x => Item.DiscountId, () => DiscountName(Item.DiscountId));
                    valueViewer(x => Item.PriceId, () => PriceName(Item.PriceId));
                    break;
            }

            valueViewer(x => Item.Details);
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.Name);
            valueEditor(x => Item.Code);
            valueEditor(x => Item.Amount);
            valueEditor(x => Item.CurrencyId, () => Currencies);

            switch (Item.Kind)
            {
                case PriceKind.Possible:
                    valueEditor(x => Item.ProductTypeId, () => ProductTypes);
                    valueEditor(x => Item.RuleSetId, () => RuleSets);
                    break;
                case PriceKind.Agreed:
                    valueEditor(x => Item.ProductId, () => Products);
                    valueEditor(x => Item.RuleOverrideId, () => RuleOverrides);
                    valueEditor(x => Item.PossiblePriceId, () => Prices);
                    break;
                case PriceKind.Arbitrary:
                    valueEditor(x => Item.ProductId, () => Products);
                    valueEditor(x => Item.PriceId, () => Prices);
                    break;
                case PriceKind.Discount:
                    valueEditor(x => Item.DiscountId, () => Discounts);
                    valueEditor(x => Item.PriceId, () => Prices);
                    break;
            }

            valueEditor(x => Item.Details);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => ProductUrls.Prices;
        protected internal override IPrice toObject(PriceView v) => new PriceViewFactory().Create(v);
        protected internal override PriceView toView(IPrice o) => new PriceViewFactory().Create(o);
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) { 
            Item = new() { Kind = Safe.Run(() => (PriceKind)(switchOfCreate ?? 1000), PriceKind.Unspecified) };
            if (fixedFilterIsProductTypeId) Item.ProductTypeId = fixedValue;
            if (fixedFilterIsProductId) Item.ProductId = fixedValue;
        }
        public string PriceName(string id) => itemName(Prices, id);
        public string DiscountName(string id) => itemName(Discounts, id);
        public string CurrencyName(string id) => itemName(Currencies, id);
        public string ProductName(string id) => itemName(Products, id);
        public string ProductTypeName(string id) => itemName(ProductTypes, id);
        public string RuleSetName(string id) => itemName(RuleSets, id);
        public string RuleOverrideName(string id) => itemName(RuleOverrides, id);
        private bool fixedFilterIsProductTypeId => Equals(FixedFilter, GetMember.Name<PriceView>(x => x.ProductTypeId));
        private bool fixedFilterIsProductId => Equals(FixedFilter, GetMember.Name<PriceView>(x => x.ProductId));
    }
}
