using Abc.Data.Rules;
using Abc.Domain.Rules;
using Abc.Facade.Rules;
using Abc.Tests.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Rules {

    [TestClass]
    public class RuleUsageViewFactoryTests :
        ViewFactoryTests<RuleUsageViewFactory, RuleUsageData, RuleUsage, RuleUsageView> { }

}