using Abc.Aids.Random;
using Abc.Data.Processes;
using Abc.Domain.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Processes {
    [TestClass]
    public class AttributeTests :SealedTests<Attribute, AttributeValue> {
        protected override Attribute createObject() => new(GetRandom.ObjectOf<AttributeValueData>());
    }
}