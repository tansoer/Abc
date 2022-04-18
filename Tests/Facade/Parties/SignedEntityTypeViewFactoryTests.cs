using Abc.Data.Parties;
using Abc.Domain.Parties.Signatures;
using Abc.Facade.Parties;
using Abc.Tests.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Parties {
    [TestClass]
    public class SignedEntityTypeViewFactoryTests : 
        ViewFactoryTests<SignedEntityTypeViewFactory, SignedEntityTypeData, SignedEntityType, SignedEntityTypeView> { }
}
