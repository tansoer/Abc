using Abc.Aids.Random;
using Abc.Data.Classifiers;
using Abc.Data.Orders;
using Abc.Domain.Classifiers;
using Abc.Domain.Classifiers.Orders;
using Abc.Domain.Common;
using Abc.Domain.Orders;
using Abc.Domain.Orders.Terms;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Orders.Terms {

    [TestClass]
    public class TermsAndConditionsTests :SealedTests<TermsAndConditions, Entity<TermsAndConditionsData>> {

        protected override TermsAndConditions createObject() => new(GetRandom.ObjectOf<TermsAndConditionsData>());
        [TestMethod] public void TypeIdTest() => isReadOnly(obj.Data.TypeId, true);
        [TestMethod] public void OrderIdTest() => isReadOnly(obj.Data.OrderId, true);

        [TestMethod] public async Task OrderTest()
            => await testItemAsync<OrderData, IOrder, IOrdersRepo>(
                obj.Data?.OrderId, () => obj.Order.Data, OrderFactory.Create);

        [TestMethod] public async Task TypeTest() {
            var d = random<ClassifierData>();
            d.ClassifierType = ClassifierType.TermsAndConditions;
            d.Id = obj.typeId;
            await testItemAsync<ClassifierData, IClassifier, IClassifiersRepo>(
                d, () => obj.Type.Data, ClassifierFactory.Create
            );
        }
        [TestMethod] public async Task IsTypeOfTest() {
            var d = random<ClassifierData>();
            d.ClassifierType = ClassifierType.TermsAndConditions;
            var t = ClassifierFactory.Create(d) as TermsAndConditionsType;
            isFalse(t.IsUnspecified);
            isFalse(new TermsAndConditions().IsTypeOf(null));
            isFalse(new TermsAndConditions().IsTypeOf(t));
            isFalse(obj.IsTypeOf(null));
            isFalse(obj.IsTypeOf(t));
            await TypeTest();
            isTrue(obj.IsTypeOf(obj.Type));
        }
        [TestMethod]
        public void IsCorrectTest() {
            isFalse(new TermsAndConditions().IsCorrect());
            isTrue(new TermsAndConditions(random<TermsAndConditionsData>()).IsCorrect());
        }

    }
}