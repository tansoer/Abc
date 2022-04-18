using Abc.Aids.Random;
using Abc.Data.Processes;
using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Processes;
using Abc.Domain.Rules;
using Abc.Infra.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Task = System.Threading.Tasks.Task;

namespace Abc.Tests.Domain.Processes {
    [TestClass] public class ProcessElementTests
        : AbstractTests<ProcessElement<OutcomesRepo, Outcome, OutcomeData>,
        ProcessElementBase<OutcomesRepo, Outcome, OutcomeData>> {
        private class testClass :ProcessElement<OutcomesRepo, Outcome, OutcomeData> {
            public testClass(OutcomeData d) : base(d) { }
        }
        protected override ProcessElement<OutcomesRepo, Outcome, OutcomeData> createObject()
            => new testClass(random<OutcomeData>());
        [TestMethod] public async Task ContextTest()
            => await testItemAsync<RuleContextData, RuleContext, IRuleContextsRepo>(
                obj.contextId, () => obj.Context.Data, d => new RuleContext(d));
        [TestMethod] public void attributeValuesTest() {
            var count = GetRandom.UInt8(10, 30);
            var r = GetRepo.Instance<IAttributeValuesRepo>();

            for (var i = 0; i < count; i++) {
                var d = GetRandom.ObjectOf<AttributeValueData>();
                if (i % 4 == 0) d.ProcessElementId = obj.Id;
                r.Add(AttributeValuesFactory.Create(d));
            }

            var t = isReadOnly() as IReadOnlyList<AttributeValue>;
            Assert.IsNotNull(t);
            Assert.AreEqual(Math.Ceiling(count / 4.0), t.Count);
        }
        [TestMethod] public async Task AttributesTest() =>
            await testListAsync<AttributeValue, AttributeValueData, IAttributeValuesRepo>(
                obj, nameof(obj.Attributes),
                x => {
                    x.ProcessElementId = obj.Id;
                    x.Type = AttributeValueType.AttributeValue;
                },
                AttributeValuesFactory.Create);

    }
}
