using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain {

    [TestClass]
    public class IsDomainTested : AssemblyTests {

        protected override string assembly => "Abc.Domain";

        protected override string nameSpace(string name) {
            return $"{assembly}.{name}";
        }
        [TestMethod] public void IsPartiesAttributesTested() => isAllTested(assembly, nameSpace("Parties.Attributes"));
        [TestMethod] public void IsPartiesCapabilitiesTested() => isAllTested(assembly, nameSpace("Parties.Capabilities"));
        [TestMethod] public void IsPartiesContactsTested() => isAllTested(assembly, nameSpace("Parties.Contacts"));
        [TestMethod] public void IsPartiesIdentifiersTested() => isAllTested(assembly, nameSpace("Parties.Identifiers"));
        [TestMethod] public void IsPartiesNamesTested() => isAllTested(assembly, nameSpace("Parties.Names"));
        [TestMethod] public void IsPartiesPreferencesTested() => isAllTested(assembly, nameSpace("Parties.Preferences"));
        [TestMethod] public void IsPartiesSignaturesTested() => isAllTested(assembly, nameSpace("Parties.Signatures"));
        [TestMethod] public void IsValuesTested() => isAllTested(assembly, nameSpace("Values"));
        [TestMethod] public void IsExceptionsTested() => isAllTested(assembly, nameSpace("Exceptions"));

    }

}
