using Abc.Aids.Methods;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Products;
using Abc.Facade.Common;

namespace Abc.Facade.Products {
    public sealed class BatchCheckedByViewFactory :AbstractViewFactory<BatchCheckedByData, BatchCheckedBy, BatchCheckedByView>{
        protected internal override BatchCheckedBy toObject(BatchCheckedByData d) => new(d);

        public override BatchCheckedByView Create(BatchCheckedBy o) {
            var v = base.Create(o);
            v.CheckedBy = o?.PartySignature?.PartySummary?.Name ?? Unspecified.String;
            v.Address = o?.PartySignature?.PartySummary?.Address ?? Unspecified.String;
            v.EmailAddress = o?.PartySignature?.PartySummary?.EmailAddress ?? Unspecified.String;
            v.PhoneNumber = o?.PartySignature?.PartySummary?.PhoneNumber ?? Unspecified.String;
            return v;
        }    
    }
}
