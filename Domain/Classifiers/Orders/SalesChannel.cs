using Abc.Data.Classifiers;
namespace Abc.Domain.Classifiers.Orders {
    public sealed class SalesChannel :Classifier<SalesChannel> {
        public SalesChannel() : this(null) { }
        public SalesChannel(ClassifierData d) : base(d) => data.ClassifierType = ClassifierType.SalesChannel;
    }
}
