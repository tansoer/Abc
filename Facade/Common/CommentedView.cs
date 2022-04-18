using System.ComponentModel;

namespace Abc.Facade.Common {
    public abstract class CommentedView :DetailedView {
        [DisplayName("Comments")] new public string Details { get; set; }
    }
}