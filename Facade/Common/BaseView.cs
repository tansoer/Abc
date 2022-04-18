using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Abc.Facade.Common {
    public abstract class BaseView {
        protected BaseView() {
            Id = Guid.NewGuid().ToString();
            Timestamp = Encoding.ASCII.GetBytes(Id[^8..]);
        }
        [Required] public string Id { get; set; }
        [DataType(DataType.Date)] [DisplayName("Valid from")] public DateTime? ValidFrom { get; set; }
        [DataType(DataType.Date)] [DisplayName("Valid to")] public DateTime? ValidTo { get; set; }
        [DataType(DataType.Date)] [DisplayName("Recorded")] public DateTime? Recorded { get; set; }
        [DataType(DataType.Date)] [DisplayName("Replaced")] public DateTime? Replaced { get; set; }
        [DisplayName("Recorded by")] public string RecordedBy { get; set; }
        [DisplayName("Recorded why")] public string RecordedWhy { get; set; }
        [Required] public byte[] Timestamp { get; set; }
    }
}