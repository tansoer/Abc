using Abc.Aids.Random;
using Abc.Facade.Common;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Parties {

    [TestClass] public class BodyMetricTypeViewTests: 
        SealedTests<BodyMetricTypeView, EntityBaseView> {
        protected override BodyMetricTypeView createObject() => GetRandom.ObjectOf<BodyMetricTypeView>();
        [TestMethod] public void RuleSetIdTest() => isNullableProperty<string>("Rule Set");
        
        [TestMethod] public void BaseTypeIdTest() => isNullableProperty<string>("Base Type");
    }
}
