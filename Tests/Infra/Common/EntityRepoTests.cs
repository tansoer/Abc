using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Infra.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Common {

    [TestClass] public class EntityRepoTests: 
        AbstractTests<EntityRepo<SystemOfUnits, SystemOfUnitsData>,
        PagedRepo<SystemOfUnits, SystemOfUnitsData>> {
        private class testClass : EntityRepo<SystemOfUnits, SystemOfUnitsData> {
            public testClass(DbContext c, DbSet<SystemOfUnitsData> s) : base(c, s) { }
        }
        protected override EntityRepo<SystemOfUnits, SystemOfUnitsData> createObject() => new testClass(null, null);
    }
}
