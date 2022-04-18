using Abc.Data.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Parties {
    [TestClass] public class NameSuffixDataTests : SealedTests<NameSuffixData, NameAttributeData> {
        [TestMethod] public void SuffixTypeIdTest() => isNullable<string>();
    }
}