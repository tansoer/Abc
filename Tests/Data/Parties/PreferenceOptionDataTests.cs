using Abc.Data.Common;
using Abc.Data.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Parties {

    [TestClass]
    public class PreferenceOptionDataTests : SealedTests<PreferenceOptionData, EntityBaseData> {

        [TestMethod] public void PreferenceTypeIdTest() => isNullable<string>();
    }
}