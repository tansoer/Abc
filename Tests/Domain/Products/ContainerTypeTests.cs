using Abc.Domain.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Products {

    [TestClass]
    public class ContainerTypeTests : SealedTests<ContainerType, BaseProductType> {

        [TestMethod] public void ColumnsTest() => isReadOnly(obj.Data.ColumnsCount);

        [TestMethod] public void RowsTest() => isReadOnly(obj.Data.RowsCount);

        [TestMethod] public void LevelsTest() => isReadOnly(obj.Data.LevelsCount);

    }

}