using Abc.Data.Common;
using Abc.Data.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Products {

    [TestClass]
    public class FeatureTypeDataTests : SealedTests<FeatureTypeData, EntityTypeData> {
        [TestMethod] public void ProductTypeIdTest() => isNullable<string>();
        [TestMethod] public void IsMandatoryTest() => isProperty<bool>();
        [TestMethod] public void NumericCodeTest() => isProperty<int>();
        [TestMethod] public void MustSatisfyAllTest() => isProperty<bool>();
        [TestMethod] public void ValueTypeTest() => isProperty<ValueType>();
    }

}