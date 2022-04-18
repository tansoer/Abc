using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Currencies {

    [TestClass]
    public class DebitCardTests : SealedTests<DebitCard, PaymentCard> {
        protected override DebitCard createObject() => new(GetRandom.ObjectOf<PaymentMethodData>());

    }
    [TestClass]
    public class LoyalityCardTests :SealedTests<LoyalityCard, PaymentCard> {
        protected override LoyalityCard createObject() => new(GetRandom.ObjectOf<PaymentMethodData>());

    }
}
