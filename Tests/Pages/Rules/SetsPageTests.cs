using System;
using Abc.Aids.Random;
using Abc.Aids.Reflection;
using Abc.Data.Rules;
using Abc.Domain.Rules;
using Abc.Facade.Rules;
using Abc.Pages.Common;
using Abc.Pages.Rules;
using Abc.Tests.Pages.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Rules {

    [TestClass]
    public class SetsPageTests : SealedViewPageTests<
        SetsPage,
        IRuleSetsRepo,
        IRuleSet,
        RuleSetView,
        RuleSetData> {

        protected override System.Type getBaseClass() =>
            typeof(ViewPage<SetsPage, IRuleSetsRepo,
                IRuleSet, RuleSetView, RuleSetData>);

        internal class testRepo : mockRepo<IRuleSet, RuleSetData>, IRuleSetsRepo { }

        private testRepo Repo;

        protected override SetsPage createObject() {

        Repo = new testRepo();
            sortOrder = rndStr;
            searchString = rndStr;
            pageIndex = GetRandom.UInt8();
            fixedFilter = rndStr;
            fixedValue = rndStr;
            createSwitch = GetRandom.UInt8(0, 10);
            return new SetsPage(Repo);
        }


        protected override string pageTitle => RuleTitles.RuleSets;

        protected override string pageUrl => RuleUrls.RuleSets;

        protected override RuleSet toObject(RuleSetData d) => new(d);

        [TestMethod]
        public void OnGetDetailsAsyncTest() {
            var id = rndStr;
            var r = obj.OnGetDetailsAsync(id,
                rndStr,
                rndStr,
                GetRandom.UInt8(),
                rndStr,
                rndStr).GetAwaiter().GetResult() as RedirectResult;

            Assert.IsNotNull(r);

            var name = GetMember.Name<RuleUsageData>(x => x.RuleSetId);
            var page = RuleUrls.Usages;
            var url = new Uri($"{page}/Index?handler=Index&fixedFilter={name}&fixedValue={id}", UriKind.Relative);
            Assert.AreEqual(url.ToString(), r.Url);
        }
    }
}
