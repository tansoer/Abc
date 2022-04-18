using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Infra.Common;
using Abc.Infra.Processes;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Infra.Processes {

    [TestClass] public class AttributeValuesRepoTests 
        :ProcessReposTests<AttributeValuesRepo, AttributeValue, AttributeValueData> {

        protected override Type getBaseClass() => typeof(PagedRepo<AttributeValue, AttributeValueData>);

        protected override AttributeValuesRepo getObject(ProcessDb c) => new(c);

        protected override DbSet<AttributeValueData> getSet(ProcessDb c) => c.AttributeValues;
    }
}