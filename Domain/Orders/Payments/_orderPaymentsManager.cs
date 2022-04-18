using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abc.Domain.Orders.Payments {
    internal class orderPaymentsManager :OrderManager, IInternalOrderPaymentsManager {
    
    }

    internal interface IInternalOrderPaymentsManager {
        //IReadOnlyList<OrderLineReceiver> LineReceivers { get; }
        //PartyInOrder Purchaser { get; }
        //PartyInOrder OrderInitiator { get; }
        //PartyInOrder PaymentReceiver { get; }
        //PartyInOrder DeliveryReceiver { get; }
        //PartyInOrder Vendor { get; }
        //PartyInOrder SalesAgent { get; }
        //PartyInOrder LineReceiver(OrderLine l);
        //bool Add(IPartyInOrder p);
        //bool Add(PartySignature s);
        //bool CanAdd(IPartyInOrder p);
        //bool CanRemove(IPartyInOrder p);
        //bool IsAddNewCorrect(AmendPartySummaryEvent e);
        //bool IsRemoveCorrect(AmendPartySummaryEvent e);
        //bool IsReplaceCorrect(AmendPartySummaryEvent e);
        //bool Remove(IPartyInOrder p);
        //bool IsCorrect(AmendPartySummaryEvent e);
        //bool IsNewEvent(AmendPartySummaryEvent e);
        //bool IsRemoveEvent(AmendPartySummaryEvent e);
        //bool IsOrderCorrect(AmendPartySummaryEvent e);
        //bool IsOrderLineCorrect(AmendPartySummaryEvent e);
        //bool IsSignatureCorrect(AmendPartySummaryEvent e);
        //bool IsEventCorrect(AmendPartySummaryEvent e);
    }
}
