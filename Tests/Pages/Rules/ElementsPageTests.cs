using System;
using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Enums;
using Abc.Aids.Extensions;
using Abc.Aids.Methods;
using Abc.Aids.Random;
using Abc.Aids.Reflection;
using Abc.Data.Currencies;
using Abc.Data.Quantities;
using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Currencies;
using Abc.Domain.Quantities;
using Abc.Domain.Rules;
using Abc.Facade.Rules;
using Abc.Pages.Common;
using Abc.Pages.Rules;
using Abc.Tests.Pages.Common;
using Abc.Tests.Pages.Currencies;
using Abc.Tests.Pages.Quantities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Rules {
    [TestClass] public class ElementsPageTests : SealedViewsPageTests<ElementsPage, IRuleElementsRepo,
        IRuleElement, RuleElementView, RuleElementData, RuleElementType> {

        protected override Type getBaseClass() =>
            typeof(ViewsPage<ElementsPage, IRuleElementsRepo,
                IRuleElement, RuleElementView, RuleElementData, RuleElementType>);

        protected override string pageTitle => RuleTitles.RuleElements;
        protected override string pageUrl => RuleUrls.RuleElements;
        protected override IRuleElement toObject(RuleElementData d) => RuleElementFactory.Create(d);

        internal class testRepo
            : mockRepo<IRuleElement, RuleElementData>,
                IRuleElementsRepo {

            public int GetNextElementIndex(bool isRuleElement, string masterId) {
                var c = list.FindAll(x =>
                    isRuleElement ? x.Data.RuleId == masterId : x.Data.RuleContextId == masterId);

                return c.Count + 1;
            }
            public void CreateContextElements(string id, string ruleId) {
                var l = list.FindAll(x => x.Data.RuleContextId == id);

                if (l.Count > 0) return;
                l = list.FindAll(x => x.Data.RuleId == ruleId);

                foreach (var e in l) {
                    var o = new RuleElementData();
                    Copy.Members(e, o);
                    o.Id = Guid.NewGuid().ToString();
                    o.RuleContextId = id;
                    o.RuleId = null;
                    list.Add(RuleElementFactory.Create(o));
                }
            }

        }

        private testRepo Repo;
        private RulesPageTests.testRepo rules;
        private UnitsPageTests.unitsRepo units;
        private CurrenciesPageTests.testRepo currencies;
        private ContextsPageTests.testRepo contexts;

        protected override ElementsPage createObject() {
            Repo = new testRepo();
            rules = new RulesPageTests.testRepo();
            units = new UnitsPageTests.unitsRepo();
            currencies = new CurrenciesPageTests.testRepo();
            contexts = new ContextsPageTests.testRepo();
            addRandomItems();
            return new ElementsPage(Repo);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(Repo, contexts, rules, units, currencies);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }
        private void addRandomItems() => addRules();
        private void addRules() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++) {
                var r = new Rule(GetRandom.ObjectOf<RuleData>());
                rules.Add(r);
                addRuleElements(r.Id);
                addContexts(r.Id);
            }
        }
        private void addContexts(string ruleId) {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++) {
                var c = GetRandom.ObjectOf<RuleContextData>();
                c.RuleId = ruleId;
                contexts.Add(new RuleContext(c));
                addRuleElements(c.Id, false);
            }
        }
        private void addRuleElements(string masterId, bool isRule = true) {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++) {
                var e = GetRandom.ObjectOf<RuleElementData>();
                e.RuleId = isRule ? masterId : null;
                e.RuleContextId = isRule ? null : masterId;
                Repo.Add(RuleElementFactory.Create(e));
                if (e.RuleElementType == RuleElementType.Quantity) addUnit(e.UnitOrCurrencyId);
                if (e.RuleElementType == RuleElementType.Money) addCurrency(e.UnitOrCurrencyId);
            }
        }
        private void addUnit(string id) {
            var c = GetRandom.ObjectOf<UnitData>();
            c.Id = id;
            units.Add(UnitFactory.Create(c));
        }
        private void addCurrency(string id) {
            var c = GetRandom.ObjectOf<CurrencyData>();
            c.Id = id;
            currencies.Add(new Currency(c));
        }
        [TestMethod]
        public override void ToObjectTest() {
            foreach (var t in (RuleElementType[]) Enum.GetValues(typeof(RuleElementType))) {
                var view = GetRandom.ObjectOf<RuleElementView>();
                view.RuleElementType = t;
                var o = obj.toObject(view);
                view = obj.toView(o);
                allPropertiesAreEqual(o.Data, view
                    , nameof(o.Data.UnitOrCurrencyId)
                    , nameof(o.Data.ActivityId)
                    , nameof(o.Data.Value));
                Assert.IsTrue(validateValues(o.Data, view));
                Assert.IsTrue(validateUnits(o.Data, view));
            }
        }
        private static bool validateValues(RuleElementData d, RuleElementView v) {
            return d.RuleElementType switch
            {
                RuleElementType.Boolean => d.Value == Variable.ToString(v.BooleanValue),
                RuleElementType.String => d.Value == v.StringValue,
                RuleElementType.Integer => d.Value == Variable.ToString(v.IntegerValue),
                RuleElementType.Decimal => d.Value == Variable.ToString(v.DecimalValue),
                RuleElementType.Double => d.Value == Variable.ToString(v.DoubleValue),
                RuleElementType.DateTime => d.Value == Variable.ToString(v.DateTimeValue),
                RuleElementType.Quantity => d.Value == Variable.ToString(v.DoubleValue),
                RuleElementType.Money => d.Value == Variable.ToString(v.DecimalValue),
                RuleElementType.Error => d.Value == v.StringValue,
                _ => true
            };
        }
        private static bool validateUnits(RuleElementData d, RuleElementView v) {
            return d.RuleElementType switch
            {
                RuleElementType.Quantity => d.UnitOrCurrencyId == v.UnitId,
                RuleElementType.Money => d.UnitOrCurrencyId == v.CurrencyId,
                _ => true
            };
        }
        [TestMethod] public override void ToViewTest() => ToObjectTest();
        [TestMethod]
        public void ContextNameTest() {
            var r = contexts.list[GetRandom.Int32(0, contexts.list.Count - 1)];
            var n = obj.ContextName(r.Id);
            Assert.AreEqual(n, r.Name);
        }
        [TestMethod]
        public void RuleNameTest() {
            var r = rules.list[GetRandom.Int32(0, rules.list.Count - 1)];
            var n = obj.RuleName(r.Id);
            Assert.AreEqual(n, r.Name);
        }

        [TestMethod]
        public void CreateUriTest() {
            const string uri = "/Rules/Elements/Create?handler=Create&pageIndex=0" +
                               "&sortOrder=&searchString=&currentFilter=&fixedFilter=&fixedValue=&switchOfCreate={0}";
            Assert.AreEqual(string.Format(uri, 1), obj.CreateUri(RuleElementType.Operator).ToString());
            Assert.AreEqual(string.Format(uri, 2), obj.CreateUri(RuleElementType.Operand).ToString());
            Assert.AreEqual(string.Format(uri, 129), obj.CreateUri(RuleElementType.Boolean).ToString());
            Assert.AreEqual(string.Format(uri, 131), obj.CreateUri(RuleElementType.Integer).ToString());
            Assert.AreEqual(string.Format(uri, 133), obj.CreateUri(RuleElementType.Double).ToString());
            Assert.AreEqual(string.Format(uri, 132), obj.CreateUri(RuleElementType.Decimal).ToString());
            Assert.AreEqual(string.Format(uri, 134), obj.CreateUri(RuleElementType.DateTime).ToString());
            Assert.AreEqual(string.Format(uri, 130), obj.CreateUri(RuleElementType.String).ToString());
            Assert.AreEqual(string.Format(uri, 136), obj.CreateUri(RuleElementType.Money).ToString());
            Assert.AreEqual(string.Format(uri, 135), obj.CreateUri(RuleElementType.Quantity).ToString());
            Assert.AreEqual(string.Format(uri, 137), obj.CreateUri(RuleElementType.Error).ToString());
            Assert.AreEqual(string.Format(uri, 0), obj.CreateUri(RuleElementType.Unspecified).ToString());
        }
        [TestMethod]
        public void IsRuleElementTest() {
            Assert.IsFalse(obj.IsRuleElement(rndStr));
            Assert.IsTrue(obj.IsRuleElement(GetMember.Name<RuleElementView>(x => x.RuleId)));
        }
        [TestMethod]
        public void RulesTest() {
            isReadOnly();
            Assert.IsInstanceOfType(obj.Rules, typeof(List<SelectListItem>));
            Assert.AreEqual(obj.Rules.Count() - 1, rules.list.Count);
        }
        [TestMethod]
        public void CurrenciesTest() {
            isReadOnly();
            Assert.IsInstanceOfType(obj.Currencies, typeof(List<SelectListItem>));
            Assert.AreEqual(obj.Currencies.Count() - 1, currencies.list.Count);
        }
        [TestMethod]
        public void UnitsTest() {
            isReadOnly();
            Assert.IsInstanceOfType(obj.Units, typeof(List<SelectListItem>));
            Assert.AreEqual(obj.Units.Count() - 1, units.list.Count);
        }

        [TestMethod]
        public void ContextsTest() {
            isReadOnly();
            Assert.IsInstanceOfType(obj.Contexts, typeof(List<SelectListItem>));
            Assert.AreEqual(obj.Contexts.Count() - 1, contexts.list.Count);
        }
        [TestMethod]
        public void RuleElementTypeTest() {
            var t = GetRandom.EnumOf<RuleElementType>();
            obj.RuleElementType = t;
            Assert.AreEqual(t, obj.RuleElementType);
        }
    }
}