using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Pages.Classifiers;

namespace Abc.Pages.Orders {
    public sealed class SalesChannelsPage :ClassifierPage<SalesChannelsPage> {
        protected override string title => OrderTitles.SalesChannels;
        protected override ClassifierType classifierType => ClassifierType.SalesChannel;
        public SalesChannelsPage(IClassifiersRepo r) : base(r) {  }
        protected internal override string pageUrl => OrderUrls.SalesChannels;
    }
}