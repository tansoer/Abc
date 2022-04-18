using System.Linq;
using Abc.Aids.Calculator;
using Abc.Data.Rules;
using Abc.Domain.Rules;

namespace Abc.Infra.Rules {

    public static class RuleDbInitializer {

        internal static string celsiusToKelvinRuleId = "CtoK";
        internal static string kelvinToCelsiusRuleId = "KtoC";
        internal static string fahrenheitToKelvinRuleId = "FtoK";
        internal static string kelvinToFahrenheitRuleId = "KtoF";
        internal static string rankneToKelvinRuleId = "RtoK";
        internal static string kelvinToRankneRuleId = "KtoR";

        private static RuleElementData rankneToKelvinConst(int idx, string name, string ruleId)
            => new DoubleVariable(idx, name, 1.8, ruleId, "Rankine to Kelvin constant").Data;
        private static RuleElementData fahrenheitToKelvinConst(int idx, string name, string ruleId)
            => new DoubleVariable(idx, name, 459.67, ruleId, "Fahrenheit to Kelvin constant").Data;
        private static RuleElementData celsiusToKelvinConst(int idx, string name, string ruleId)
            => new DoubleVariable(idx, name, 273.15, ruleId, "Celsius to Kelvin constant").Data;

        public static void Initialize(RuleDb db) {
            if (db is null) return;
            initializeTemperatureRules(db);
        }
        private static void initializeTemperatureRules(RuleDb db) {
            var setId = initializeTemperatureRuleSet(db);
            initializeTemperatureRules(db, setId);
        }
        private static string initializeTemperatureRuleSet(RuleDb db) {
            if (db.RuleSets.Any()) return string.Empty;
            var set = new RuleSetData {
                Id = "TempRules",
                Code = "TempRules",
                Name = "Temperature Rules",
                Details =
                    "Temperature units conversion rules from the Celsius, Fahrenheit and Rankine to Kelvin and vice versa."
            };
            db.RuleSets.Add(set);
            db.SaveChanges();

            return set.Id;
        }
        private static void initializeTemperatureRules(RuleDb db, string setId) {
            if (db.RuleElements.Any()) return;
            addCtoK(db, setId);
            addKtoC(db, setId);
            addFtoK(db, setId);
            addKtoF(db, setId);
            addRtoK(db, setId);
            addKtoR(db, setId); 
        }
        private static void addKtoR(RuleDb db, string setId) {
            var x = new RuleData {
                Id = kelvinToRankneRuleId, 
                Code = kelvinToRankneRuleId, 
                Name = "Kelvin to Rankine", 
                Details = "Kelvin to Rankine conversion rule"
            };
            db.Rules.Add(x);
            db.RuleUsages.Add(new RuleUsageData { RuleId = x.Id, RuleSetId = setId });
            db.RuleElements.Add(new Operand(1, "a", x.Id, "Temperature in Kelvin").Data);
            db.RuleElements.Add(rankneToKelvinConst(2, "b", x.Id));
            db.RuleElements.Add(new Operator(3, Operation.Divide, x.Id).Data);
            db.SaveChanges();
        }
        private static void addRtoK(RuleDb db, string setId) {
            var x = new RuleData {
                Id = rankneToKelvinRuleId,
                Code = rankneToKelvinRuleId,
                Name = "Rankine to Kelvin",
                Details = "Rankine to Kelvin conversion rule"
            };
            db.Rules.Add(x);
            db.RuleUsages.Add(new RuleUsageData { RuleId = x.Id, RuleSetId = setId });
            db.RuleElements.Add(new Operand(1, "a", x.Id, "Temperature in Rankine").Data);
            db.RuleElements.Add(rankneToKelvinConst(2, "b", x.Id));
            db.RuleElements.Add(new Operator(3, Operation.Multiply, x.Id).Data);
            db.SaveChanges();
        }
        private static void addKtoF(RuleDb db, string setId) {
            var x = new RuleData {
                Id = kelvinToFahrenheitRuleId,
                Code = kelvinToFahrenheitRuleId,
                Name = "Kelvin to Fahrenheit",
                Details = "Kelvin to Fahrenheit conversion rule"
            };
            db.Rules.Add(x);
            db.RuleUsages.Add(new RuleUsageData { RuleId = x.Id, RuleSetId = setId });
            db.RuleElements.Add(new Operand(1, "a", x.Id, "Temperature in Kelvin").Data);
            db.RuleElements.Add(rankneToKelvinConst(2, "b", x.Id));
            db.RuleElements.Add(new Operator(3, Operation.Multiply, x.Id).Data);
            db.RuleElements.Add(fahrenheitToKelvinConst(4, "c", x.Id));
            db.RuleElements.Add(new Operator(5, Operation.Subtract, x.Id).Data);
            db.SaveChanges();
        }
        private static void addFtoK(RuleDb db, string setId) {
            var x = new RuleData {
                Id = fahrenheitToKelvinRuleId,
                Code = fahrenheitToKelvinRuleId,
                Name = "Fahrenheit to Kelvin",
                Details = "Fahrenheit to Kelvin conversion rule"
            };
            db.Rules.Add(x);
            db.RuleUsages.Add(new RuleUsageData { RuleId = x.Id, RuleSetId = setId });
            db.RuleElements.Add(new Operand(1, "a", x.Id, "Temperature in Fahrenheit").Data);
            db.RuleElements.Add(fahrenheitToKelvinConst(2, "b", x.Id));
            db.RuleElements.Add(new Operator(3, Operation.Add, x.Id).Data);
            db.RuleElements.Add(rankneToKelvinConst(4, "c", x.Id));
            db.RuleElements.Add(new Operator(5, Operation.Divide, x.Id).Data);
            db.SaveChanges();
        }
        private static void addKtoC(RuleDb db, string setId) {
            var x = new RuleData {
                Id = kelvinToCelsiusRuleId,
                Code = kelvinToCelsiusRuleId,
                Name = "Kelvin to Celsius",
                Details = "Kelvin to Celsius conversion rule"
            };
            db.Rules.Add(x);
            db.RuleUsages.Add(new RuleUsageData { RuleId = x.Id, RuleSetId = setId });
            db.RuleElements.Add(new Operand(1, "a", x.Id, "Temperature in Kelvin").Data);
            db.RuleElements.Add(celsiusToKelvinConst(2, "b", x.Id));
            db.RuleElements.Add(new Operator(3, Operation.Subtract, x.Id).Data);
            db.SaveChanges();
        }
        private static void addCtoK(RuleDb db, string setId) {
            var x = new RuleData {
                Id = celsiusToKelvinRuleId,
                Code = celsiusToKelvinRuleId,
                Name = "Celsius to Kelvin",
                Details = "Celsius to Kelvin conversion rule"
            };
            db.Rules.Add(x);
            db.RuleUsages.Add(new RuleUsageData { RuleId = x.Id, RuleSetId = setId });
            db.RuleElements.Add(new Operand(1, "a", x.Id, "Temperature in Celsius").Data);
            db.RuleElements.Add(celsiusToKelvinConst(2, "b", x.Id));
            db.RuleElements.Add(new Operator(3, Operation.Add, x.Id).Data);
            db.SaveChanges();
        }

    }

}
