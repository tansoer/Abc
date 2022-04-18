using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests {
    public class AssemblyTests : AssemblyBaseTests {
        [TestMethod] public void IsClassifiersTested() => isAllTested(assembly, nameSpace(nameof(Abc.Data.Classifiers)));
        [TestMethod] public void IsCommonTested() => isAllTested(assembly, nameSpace(nameof(Abc.Data.Common)));
        [TestMethod] public void IsCurrenciesTested() => isAllTested(assembly, nameSpace(nameof(Abc.Data.Currencies)));
        [TestMethod] public void IsInventoryTested() => isAllTested(assembly, nameSpace(nameof(Abc.Data.Inventory)));
        [TestMethod] public void IsOrdersTested() => isAllTested(assembly, nameSpace(nameof(Abc.Data.Orders)));
        [TestMethod] public void IsPartiesTested() => isAllTested(assembly, nameSpace(nameof(Abc.Data.Parties)));
        [TestMethod] public void IsProcessesTested() => isAllTested(assembly, nameSpace(nameof(Abc.Data.Processes)));
        [TestMethod] public void IsProductsTested() => isAllTested(assembly, nameSpace(nameof(Abc.Data.Products)));
        [TestMethod] public void IsQuantitiesTested() => isAllTested(assembly, nameSpace(nameof(Abc.Data.Quantities)));
        [TestMethod] public void IsRolesTested() => isAllTested(assembly, nameSpace(nameof(Abc.Data.Roles)));
        [TestMethod] public void IsRulesTested() => isAllTested(assembly, nameSpace(nameof(Abc.Data.Rules)));
    }
}
