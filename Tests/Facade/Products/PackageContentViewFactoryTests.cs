using Abc.Data.Products;
using Abc.Domain.Products.Packets;
using Abc.Facade.Products;
using Abc.Tests.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Products {
    [TestClass] public class PackageContentViewFactoryTests :ViewFactoryTests<PackageContentViewFactory, PackageContentData,
        PackageContent, PackageContentView> {}
}
