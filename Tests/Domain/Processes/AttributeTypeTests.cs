using Abc.Aids.Random;
using Abc.Data.Processes;
using Abc.Domain.Common;
using Abc.Domain.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Task = System.Threading.Tasks.Task;

namespace Abc.Tests.Domain.Processes {
    [TestClass]
    public class AttributeTypeTests :SealedTests<AttributeType, Entity<AttributeTypeData>> {
        protected override AttributeType createObject() => new(GetRandom.ObjectOf<AttributeTypeData>());
        [TestMethod] public void IsMandatoryTest() => isReadOnly(obj.Data.IsMandatory);
        [TestMethod] public void ElementTypeIdTest() => isReadOnly(obj.Data.ElementTypeId);
        [TestMethod] public async Task AttributeValuesTest()
            => await testListAsync<AttributeValue, AttributeValueData, IAttributeValuesRepo>(
                x => x.AttributeTypeId = obj.Id, AttributeValuesFactory.Create);
        [TestMethod] public async Task PossibleValuesTest() {
            await AttributeValuesTest();
            var l = obj
                .AttributeValues.Where(x => x is AttributePossibleValue).ToList();
            areEqual(obj.PossibleValues.Count, l.Count);
        }
    }
}