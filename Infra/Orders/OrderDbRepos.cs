using Abc.Domain.Common;
using Abc.Domain.Currencies;
using Abc.Domain.Orders;
using Abc.Domain.Orders.Delivery;
using Abc.Domain.Orders.Discounts;
using Abc.Domain.Orders.Lines;
using Abc.Domain.Orders.Payments;
using Abc.Domain.Orders.Statuses;
using Abc.Domain.Orders.Terms;
using Microsoft.Extensions.DependencyInjection;

namespace Abc.Infra.Orders {
    public static class OrderDbRepos {
        public static void Register(IServiceCollection c) {
            c.AddTransient<IOrdersRepo, OrdersRepo>();
            c.AddTransient<IDiscountsRepo, DiscountsRepo>();
            c.AddTransient<IReturnedItemsRepo, ReturnedItemsRepo>();
            c.AddTransient<IOrderLineItemsRepo, OrderLineItemsRepo>();
            c.AddTransient<ISalesTaxPoliciesRepo, SalesTaxPoliciesRepo>();
            c.AddTransient<IOrderEventsRepo, OrderEventsRepo>();
            c.AddTransient<IOrderLinesRepo, OrderLinesRepo>();
            c.AddTransient<IDiscountTypesRepo, DiscountTypesRepo>();
            c.AddTransient<ITermsAndConditionsRepo, TermsAndConditionsRepo>();
            c.AddTransient<IProductDeliveriesRepo, ProductDeliveriesRepo>();
            c.AddTransient<IDeliveryTypesRepo, DeliveryTypesRepo>();
            c.AddTransient<IManagersRepo, ManagersRepo>();
            c.AddTransient<IInvoicesRepo, InvoicesRepo>();
            c.AddTransient<IPaymentsRepo, PaymentsRepo>();
            c.AddTransient<IOrderStatusesRepo, OrderStatusesRepo>();
        }
    }
}