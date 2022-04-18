using Abc.Data.Orders;
using Abc.Domain.Orders.Terms;
using Abc.Facade.Orders;
using Abc.Tests.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Orders {
    [TestClass]
    public class TermsAndConditionsViewFactoryTests :ViewFactoryTests<TermsAndConditionsViewFactory,
        TermsAndConditionsData, TermsAndConditions, TermsAndConditionsView> {
    }
}
