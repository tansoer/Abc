using System;
using System.ComponentModel.DataAnnotations;

namespace Abc.Data.Common {
    public abstract class BaseData {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public DateTime? Recorded { get; set; }
        public DateTime? Replaced { get; set; }
        public string RecordedBy { get; set; }
        public string RecordedWhy { get; set; }
        [Timestamp] public byte[] Timestamp { get; set; } = Array.Empty<byte>();
        public override string ToString() => $"Id = {Id} ({base.ToString()})";
    }
}
