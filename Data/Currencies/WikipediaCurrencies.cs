using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Xml;
using Abc.Aids.Methods;

namespace Abc.Data.Currencies {

    public static class WikipediaCurrencies {

        internal static List<CurrencyData> currencies;
        internal static string page;
        internal static XmlDocument doc;
        internal static XmlNodeList table;
        internal static XmlNode row;
        internal static bool isComplete;
        internal static string state;

        public static List<CurrencyData> Get =>
            Safe.Run(() => {
                init();
                foreach (XmlNode r in table) {
                    row = r;
                    isComplete = row.ChildNodes.Count == 6;
                    var d = toCurrency();

                    currencies.Add(d);
                }

                return currencies;
            }, new List<CurrencyData>());

        internal static void init() {
            newList();
            page = getPage();
            doc = getDocument();
            table = getTable();
            state = "";
        }
        internal static CurrencyData toCurrency() {
            var d = new CurrencyData();
            state = isComplete ? getString(0) : state;
            d.Details = state;
            d.Id = getString(3);
            d.Code = d.Id;
            d.Name = getString(1);
            d.MajorUnitSymbol = getString(2);
            d.RatioOfMinorUnit = getDouble(5);
            d.MinorUnitSymbol = getString(4);

            return d;
        }
        internal static double getDouble(int i)
            => Safe.Run(
                () => double.TryParse(getString(i), out var x) ? x : 0
                , 0);

        internal static string getString(int i) => Safe.Run(
            () => row?.ChildNodes[isComplete ? i : i - 1]?.InnerText?.Trim()
            , string.Empty);

        internal static XmlNodeList getTable()
            => doc
                .SelectSingleNode("descendant::table")
                ?.ChildNodes[0]
                .ChildNodes;

        internal static XmlDocument getDocument() {

            var d = new XmlDocument();
            page = correctSyntaxErrors(page);
            d.LoadXml(page);

            return d;
        }
        private static string correctSyntaxErrors(string s) {
            s = s.Replace("<input type=\"hidden\" name=\"title\" value=\"Special:Search\">",
                "<input type=\"hidden\" name=\"title\" value=\"Special:Search\"/>");

            return s;
        }
        internal static void newList() => currencies = new List<CurrencyData>();

        internal static string getPage() {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "C# console program");

            const string url = "https://en.wikipedia.org/wiki/List_of_circulating_currencies";
            var content = client.GetStringAsync(url).GetAwaiter().GetResult();

            return content;
        }

    }

}
