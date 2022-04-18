using System;
using Abc.Data.Parties;
using Abc.Domain.Parties.Signatures;

namespace Abc.Domain.Parties.Attributes {

    public abstract class BodyMetric<T> : BasePartyAttribute<BodyMetricData>, IBodyMetric
        where T : IComparable {

        protected internal BodyMetric(): this(null) { }
        protected internal BodyMetric(BodyMetricData d) : base(d) { }

        public abstract T Value { get; }
        public BodyMetricType Type => item<IBodyMetricTypesRepo, BodyMetricType>(typeId);
        public PartySignature Signature => item<IPartySignaturesRepo, PartySignature>(signatureId);

        internal string signatureId => get(Data?.SignatureId);
        internal string typeId => get(Data?.TypeId);

    }
}