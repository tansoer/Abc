using Abc.Data.Quantities;
using Abc.Domain.Common;
using Abc.Domain.Quantities;
using Abc.Infra.Quantities;

namespace Abc.Tests.Domain.Quantities {

    internal class derivedMeasureTestData {

        private readonly MeasureTermsRepo terms;
        private readonly MeasuresRepo measures;
        private readonly Measure obj;

        public derivedMeasureTestData(Measure m) {
            measures = GetRepo.Instance<IMeasuresRepo>() as MeasuresRepo;
            terms = GetRepo.Instance<IMeasureTermsRepo>() as MeasureTermsRepo;
            obj = m;
        }

        internal void add() {
            addTermAndMeasure("a", "aa", 1);
            addTermAndMeasure("b", "bb", -1);
            addTermAndMeasure("c", "cc", 2);
            addTermAndMeasure("d", "dd", -2);
        }

        private void addTermAndMeasure(string code, string name, int power) {
            var dMeasure = new MeasureData { Id = code, Code = code, Name = name, MeasureType = MeasureType.Base };
            var dTerm = new CommonTermData { MasterId = obj.Id, TermId = dMeasure.Id, Power = power };
            terms.Add(new MeasureTerm(dTerm));
            measures.Add(new BaseMeasure(dMeasure));
        }

    }

}
