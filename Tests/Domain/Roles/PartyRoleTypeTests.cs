using Abc.Aids.Calculator;
using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Data.Roles;
using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Parties;
using Abc.Domain.Parties.Capabilities;
using Abc.Domain.Roles;
using Abc.Domain.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Roles {
    [TestClass]
    public class PartyRoleTypeTests : SealedTests<PartyRoleType, EntityType<IPartyRoleTypesRepo, PartyRoleType, PartyRoleTypeData>> {

        protected override PartyRoleType createObject() =>
            new (GetRandom.ObjectOf<PartyRoleTypeData>());

        [DataRow(PartyType.Person)]
        [DataRow(PartyType.Organization)]
        [TestMethod]
        public void CanPlayRoleTest(PartyType partyType) {
            var party = createPersonAndConstraintType(partyType);

            isTrue(obj.CanPlayRole(party));
            isFalse(obj.CanPlayRole(null));
        }

        private IParty createPersonAndConstraintType(PartyType partyType) {
            var constraintData = GetRandom.ObjectOf<PartyRoleConstraintData>();
            constraintData.PartyRoleTypeId = obj.Data.Id;
            var constraintTypeData = GetRandom.ObjectOf<PartyRoleConstraintTypeData>();
            constraintTypeData.Id = constraintData.RoleConstraintTypeId;
            constraintTypeData.PartyType = partyType;
            constraintTypeData.OrganizationTypeId = rndStr;
            var partyData = GetRandom.ObjectOf<PartyData>();
            partyData.PartyType = partyType;
            partyData.OrganizationTypeId = constraintTypeData.OrganizationTypeId;

            GetRepo.Instance<IPartyRoleConstraintTypesRepo>().Add(new PartyRoleConstraintType(constraintTypeData));
            GetRepo.Instance<IPartyRoleConstraintsRepo>().Add(new PartyRoleConstraint(constraintData));
            return PartyFactory.Create(partyData);
        }

        [DataRow(true, true, true)]
        [DataRow(false, false, true)]
        [DataRow(false, false, false)]
        [TestMethod]
        public void IsCapableForRoleTest(bool expected, bool capability1, bool capability2) {
            var partyData = GetRandom.ObjectOf<PartyData>();

            addPartyCapabilities(partyData, capability1, capability2);
            getRequirements();

            areEqual(expected, obj.IsCapableForRole(PartyFactory.Create(partyData)));
        }
        private static void addPartyCapabilities(PartyData partyData, bool capability1, bool capability2) {
            var ruleContextData = GetRandom.ObjectOf<RuleContextData>();
            var capabilityData = GetRandom.ObjectOf<PartyCapabilityData>();
            capabilityData.PartyId = partyData.Id;
            capabilityData.RuleContextId = ruleContextData.Id;

            var elementsRepo = GetRepo.Instance<IRuleElementsRepo>();
            elementsRepo.Add(new BooleanVariable("IsCapableOfDoingTask1", capability1, ruleContextData.Id, true));
            elementsRepo.Add(new BooleanVariable("IsCapableOfDoingTask2", capability2, ruleContextData.Id, true));

            GetRepo.Instance<IPartyCapabilitiesRepo>().Add(new PartyCapability(capabilityData));
            GetRepo.Instance<IRuleContextsRepo>().Add(new RuleContext(ruleContextData));
        }
        private RuleSetData getRequirements() {
            var requirementsData = GetRandom.ObjectOf<RuleSetData>();
            requirementsData.Id = obj.ruleSetId;
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


        [DataRow(true, true, true)]
        [DataRow(false, false, true)]
        [DataRow(false, false, false)]
        [TestMethod]
        public void IsCapableForMandatoryResponsibilitiesTest(bool expected, bool capability1, bool capability2) {
            var partyData = GetRandom.ObjectOf<PartyData>();
            addPartyCapabilities(partyData, capability1, capability2);
            obj.AddMandatoryResponsibility(getResponsibilityType());

            areEqual(expected, obj.IsCapableForMandatoryResponsibilities(PartyFactory.Create(partyData)));
        }

        [DataRow(true, true, true)]
        [DataRow(false, false, true)]
        [DataRow(false, false, false)]
        [TestMethod]
        public void IsCapableForOptionalResponsibilitiesTest(bool expected, bool capability1, bool capability2) {
            var partyData = GetRandom.ObjectOf<PartyData>();
            addPartyCapabilities(partyData, capability1, capability2);
            obj.AddOptionalResponsibility(getResponsibilityType());

            areEqual(expected, obj.IsCapableForOptionalResponsibilities(PartyFactory.Create(partyData)));
        }
        
        [TestMethod]
        public void AddMandatoryResponsibilityTest() {
            var count = obj.MandatoryResponsibilities.Count;
            obj.AddMandatoryResponsibility(getResponsibilityType());
            areEqual(count + 1, obj.MandatoryResponsibilities.Count);
        }

        [TestMethod]
        public void AddOptionalResponsibilityTest() {
            var count = obj.OptionalResponsibilities.Count;
            obj.AddOptionalResponsibility(getResponsibilityType());
            areEqual(count + 1, obj.OptionalResponsibilities.Count);
        }

        [TestMethod]
        public void DeleteResponsibilityTest() {
            var responsibilityType = getResponsibilityType();
            obj.AddMandatoryResponsibility(responsibilityType);
            var count = obj.MandatoryResponsibilities.Count;
            PartyRoleType.DeleteResponsibility(obj.FindResponsibility(responsibilityType));
            areEqual(count - 1, obj.MandatoryResponsibilities.Count);
        }

        [TestMethod]
        public void FindResponsibilityTest() {
            var responsibilityType = getResponsibilityType();
            obj.AddMandatoryResponsibility(responsibilityType);
            var foundResponsibility = obj.FindResponsibility(responsibilityType);
            areEqual(responsibilityType.Id, foundResponsibility.typeId);
        }

        [TestMethod]
        public void HasResponsibilityTest() {
            var responsibilityType = getResponsibilityType();
            isFalse(obj.HasResponsibility(responsibilityType));
            obj.AddMandatoryResponsibility(responsibilityType);
            isTrue(obj.HasResponsibility(responsibilityType));
        }

        [TestMethod] public void BaseTypeTest() => isNotNull(obj.BaseType);

        [TestMethod] public async Task ConstraintsTest() =>
            await testListAsync<PartyRoleConstraint, PartyRoleConstraintData, IPartyRoleConstraintsRepo>(
                obj, nameof(obj.Constraints),
                x => x.PartyRoleTypeId = obj.Id,
                d => new PartyRoleConstraint(d));

        [TestMethod] public async Task RequirementsTest() {
            await testItemAsync<RuleSetData, IRuleSet, IRuleSetsRepo>(
                obj.ruleSetId, () => obj.Requirements.Data, d => new RuleSet(d));
            obj = createObject();
            isNotNull(obj.Requirements);
            isNotNull(obj.Requirements.Data);
        }

        [TestMethod]
        public void MandatoryResponsibilitiesTest() {
            for (int i = 0; i < 3; i++) obj.AddOptionalResponsibility(getResponsibilityType());
            for (int i = 0; i < 4; i++) obj.AddMandatoryResponsibility(getResponsibilityType());
            areEqual(4, obj.MandatoryResponsibilities.Count);
            isReadOnly();
        }

        [TestMethod]
        public void OptionalResponsibilitiesTest() {
            for (int i = 0; i < 3; i++) obj.AddOptionalResponsibility(getResponsibilityType());
            for (int i = 0; i < 4; i++) obj.AddMandatoryResponsibility(getResponsibilityType());
            areEqual(3, obj.OptionalResponsibilities.Count);
            isReadOnly();
        }

        private ResponsibilityType getResponsibilityType()
        {
            var responsibilityTypeData = GetRandom.ObjectOf<ResponsibilityTypeData>();
            responsibilityTypeData.RequirementsRuleSetId = getRequirements().Id;
            var responsibilityType = new ResponsibilityType(responsibilityTypeData);
            GetRepo.Instance<IResponsibilityTypesRepo>().Add(responsibilityType);
            return responsibilityType;
        }
    }
}
