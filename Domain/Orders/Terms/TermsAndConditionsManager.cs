using Abc.Data.Orders;
using Abc.Domain.Parties.Signatures;
using System.Collections.Generic;
using System.Linq;

namespace Abc.Domain.Orders.Terms {

    public interface ITermsAndConditionsManager:IOrderManager {
        bool Amend(AmendTermsAndConditionsEvent e);
        IReadOnlyList<TermsAndConditions> Terms { get; }
    }
    public sealed class TermsAndConditionsManager :OrderManager, ITermsAndConditionsManager {
        public TermsAndConditionsManager() : this(null) { }
        public TermsAndConditionsManager(IOrder o) : base(o) { }

        internal static string orderId 
            => nameOf<TermsAndConditionsData>(x => x.OrderId);

        public IReadOnlyList<TermsAndConditions> Terms
            => list<ITermsAndConditionsRepo, TermsAndConditions>(orderId, order?.Id);
        public bool Amend(AmendTermsAndConditionsEvent e) {
            if (!(e?.IsCorrect()??false)) return false;
            if (!isOrderCorrect(e)) return false;
            if (!isSignatureCorrect(e)) return false;
            if (!isEventCorrect(e)) return false;
            if (e.IsNewEvent) return addNew(e);
            if (e.IsRemoveEvent) return remove(e);
            return replace(e);
        }

        internal bool replace(AmendTermsAndConditionsEvent e) => isReplaceCorrect(e)
            && delete<ITermsAndConditionsRepo, TermsAndConditions>(e.OldTermsAndConditions)
            && add<ITermsAndConditionsRepo, TermsAndConditions>(e.TermsAndConditions)
            && add<IPartySignaturesRepo, PartySignature>(e.Authorization);

        internal bool remove(AmendTermsAndConditionsEvent e) => isRemoveCorrect(e) 
            && delete<ITermsAndConditionsRepo, TermsAndConditions>(e.OldTermsAndConditions)
            && add<IPartySignaturesRepo, PartySignature>(e.Authorization);

        internal bool addNew(AmendTermsAndConditionsEvent e) =>
            isAddNewCorrect(e)
            && add<ITermsAndConditionsRepo, TermsAndConditions>(e.TermsAndConditions)
            && add<IPartySignaturesRepo, PartySignature>(e.Authorization);

        internal static bool isEventCorrect(AmendTermsAndConditionsEvent e) 
            => (e?.OldTermsAndConditions?.IsCorrect()??false)
            || (e?.TermsAndConditions?.IsCorrect()??false);

        internal bool isReplaceCorrect(AmendTermsAndConditionsEvent e)
            => (e is not null) 
            && exists(e?.OldTermsAndConditions)
            && !exists(e?.TermsAndConditions);
        internal bool isRemoveCorrect(AmendTermsAndConditionsEvent e) 
            => (e is not null) 
            && exists(e?.OldTermsAndConditions);
        internal bool isAddNewCorrect(AmendTermsAndConditionsEvent e) 
            => (e is not null) 
            && !exists(e?.TermsAndConditions);
        internal bool exists(TermsAndConditions t) 
            => Terms.FirstOrDefault(x => x.Id == t?.Id) != null;
    }
}
