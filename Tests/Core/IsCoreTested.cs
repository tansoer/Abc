using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Core {
    [TestClass] public class IsCoreTested : AssemblyBaseTests {
        protected override string assembly => $"{nameof(Abc)}.{nameof(Abc.Core)}";
        protected override string nameSpace(string name) { return $"{assembly}.{name}"; }
        [TestMethod] public void IsUnitsTested() => isAllTested(assembly, nameSpace(nameof(Abc.Core.Units)));
        [TestMethod] public void IsRoundingTested() => isAllTested(assembly, nameSpace(nameof(Abc.Core.Rounding)));
        [TestMethod] public void IsLoincTested() => isAllTested(assembly, nameSpace(nameof(Abc.Core.Loinc)));
    }
}
