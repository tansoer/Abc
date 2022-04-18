using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Facade.Common;

namespace Abc.Facade.Currencies {

    public sealed class CurrencyViewFactory :
        AbstractViewFactory<CurrencyData, Currency, CurrencyView> {
        protected internal override Currency toObject(CurrencyData d) => new Currency(d);
        public override CurrencyView Create(Currency o) {
            var v = base.Create(o);
            v.FullName = o.FullName;
            v.Formula = o.Formula;
            return v;
        }

    }

}