namespace Abc.Data.Processes {
    public abstract class ProcessElementTypeData: ProcessElementBaseData, IProcessElementTypeData { 
        public string RuleSetId { get; set; }
        public string BaseTypeId { get; set; }
    }
}
