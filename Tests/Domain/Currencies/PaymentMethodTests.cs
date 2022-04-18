using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Domain.Common;
using Abc.Domain.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Currencies {

    [TestClass]
    public class PaymentMethodTests : AbstractTests<PaymentMethod, Entity<PaymentMethodData>> {
        private class testClass : PaymentMethod {
            public testClass(PaymentMethodData d = null) : base(d) { }
        }
        protected override PaymentMethod createObject() => new testClass(GetRandom.ObjectOf<PaymentMethodData>());
    }
}
