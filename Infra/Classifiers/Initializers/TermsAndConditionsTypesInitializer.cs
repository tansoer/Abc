using System.Collections.Generic;
using Abc.Data.Classifiers;
using Abc.Infra.Classifiers;
using Abc.Infra.Classifiers.Initializers;

namespace Abc.Infra.Parties.Initializers {
    public static class TermsAndConditionsTypesInitializer {

        internal static string businessTermsId => "Business terms & conditions";
        internal static string medicalTermsId => "Medical service terms & conditions";
        internal static string medicalTermsDef =>
            "Medical service agreements are common in health care industry. It can be an agreement between patients " +
            "and physician or physician and hospital or physician and Government. The agreement emphasises that they " +
            "are agreeing to give services mentioned. Physician and hospital agreement, management services agreement, " +
            "physician recruitment agreement, medical directorship arrangements are a few common types of medical service agreement out there.";
        internal static string businessTermsDef =>
            "Example terms and conditions of business is a generic template of business terms and conditions " +
            "that a business owner can adopt, customize, and use as the terms and conditions of their business. " +
            "Some of the following may not be applicable to your business, but they can be a source of " +
            "ideas as you develop the terms and conditions of your business.";
        internal static string guarranteesDef => "A guarantee or warranty reassures your customers of the quality of your " +
            "services or products. Guarantees and warranties should also be time-specific so that you won't have to " +
            "deal with a refund request five years after rendering a service or delivering a product. Settle for a " +
            "realistic and suitable timeline for your business, such as 30 days or 12 months.";
        internal static string termsOfPaymentDef => "Your terms of payment should include prices, late payment fees or interest, " +
            "returned check surcharges, and so on";
        internal static string servicesAndProductsDef => "All businesses should include in their terms and conditions a " +
            "description of the services and products they offer their customers. The smaller the business, the greater " +
            "the need for a more specific description of its services and products. In addition to providing clarity, specific " +
            "descriptions help to avoid issues arising from customers expecting more than your business offers for a given fees";
        internal static string limitationsDef => "It is not a safe practice to offer guarantees and warranties in a high-risk industry. " +
            "Instead, businesses should disclaim warranties to minimize liability.";
        internal static string refundPolicyDef => "Retail businesses typically have different refund policies. A refund policy is " +
            "usually given its own webpage on e-commerce sites. Service contracts sometimes don't include refund options. " +
            "Wherever the refund policy of your business occurs, make sure it's consistent. Otherwise, " +
            "courts will rule against you in the event of a lawsuit.";
        internal static string timelinesDef => "Delivery timelines depend on the kind of service or products you sell. There are deadlines" +
            " for services and scheduled delivery dates for products. Agree on a deadline or delivery date " +
            "only if you're sure you can hold up your end of the bargain.";
        internal static string privacyPolicyDef => "If your business involves personally identifiable information, it needs a privacy " +
            "policy as much as it needs terms and conditions. It's also good practice to make reference to confidentiality " +
            "or privacy in the terms and conditions. However, nothing can take the place of a full-blown privacy policy " +
            "in a business that involves sensitive information.";

        internal static string solutionsForABreachDef => "A breach of agreement can come in various forms including but " +
            "not limited to the following: (a) Poor-quality products and services; (b)Failure to pay for satisfactory goods or services; " +
            "(c) Making a refund request after the expiry of valid timelines; (d) Delivery of damaged goods. Refund procedures are used " +
            "to solve the problem of rendering substandard services or delivering poor-quality products.Another " +
            "remedy is to indicate preferred methods of dispute resolution.It's best for small businesses to avoid " +
            "expensive lawsuits and use optional dispute resolution methods. For instance, the terms and conditions " +
            "of some businesses specify that all disputes are to be resolved by arbitration and that customers are required " +
            "to waive their rights to a court trial. Sometimes even the venue and applicable rules of the arbitration process " +
            "are specified.Specifying arbitration or mediation as a means of dispute resolution can save your business unnecessary legal fees.";
        internal static string agreementTerminationDef => "Agreement termination clauses define how the business " +
            "relationship that is stated in the terms and conditions should come to a close. " +
            "They typically state that termination takes place by unanimous agreement " +
            "or on the condition that a party notifies other parties of their intent " +
            "to terminate 30 days before the effective date. Some businesses describe their exact agreement " +
            "termination process.Agreement termination clauses prepare business owners for " +
            "contract terminations. That way, businesses aren't taken by surprise and are prepared to replace lost deals with new ones.";
        internal static string governingLawsDef => "Adding governing laws to your company's terms and conditions is a good practice, "+
            "especially if your services or products are not limited by location. For instance, " +
            "if you deal with goods that are shipped across the country or overseas, you should " +
            "indicate that all disputes will be settled close to home by limiting the jurisdictions " +
            "where customer complaints can be made. Typically, a footnote in the section for dispute " +
            "resolution or a miscellaneous section can serve this purpose.It is best to choose the home " +
            "jurisdiction of your business for dispute resolution in order to save costs in case of a dispute." +
            "If you need help with example of terms and conditions of a business, post your legal need " +
            "at UpCounsel's marketplace. UpCounsel accepts only the top 5 percent of lawyers to its site. " +
            "Lawyers on UpCounsel come from law schools such as Harvard Law and Yale Law and average " +
            "14 years of legal experience, including work with or on behalf of companies like Google, " +
            "Menlo Ventures, and Airbnb.";

        internal static ClassifierData businessTerms =>
            new ClassifierData(ClassifierType.TermsAndConditions, null, businessTermsDef, businessTermsId, businessTermsId);
        private static List<ClassifierData> termsAndConditions => new() {
            businessTerms,
            new ClassifierData(ClassifierType.TermsAndConditions, businessTerms.Id, servicesAndProductsDef, "Services and products"),
            new ClassifierData(ClassifierType.TermsAndConditions, businessTerms.Id, termsOfPaymentDef, "Terms of payment"),
            new ClassifierData(ClassifierType.TermsAndConditions, businessTerms.Id, guarranteesDef, "Guarrantees"),
            new ClassifierData(ClassifierType.TermsAndConditions, businessTerms.Id, limitationsDef, "Limitations due to liability"),
            new ClassifierData(ClassifierType.TermsAndConditions, businessTerms.Id, refundPolicyDef, "Refund policy"),
            new ClassifierData(ClassifierType.TermsAndConditions, businessTerms.Id, timelinesDef, "Timelines for delivery"),
            new ClassifierData(ClassifierType.TermsAndConditions, businessTerms.Id, privacyPolicyDef, "Privacy policy"),
            new ClassifierData(ClassifierType.TermsAndConditions, businessTerms.Id, solutionsForABreachDef, "Solutions for a breach"),
            new ClassifierData(ClassifierType.TermsAndConditions, businessTerms.Id, agreementTerminationDef, "Agreement termination"),
            new ClassifierData(ClassifierType.TermsAndConditions, businessTerms.Id, governingLawsDef, "Governing laws"),
            new ClassifierData(ClassifierType.TermsAndConditions, null, medicalTermsDef, medicalTermsId, medicalTermsId)
        };

        public static void Initialize(ClassifierDb c)
            => ClassifierInitializer.Initialize(c, termsAndConditions);
    }
}
