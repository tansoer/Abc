using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Facade.Quantities;
using Abc.Pages.Quantities;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Quantities {

    [TestClass]
    public class SystemsOfUnitsPageTests
        : SealedViewFactoryPageTests<SystemsOfUnitsPage,
            ISystemsOfUnitsRepo,
            SystemOfUnits,
            SystemOfUnitsView,
            SystemOfUnitsData,
            SystemOfUnitsViewFactory> {

        private class testRepo : mockRepo<SystemOfUnits, SystemOfUnitsData>, ISystemsOfUnitsRepo { }

        private testRepo Repo;
        protected override SystemsOfUnitsPage createObject() {
            Repo = new testRepo();
            return new SystemsOfUnitsPage(Repo);
        }
        protected override string pageTitle => QuantityTitles.SystemsOfUnits;
        protected override string pageUrl => QuantityUrls.SystemsOfUnits;
        protected override SystemOfUnits toObject(SystemOfUnitsData d) => new (d);
    }

}
