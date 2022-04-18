using System.Collections.Generic;
using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Facade.Currencies;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;

namespace Abc.Pages.Currencies {
    public sealed class RateTypesPage : 
        ViewPage<RateTypesPage, IRateTypesRepo, RateType, RateTypeView, RateTypeData> {
        protected override string title => MoneyTitles.RateTypes;
        public RateTypesPage(IRateTypesRepo r) : base(r) { }
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Timestamp)
        };
        protected override void addFields() {
            addField(x => x.Item.Id);
            addField(x => x.Item.Name);
            addField(x => x.Item.Code);
            addField(x => x.Item.Details);
            addField(x => x.Item.ValidFrom);
            addField(x => x.Item.ValidTo);
        }
        protected internal override string pageUrl => MoneyUrls.RateTypes;
        protected internal override RateType toObject(RateTypeView v) =>
            new RateTypeViewFactory().Create(v);
        protected internal override RateTypeView toView(RateType o) =>
            new RateTypeViewFactory().Create(o);
    }
}
