using Abc.Data.Common;
using Abc.Data.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Parties {

    [TestClass]
    public class BodyMetricTypeDataTests : SealedTests<BodyMetricTypeData, EntityTypeData> {

        [TestMethod] public void RuleSetIdTest() => isNullable<string>();

        [TestMethod] public void BaseTypeIdTest() => isNullable<string>();
    }
}