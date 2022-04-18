using System.Collections.Generic;
using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Quantities {
    [TestClass]
    public class MetricTests :AbstractTests<Metric<MeasureData, MeasureTerm>, BaseMetric<MeasureData>> {

        private class testClass :Metric<MeasureData, MeasureTerm> {
            public override IReadOnlyList<MeasureTerm> Terms { get; } = null;
        }
        protected override Metric<MeasureData, MeasureTerm> createObject() => new testClass();

        [TestMethod] public void TermsTest() => isAbstractReadOnly();
    }
}
