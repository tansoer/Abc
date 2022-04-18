using Abc.Aids.Constants;
using Abc.Aids.Enums;
using Abc.Aids.Extensions;
using Abc.Aids.Random;
using Abc.Data.Parties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Abc.Infra.Parties.Initializers {
    public static class PartiesInitializer {
        private static PartyContactData address;
        private static List<string> persons => new() {
            "Harry Potter", "Hermione Granger", "Ronald Weasley"
        };
        private static List<string> organizations => new() {
            "Hogwarts School of Witchcraft and Wizardry",
            "Ministry of Magic"
        };
        public static void Initialize(PartyDb c) {
            if (c is null) return;
            var set = c.Parties;
            if (set.FirstOrDefault() != null) return;
            addPersons(c);
            addOrganizations(c);
            c.SaveChanges();
        }
        private static void addOrganizations(PartyDb c) {
            foreach (var p in organizations) {
                var d = GetRandom.ObjectOf<PartyData>();
                d.PartyType = PartyType.Organization;
                d.Gender = IsoGender.NotApplicable;
                d.ValidFrom = null;
                c.Add(d);
                addName(c, d, p);
                addContacts(c, d, p);
            }
        }
        private static DateTime? getDoB(string name) =>
            name.StartsWith("Hermione")
                ? new DateTime(1979, 09, 19)
                : name.StartsWith("Roland")
                    ? new DateTime(1980, 3, 1)
                    : new DateTime(1980, 7, 31);
        private static IsoGender getGender(string name)
            => name.StartsWith("Hermione") ? IsoGender.Female : IsoGender.Male;
        private static void addPersons(PartyDb c) {
            foreach (var p in persons) {
                var d = GetRandom.ObjectOf<PartyData>();
                d.PartyType = PartyType.Person;
                d.Gender = getGender(p);
                d.ValidFrom = getDoB(p);
                c.Add(d);
                addName(c, d, p);
                addContacts(c, d, p);
            }
        }
        private static void addContacts(PartyDb c, PartyData party, string name) {
            addEmail(c, party, name);
            addWeb(c, party, name);
            addPhone(c, party);
            addAddress(c, party, name);
        }
        private static void addAddress(PartyDb c, PartyData party, string name) {
            if (address == null) {
                address = new PartyContactData {
                    ContactType = ContactType.Email,
                    Name = $"{name.GetHead(Character.Space)}@Hogwarts.ac.uk"
                };
                addContact(c, party, address);
            } else
                addPartyContact(c, party, address);
        }
        private static void addPartyContact(PartyDb c, PartyData party, PartyContactData contact) {
            var d = new PartyContactUsageData {
                PartyId = party.Id,
                Name = contact.Id,
                Code = getContactType(party, contact)
            };
            c.Add(d);
        }
        private static string getContactType(PartyData party, PartyContactData contact) {
            if (party.PartyType == PartyType.Organization) return "Official";
            if (contact.ContactType == ContactType.Email) return "Private";
            if (contact.ContactType == ContactType.Telecom) return "Home";
            if (contact.ContactType == ContactType.Postal) return "Emergency";
            return "School";
        }
        private static void addContact(PartyDb c, PartyData party, PartyContactData contact) {
            contact.Id = Guid.NewGuid().ToString();
            c.Add(contact);
            addPartyContact(c, party, contact);
        }
        private static void addPhone(PartyDb c, PartyData party) {
            var d = new PartyContactData {
                ContactType = ContactType.Telecom,
                Name = $"{GetRandom.UInt32(1111111, 9999999)}",
                Device = Data.Common.TelecomDeviceType.Mobile,
                RegionOrStateOrCountryCode = "44",
                CityOrAreaCode = "113",
                NationalDirectDialingPrefix = "0",
            };
            addContact(c, party, d);
        }

        private static void addWeb(PartyDb c, PartyData party, string name) {
            var d = new PartyContactData {
                ContactType = ContactType.Web,
                Name = $"www.{name.GetHead(Character.Space)}.Hogwarts.ac.uk"
            };
            addContact(c, party, d);
        }

        private static void addEmail(PartyDb c, PartyData party, string name) {
            var d = new PartyContactData {
                ContactType = ContactType.Email,
                Name = $"{name.GetHead(Character.Space)}@Hogwarts.ac.uk"
            };
            addContact(c, party, d);
        }

        private static void addName(PartyDb c, PartyData party, string name) {
            if (party.PartyType == PartyType.Person) addPersonName(c, party, name);
            else addOrganizationName(c, party, name);
        }

        private static void addOrganizationName(PartyDb c, PartyData party, string name) {
            var d = new PartyNameData {
                Id = Guid.NewGuid().ToString(),
                PartyId = party.Id,
                NameType = NameType.Official,
                PartyType = PartyType.Organization,
                Name = name
            };
            c.Add(d);
        }

        private static void addPersonName(PartyDb c, PartyData party, string name) {
            var d = new PartyNameData {
                Id = Guid.NewGuid().ToString(),
                PartyId = party.Id,
                NameType = NameType.Official,
                PartyType = PartyType.Person,
                GivenName = name.GetHead(Character.Space).Trim(),
                Name = name.GetTail(Character.Space).Trim()
            };
            c.Add(d);
            addNamePrefix(c, party, d);
        }

        private static void addNamePrefix(PartyDb c, PartyData party, PartyNameData name) {
            if (party.Gender == IsoGender.Female) addMiss(c, name);
            else addMister(c, name);
        }

        private static void addMister(PartyDb c, PartyNameData name) {
            var d = new NamePrefixData {
                NameId = name.Id,
                PrefixTypeId = "Mr"
            };
            c.Add(d);
        }

        private static void addMiss(PartyDb c, PartyNameData name) {
            var d = new NamePrefixData {
                NameId = name.Id,
                PrefixTypeId = "Miss"
            };
            c.Add(d);
        }
    }
}
