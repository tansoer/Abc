using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Parties.Signatures;
using Abc.Facade.Parties;
using Abc.Pages.Common;
using Abc.Pages.Parties;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Pages.Parties {
    [TestClass]
    public class SignedEntityTypesPageTests : SealedViewFactoryPageTests<
        SignedEntityTypesPage, 
        ISignedEntityTypesRepo, 
        SignedEntityType, 
        SignedEntityTypeView, 
        SignedEntityTypeData, 
        SignedEntityTypeViewFactory> {

        private class testRepo :mockRepo<SignedEntityType, SignedEntityTypeData>, ISignedEntityTypesRepo { }

        private testRepo Repo;

        protected override SignedEntityTypesPage createObject() {
            Repo = new testRepo();
            addRandomTypes();
            return new SignedEntityTypesPage(Repo);
        }
        private void addRandomTypes() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++)
                Repo.Add(new SignedEntityType(GetRandom.ObjectOf<SignedEntityTypeData>()));
        }
        protected override string pageTitle => PartyTitles.SignedEntityTypes;
        protected override string pageUrl => PartyUrls.SignedEntityTypes;
        protected override SignedEntityType toObject(SignedEntityTypeData d) => new(d);

    }
}
