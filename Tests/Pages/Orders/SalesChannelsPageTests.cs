using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Pages.Orders;
using Abc.Tests.Pages.Classifiers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Orders {
    [TestClass]
    public class SalesChannelsPageTests :ClassifierPageBaseTests<
        SalesChannelsPage> {

        protected override string pageTitle => OrderTitles.SalesChannels;

        protected override string pageUrl => OrderUrls.SalesChannels;

        internal override ClassifierType classifierType => ClassifierType.SalesChannel;

        internal override SalesChannelsPage createObject(IClassifiersRepo r) => new (r);
    }
}
