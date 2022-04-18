using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Facade.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Quantities {
    [TestClass] public class UnitRulesViewFactoryTests :TestsBase {
        [TestInitialize] public virtual void TestInitialize() => type = typeof(UnitRulesViewFactory);
        [TestMethod] public void CreateTest() { }
        [TestMethod] public void CreateObjectTest() {
            var view = random<UnitRulesView>();
            var data = new UnitRulesViewFactory().Create(view).Data;
            allPropertiesAreEqual(view, data, nameof(UnitRulesView.FromFormula), nameof(UnitRulesView.ToFormula));
        }
        [TestMethod] public void CreateViewTest() {
            var data = random<UnitRulesData>();
            var view = new UnitRulesViewFactory().Create(new UnitRules(data));

            allPropertiesAreEqual(view, data, nameof(UnitRulesView.FromFormula), nameof(UnitRulesView.ToFormula));
        }
    }

}
