using Abc.Aids.Methods;
using Abc.Data.Parties;
using Abc.Domain.Parties;
using Abc.Facade.Common;

namespace Abc.Facade.Parties {
    public sealed class PartyViewFactory :AbstractViewFactory<PartyData, IParty, PartyView> {
        protected internal override IParty toObject(PartyData d) => PartyFactory.Create(d);
        public override PartyView Create(IParty o) {
            var v = new PartyView();
            if (!(o is null)) Copy.Members(o.Data, v);
            v.Name = o.GetName();
            return v;
        }
    }
}
