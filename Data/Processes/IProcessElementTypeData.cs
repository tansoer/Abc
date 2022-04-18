namespace Abc.Data.Processes {
    public interface IProcessElementTypeData:IProcessElementBaseData {
        public string RuleSetId { get; set; }
        public string BaseTypeId { get; set; }
    }
}
