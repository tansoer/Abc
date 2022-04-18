using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Parties.Attributes;
using Abc.Domain.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Parties.Attributes {

    [TestClass]
    public class BodyMetricTypeTests : SealedTests<BodyMetricType, Entity<BodyMetricTypeData>> {

        protected override BodyMetricType createObject() => new (GetRandom.ObjectOf<BodyMetricTypeData>());

        [TestMethod] public void BaseTypeIdTest() => isReadOnly(obj.Data.BaseTypeId);

        [TestMethod] public void RuleSetIdTest() => isReadOnly(obj.Data.RuleSetId);

        [TestMethod]
        public async Task BaseTypeTest()
            => await testItemAsync<BodyMetricTypeData, BodyMetricType, IBodyMetricTypesRepo>(
                obj.BaseTypeId, () => obj.BaseType.Data, d => new BodyMetricType(d));

        [TestMethod]
        public async Task RuleSetTest()
            => await testItemAsync<RuleSetData, IRuleSet, IRuleSetsRepo>(
                obj.RuleSetId, () => obj.RuleSet.Data, d => new RuleSet(d));

    }

}