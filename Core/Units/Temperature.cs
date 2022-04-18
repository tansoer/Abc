using System.Collections.Generic;

namespace Abc.Core.Units {
    public static class Temperature {
        public static UnitInfo Measure = new UnitInfo (
            temperatureName,
            temperatureCode,
            temperatureDisplayName,
            temperatureDef
        );
        public static List<UnitInfo> Units =>
            new List<UnitInfo> {
                new UnitInfo(kelvinName, kelvinCode),
                new UnitInfo(celsiusName, celsiusCode),
                new UnitInfo(fahrenheitName, fahrenheitCode),
                new UnitInfo(rankineName, rankineCode)
            };
        internal const string temperatureName = "Temperature";
        internal const string temperatureDisplayName = "Thermodynamic Temperature";
        internal const string temperatureCode = "T";
        internal const string temperatureDef = "Thermodynamic temperature is the absolute "
            + "measure of temperature " 
            + "and is one of the principal parameters of thermodynamics.";
        internal const string celsiusName = "Celsius";
        internal const string celsiusCode = "°C";
        internal const string fahrenheitName = "Fahrenheit";
        internal const string fahrenheitCode = "°F";
        internal const string kelvinName = "Kelvin";
        internal const string kelvinCode = "°K";
        internal const string rankineName = "Rankine";
        internal const string rankineCode = "°R";
        internal const string celsiusToKelvinRuleId = "Celsius to Kelvin";
        internal const string fahrenheitToKelvinRuleId = "Fahrenheit to Kelvin";
        internal const string rankineToKelvinRuleId = "Rankine to Kelvin";
        internal const string kelvinToCelsiusRuleId = "Kelvin to Celsius";
        internal const string kelvinToFahrenheitRuleId = "Kelvin to Fahrenheit";
        internal const string kelvinToRankineRuleId = "Kelvin to Rankine";
        internal const double celsiusFactor = 273.15;
        internal const double fahrenheitFactor = 459.67;
        internal const double rankineFactor = 1.8000;
    }
}
