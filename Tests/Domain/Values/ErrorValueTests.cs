using Abc.Aids.Random;
using Abc.Data.Common;
using Abc.Domain.Values;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Values {

    [TestClass] public class ErrorValueTests :BaseValueTests<ErrorValue, string> {
        protected override ErrorValue varX(string v) => new ErrorValue(randomData());
        protected override ErrorValue varY(string v) => new ErrorValue(randomData());
        protected override ErrorValue varZ(string v) => new ErrorValue(randomData());
        private static ValueData randomData() {
            var d = GetRandom.ObjectOf<ValueData>();
            d.ValueType = ValueType.Error;
            return d;
        }
    }
}