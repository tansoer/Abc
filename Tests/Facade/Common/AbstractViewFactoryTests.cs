using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Facade.Common;
using Abc.Facade.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Common {
        [TestClass]
        public class AbstractViewFactoryTests :
            AbstractTests<AbstractViewFactory<CurrencyData, Currency, CurrencyView>, object> {

        private class testClass :AbstractViewFactory<CurrencyData, Currency, CurrencyView> {
            protected internal override Currency toObject(CurrencyData d) => new Currency(d); 
        }
        protected override AbstractViewFactory<CurrencyData, Currency, CurrencyView> createObject() => new testClass();
        [TestMethod] public void CreateObject() {
            var v = GetRandom.ObjectOf<CurrencyView>();
            var o = obj.Create(v);
            allPropertiesAreEqual(v, o.Data, nameof(v.FullName), nameof(v.Formula));
        }
        [TestMethod] public void CreateView() {
            var d = GetRandom.ObjectOf<CurrencyData>();
            var v = obj.Create(new Currency(d));
            allPropertiesAreEqual(d, v);
        }
        [TestMethod] public void CreateTest() { }
    }
}
