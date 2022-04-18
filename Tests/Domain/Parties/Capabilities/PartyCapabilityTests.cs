using Abc.Aids.Random;
using Abc.Data.Roles;
using Abc.Data.Rules;
using Abc.Domain.Parties.Attributes;
using Abc.Domain.Parties.Capabilities;
using Abc.Domain.Roles;
using Abc.Domain.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using Operation = Abc.Aids.Calculator.Operation;

namespace Abc.Tests.Domain.Parties.Capabilities {
    [TestClass] public class PartyCapabilityTests
        :SealedTests<PartyCapability, BasePartyAttribute<PartyCapabilityData>> {
        protected override PartyCapability createObject()
            => new(random<PartyCapabilityData>());

        [TestMethod] public void RuleContextIdTest() => isReadOnly(obj.Data.RuleContextId);

        [TestMethod] public async Task CapabilityTest() {
            var d = await createContext(null);
            await testItemAsync<RuleContextData, RuleContext, IRuleContextsRepo>(
                d, () => obj.Capability.Data, x => new RuleContext(x));
        }

        [TestMethod] public async Task IsCapableTest()
            => await testIsCapable(async d => await addValidRequirements(d),
                true, 3, 1, true);

        [TestMethod] public async Task IsCapableNoContextTest()
            => await testIsCapable(async d => await addValidRequirements(d),
                false, 0, 1, false);

        [TestMethod] public async Task IsCapableNoRulesTest()
            => await testIsCapable(_ => { }, true, 3, 0, true);

        [TestMethod] public async Task IsNotCapableTest()
            => await testIsCapable(async d => await addNotValidRequirements(d),
                true, 3, 1, false);

        private static string java => "Java skill level";
        private static string sql => "SQL skill level";
        private static string uml => "UML skill level";
        private static int idx { get; set; }
        private static RuleData rule { get; set; }
        private static RuleSetData set { get; set; }

        private async Task testIsCapable(Action<ResponsibilityTypeData> a,
            bool requirements, int variables, int rules, bool expected) {
            var r = await createRequirementsAndContext(a, requirements);
            areEqual(variables, obj.Capability.Variables.Count, "Variables");
            areEqual(rules, obj.Capability.RuleSet.Rules.Count, "rules");
            areEqual(expected, obj.IsCapable(r), "Is capable");
        }

        private static async Task addValidRequirements(ResponsibilityTypeData d)
            => await addRequirements(d, 5.0);

        private static async Task addNotValidRequirements(ResponsibilityTypeData d)
            => await addRequirements(d, 6.0);

        private static async Task addSet(ResponsibilityTypeData r) {
            set = random<RuleSetData>();
            set.Name = "Rule set: " + r.Name;
            set.Id = r.RequirementsRuleSetId;
            await addAsync<IRuleSetsRepo, IRuleSet>(new RuleSet(set));
        }

        private static async Task addRule(ResponsibilityTypeData r, double v) {
            rule = random<RuleData>();
            rule.Name = "Rule: " + r.Name;
            rule.RuleKind = rndBool ? RuleKind.Rule : RuleKind.ActivityRule;
            await addRequirements(v);
            await addAsync<IRulesRepo, BaseRule>(new Rule(rule));
        }

        private static async Task addRuleUsage(RuleSetData s, RuleData r) {
            var u = random<RuleUsageData>();
            u.RuleId = r.Id;
            u.RuleSetId = s.Id;
            await addAsync<IRuleUsagesRepo, RuleUsage>(new RuleUsage(u));
        }

        private static async Task addRequirements(double v) {
            idx = 1;
            await addOperand(java);
            await addVariable(java, 8.0);
            await addOperator(Operation.IsLess);
            await addOperator(Operation.Not);
            await addOperand(sql);
            await addVariable(sql, v);
            await addOperator(Operation.IsLess);
            await addOperator(Operation.Not);
            await addOperand(uml);
            await addVariable(uml, v);
            await addOperator(Operation.IsLess);
            await addOperator(Operation.Not);
            await addOperator(Operation.Or);
            await addOperator(Operation.And);
        }

        private static async Task addRequirements(ResponsibilityTypeData r, double v) {
            await addSet(r);
            await addRule(r, v);
            await addRuleUsage(set, rule);
        }

        private static async Task addVariable(string n, double v)
            => await addAsync<IRuleElementsRepo, IRuleElement>(
                new DoubleVariable(idx++, n + " requirement", v, rule.Id, null));

        private static async Task addOperand(string n)
            => await addAsync<IRuleElementsRepo, IRuleElement>(
                new Operand(idx++, n, rule.Id, null));

        private static async Task addOperator(Operation o)
            => await addAsync<IRuleElementsRepo, IRuleElement>(
                new Operator(idx++, o, rule.Id));

        private static async Task addCapabilities(RuleContextData c) {
            await addCapability(java, 8, c);
            await addCapability(sql, 5, c);
            await addCapability(uml, 5, c);
        }

        private static async Task addCapability(string name, int value, RuleContextData c)
            => await addAsync<IRuleElementsRepo, IRuleElement>(
                VariableFactory.Create(name, value, c?.Id, true));

        private async Task<RuleContextData> createContext(IRuleSet r, bool hasCapabilities = true) {
            var d = random<RuleContextData>();
            d.Name = "Test context";
            d.RuleSetId = r?.Id;
            d.Id = obj.RuleContextId;
            if (hasCapabilities) await addCapabilities(d);
            return d;
        }

        private async Task<ResponsibilityType> createRequirementsAndContext(Action<ResponsibilityTypeData> a,
            bool requirements) {
            var r = createRequirements(a);
            var d = await createContext(r?.Requirements, requirements);
            await addAsync<IRuleContextsRepo, RuleContext>(new RuleContext(d));
            return r;
        }

        private static ResponsibilityType createRequirements(Action<ResponsibilityTypeData> a) {
            var d = random<ResponsibilityTypeData>();
            d.Name = "Java developer responsibilities";
            a(d);
            return new ResponsibilityType(d);
        }
    }
}