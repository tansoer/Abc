using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Facade.Common;
using Abc.Facade.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Products {
    [TestClass] public class ContainerItemViewFactoryTests :SealedTests<ContainerItemViewFactory, 
        AbstractViewFactory<ContainerItemData, ContainerItem, ContainerItemView>>{
        [TestMethod] public void CreateTest() { }

        [TestMethod]
        public void CreateObjectTest() {
            var view = GetRandom.ObjectOf<ContainerItemView>();
            var data = new ContainerItemViewFactory().Create(view).Data;

            allPropertiesAreEqual(view, data);
        }

        [TestMethod]
        public void CreateViewTest() {
            var data = GetRandom.ObjectOf<ContainerItemData>();
            var view = new ContainerItemViewFactory().Create(new ContainerItem(data));

            allPropertiesAreEqual(view, data);
        }
    }
}
