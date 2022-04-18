using Abc.Domain.Common;

namespace Abc.Domain.Rules {

    public interface IRuleElementsRepo : IRepo<IRuleElement> {

        int GetNextElementIndex(bool isRuleElement, string masterId);

        void CreateContextElements(string id, string ruleId);

    }

}
