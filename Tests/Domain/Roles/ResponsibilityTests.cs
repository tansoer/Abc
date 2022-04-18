using System.Collections.Generic;
using Abc.Aids.Random;
using Abc.Data.Roles;
using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Parties.Capabilities;
using Abc.Domain.Roles;
using Abc.Domain.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Abc.Aids.Calculator;

namespace Abc.Tests.Domain.Roles {
    [TestClass]
    public class ResponsibilityTests : SealedTests<Responsibility, Entity<ResponsibilityData>> {
        protected override Responsibility createObject() =>
            new (GetRandom.ObjectOf<ResponsibilityData>());

        [TestMethod] public void IsOptionalTest() => isReadOnly(obj.Data.IsOptional);

        [TestMethod]
        public async Task TypeTest() {
            await testItemAsync<ResponsibilityTypeData, ResponsibilityType, IResponsibilityTypesRepo>(
                obj.typeId, () => obj.Type.Data, d => new ResponsibilityType(d));
            obj = createObject();
            isNotNull(obj.Type);
            isNotNull(obj.Type.Data);
        }

        [TestMethod]
        public void IsCapableTest() {
            getResponsibilityType();
            isTrue(obj.IsCapable(getPartyCapability(true, true)));
            isFalse(obj.IsCapable(getPartyCapability(true, false)));
        }

        private static RuleSetData getRequirements() {
            var requirementsData = GetRandom.ObjectOf<RuleSetData>();
            var elementsRepo = GetRepo.Instance<IRuleElementsRepo>();
            var ruleData = GetRandom.ObjectOf<RuleData>();

            elementsRepo.Add(new Operand(1, "IsCapableOfDoingTask1", ruleData.Id, "Party is capable of doing task 1"));
            elementsRepo.Add(new Operand(2, "IsCapableOfDoingTask2", ruleData.Id, "Party is capable of doing task 2"));
            elementsRepo.Add(new Operator(3, Operation.And, ruleData.Id));

            GetRepo.Instance<IRulesRepo>().Add(new Rule(ruleData));
            GetRepo.Instance<IRuleUsagesRepo>().Add(new RuleUsage(new RuleUsageData { RuleId = ruleData.Id, RuleSetId = requirementsData.Id }));
            GetRepo.Instance<IRuleSetsRepo>().Add(new RuleSet(requirementsData));
            return requirementsData;
        }

        private void getResponsibilityType() {
            var responsibilityTypeData = GetRandom.ObjectOf<ResponsibilityTypeData>();
            responsibilityTypeData.RequirementsRuleSetId = getRequirements().Id;
            responsibilityTypeData.Id = obj.typeId;
            var responsibilityType = new ResponsibilityType(responsibilityTypeData);
            GetRepo.Instance<IResponsibilityTypesRepo>().Add(responsibilityType);
        }

        private static IReadOnlyList<PartyCapability> getPartyCapability(bool capability1, bool capability2) {
            var ruleContextData = GetRandom.ObjectOf<RuleContextData>();
            var capabilityData = GetRandom.ObjectOf<PartyCapabilityData>();
            capabilityData.RuleContextId = ruleContextData.Id;

            var elementsRepo = GetRepo.Instance<IRuleElementsRepo>();
            elementsRepo.Add(new BooleanVariable("IsCapableOfDoingTask1", capability1, ruleContextData.Id, true));
            elementsRepo.Add(new BooleanVariable("IsCapableOfDoingTask2", capability2, ruleContextData.Id, true));

            GetRepo.Instance<IRuleContextsRepo>().Add(new RuleContext(ruleContextData));

            return new List<PartyCapability> {
                new PartyCapability(capabilityData),
                new PartyCapability(GetRandom.ObjectOf<PartyCapabilityData>())
            };
        }
    }
}
