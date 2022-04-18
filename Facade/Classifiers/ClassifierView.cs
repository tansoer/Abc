using System.ComponentModel;
using Abc.Data.Classifiers;
using Abc.Facade.Common;

namespace Abc.Facade.Classifiers {
    public sealed class ClassifierView :EntityTypeView {
        [DisplayName("Classifier Type")]
        public ClassifierType ClassifierType { get; set; }
    }
}
