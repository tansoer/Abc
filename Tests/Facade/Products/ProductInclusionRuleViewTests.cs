using Abc.Data.Products;
using Abc.Facade.Common;
using Abc.Facade.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Products {
    [TestClass]
    public class ProductInclusionRuleViewTests :SealedTests<ProductInclusionRuleView, EntityBaseView> {
        [TestMethod] public void InclusionKindTest() => isProperty<ProductInclusionKind>("Inclusion Kind");
        [TestMethod] public void MinimumAmountTest() => isProperty<double>("Minimum amount");
        [TestMethod] public void MaximumAmountTest() => isProperty<double>("Maximum amount");
        [TestMethod] public void UnitIdTest() => isProperty<string>("Unit");
        [TestMethod] public void ProductSetIdTest() => isProperty<string>("Product Set");
        [TestMethod] public void PackageTypeIdTest() => isProperty<string>("Package Type");
        [TestMethod] public void ProductInclusionIdTest() => isProperty<string>("Master Inclusion Rule");
        [TestMethod] public void ToStringTest()
            => areEqual(new ProductInclusionRuleViewFactory().Create(obj).ToString(), obj.ToString());
    }
}
