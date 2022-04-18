using Abc.Aids.Random;
using Abc.Facade.Common;
using Abc.Facade.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Rules {

    [TestClass]
    public class RuleSetViewTests : SealedTests<RuleSetView, EntityBaseView> {

        protected override RuleSetView createObject() => GetRandom.ObjectOf<RuleSetView>();

    }

}