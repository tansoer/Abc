using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids {

    [TestClass] public class IsAidsTested :AssemblyBaseTests {
        protected override string assembly => $"{nameof(Abc)}.{nameof(Abc.Aids)}";
        protected override string nameSpace(string name) => $"{assembly}.{name}"; 
        [TestMethod] public void IsCalculatorTested() => isAllTested(assembly, nameSpace(nameof(Abc.Aids.Calculator)));
        [TestMethod] public void IsEnumsTested() => isAllTested(assembly, nameSpace(nameof(Abc.Aids.Enums)));
        [TestMethod] public void IsConstantsTested() => isAllTested(assembly, nameSpace(nameof(Abc.Aids.Constants)));
        [TestMethod] public void IsExtensionsTested() => isAllTested(assembly, nameSpace(nameof(Abc.Aids.Extensions)));
        [TestMethod] public void IsLoggingTested() => isAllTested(assembly, nameSpace(nameof(Abc.Aids.Logging)));
        [TestMethod] public void IsRandomTested() => isAllTested(assembly, nameSpace(nameof(Abc.Aids.Random)));
        [TestMethod] public void IsReflectionTested() => isAllTested(assembly, nameSpace(nameof(Abc.Aids.Reflection)));
        [TestMethod] public void IsServicesTested() => isAllTested(assembly, nameSpace(nameof(Abc.Aids.Services)));
        [TestMethod] public void IsMethodsTested() => isAllTested(assembly, nameSpace(nameof(Abc.Aids.Methods)));
        [TestMethod] public void IsValuesTested() => isAllTested(assembly, nameSpace(nameof(Abc.Aids.Values)));
        [TestMethod] public void IsRegionsTested() => isAllTested(assembly, nameSpace(nameof(Abc.Aids.Regions)));
        [TestMethod] public void IsRegExpTested() => isAllTested(assembly, nameSpace(nameof(Abc.Aids.RegExp)));
        [TestMethod] public void IsClassesTested() => isAllTested(assembly, nameSpace(nameof(Abc.Aids.Classes)));
        [TestMethod] public void IsFormatsTested() => isAllTested(assembly, nameSpace(nameof(Abc.Aids.Formats)));
    }
}
