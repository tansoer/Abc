using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Infra.Common;
using Abc.Infra.Processes;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace Abc.Tests.Infra.Processes
{
    [TestClass] public class AttributeTypesRepoTests :ProcessReposTests<AttributeTypesRepo, AttributeType,
        AttributeTypeData> {
        protected override Type getBaseClass() => typeof(EntityRepo<AttributeType,
            AttributeTypeData>);
        protected override AttributeTypesRepo getObject(ProcessDb c) => new(c);
        protected override DbSet<AttributeTypeData> getSet(ProcessDb c) => c.AttributeTypes;
    }
}