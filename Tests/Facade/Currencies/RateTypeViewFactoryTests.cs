using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Facade.Currencies;
using Abc.Tests.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Currencies {

    [TestClass]
    public class RateTypeViewFactoryTests : 
        ViewFactoryTests<RateTypeViewFactory, RateTypeData, RateType, RateTypeView>{ }
}
