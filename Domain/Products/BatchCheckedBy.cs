using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Parties.Signatures;

namespace Abc.Domain.Products {

    public sealed class BatchCheckedBy : Entity<BatchCheckedByData> {
        public BatchCheckedBy() : this(null) { }
        public BatchCheckedBy(BatchCheckedByData d) : base(d) { }
        public string PartySignatureId => data?.PartySignatureId ?? Unspecified.String;
        public string BatchId => data?.BatchId ?? Unspecified.String;
        public PartySignature PartySignature
            => new GetFrom<IPartySignaturesRepo, PartySignature>().ById(PartySignatureId);
        public Batch Batch => new GetFrom<IBatchesRepo, Batch>().ById(BatchId);
    }
}