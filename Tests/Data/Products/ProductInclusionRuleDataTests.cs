using Abc.Data.Common;
using Abc.Data.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Products {

    [TestClass]
    public class ProductInclusionRuleDataTests : SealedTests<ProductInclusionRuleData, EntityBaseData> {

        [TestMethod] public void InclusionKindTest() => isProperty<ProductInclusionKind>();
        [TestMethod] public void MinimumAmountTest() => isProperty<double>();
        [TestMethod] public void MaximumAmountTest() => isProperty<double>();
        [TestMethod] public void UnitIdTest() => isNullable<string>();
        [TestMethod] public void ProductSetIdTest() => isNullable<string>();
        [TestMethod] public void PackageTypeIdTest() => isNullable<string>();
        [TestMethod] public void ProductInclusionIdTest() => isNullable<string>();

    }

}