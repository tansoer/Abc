using Abc.Data.Orders;
using Abc.Domain.Classifiers;
using Abc.Domain.Common;
using Abc.Domain.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Abc.Data.Classifiers;
using Abc.Facade.Orders;

namespace Abc.Tests.Domain.Orders {
    [TestClass] public class SalesTaxPolicyTests :SealedTests<SalesTaxPolicy, Entity<SalesTaxPolicyData>> {
        protected override SalesTaxPolicy createObject() 
            => new SalesTaxPolicy(random<SalesTaxPolicyData>());
        [TestMethod] public void TaxationRateTest() => isReadOnly(obj.Data?.TaxationRate);
        [TestMethod] public void TaxationTypeIdTest() => isReadOnly(obj.Data?.TaxationTypeId);
        [TestMethod] public async Task TaxationTypeTest() {
            var d = random<ClassifierData>();
            d.ClassifierType = ClassifierType.TaxationType;
            d.Id = obj.TaxationTypeId;
            await testItemAsync<ClassifierData, IClassifier, IClassifiersRepo>(
                d, () => obj.TaxationType.Data, ClassifierFactory.Create);
        }
    }
}
