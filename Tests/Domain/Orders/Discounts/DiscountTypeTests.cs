using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Data.Orders;
using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Currencies;
using Abc.Domain.Orders.Discounts;
using Abc.Domain.Rules;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using static Abc.Tests.Domain.Common.EntityTests;
using static Abc.Tests.Domain.Rules.RuleSetTests;

namespace Abc.Tests.Domain.Orders.Discounts {

    [TestClass]
    public class DiscountTypeTests 
        :SealedTests<DiscountType, EntityType<IDiscountTypesRepo, IDiscountType, DiscountTypeData>> {

        internal class MockDiscountType :MockEntity<DiscountTypeData>, IDiscountType {
            public IRuleSet Eligibility => Mock.Func<IRuleSet>();
            public Discount GetDiscount(RuleOverride o) => Mock.Func<Discount>(o);
            public Discount GetDiscount(RuleContext c) => Mock.Func<Discount>(c);
            public Discount GetDiscount(RuleContext c, params RuleOverride[] overrides) 
                => Mock.Func<Discount>(c, overrides);
        }

        private MockRuleSet mockRuleSet;
        private RuleContext testRuleContext;
        private RuleOverride testRuleOverride;
        private string nameOfEvaluate;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            mockRuleSet = new MockRuleSet();
            obj.eligibility = mockRuleSet;
            testRuleContext = new RuleContext(random<RuleContextData>());
            testRuleOverride = new RuleOverride(random<RuleOverrideData>());
            nameOfEvaluate = nameof(obj.Eligibility.Evaluate);
        }
        protected override DiscountType createObject() => new(GetRandom.ObjectOf<DiscountTypeData>());

        [TestMethod]
        public async Task EligibilityTest() {
            obj.eligibility = null;
            await testItemAsync<RuleSetData, IRuleSet, IRuleSetsRepo>(
                obj.Data?.EligibilityRuleSetId, () => obj.Eligibility.Data, d => new RuleSet(d));
        }
        [TestMethod]
        public void EligibilityRuleSetIdTest()
            => isReadOnly(obj.Data.EligibilityRuleSetId, true);
        [TestMethod]
        public void GetDiscountByRuleOverrideTest() {
            isInstanceOfType(obj.GetDiscount(testRuleOverride), typeof(UnspecifiedDiscount));
            areSame(testRuleOverride, getEvaluateParam(0));
        }
        [TestMethod]
        public void GetDiscountByRuleOverrideStackTest()
            => mockRuleSet.testCallingStack(() => obj.GetDiscount(testRuleOverride),
                nameof(obj.GetDiscount),
                nameof(obj.isApplicable),
                nameof(obj.Eligibility.Evaluate));

        [TestMethod]
        public void GetDiscountByRuleContextTest() {
            isInstanceOfType(obj.GetDiscount(testRuleContext), typeof(UnspecifiedDiscount));
            areSame(testRuleContext, getEvaluateParam(0));
        }
        [TestMethod]
        public void GetDiscountByRuleContextStackTest()
            => mockRuleSet.testCallingStack(() => obj.GetDiscount(testRuleContext),
                nameof(obj.GetDiscount),
                nameof(obj.isApplicable),
                nameof(obj.Eligibility.Evaluate));
        [TestMethod]
        public void GetDiscountTest() {
            isInstanceOfType(obj.GetDiscount(testRuleContext, testRuleOverride), typeof(UnspecifiedDiscount));
            areSame(testRuleContext, getEvaluateParam(0));
            areSame(testRuleOverride, (getEvaluateParam(1) as object[])[0]);
        }
        [TestMethod]
        public void GetDiscountStackTest()
            => mockRuleSet.testCallingStack(() => obj.GetDiscount(testRuleContext, testRuleOverride),
                nameof(obj.GetDiscount),
                nameof(obj.isApplicable),
                nameof(obj.Eligibility.Evaluate));
        [TestMethod] public void AmountTest() => isReadOnly(obj.Data.Amount, true);
        [TestMethod]
        public async Task CurrencyTest()
            => await testItemAsync<CurrencyData, Currency, ICurrencyRepo>(
                obj.currencyId, () => obj.currency.Data, d => new Currency(d));
        [TestMethod] public void CurrencyIdTest() => isReadOnly(obj.Data.CurrencyId, true);

        [DataRow(DiscountsType.Monetary)]
        [DataRow(DiscountsType.Percentage)]
        [DataRow(DiscountsType.Unspecified)]
        [DataTestMethod]
        public void CreateDataTest(DiscountsType t) {
            var d = obj.createData(t);
            testData(d, t);
        }
        private void testData(DiscountData d, DiscountsType t) {
            areEqual(t, d.DiscountType);
            areEqual(obj.Id, d.DiscountTypeId);
            areEqual(obj.Name, d.Name);
            areEqual(obj.Code, d.Code);
            areEqual(obj.Details, d.Details);
            isTrue(d.ValidFrom < DateTime.Now);
            isTrue(d.ValidFrom > DateTime.Now.AddSeconds(-1));
            areEqual((DateTime?)null, d.ValidTo);
            areEqual((t is not DiscountsType.Unspecified) ? obj.amount : 0, d.Amount);
            areEqual((t is DiscountsType.Monetary) ? obj.currencyId : null, d.CurrencyId);
            isGuid(d.Id);
        }
        [TestMethod]
        public async Task HasDiscountTest() {
            var d = obj.hasDiscount();
            isInstanceOfType<PercentageDiscount>(d);
            testData(d.Data, DiscountsType.Percentage);
            await CurrencyTest();
            d = obj.hasDiscount();
            isInstanceOfType<MonetaryDiscount>(d);
            testData(d.Data, DiscountsType.Monetary);
        }

        [TestMethod]
        public void MonetaryDiscountTest() => testCreateDiscount(obj.monetaryDiscount,
            typeof(MonetaryDiscount), DiscountsType.Monetary);
        [TestMethod]
        public void PersentageDiscountTest() => testCreateDiscount(obj.persentageDiscount,
            typeof(PercentageDiscount), DiscountsType.Percentage);
        [TestMethod]
        public void NoDiscountTest() => testCreateDiscount(obj.noDiscount,
            typeof(UnspecifiedDiscount), DiscountsType.Unspecified);
        private void testCreateDiscount(Func<Discount> create, Type isType, DiscountsType t) {
            var d = create();
            isInstanceOfType(d, isType);
            testData(d.Data, t);
        }
        [TestMethod]
        public void IsApplicableByRuleOverrideTest() {
            isFalse(obj.isApplicable(testRuleOverride));
            areSame(testRuleOverride, getEvaluateParam(0));
        }
        [TestMethod]
        public void IsApplicableByRuleOverrideStackTest() 
            => mockRuleSet.testCallingStack(() => obj.isApplicable(testRuleOverride),
                nameof(obj.isApplicable),
                nameof(obj.Eligibility.Evaluate));

        [TestMethod]
        public void IsApplicableByRuleContextTest() {
            isFalse(obj.isApplicable(testRuleContext));
            areSame(testRuleContext, getEvaluateParam(0));
        }
        [TestMethod]
        public void IsApplicableByRuleContextStackTest()
            => mockRuleSet.testCallingStack(() => obj.isApplicable(testRuleContext),
                nameof(obj.isApplicable),
                nameof(obj.Eligibility.Evaluate));
        [TestMethod]
        public void IsApplicableTest() {
            isFalse(obj.isApplicable(testRuleContext, testRuleOverride));
            areSame(testRuleContext, getEvaluateParam(0));
            areSame(testRuleOverride, (getEvaluateParam(1) as object[])[0]);
        }
        [TestMethod]
        public void IsApplicableStackTest()
            => mockRuleSet.testCallingStack(() => obj.isApplicable(testRuleContext, testRuleOverride),
                nameof(obj.isApplicable),
                nameof(obj.Eligibility.Evaluate));

        [TestMethod]
        public void IsApplicableByVariableTest() {
            var n = random<string>();
            var r = GetRandom.AnyValue();
            while (r is bool) r = GetRandom.AnyValue();
            isTrue(DiscountType.isApplicable(VariableFactory.Create(n, true)));
            isFalse(DiscountType.isApplicable(VariableFactory.Create(n, false)));
            isFalse(DiscountType.isApplicable(VariableFactory.Create(n, r)));
        }
        private object getEvaluateParam(int idx) => mockRuleSet.GetParam(nameOfEvaluate, idx);
    }
}