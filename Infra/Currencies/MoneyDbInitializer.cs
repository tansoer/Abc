using System.Collections.Generic;
using System.Linq;
using Abc.Data.Currencies;

namespace Abc.Infra.Currencies {
    public sealed class MoneyDbInitializer : DbInitializer {
        private static List<string> currencies = new();
        private static readonly List<CurrencyData> wikipediaCurrencies = WikipediaCurrencies.Get;
        private static readonly List<CurrencyData> currencyCodes = CurrencyNumericCodes.Get;
        private static readonly List<CurrencyData> isoCurrencyes = IsoCurrencies.Get;
        public static void Initialize(MoneyDb db, bool isTesting = false) {
            if (db is null) return;
            initializeCardAssociations(db);
            initializeCurrencies(db);
            initializeCurrencyUsages(db);
            initializeRateTypes(db);
            initializeEuroRates(db, isTesting);
        }
        private static void initializeEuroRates(MoneyDb db, bool dayRates = false) {
            if (db.Rates.Any()) return;
            currencies = new List<string>();
            var rates = EuroRates.Get(dayRates);
            foreach (var rate in rates) ensureCurrency(db, rate.CurrencyId);
            addSet(rates, db);
        }
        private static void ensureCurrency(MoneyDb db, string id) {
            if (currencies.Contains(id)) return;
            if (db.Currencies.Find(id) == null)
                addItem(new CurrencyData { Id = id, Code = id, Name = id }, db);
            currencies.Add(id);
        }
        private static void initializeCurrencyUsages(MoneyDb db) {
            if (db.CurrencyUsages.Any()) return;
            var usages = CurrencyUsages.Get;
            addSet(usages, db);
        }
        private static void initializeCurrencies(MoneyDb db) {
            if (db.Currencies.Any()) return;
            foreach (var currency in isoCurrencyes) {
                setMinorUnit(currency, wikipediaCurrencies.Find(x => x.Id == currency.Id));
                setNumericCode(currency, currencyCodes.Find(x => x.Id == currency.Id));
                addItem(currency, db);
            }
        }
        private static void setMinorUnit(CurrencyData currency, CurrencyData data) {
            if (data != null) {
                currency.MinorUnitSymbol = data.MinorUnitSymbol;
                currency.RatioOfMinorUnit = data.RatioOfMinorUnit;
            }
        }
        private static void setNumericCode(CurrencyData currency, CurrencyData data) {
            if (data != null)
                currency.NumericCode = data.NumericCode;
        }
        private static void initializeRateTypes(MoneyDb db) {
            if (db.RateTypes.Any()) return;
            var rateTypes = new[] {
            new RateTypeData { Name = "European Central Bank",
                Id = "ECB",
                Code = "ECB",
                Details =
                    "Euro foreign exchange reference rates are usually updated around 16:00 CET on every working day," +
                    "except on TARGET closing days. They are based on a regular daily concertation procedure " +
                    "between central banks across Europe, which normally takes place at 14:15 CET."
            },
            new RateTypeData {
                Id = "FED", Name = "The Federal Reserve System", Code = "FED",
                Details =
                    "The Federal Reserve System is the central bank of the United States."
            },
            new RateTypeData {
                Id = "SELL", Name = "Local Selling Rate", Code = "SELL",
                Details = "Use for indication local selling day rates of currencies."
            },
            new RateTypeData {
                Id = "BUY", Name = "Local Buying Rate", Code = "BUY",
                Details =
                    "Use for indication local buying day rates of currencies."
            }
        };
            addSet(rateTypes, db);
        }
        private static void initializeCardAssociations(MoneyDb db) {
            if (db.CardAssociations.Any()) return;
            var cards = new[] {
            new CardAssociationData {
                Id = "AMEX", Name = "American Express", Code = "AMEX",
                Details =
                    "The American Express Company, also known as Amex, is an American multinational financial services corporation."
            },
            new CardAssociationData {
                Id = "DCI", Name = "Diners Club", Code = "DCI",
                Details =
                    "Diners Club International (DCI), founded as Diners Club, is a charge card company owned by Discover Financial Services."
            },
            new CardAssociationData {
                Id = "MC", Name = "Mastercard", Code = "MC",
                Details = "Mastercard Incorporated is an American multinational financial services corporation."
            },
            new CardAssociationData {
                Id = "VISA", Name = "Visa", Code = "VISA",
                Details =
                    "Visa Inc. is an American multinational financial services corporation."
            }
        };
            addSet(cards, db);
        }
    }
}

