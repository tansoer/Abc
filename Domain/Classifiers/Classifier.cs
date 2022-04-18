using Abc.Data.Classifiers;
using Abc.Domain.Common;

namespace Abc.Domain.Classifiers {
    public abstract class Classifier<TType> :Entity<ClassifierData>, IClassifier 
        where TType: class, IClassifier, new() { 
        protected Classifier() : this(null) { }
        protected Classifier(ClassifierData d) : base(d) { }
        public TType BaseType => (item<IClassifiersRepo, IClassifier>(BaseTypeId) as TType)?? new TType();
        public bool IsTypeOf(ClassifierType t) => Data?.ClassifierType == t;
        public ClassifierType ClassifierType => Data?.ClassifierType ?? ClassifierType.Unspecified;
        public string BaseTypeId => get(Data?.BaseTypeId);
    }
}
