using Abc.Data.Classifiers;
using Abc.Domain.Common;

namespace Abc.Domain.Classifiers {
    public interface IClassifier :IEntity<ClassifierData> {
        bool IsTypeOf(ClassifierType t);
    }
}