using Abc.Data.Classifiers;

namespace Abc.Domain.Classifiers.Orders {
    public sealed class OrderType :Classifier<OrderType> {
        public OrderType() : this(null) { }
        public OrderType(ClassifierData d) : base(d) => data.ClassifierType = ClassifierType.Order;
    }
}
