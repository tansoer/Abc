using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Xml;
using Abc.Aids.Methods;

namespace Abc.Data.Currencies {
    public static class CurrencyNumericCodes {
        internal static List<CurrencyData> currencies;
        internal static string page;
        internal static XmlDocument doc;
        internal static XmlNodeList table;
        internal static XmlNode row;

        public static List<CurrencyData> Get =>
            Safe.Run(() => {
                init();
                foreach (XmlNode r in table) {
                    row = r;
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
        }
        internal static CurrencyData toCurrency() {
            var d = new CurrencyData();
            d.Id = getString(2);
            d.Code = d.Id;
            d.Name = getString(1);
            d.NumericCode = getString(3);
            return d;
        }

        internal static string getString(int i) => Safe.Run(
            () => row?.ChildNodes[i]?.InnerText?.Trim()
            , string.Empty);

        internal static XmlNodeList getTable()
            => doc
                .SelectSingleNode("descendant::table")
                .ChildNodes[1]
                .ChildNodes;

        internal static XmlDocument getDocument() {
            page = page.Substring(page.IndexOf("<table class", StringComparison.Ordinal));
            page = page.Remove(page.IndexOf("</div></div></div></div></div></div>", StringComparison.Ordinal));
            page = page.Replace("&nbsp;", "");
            page = page.Replace("&rsquo;", "");
            var d = new XmlDocument();
            d.LoadXml(page);

            return d;
        }
        internal static void newList() => currencies = new List<CurrencyData>();


        internal static string getPage() {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "C# console program");
            const string url = "https://www.iban.com/currency-codes";
            var content = client.GetStringAsync(url).GetAwaiter().GetResult();

            return content;
        }
    }
}
