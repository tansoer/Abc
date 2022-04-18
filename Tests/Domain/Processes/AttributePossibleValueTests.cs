using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Data.Processes;
using Abc.Domain.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Processes {
    [TestClass]
    public class AttributePossibleValueTests :SealedTests<AttributePossibleValue, AttributeValue> {

        protected override AttributePossibleValue createObject() => 
            new(GetRandom.ObjectOf<AttributeValueData>());

        [TestMethod] public void EqualityTest() => isProperty<Relation>();
    }
}