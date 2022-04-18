using Abc.Data.Common;
using Abc.Data.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Products {
    [TestClass]
    public class ContainerItemDataTests : SealedTests<ContainerItemData, EntityBaseData> {
        [TestMethod] public void ContainerIdTest() => isNullable<string>();
        [TestMethod] public void ProductIdTest() => isNullable<string>();
        [TestMethod] public void RowNumberTest() => isProperty<byte>();
        [TestMethod] public void ColumnNumberTest() => isProperty<byte>();
        [TestMethod] public void LevelNumberTest() => isProperty<byte>();
    }
}
