using Abc.Data.Common;
using Abc.Data.Currencies;
using Abc.Data.Orders;
using Microsoft.EntityFrameworkCore;

namespace Abc.Infra.Orders {
    public class OrderDb :BaseDb<OrderDb> {

        public OrderDb(DbContextOptions<OrderDb> o) : base(o) { }
        public DbSet<InvoiceData> Invoices { get; set; }
        public DbSet<ProductDeliveryData> ProductDeliveries { get; set; }
        public DbSet<DeliveryTypeData> DeliveryTypes { get; set; }
        public DbSet<DiscountData> Discounts { get; set; }
        public DbSet<DiscountTypeData> DiscountTypes { get; set; }
        public DbSet<OrderData> Orders { get; set; }
        public DbSet<OrderEventData> OrderEvents { get; set; }
        public DbSet<OrderLineData> OrderLines { get; set; }
        public DbSet<ManagerData> OrderManagers { get; set; }
        public DbSet<PaymentData> OrderPayments { get; set; }
        public DbSet<OrderLineItemData> OrderLineItems { get; set; }
        public DbSet<ReturnedItemData> ReturnedItems { get; set; }
        public DbSet<SalesTaxPolicyData> SalesTaxPolicies { get; set; }
        public DbSet<TermsAndConditionsData> TermsAndConditions { get; set; }
        public DbSet<OrderStatusData> OrderStatuses { get; internal set; }

        protected override void OnModelCreating(ModelBuilder b) {
            base.OnModelCreating(b);
            InitializeTables(b);
        }
        public static void InitializeTables(ModelBuilder b) {
            if (b is null) return;
            var s = "Order";
            toTable<OrderStatusData>(b, nameof(OrderStatuses), s);
            toTable<InvoiceData>(b, nameof(Invoices), s);
            toTable<ProductDeliveryData>(b, nameof(ProductDeliveries), s);
            toTable<DeliveryTypeData>(b, nameof(DeliveryTypes), s);
            toTableWithDecimalCol<DiscountData>(b, nameof(Discounts), s, x => x.Amount);
            toTableWithDecimalCol<DiscountTypeData>(b, nameof(DiscountTypes), s, x => x.Amount);
            toTable<OrderData>(b, nameof(Orders), s);
            toTable<OrderEventData>(b, nameof(OrderEvents), s);
            toTableWithDecimalCol<OrderLineData>(b, nameof(OrderLines), s, x => x.Amount);
            toTable<ManagerData>(b, nameof(OrderManagers), s);
            toTableWithDecimalCol<PaymentData>(b, nameof(OrderPayments), s, x => x.Amount);
            toTable<OrderLineItemData>(b, nameof(OrderLineItems), s);
            toTable<ReturnedItemData>(b, nameof(ReturnedItems), s);
            toTableWithDecimalCol<SalesTaxPolicyData>(b, nameof(SalesTaxPolicies), s, x => x.TaxationRate);
            toTable<TermsAndConditionsData>(b, nameof(TermsAndConditions), s);
        }
    }
}