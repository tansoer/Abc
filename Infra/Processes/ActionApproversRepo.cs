using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Infra.Common;

namespace Abc.Infra.Processes {
    public sealed class ActionApproversRepo 
        :EntityRepo<ActionApprover, ActionApproverData>, IActionApproversRepo {
        public ActionApproversRepo(ProcessDb c = null) : base(c, c?.ActionApprovers) { }
    }
    public sealed class AttributeTypesRepo
        :EntityRepo<AttributeType, AttributeTypeData>, IAttributeTypesRepo {
        public AttributeTypesRepo(ProcessDb c = null) : base(c, c?.AttributeTypes) { }
    }
}