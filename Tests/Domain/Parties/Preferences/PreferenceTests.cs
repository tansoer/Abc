using Abc.Aids.Random;
using Abc.Data.Classifiers;
using Abc.Data.Parties;
using Abc.Data.Quantities;
using Abc.Domain.Classifiers;
using Abc.Domain.Parties.Attributes;
using Abc.Domain.Parties.Preferences;
using Abc.Domain.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Parties.Preferences {

    [TestClass] public class PreferenceTests : SealedTests<Preference, BasePartyAttribute<PreferenceData>> {
        protected override Preference createObject() => new (GetRandom.ObjectOf<PreferenceData>());
        [TestMethod] public void typeIdTest() => isReadOnly(obj.Data.PreferenceTypeId);
        [TestMethod] public void optionIdTest() => isReadOnly(obj.Data.PreferenceOptionId);
        [TestMethod] public void unitIdTest() => isReadOnly(obj.Data.UnitId);
        [TestMethod] public void amountTest() => isReadOnly(obj.Data.Weight);
        [TestMethod] public async Task TypeTest() {
            isReadOnly();
            var d = GetRandom.ObjectOf<ClassifierData>();
            d.ClassifierType = ClassifierType.PartyPreference;
            d.Id = obj.typeId;
            await testItemAsync<ClassifierData, IClassifier, IClassifiersRepo>(d, () => obj.Type.Data,
                ClassifierFactory.Create);
        }
        [TestMethod] public async Task OptionTest() =>
            await testItemAsync<PreferenceOptionData, PreferenceOption, IPreferenceOptionsRepo>(obj.optionId, () => obj.Option.Data,
                d => new PreferenceOption(d));        
        [TestMethod] public void WeightTest() {
            Quantity expected = new (obj.amount, obj.unit);
            Assert.AreEqual(expected.Amount, obj.Weight.Amount, 0.001);
            areEqual(expected.Unit.Id, obj.Weight.Unit.Id);
        }
        [TestMethod] public async Task unitTest() => await testItemAsync<UnitData, Unit, IUnitsRepo>(
            obj.unitId, () => obj.unit.Data, UnitFactory.Create);
    }
}
