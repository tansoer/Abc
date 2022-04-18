using System;
using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Methods;
using Abc.Data.Rules;
using Abc.Domain.Rules;
using Abc.Infra.Common;

namespace Abc.Infra.Rules {

    public sealed class RuleElementsRepo : PagedRepo<IRuleElement, RuleElementData>,
        IRuleElementsRepo {
        public RuleElementsRepo(RuleDb c = null) : base(c, c?.RuleElements) { }
        protected internal override IRuleElement toDomainObject(RuleElementData d)
            => RuleElementFactory.Create(d);
        internal List<RuleElementData> getElements(bool isRuleElement, string masterId) {
            var r = dbSet.
                Where(x => isRuleElement
                    ? x.RuleId == masterId
                    : x.RuleContextId == masterId).ToList();
            return r;
        }
        public int GetNextElementIndex(bool isRuleElement, string masterId) {
            var r = getElements(isRuleElement, masterId);
            return r.Count + 1;
        }
        public void CreateContextElements(string id, string ruleId) {
            var l = getElements(false, id);
            if (l.Count > 0) return;
            l = getElements(true, ruleId);
            foreach (var e in l) {
                var o = new RuleElementData();
                Copy.Members(e, o);
                o.Id = Guid.NewGuid().ToString();
                o.RuleContextId = id;
                o.RuleId = null;
                dbSet.Add(o);
            }
            db.SaveChanges();
        }
        //TODO: public override string SortOrder => GetMember.Name<RuleElementData>(x => x.Index);
    }
}

