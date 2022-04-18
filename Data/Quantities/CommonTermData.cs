using Abc.Core.Units;
using Abc.Data.Common;

namespace Abc.Data.Quantities {

    public sealed class CommonTermData :BaseData {
        public CommonTermData() : this(null) { }
        public CommonTermData(string masterId = null, double power = 0, string termId = null) {
            MasterId = masterId;
            Power = power;
            TermId = termId;
        }
        public CommonTermData(UnitInfo u, TermInfo t) : this(u?.Id, t?.Power ?? 0, t?.TermId) { }
        public double Power { get; set; }
        public string TermId { get; set; }
        public string MasterId { get; set; }
    }
}
