using Abc.Data.Common;
using System.ComponentModel;
using Abc.Facade.Common;

namespace Abc.Facade.Products {

    public sealed class FeatureTypeView : EntityTypeView {

        [DisplayName("Is Mandatory")]
        public bool IsMandatory { get; set; }

        [DisplayName("Product Type")]
        public string ProductTypeId { get; set; }

        [DisplayName("Numeric Code")]
        public int NumericCode { get; set; }

        [DisplayName("Must Satisfy All")]
        public bool MustSatisfyAll { get; set; }

        [DisplayName("Value Type")]
        public ValueType ValueType { get; set; }
    }

}