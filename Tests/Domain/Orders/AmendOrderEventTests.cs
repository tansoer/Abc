using Abc.Aids.Random;
using Abc.Data.Orders;
using Abc.Data.Parties;
using Abc.Domain.Parties.Signatures;
using Abc.Facade.Orders;
using System;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Orders {
    public abstract class AmendOrderEventTests<TClass, TBaseClass> :SealedTests<TClass, TBaseClass>
        where TClass : class {
        protected static async Task setIsSigned(OrderEventData d, bool isSigned) {
            if (!isSigned) return;
            var sd = random<PartySignatureData>();
            sd.Id = d.AuthorizedPartySignatureId;
            sd.ValidFrom = GetRandom.DateTime(DateTime.Now.AddYears(-10), DateTime.Now);
            sd.ValidTo = null;
            await addAsync<IPartySignaturesRepo, PartySignature>(new PartySignature(sd));
        }
        protected static void setIsNotProcessed(OrderEventData d, bool isNotProcessed) => d.IsProcessed = !isNotProcessed;
    }
}