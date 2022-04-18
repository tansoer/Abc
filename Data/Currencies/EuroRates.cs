using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Xml;
using Abc.Aids.Methods;
using Abc.Aids.Regions;

namespace Abc.Data.Currencies {
    public static class EuroRates {
        internal const string historyUrl = "http://www.ecb.europa.eu/stats/eurofxref/eurofxref-hist.xml";
        internal const string dailyUrl = "http://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";
        internal static List<ExchangeRateData> rates;
        internal static string page;
        internal static XmlDocument doc;
        internal static XmlNodeList table;
        internal static XmlNode row;
        internal static string exchangeRateTypeId = "ECB";
        internal static string url;
        internal static List<string> currencies = new List<string>();
        public static List<ExchangeRateData> Get(bool day = true) {
            url = day ? dailyUrl : historyUrl;
            currencies = new List<string>();
            return Safe.Run(() => {
                init();
                foreach (XmlNode r in table) {
                    row = r;
                    var validFrom = DateTime.ParseExact(r.Attributes[0].Value, "yyyy-MM-dd", UseCulture.Invariant);
                    foreach (XmlNode n in r.ChildNodes) {
                        var currency = n.Attributes[0].Value;
                        var rate = decimal.Parse(n.Attributes[1].Value, UseCulture.Invariant);
                        var d = new ExchangeRateData(currency, rate, exchangeRateTypeId, validFrom);
                        rates.Add(d);
                    }
                }
                return rates;
            }, new List<ExchangeRateData>());
        }
        internal static void init() {
            newList();
            page = getPage();
            doc = getDocument();
            table = getTable();
        }
        internal static XmlNodeList getTable() => doc.ChildNodes[1].ChildNodes[2].ChildNodes;
        internal static XmlDocument getDocument() {
            var d = new XmlDocument();
            d.LoadXml(page);
            return d;
        }
        internal static void newList() => rates = new List<ExchangeRateData>();
        internal static string getPage() {
            using var client = new HttpClient();
            var content = client.GetStringAsync(url ?? dailyUrl).GetAwaiter().GetResult();
            return content;
        }
    }
}
