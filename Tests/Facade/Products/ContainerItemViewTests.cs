using Abc.Facade.Common;
using Abc.Facade.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Products {
    [TestClass] public class ContainerItemViewTests :SealedTests<ContainerItemView, EntityBaseView>{
        [TestMethod] public void ContainerIdTest() => isNullableProperty<string>("Container");
        [TestMethod] public void ProductIdTest() => isNullableProperty<string>("Product");
        [TestMethod] public void RowNumberTest() => isProperty<byte>("Row Number");
        [TestMethod] public void ColumnNumberTest() => isProperty<byte>("Column Number");
        [TestMethod] public void LevelNumberTest() => isProperty<byte>("Level Number");
    }
}
