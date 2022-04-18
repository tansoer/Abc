using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Currencies {

    [TestClass]
    public class CheckTests : SealedTests<Check, BaseBankAccount> {

        protected override Check createObject()
            => new (GetRandom.ObjectOf<PaymentMethodData>());

        [TestMethod] public void CheckNumberTest() => isReadOnly(obj.Data.CardOrCheckNumber);

        [TestMethod] public void PayeeTest() => isReadOnly(obj.Data.Payee);

        [TestMethod] public void DateWrittenTest() => isReadOnly(obj.Data.ValidFrom);

    }

}
