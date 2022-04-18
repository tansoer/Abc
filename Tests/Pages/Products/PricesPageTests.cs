using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Data.Orders;
using Abc.Data.Products;
using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Currencies;
using Abc.Domain.Orders.Discounts;
using Abc.Domain.Products;
using Abc.Domain.Products.Price;
using Abc.Domain.Rules;
using Abc.Facade.Orders;
using Abc.Facade.Products;
using Abc.Pages.Products;
using Abc.Tests.Pages.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Abc.Tests.Pages.Products {
    [TestClass]
    public class PricesPageTests :SealedViewsPageTests<PricesPage, IPricesRepo, IPrice, 
        PriceView, PriceData, PriceKind> {
        protected override string pageTitle => ProductTitles.Prices;
        protected override string pageUrl => ProductUrls.Prices;
        protected override IPrice toObject(PriceData d) => PriceFactory.Create(d);

        private PriceData Data;
        private DiscountData discountData;
        private CurrencyData currencyData;
        private ProductData productData;
        private ProductTypeData productTypeData;
        private RuleSetData ruleSetData;
        private RuleOverrideData ruleOverrideData;
 
        private class Repo : mockRepo<IPrice, PriceData>, IPricesRepo { };
        private class discountsRepo : mockRepo<IDiscount, DiscountData>, IDiscountsRepo { };
        private class currencyRepo : mockRepo<Currency, CurrencyData>, ICurrencyRepo { };
        private class productsRepo : mockRepo<IProduct, ProductData>, IProductsRepo { };
        private class productTypesRepo : mockRepo<IProductType, ProductTypeData>, IProductTypesRepo { };
        private class ruleSetsRepo : mockRepo<IRuleSet, RuleSetData>, IRuleSetsRepo { };
        private class ruleOverridesRepo : mockRepo<RuleOverride, RuleOverrideData>, IRuleOverridesRepo { };
        private Repo repo;
        private discountsRepo discounts;
        private currencyRepo currencies;
        private productsRepo products;
        private productTypesRepo productTypes;
        private ruleSetsRepo ruleSets;
        private ruleOverridesRepo ruleOverrides;

        protected override PricesPage createObject() {
            initializeRepos();
            setRandomData();
            addRandomDataToRepos();
            
            return new PricesPage(repo);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(repo, discounts, currencies, products, productTypes, ruleSets, ruleOverrides);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }

        private void initializeRepos() {
            repo = new();
            discounts = new();
            currencies = new();
            products = new();
            productTypes = new();
            ruleSets = new();
            ruleOverrides = new();
        }

        private void setRandomData() {
            Data = GetRandom.ObjectOf<PriceData>();
            discountData = GetRandom.ObjectOf<DiscountData>();
            currencyData = GetRandom.ObjectOf<CurrencyData>();
            productData = GetRandom.ObjectOf<ProductData>();
            productTypeData = GetRandom.ObjectOf<ProductTypeData>();
            ruleSetData = GetRandom.ObjectOf<RuleSetData>();
            ruleOverrideData = GetRandom.ObjectOf<RuleOverrideData>();
        }

        private void addRandomDataToRepos() {
            addRandomPrices();
            addRandomDiscounts();
            addRandomCurrencies();
            addRandomProducts();
            addRandomProductTypes();
            addRandomRuleSets();
            addRandomRuleOverrides();
        }

        private void addRandomPrices() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? Data : GetRandom.ObjectOf<PriceData>();
                repo.Add(PriceFactory.Create(d));
            }
        }
        private void addRandomDiscounts() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? discountData : GetRandom.ObjectOf<DiscountData>();
                discounts.Add(DiscountFactory.Create(d));
            }
        }
        private void addRandomCurrencies() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? currencyData : GetRandom.ObjectOf<CurrencyData>();
                currencies.Add(new(d));
            }
        }
        private void addRandomProducts() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? productData : GetRandom.ObjectOf<ProductData>();
                products.Add(ProductFactory.Create(d));
            }
        }
        private void addRandomProductTypes() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? productTypeData : GetRandom.ObjectOf<ProductTypeData>();
                productTypes.Add(ProductTypeFactory.Create(d));
            }
        }
        private void addRandomRuleSets() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? ruleSetData : GetRandom.ObjectOf<RuleSetData>();
                ruleSets.Add(new RuleSet(d));
            }
        }
        private void addRandomRuleOverrides() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);

            for (var i = 0; i < count; i++) {
                var d = i == idx ? ruleOverrideData : GetRandom.ObjectOf<RuleOverrideData>();
                ruleOverrides.Add(new(d));
            }
        }

        [TestMethod]
        public void PricesTest() {
            var list = repo.Get();
            Assert.AreEqual(list.Count + 1, obj.Prices.Count());
        }

        [TestMethod]
        public void DiscountsTest() {
            var list = discounts.Get();
            Assert.AreEqual(list.Count + 1, obj.Discounts.Count());
        }

        [TestMethod]
        public void CurrenciesTest() {
            var list = currencies.Get();
            Assert.AreEqual(list.Count + 1, obj.Currencies.Count());
        }

        [TestMethod]
        public void ProductsTest() {
            var list = products.Get();
            Assert.AreEqual(list.Count + 1, obj.Products.Count());
        }

        [TestMethod]
        public void ProductTypesTest() {
            var list = productTypes.Get();
            Assert.AreEqual(list.Count + 1, obj.ProductTypes.Count());
        }

        [TestMethod]
        public void RuleSetsTest() {
            var list = ruleSets.Get();
            Assert.AreEqual(list.Count + 1, obj.RuleSets.Count());
        }

        [TestMethod]
        public void RuleOverridesTest() {
            var list = ruleOverrides.Get();
            Assert.AreEqual(list.Count + 1, obj.RuleOverrides.Count());
        }

        [TestMethod]
        public void PriceNameTest() {
            var name = obj.PriceName(Data.Id);
            Assert.AreEqual(Data.Name, name);
        }

        [TestMethod]
        public void DiscountNameTest() {
            var name = obj.DiscountName(discountData.Id);
            Assert.AreEqual(discountData.Name, name);
        }

        [TestMethod]
        public void CurrencyNameTest() {
            var name = obj.CurrencyName(currencyData.Id);
            Assert.AreEqual(currencyData.Name, name);
        }

        [TestMethod]
        public void ProductNameTest() {
            var name = obj.ProductName(productData.Id);
            Assert.AreEqual(productData.Name, name);
        }

        [TestMethod]
        public void ProductTypeNameTest() {
            var name = obj.ProductTypeName(productTypeData.Id);
            Assert.AreEqual(productTypeData.Name, name);
        }

        [TestMethod]
        public void RuleSetNameTest() {
            var name = obj.RuleSetName(ruleSetData.Id);
            Assert.AreEqual(ruleSetData.Name, name);
        }

        [TestMethod]
        public void RuleOverrideNameTest() {
            var name = obj.RuleOverrideName(ruleOverrideData.Id);
            Assert.AreEqual(ruleOverrideData.Name, name);
        }
    }
}
