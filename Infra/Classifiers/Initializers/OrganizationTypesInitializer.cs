using System.Collections.Generic;
using Abc.Data.Classifiers;
using Abc.Infra.Classifiers;
using Abc.Infra.Classifiers.Initializers;

namespace Abc.Infra.Parties.Initializers {

    public static class OrganizationTypesInitializer {

        internal static string orgUnitId => "Organization Unit";
        internal static string companyId => "Company";
        internal static string orgUnitDef =>
            "An Organization Unit is used to describe the structure of the organization";
        internal static string teamDef => "A Department is commonly divided into one or more Teams";
        internal static string departmentDef => "A Division is commonly divided into one or more Departments";
        internal static string divisionDef => "A Company is commonly divided into one or more Divisions";
        internal static string generalCorpDef => "A General Corporation (also known as a C corporation) is a Company " +
                                                 "that is allowed to issue more than one class of stock and that can " +
                                                 "have unlimited stockholders";
        internal static string closeCorpDef => "A Close Corporation has a maximum of around 30 to 50 stockholders " +
                                               "(depending on the state); " +
                                               "new shares must usually first be offered to existing stockholders";
        internal static string subChaptersCorpDef => "An S corporation is a Company with a special tax status such " +
                                                     "that it does not pay federal corporate income tax but passes profits " +
                                                     "or losses directly to the stockholders, where these are reported " +
                                                     "on their personal tax returns (ensures that tax is paid only once)";
        internal static string ltdCorpDef => "This is a less restricted form of Company than a corporation. " +
                                             "It has both the limited liability protection of a company and " +
                                             "taxation similar to a sole proprietorship or partnership";
        internal static ClassifierData orgUnit => new(ClassifierType.Organization, null, orgUnitDef, orgUnitId, orgUnitId);
        internal static ClassifierData company => new(ClassifierType.Organization, null, null, companyId, companyId);

        private static List<ClassifierData> organizationTypes => new() {
            company,
            orgUnit,
            new ClassifierData(ClassifierType.Organization, company.Id, generalCorpDef, "General Corporation", code:"C Corp"),
            new ClassifierData(ClassifierType.Organization, company.Id, closeCorpDef, "Close Corporation"),
            new ClassifierData(ClassifierType.Organization, company.Id, subChaptersCorpDef, "Sub Chapters Corporation", code:"S Corp"),
            new ClassifierData(ClassifierType.Organization, company.Id, ltdCorpDef, "Limited Liability Corporation", code:"Ltd"),
            new ClassifierData(ClassifierType.Organization, orgUnit.Id, divisionDef, "Division"),
            new ClassifierData(ClassifierType.Organization, orgUnit.Id, departmentDef, "Department"),
            new ClassifierData(ClassifierType.Organization, orgUnit.Id, teamDef, "Team")
        };

        public static void Initialize(ClassifierDb c) 
            => ClassifierInitializer.Initialize(c, organizationTypes);
    }
}
