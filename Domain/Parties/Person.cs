using System;
using System.Collections.Generic;
using Abc.Aids.Enums;
using Abc.Data.Parties;
using Abc.Domain.Common;
using Abc.Domain.Parties.Attributes;
using Abc.Domain.Parties.Names;

namespace Abc.Domain.Parties {

    public sealed class Person : Party<PersonName> {

        public Person() : this(null) { }

        public Person(PartyData d) : base(d) { }

        public IsoGender Gender => IsUnspecified ? IsoGender.NotKnown : Data.Gender;

        public IReadOnlyList<PersonEthnicity> Ethnicity => new GetFrom<IPersonEthnicitiesRepo, PersonEthnicity>().ListBy(partyId, Id);

        public IReadOnlyList<IBodyMetric> BodyMetrics => new GetFrom<IBodyMetricsRepo, IBodyMetric>().ListBy(partyId, Id);

        public DateTime DateOfBirth => Data?.ValidFrom ?? DateTime.MinValue;


    }

}
