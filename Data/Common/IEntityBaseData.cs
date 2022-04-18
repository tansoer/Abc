using System;

namespace Abc.Data.Common {
    public interface IEntityBaseData {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Details { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
    }
}
