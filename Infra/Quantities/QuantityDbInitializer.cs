using System;
using System.Collections.Generic;
using System.Linq;
using Abc.Core.Units;
using Abc.Data.Common;
using Abc.Data.Quantities;
using Abc.Infra.Rules;
using Microsoft.EntityFrameworkCore;
using UnitData = Abc.Data.Quantities.UnitData;

namespace Abc.Infra.Quantities {
    public class QuantityDbInitializer :DbInitializer {
        internal static QuantityDb db;
        public static void Initialize(QuantityDb c) {
            if (c is null) return;
            db = c;
            initialize(SystemOfUnits.Units);
            initialize(Area.Measure, Area.Units);
            initialize(Counter.Measure, Counter.Units);
            initialize(Current.Measure, Current.Units);
            initialize(Distance.Measure, Distance.Units);
            initialize(Luminos.Measure, Luminos.Units);
            initialize(ManHour.Measure, ManHour.Units);
            initialize(Mass.Measure, Mass.Units);
            initialize(Persentage.Measure, Persentage.Units);
            initialize(Substance.Measure, Substance.Units);
            initialize(Temperature.Measure, Temperature.Units);
            addRules(Temperature.Units);
            initialize(Time.Measure, Time.Units);
            initialize(Volume.Measure, Volume.Units);
            initialize(Density.Measure, Density.Units);
            initialize(Acidity.Measure, Acidity.Units);
            initialize(Acceleration.Measure, Acceleration.Units);
            initialize(Force.Measure, Force.Units);
            initialize(Pressure.Measure, Pressure.Units);
        }
        private static void addRules(List<UnitInfo> units) {
            addRule(Temperature.celsiusName, SystemOfUnits.SiSystemId, RuleDbInitializer.celsiusToKelvinRuleId, RuleDbInitializer.kelvinToCelsiusRuleId);
            addRule(Temperature.rankineName, SystemOfUnits.SiSystemId, RuleDbInitializer.rankneToKelvinRuleId, RuleDbInitializer.kelvinToRankneRuleId);
            addRule(Temperature.fahrenheitName, SystemOfUnits.SiSystemId, RuleDbInitializer.fahrenheitToKelvinRuleId, RuleDbInitializer.kelvinToFahrenheitRuleId);
        }
        private static void addRule(string unitId, string siSystemId, string toKelvinRuleId, string fromKelvinRuleId) {
            var d = new UnitRulesData();
            d.Id = unitId;
            d.UnitId = unitId;
            d.SystemOfUnitsId = siSystemId;
            d.ToBaseUnitRuleId = toKelvinRuleId;
            d.FromBaseUnitRuleId = fromKelvinRuleId;
            addItem(d, db);
        }
        internal static void initialize(IEnumerable<UnitInfo> data) {
            foreach (var d in data) {
                addItem(new SystemOfUnitsData(d), db);
            }
        }
        internal static void initialize(UnitInfo measure, List<UnitInfo> units) {
            addMeasure(measure);
            addTerms(measure, db.CommonTerms);
            addUnits(units, measure.Id);
            addTerms(units);
            addUnitFactors(units, SystemOfUnits.SiSystemId);
        }
        internal static void addUnitFactors(List<UnitInfo> units, string siSystemId) {
            foreach (var d in units) {
                if (Math.Abs(d.Factor) < double.Epsilon) continue;
                var o = db.UnitFactors.FirstOrDefaultAsync(
                    m => m.SystemOfUnitsId == siSystemId && m.UnitId == d.Id).GetAwaiter().GetResult();
                if (o is not null) continue;
                addItem(new UnitFactorData(d, siSystemId), db);
            }
        }
        internal static void addTerms(List<UnitInfo> units) {
            foreach (var d in units)
                addTerms(d, db.CommonTerms);
        }
        internal static void addTerms(UnitInfo measure, DbSet<CommonTermData> set) {
            foreach (var d in measure.Terms) {
                var o = set.FirstOrDefaultAsync(
                    m => m.MasterId == measure.Id && m.TermId == d.TermId).GetAwaiter().GetResult();
                if (o is not null) continue;
                addItem(new CommonTermData(measure, d), db);
            }
        }
        internal static void addMeasure(UnitInfo measure) 
            => addItem(new MeasureData(measure), db);
        internal static void addUnits(IEnumerable<UnitInfo> units, string measureId) {
            foreach (var d in units) addItem(new UnitData(d, measureId), db);
        }
        internal static T getItem<T>(IQueryable<T> set, string id) where T : EntityBaseData
            => set.FirstOrDefaultAsync(m => m.Id == id).GetAwaiter().GetResult();
    }
}
