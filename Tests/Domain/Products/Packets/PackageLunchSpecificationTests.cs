using Abc.Domain.Products;
using Abc.Domain.Products.Packets;

namespace Abc.Tests.Domain.Products.Packets {

    public sealed class PackageLunchSpecificationTests : PackageSpecificationTests {

        private ProductType s1;
        private ProductType s2;
        private ProductType s3;
        private ProductType s4;
        private ProductType m1;
        private ProductType d4;
        private ProductType d3;
        private ProductType d2;
        private ProductType d1;
        private ProductType m4;
        private ProductType m3;
        private ProductType m2;
        private ProductSet s;
        private ProductSet m;
        private ProductSet d;

        public void Run(PackageType t) {
            createMenu();
            createSets();
            addProductInclusions(t);
            validatePackages(t);
        }
        private void validatePackages(PackageType t) {
            isCorrectPackage(t, getRandomProductType(s), getRandomProductType(m), getRandomProductType(d));
            isNotCorrectPackage(t);
            isNotCorrectPackage(t, getRandomProductType(s));
            isNotCorrectPackage(t, getRandomProductType(s), getRandomProductType(m));
            isNotCorrectPackage(t, getRandomProductType(s), getRandomProductType(s), getRandomProductType(m));
        }
        private void addProductInclusions(PackageType package) {
            addProductInclusion(package, s, 1, 1);
            addProductInclusion(package, m, 1, 1);
            addProductInclusion(package, d, 1, 1);
        }
        private void createSets() {
            s = crProductSet("Starters", s1, s2, s3, s4);
            m = crProductSet("Main courses", m1, m2, m3, m4);
            d = crProductSet("Desserts", d1, d2, d3, d4);
        }
        private void createMenu() {
            s1 = crProductType("Petite assiette de crudites");
            s2 = crProductType("Potage du chef");
            s3 = crProductType("Calamars marines aux feuilles de citron");
            s4 = crProductType("Salade de chevre chaud");
            m1 = crProductType("Plat du jour");
            m2 = crProductType("Fricassee de supreme de poulet");
            m3 = crProductType("Medaillons de veau aux graines de sesame");
            m4 = crProductType("Cotes dagneau braisees aux amandes");
            d1 = crProductType("Selection de fromages fins");
            d2 = crProductType("Tarte aux fraises");
            d3 = crProductType("Creme brule a la vanille");
            d4 = crProductType("Assortiment du chariot de desserts");
        }
    }

}