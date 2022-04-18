using Abc.Data.Parties;
using Abc.Domain.Parties.Signatures;
using Abc.Facade.Parties;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using System.Collections.Generic;

namespace Abc.Pages.Parties {
    public sealed class SignedEntityTypesPage :ViewFactoryPage<SignedEntityTypesPage, ISignedEntityTypesRepo,
        SignedEntityType, SignedEntityTypeView, SignedEntityTypeData, SignedEntityTypeViewFactory> {
        public SignedEntityTypesPage(ISignedEntityTypesRepo r) : base(r) { }
        protected override string title => PartyTitles.SignedEntityTypes;
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id)
        };
        protected override void addFields() {
            addField(x => x.Item.Name);
            addField(x => x.Item.Code);
            addField(x => x.Item.Details);
            addField(x => x.Item.ValidFrom);
            addField(x => x.Item.ValidTo);
        }
        protected internal override string pageUrl => PartyUrls.SignedEntityTypes;
    }
}
