using Abc.Data.Common;
using Abc.Facade.Common;
using Abc.Facade.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Products {

    [TestClass]
    public class FeatureTypeViewTests : SealedTests<FeatureTypeView, EntityTypeView> {

        [TestMethod] public void ProductTypeIdTest() => isNullableProperty<string>("Product Type");
        [TestMethod] public void IsMandatoryTest() => isProperty<bool>("Is Mandatory");
        [TestMethod] public void NumericCodeTest() => isProperty<int>("Numeric Code");
        [TestMethod] public void MustSatisfyAllTest() => isProperty<bool>("Must Satisfy All");
        [TestMethod] public void ValueTypeTest() => isProperty<ValueType>("Value Type");

    }

}