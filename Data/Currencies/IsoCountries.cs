using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Abc.Data.Currencies {

    public static class IsoCountries {

        public static List<CountryData> Get {
            get {
                var countries = new List<CountryData>();

                foreach (var c in RegionInfo) {
                    var d = new CountryData {
                        Id = c.ThreeLetterISORegionName,
                        Name = c.EnglishName,
                        OfficialName = c.DisplayName,
                        NativeName = c.NativeName,
                        Code = c.TwoLetterISORegionName,
                        IsLoyaltyProgram = false,
                        IsIsoCountry = true
                    };
                    var numericCode = c.GeoId;
                    d.NumericCode = fixNumericCodeForBotswana(numericCode, d.Id).ToString();
                    countries.Add(d);
                }

                return countries;
            }
        }

        public static List<RegionInfo> RegionInfo {
            get {
                var list = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                    .Select(i => new RegionInfo(i.LCID))
                    .Distinct()
                    .ToList();
                removeNotCountries(list);
                return list.OrderBy(p => p.EnglishName).ToList();
            }
        }
        private static void removeNotCountries(List<RegionInfo> list) {
            remove(list, "World");
            remove(list, "Caribbean");
            remove(list, "Latin America");
        }
        private static void remove(List<RegionInfo> list, string s) {
            var x = list.Find(y => y.EnglishName == s);

            if (x is null) return;
            list.Remove(x);
        }

        private static int fixNumericCodeForBotswana(int numericCode, string id) {
            return id == "BWA" ? 999 : numericCode;
        }

    }

}