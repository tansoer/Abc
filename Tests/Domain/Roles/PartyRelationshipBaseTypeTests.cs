using Abc.Aids.Calculator;
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
    public class PartyRelationshipBaseTypeTests :AbstractTests<
        PartyRelationshipBaseType<TestClassForPartyRelationshipBaseTypeData>,
        RelationshipType<TestClassForPartyRelationshipBaseTypeData>> {

        private class testClass :PartyRelationshipBaseType<TestClassForPartyRelationshipBaseTypeData> {
            public testClass() : this(null) { }
            public testClass(TestClassForPartyRelationshipBaseTypeData d) : base(d) { }
        }

        protected override PartyRelationshipBaseType<TestClassForPartyRelationshipBaseTypeData> createObject() =>
            new testClass(GetRandom.ObjectOf<TestClassForPartyRelationshipBaseTypeData>());

        [TestMethod]
        public void CanFormRelationshipTest() { 
            var consumer = new PartyRole(GetRandom.ObjectOf<PartyRoleData>());
            var provider = new PartyRole(GetRandom.ObjectOf<PartyRoleData>());
            addRelationshipConstraint(consumer, provider);

            isTrue(obj.CanFormRelationship(consumer, provider));
        }

        [TestMethod]
        [DataRow(5, 10, false)]
        [DataRow(15, 10, true)]
        public void CanFormRelationshipWithRuleContextTest(double actualAge, double requiredAge, bool allowed) {
            var consumer = new PartyRole(GetRandom.ObjectOf<PartyRoleData>());
            var provider = new PartyRole(GetRandom.ObjectOf<PartyRoleData>());
            addRelationshipConstraint(consumer, provider);
            
            var context = addRequirements(actualAge, requiredAge);


            areEqual(allowed, obj.CanFormRelationship(consumer, provider, context));
        }

        private RuleContext addRequirements(double actualAge, double requiredAge) {
            var requirementsData = GetRandom.ObjectOf<RuleSetData>();
            requirementsData.Id = obj.Data.RuleSetId;
            var requirements = new RuleSet(requirementsData);
            var rule = new Rule(GetRandom.ObjectOf<RuleData>());
            var ruleUsage = new RuleUsage(new RuleUsageData {
                RuleId = rule.Id,
                RuleSetId = requirements.Id });
            var ruleContext = new RuleContext(GetRandom.ObjectOf<RuleContextData>());
            ruleContext.Data.RuleId = rule.Id;

            var eR = GetRepo.Instance<IRuleElementsRepo>();
            GetRepo.Instance<IRulesRepo>().Add(rule);
            eR.Add(new Operand(1, "Age", rule.Id, "Subject's age"));
            eR.Add(new Operand(2, "RequiredAge", rule.Id, "Required age"));
            eR.Add(new Operator(3, Operation.IsGreater, rule.Id));
            GetRepo.Instance<IRuleSetsRepo>().Add(requirements);
            GetRepo.Instance<IRuleUsagesRepo>().Add(ruleUsage);
            eR.Add(new DoubleVariable(1, "Age", actualAge, ruleContext.Id, "Subject's age", true));
            eR.Add(new DoubleVariable(2, "RequiredAge", requiredAge, ruleContext.Id, "Required age", true));

            return ruleContext;
        }

        private void addRelationshipConstraint(PartyRole consumer, PartyRole provider) {
            var relationShipConstraintType = new RelationshipConstraintType(new RelationshipConstraintTypeData {
                ConsumerRoleTypeId = consumer.typeId,
                ProviderRoleTypeId = provider.typeId,
            });
            GetRepo.Instance<IRelationshipConstraintTypesRepo>().Add(relationShipConstraintType);
            var relationShipConstraint = new RelationshipConstraint(new RelationshipConstraintData {
                ConstraintTypeId = relationShipConstraintType.Id,
                RelationshipTypeId = obj.Id
            });
            GetRepo.Instance<IRelationshipConstraintsRepo>().Add(relationShipConstraint);
        }

        [TestMethod]
        public async Task RequirementsTest() {
            await testItemAsync<RuleSetData, IRuleSet, IRuleSetsRepo>(
                obj.ruleSetId, () => obj.Requirements.Data, d => new RuleSet(d));
            obj = createObject();
            isNotNull(obj.Requirements);
            isNotNull(obj.Requirements.Data);
        }

        [TestMethod]
        public async Task ConstraintsTest() =>
            await testListAsync<RelationshipConstraint, RelationshipConstraintData, IRelationshipConstraintsRepo>(
                obj, nameof(obj.Constraints), 
                x => x.RelationshipTypeId = obj.Id,
                d => new RelationshipConstraint(d));
    }
}