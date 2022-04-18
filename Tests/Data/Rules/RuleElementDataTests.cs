using Abc.Aids.Calculator;
using Abc.Aids.Enums;
using Abc.Data.Common;
using Abc.Data.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Rules {

    [TestClass]
    public class RuleElementDataTests : SealedTests<RuleElementData, EntityBaseData> {


        [TestMethod] public void RuleIdTest() => isNullable<string>();

        [TestMethod] public void RuleContextIdTest() => isNullable<string>();

        [TestMethod] public void ActivityIdTest() => isNullable<string>();

        [TestMethod] public void IndexTest() => isProperty<int>();

        [TestMethod] public void UnitOrCurrencyIdTest() => isNullable<string>();

        [TestMethod] public void ValueTest() => isNullable<string>();

        [TestMethod] public void RuleElementTypeTest() => isProperty<RuleElementType>();

        [TestMethod] public void OperationTest() => isProperty<Operation>();

    }

}

