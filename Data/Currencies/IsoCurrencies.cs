using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Abc.Data.Currencies {

    public static class IsoCurrencies {

        internal static List<CurrencyData> currencies = new();

        public static List<CurrencyData> Get {
            get {
                createList();
                compose();

                return currencies;
            }
        }
        internal static void compose() {
            foreach (var d in
                from c in IsoCountries.RegionInfo
                where !isInList(c.ISOCurrencySymbol)
                select toCurrencyData(c)) toList(d);
        }
        internal static void createList() => currencies = new List<CurrencyData>();
        internal static void toList(CurrencyData d) => currencies.Add(d);
        internal static bool isInList(string id)
            => currencies.Find(x => x.Id == id) != null;
        internal static CurrencyData toCurrencyData(RegionInfo r)
            => new() {
                Id = r.ISOCurrencySymbol,
                Name = r.CurrencyEnglishName,
                Code = r.ISOCurrencySymbol,
                MajorUnitSymbol = r.CurrencySymbol,
                IsIsoCurrency = true
            };

    }

}