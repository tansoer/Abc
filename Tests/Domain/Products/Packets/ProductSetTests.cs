using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Products;
using Abc.Domain.Products.Packets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products.Packets {

    [TestClass]
    public class ProductSetTests : SealedTests<ProductSet, Entity<ProductSetData>> {

        [TestMethod] public void PackageTypeIdTest() => isReadOnly();

        [TestMethod]
        public async Task RelatedTypesTest() =>
            await testListAsync<ProductSetContent, ProductSetContentData, IProductSetContentsRepo>(
                x => x.ProductSetId = obj.Id,
                d => new ProductSetContent(d));


        [TestMethod]
        public async Task ContentsTest() {
            isReadOnly();
            Assert.AreEqual(0, obj.Contents.Count);
            await RelatedTypesTest();

            foreach (var e in obj.RelatedTypes) {
                var d = GetRandom.ObjectOf<ProductTypeData>();
                d.Id = e.ProductTypeId;
                await addAsync<IProductTypesRepo, IProductType>(ProductTypeFactory.Create(d));
            }

            Assert.AreNotEqual(0, obj.Contents.Count);
            Assert.AreEqual(obj.RelatedTypes.Count, obj.Contents.Count);

        }

        [TestMethod]
        public async Task GetElementTest() {
            await ContentsTest();
            var i = GetRandom.Int32(0, obj.Contents.Count - 1);
            var expected = obj.Contents[i];
            var actual = obj.GetElement(i);
            allPropertiesAreEqual(expected.Data, actual.Data);
            Assert.IsInstanceOfType(obj.GetElement(GetRandom.Int32(max: 0)), typeof(ProductTypeError));
            Assert.IsInstanceOfType(obj.GetElement(GetRandom.Int32(obj.Contents.Count)), typeof(ProductTypeError));
        }

        [TestMethod]
        public async Task NumberOfElementsTest() {
            await ContentsTest();
            Assert.AreEqual(obj.Contents.Count, obj.NumberOfElements);
        }

        [TestMethod]
        public async Task IsSubsetTest() {
            await ContentsTest();
            isListASubsetTest();
            isSetASubsetTest();
        }
        private void isSetASubsetTest() {
            var s = new ProductSet(GetRandom.ObjectOf<ProductSetData>());
            Assert.IsTrue(obj.IsSubset(new ProductSet()));
            Assert.IsTrue(obj.IsSubset((ProductSet) null));
            Assert.IsTrue(obj.IsSubset(s));

            foreach (var e in obj.Contents) {
                if (rndBool) continue;
                var c = GetRandom.ObjectOf<ProductSetContentData>();
                c.ProductSetId = s.Id;
                c.ProductTypeId = e.Id;
                GetRepo.Instance<IProductSetContentsRepo>().Add(new ProductSetContent(c));
            }

            Assert.IsTrue(obj.IsSubset(s));

            var dc = GetRandom.ObjectOf<ProductSetContentData>();
            var dt = GetRandom.ObjectOf<ProductTypeData>();
            dc.ProductSetId = s.Id;
            dc.ProductTypeId = dt.Id;
            GetRepo.Instance<IProductSetContentsRepo>().Add(new ProductSetContent(dc));
            GetRepo.Instance<IProductTypesRepo>().Add(ProductTypeFactory.Create(dt));
            Assert.IsFalse(obj.IsSubset(s));

        }
        private void isListASubsetTest() {
            var list = obj.Contents.Where(_ => !rndBool).ToList();

            Assert.IsTrue(obj.IsSubset(list));
            Assert.IsTrue(obj.IsSubset(new List<IProductType>()));
            Assert.IsTrue(obj.IsSubset((List<IProductType>) null));
            list.Add(ProductTypeFactory.Create(GetRandom.ObjectOf<ProductTypeData>()));
            Assert.IsFalse(obj.IsSubset(list));
        }

        [TestMethod]
        public async Task ContainsAnyTest() {
            await ContentsTest();
            Assert.IsFalse(obj.ContainsAny(null));

            var list = new List<IProduct>();
            Assert.IsFalse(obj.ContainsAny(list));

            for (var i = 0; i < GetRandom.UInt8(1); i++)
                list.Add(ProductFactory.Create(GetRandom.ObjectOf<ProductData>()));
            Assert.IsFalse(obj.ContainsAny(list));

            var idx = GetRandom.Int32(0, obj.Contents.Count - 1);
            var t = obj.GetElement(idx);
            var p = GetRandom.ObjectOf<ProductData>();
            p.ProductTypeId = t.Id;
            list.Add(ProductFactory.Create(p));
            Assert.IsTrue(obj.ContainsAny(list));
        }

        [TestMethod]
        public void ToStringTest() {
            testWithNoContents();
            testWithContents();
        }
        private void testWithNoContents() {
            areEqual(0, obj.Contents.Count);
            areEqual("{}", obj.ToString());
        }
        private void testWithContents() {
            var setContentRepo = GetRepo.Instance<IProductSetContentsRepo>();
            var typesRepo = GetRepo.Instance<IProductTypesRepo>();
            var setData = GetRandom.ObjectOf<ProductSetData>();

            var productTypeData1 = GetRandom.ObjectOf<ProductTypeData>();
            productTypeData1.Name = rndStr;
            var productTypeData2 = GetRandom.ObjectOf<ProductTypeData>();
            productTypeData2.Name = rndStr;


            var setContentData1 = GetRandom.ObjectOf<ProductSetContentData>();
            setContentData1.ProductSetId = setData.Id;
            setContentData1.ProductTypeId = productTypeData1.Id;
            var setContentData2 = GetRandom.ObjectOf<ProductSetContentData>();
            setContentData2.ProductSetId = setData.Id;
            setContentData2.ProductTypeId = productTypeData2.Id;


            setContentRepo.Add(new ProductSetContent(setContentData1));
            setContentRepo.Add(new ProductSetContent(setContentData2));
            typesRepo.Add(ProductTypeFactory.Create(productTypeData1));
            typesRepo.Add(ProductTypeFactory.Create(productTypeData2));
            obj = new(setData);

            string expected = "{" + $"{productTypeData1.Name}, {productTypeData2.Name}" + "}";
            string actual = obj.ToString();

            areEqual(expected, actual);
        }
    }

}