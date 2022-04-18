using Abc.Data.Orders;
using Abc.Domain.Parties.Signatures;
using System;

namespace Abc.Domain.Orders.Terms {
    public sealed class AmendTermsAndConditionsEvent :AmendEvent {
        internal TermsAndConditions termsAndConditions;
        internal TermsAndConditions oldTermsAndConditions;
        public AmendTermsAndConditionsEvent() : this(null) { }
        public AmendTermsAndConditionsEvent(OrderEventData d) : base(d) { }
        public AmendTermsAndConditionsEvent(IOrder o, PartySignature s,
            TermsAndConditions termsAndConditions = null, 
            TermsAndConditions oldTermsAndConditions = null) : base(null) {
            order = validateOrder(o);
            signature = validateSignature(s);
            this.termsAndConditions = validateTerm(termsAndConditions);
            this.oldTermsAndConditions = validateTerm(oldTermsAndConditions);
            updateData(); 
        }
        internal TermsAndConditions validateTerm(TermsAndConditions t) {
            var d = t?.Data?? new TermsAndConditionsData();
            d.OrderId = order?.Id;
            d.ValidFrom = DateTime.Now;
            d.ValidTo = null;
            t = new TermsAndConditions(d);
            return t;
        }
        protected internal override void setData(OrderEventData d) {
            base.setData(d);
            d.OrderEventType = OrderEventType.AmendTermsAndConditionsEvent;
            d.TermsAndConditionsId = termsAndConditions?.Id;
            d.OldTermsAndConditionsId = oldTermsAndConditions?.Id;
        }
        internal string oldTermsAndConditionsId => get(Data?.OldTermsAndConditionsId);
        internal string termsAndConditionsId => get(Data?.TermsAndConditionsId);
        public TermsAndConditions OldTermsAndConditions => oldTermsAndConditions ?? item<ITermsAndConditionsRepo, TermsAndConditions>(oldTermsAndConditionsId);
        public TermsAndConditions TermsAndConditions => termsAndConditions ?? item<ITermsAndConditionsRepo, TermsAndConditions>(termsAndConditionsId);
        public override bool IsNewEvent => OldTermsAndConditions.IsUnspecified && !TermsAndConditions.IsUnspecified;
        public override bool IsRemoveEvent => !OldTermsAndConditions.IsUnspecified && TermsAndConditions.IsUnspecified;
    }
}
