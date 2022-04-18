using Abc.Data.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Parties {

    [TestClass] public class NamePrefixDataTests: SealedTests<NamePrefixData, NameAttributeData> {
        [TestMethod] public void PrefixTypeIdTest() => isNullable<string>();
    }
}

