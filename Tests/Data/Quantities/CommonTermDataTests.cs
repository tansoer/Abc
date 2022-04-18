using Abc.Aids.Random;
using Abc.Data.Common;
using Abc.Data.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Quantities {

    [TestClass]
    public class CommonTermDataTests :SealedTests<CommonTermData, BaseData> {
        protected override CommonTermData createObject() => GetRandom.ObjectOf<CommonTermData>();
        [TestMethod] public void TermIdTest() => isNullable<string>();
        [TestMethod] public void MasterIdTest() => isNullable<string>();
        [TestMethod] public void PowerTest() => isProperty<double>();
    }
}
