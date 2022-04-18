using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Infra.Currencies;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Currencies {

    [TestClass]
    public class PaymentMethodsRepoTests 
        : MoneyRepoTests<PaymentMethodsRepo, PaymentMethod, PaymentMethodData> {
        protected override PaymentMethodsRepo getObject(MoneyDb c) => new PaymentMethodsRepo(c);
        protected override DbSet<PaymentMethodData> getSet(MoneyDb c) => c.PaymentMethods;
    }
}