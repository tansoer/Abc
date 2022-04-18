using Abc.Data.Currencies;
using Abc.Domain.Parties.Contacts;
using Abc.Facade.Currencies;
using Abc.Tests.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Currencies {

    [TestClass]
    public class CountryViewFactoryTests :
        ViewFactoryTests<CountryViewFactory, CountryData, Country, CountryView> { }
}
