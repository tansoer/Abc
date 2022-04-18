using Abc.Aids.Random;
using Abc.Data.Roles;
using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Roles;
using Abc.Domain.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Roles {
    [TestClass]
    public class ResponsibilityTypeTests :SealedTests<ResponsibilityType,
        EntityType<IResponsibilityTypesRepo, ResponsibilityType, ResponsibilityTypeData>> {
        protected override ResponsibilityType createObject()
            => new(GetRandom.ObjectOf<ResponsibilityTypeData>());

        [TestMethod] public void BaseTypeTest() => isReadOnly();

        [TestMethod] public async Task RequirementsTest() {
            await testItemAsync<RuleSetData, IRuleSet, IRuleSetsRepo>(obj.requirementsRuleSetId,
                () => obj.Requirements.Data, d => new RuleSet(d));
            obj = createObject();
            isNotNull(obj.Requirements);
            isNotNull(obj.Requirements.Data);
        }

        [TestMethod] public async Task ConditionsOfSatisfactionTest() {
            await testItemAsync<RuleSetData, IRuleSet, IRuleSetsRepo>(obj.satisfactionRuleSetId,
                () => obj.ConditionsOfSatisfaction.Data, d => new RuleSet(d));
            obj = createObject();
            isNotNull(obj.ConditionsOfSatisfaction);
            isNotNull(obj.ConditionsOfSatisfaction.Data);
        }
    }
}
