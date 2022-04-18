using Abc.Data.Common;
using Abc.Data.Orders;
using Abc.Domain.Common;
using Abc.Domain.Orders.Discounts;
using System.Collections.Generic;
using System.Linq;

namespace Abc.Domain.Orders {

    public interface IOrdersManager : IManager {

    }
    public interface IOrdersManagersRepo :IRepo<IOrdersManager> {

    }

    public sealed class OrdersManager : Manager<IOrdersRepo, IOrder>, IOrdersManager {
        internal IReadOnlyList<IDiscountType> mockDiscountTypes { get; set; }
        internal string managerInTaxPolicy = nameOf<SalesTaxPolicyData>(x => x.OrderManagerId);
        internal string managerInDiscountType = nameOf<DiscountTypeData>(x => x.OrderManagerId);
        internal string managerInOrder = nameOf<OrderData>(x => x.OrderManagerId);

        public OrdersManager() : this(null) { }
        public OrdersManager(ManagerData d) : base(d) { }

        public IReadOnlyList<IOrder> Orders => list<IOrdersRepo, IOrder>(managerInOrder, Id);
        public IReadOnlyList<SalesTaxPolicy> TaxPolicies => list<ISalesTaxPoliciesRepo, SalesTaxPolicy>(managerInTaxPolicy, Id);
        public IReadOnlyList<IDiscountType> DiscountTypes => mockDiscountTypes??list<IDiscountTypesRepo, IDiscountType>(managerInDiscountType, Id);

        public IReadOnlyList<Discount> GetDiscounts(IOrder o) {
            var l = new List<Discount>();
            var order = o as SalesOrder;
            if (!isListed(order)) return l.AsReadOnly();
            l.AddRange(from d in DiscountTypes select d.GetDiscount(order.DiscountRuleContext));
            return l.AsReadOnly();
        }
        public override void Add(IOrder o) {
            if (!isOrder(o)) base.Add(newOrder(o));
        }
        public void Add(SalesTaxPolicy p) {
            if (!isSalesTaxPolicy(p)) add<ISalesTaxPoliciesRepo, SalesTaxPolicy>(newTaxPolicy(p));
        }
        public void Add(DiscountType d) {
            if (!isDiscountType(d)) add<IDiscountTypesRepo, IDiscountType>(newDiscountType(d));
        }

        public void Remove(IOrder o) {
            if (isOrder(o)) delete<IOrdersRepo, IOrder>(o);
        }
        public void Remove(SalesTaxPolicy p) {
            if (isSalesTaxPolicy(p)) delete<ISalesTaxPoliciesRepo, SalesTaxPolicy>(p);
        }
        public void Remove(DiscountType d) {
            if (isDiscountType(d)) delete<IDiscountTypesRepo, IDiscountType>(d);
        }

        internal bool isOrder(IOrder o) => isListed(o);
        internal bool isSalesTaxPolicy(SalesTaxPolicy p) => isListed(TaxPolicies, p.Id);
        internal bool isDiscountType(DiscountType d) => isListed(DiscountTypes, d.Id);

        internal IOrder newOrder(IOrder o) {
            var d = o.Data;
            d.OrderManagerId = Id;
            return OrderFactory.Create(d);
        }
        internal SalesTaxPolicy newTaxPolicy(SalesTaxPolicy p) {
            var d = p?.Data?? new SalesTaxPolicyData();
            d.OrderManagerId = Id;
            return new SalesTaxPolicy(d);
        }
        internal DiscountType newDiscountType(DiscountType d) {
            var x = d?.Data?? new DiscountTypeData();
            x.OrderManagerId = Id;
            return new DiscountType(x);
        }

        internal static bool isListed<T>(IReadOnlyList<T> l, string id) where T : IEntity
           => l.FirstOrDefault(x => x.Id == id) is not null;
        internal bool isListed(IOrder o) {
            if (o is null) return false;
            var x = FindById(o.Id);
            return x.OrderManagerId == o.OrderManagerId;
        }
    }
}

