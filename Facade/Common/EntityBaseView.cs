using System.ComponentModel.DataAnnotations;

namespace Abc.Facade.Common {
    public abstract class EntityBaseView: DetailedView  {
        public string Code { get; set; }
        [Required] public string Name { get; set; }
    }
}