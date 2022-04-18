namespace Abc.Data.Processes {
    public abstract class ProcessElementData: ProcessElementBaseData, IProcessElementData {
        public string RuleContextId { get; set; }
    }
}
