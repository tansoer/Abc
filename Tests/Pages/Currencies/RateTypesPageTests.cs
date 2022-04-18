using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Facade.Currencies;
using Abc.Pages.Common;
using Abc.Pages.Currencies;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Currencies {
    [TestClass]
    public class RateTypesPageTests :SealedViewPageTests<
        RateTypesPage,
        IRateTypesRepo,
        RateType,
        RateTypeView,
        RateTypeData> {

        protected override System.Type getBaseClass() =>
            typeof(ViewPage<RateTypesPage, IRateTypesRepo,
                RateType, RateTypeView, RateTypeData>);

        private class testRepo
            :mockRepo<RateType, RateTypeData>,
                IRateTypesRepo { }

        private testRepo Repo;

        protected override RateTypesPage createObject() {
            Repo = new testRepo();
            addRandomRateTypes();
            return new RateTypesPage(Repo);
        }
        private void addRandomRateTypes() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++)
                Repo.Add(new RateType(GetRandom.ObjectOf<RateTypeData>()));
        }

        protected override string pageTitle => MoneyTitles.RateTypes;
        protected override string pageUrl => MoneyUrls.RateTypes;

        protected override RateType toObject(RateTypeData d) => new(d);
    }
}
