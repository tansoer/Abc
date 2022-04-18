using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Facade.Common;


namespace Abc.Facade.Classifiers {
    public sealed class ClassifierViewFactory :AbstractViewFactory<ClassifierData, IClassifier, ClassifierView> {
        protected internal override IClassifier toObject(ClassifierData d)
            => ClassifierFactory.Create(d);
    }
}
