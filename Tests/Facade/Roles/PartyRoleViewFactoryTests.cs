﻿using Abc.Data.Roles;
using Abc.Domain.Roles;
using Abc.Facade.Roles;
using Abc.Tests.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Roles {
    [TestClass] public class PartyRoleViewFactoryTests :ViewFactoryTests<PartyRoleViewFactory, PartyRoleData,
        PartyRole, PartyRoleView> { }
}
