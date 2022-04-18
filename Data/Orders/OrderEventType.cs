namespace Abc.Data.Orders {
    public enum OrderEventType {
        Unspecified = 0,
        OpenEvent = 1,
        CloseEvent = 2,
        CancelEvent = 3,
        AmendOrderLineEvent = 4,
        AmendTermsAndConditionsEvent = 5,
        AmendPartySummaryEvent = 6,
        DiscountEvent = 7,
        DispatchEvent = 8,
        ReceiptEvent = 9,
        InvoiceEvent = 10,
        MakePaymentEvent = 11,
        AcceptPaymentEvent = 12,
        MakeRefundEvent = 13,
        AcceptRefundEvent = 14,
    }
}
