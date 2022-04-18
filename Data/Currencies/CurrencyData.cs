using Abc.Data.Common;

namespace Abc.Data.Currencies {

    public sealed class CurrencyData : MetricBaseData {
        public string NumericCode { get; set; }
        public string MajorUnitSymbol { get; set; }
        public string MinorUnitSymbol { get; set; }
        public double RatioOfMinorUnit { get; set; }
        public bool IsIsoCurrency { get; set; }
    }

}