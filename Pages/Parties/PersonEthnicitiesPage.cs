using Abc.Aids.Enums;
using Abc.Data.Classifiers;
using Abc.Data.Parties;
using Abc.Domain.Classifiers;
using Abc.Domain.Parties;
using Abc.Domain.Parties.Attributes;
using Abc.Facade.Parties;
using Abc.Pages.Common;
using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Abc.Pages.Parties {
    public sealed class PersonEthnicitiesPage :ViewFactoryPage<PersonEthnicitiesPage, IPersonEthnicitiesRepo,
        PersonEthnicity, PersonEthnicityView, PersonEthnicityData, PersonEthnicityViewFactory> {
        protected override string title => PartyTitles.PersonEthnicities;
        protected internal override string subtitle => EthnicityName(FixedValue);
        public PersonEthnicitiesPage(IPersonEthnicitiesRepo r, IPartiesRepo p, IClassifiersRepo c) :
            base(r) {
            Ethnicities = selectListByName<IClassifiersRepo, IClassifier, ClassifierData>(x => x.Data.ClassifierType == ClassifierType.PartyEthnicity);
            Persons = selectListById<IPartiesRepo, IParty, PartyData>(x => x.Data.PartyType == PartyType.Person, x => (x as Person)?.GetName()?.ToString()?? x.Id);
        }
        public IEnumerable<SelectListItem> Ethnicities { get; }
        public IEnumerable<SelectListItem> Persons { get; }
        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.Id),
            field(x => Item.PartyId),
            field(x => Item.Name)
        };
        protected override void addFields() {
            addField(x => Item.PartyId, () => Persons, () => PersonName(Item.PartyId));
            addField(x => Item.EthnicityId, () => Ethnicities, () => EthnicityName(Item.EthnicityId));
            addField(x => Item.ValidFrom);
            addField(x => Item.ValidTo);
        }
        protected override List<ExpressionHelper> getFieldsForViewers() =>
            FixedFilter == null ? Helpers : Helpers.GetRange(1, 3);
        protected override List<ExpressionHelper> getFieldsForEditors() 
            => getFieldsForViewers();
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) 
            => Item = new PersonEthnicityView { Id = Guid.NewGuid().ToString(), Name = "N/A" };
        protected internal override string pageUrl => PartyUrls.PersonEthnicities;
        public string EthnicityName(string id) => itemName(Ethnicities, id);
        public string PersonName(string id) => itemName(Persons, id);
    }
}


