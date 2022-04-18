using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Abc.Data.Currencies {

    public static class CurrencyUsages {

        internal static List<CurrencyUsageData> usages = new List<CurrencyUsageData>();

        public static List<CurrencyUsageData> Get {
            get {
                createList();
                compose();

                return usages;
            }
        }
        internal static void compose() {
            foreach (var d in
                from c in IsoCountries.RegionInfo
                where !isInList(c.ThreeLetterISORegionName, c.ISOCurrencySymbol)
                select toCurrencyData(c)) toList(d);
        }
        internal static void createList() => usages = new List<CurrencyUsageData>();
        internal static void toList(CurrencyUsageData d) => usages.Add(d);
        internal static bool isInList(string countryId, string currencyId)
            => usages.Find(x => x.CountryId == countryId
                                && x.CurrencyId == currencyId) != null;
        internal static CurrencyUsageData toCurrencyData(RegionInfo r)
            => new CurrencyUsageData {
                CountryId = r.ThreeLetterISORegionName,
                CurrencyId = r.ISOCurrencySymbol,
                CurrencyNativeName = r.CurrencyNativeName
            };

    }

}
