using Abc.Data.Common;

namespace Abc.Data.Processes {

    public sealed class ProcessData : EntityBaseData { 
        //TODO Gunnar : think about medical care - meybe process needs some relations to
        //patient and or in geeral case to some othed items.
        //Maybe something like in the Signed Entity with specified Type and Id.
        //Or maybe there will be some possibility to identify needed attributes with 
        //possible values like in Product amd Product Type 
        public string ProcessTypeId { get; set; }
        public string ManagerPartyRoleId { get; set; }
        public string InitiatorPartyRoleId { get; set; }
        public string PriorityClassifierId { get; set; }
        public string RuleContextId { get; set; }
    }
}
