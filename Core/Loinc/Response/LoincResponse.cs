using Abc.Core.Units;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Abc.Core.Loinc.Response {
    [Table("Table_Loinc")] public sealed class LoincResponse {
        [Column("LOINC_NUM")] public string Id { get; set; }
        [Column("COMPONENT")] public string Component { get; set; }
        [Column("SCALE_TYP")] public string Scale { get; set; }
        [Column("SYSTEM")] public string System { get; set; }
        [Column("CLASS")] public string Class { get; set; }
        [Column("TIME_ASPCT")] public string TimeAspect { get; set; }
        [Column("LONG_COMMON_NAME")] public string LongCommonName { get; set; }
        [Column("EXAMPLE_UCUM_UNITS")] public string ExampleUnits { get; set; }
        public List<string> GetUnitCodes() {
            var codes = ExampleUnits?.Split(';').ToList();
            replace(codes, "[in_us]", Distance.inchesName);
            replace(codes, "[lb_av]", Mass.poundsName);
            replace(codes, "Cel", Temperature.celsiusCode);
            replace(codes, "[degF]",Temperature.fahrenheitCode);
            return codes;
        }
        internal static void replace (List<string> l, string loincCode, string ourCode) {
            if (l != null && l.Any(x => x == loincCode)) l[l.IndexOf(loincCode)] = ourCode;
        }
    }
}
