using Abc.Aids.Random;
using Abc.Data.Rules;
using Abc.Domain.Parties.Signatures;
using Abc.Domain.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Rules {
    [TestClass] public class RuleOverrideTests :SealedTests<RuleOverride, BasePartySignature<RuleOverrideData>> {
        protected override RuleOverride createObject()
            => new(GetRandom.ObjectOf<RuleOverrideData>());

        [TestMethod] public void RuleSetIdTest() => isReadOnly(obj.Data.RuleSetId);
        [TestMethod] public void RuleIdTest() => isReadOnly(obj.Data.RuleId);
        [TestMethod] public void RuleContextIdTest() => isReadOnly(obj.Data.RuleContextId);
        [TestMethod] public void VariableIdTest() => isReadOnly(obj.Data.VariableId);

        [TestMethod] public async Task RuleSetTest() =>
            await testItemAsync<RuleSetData, IRuleSet, IRuleSetsRepo>(
                obj.RuleSetId, () => obj.RuleSet.Data, d => new RuleSet(d));

        [TestMethod] public async Task RuleTest() =>
            await testItemAsync<RuleData, BaseRule, IRulesRepo>(
                obj.RuleId, () => obj.Rule.Data, d => {
                    if (d is null) return new UnspecifiedRule();
                    if (d.RuleKind == RuleKind.Unspecified) d.RuleKind = RuleKind.ActivityRule;
                    if (d.RuleKind == RuleKind.Rule) d.RuleId = null;
                    return RuleFactory.Create(d);
                });

        [TestMethod] public async Task RuleContextTest() =>
            await testItemAsync<RuleContextData, RuleContext, IRuleContextsRepo>(
                obj.RuleContextId, () => obj.RuleContext.Data, d => new RuleContext(d));

        [TestMethod] public async Task RuleVariableTest() =>
            await testItemAsync<RuleElementData, IRuleElement, IRuleElementsRepo>(
                obj.VariableId,
                () => obj.RuleVariable.Data,
                d => {
                    RuleSetTests.correctAVariable(d).GetAwaiter();
                    return VariableFactory.Create(d);
                });
    }
}