using Abc.Data.Common;

namespace Abc.Data.Processes {
    public abstract class ProcessElementBaseData : EntityBaseData, IProcessElementBaseData {
        public string NextElementId { get; set; }
        public string PreviousElementId { get; set; }
    }
}
