using Abc.Data.Currencies;
using Abc.Domain.Parties.Contacts;
using Abc.Facade.Attributes;
using Abc.Infra.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;

namespace Abc.Tests.Facade.Attributes {
    [TestClass] public class UniqueAttributeTests :SealedTests<UniqueAttribute, ValidationAttribute> {
        private CountryData d;
        protected override UniqueAttribute createObject() => new (typeof(ICountriesRepo));
        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            d = random<CountryData>();
        }
        [TestMethod] public void CreateTest() {
            isInstanceOfType(obj.repo, typeof(CountriesRepo));
            areEqual(obj.type.Name, typeof(Country).Name);
        }
        [DataRow(null, false)]
        [DataRow(false, false)]
        [DataRow(true, true)]
        [TestMethod] public void isEditingTest(bool? isEditing, bool excpected) {
            obj.id = rndStr;
            d.Id = obj.id;
            obj.currentObj = isEditing is null? null : new Country(d);
            if (isEditing == true) obj.repo.Add(obj.currentObj);
            areEqual(excpected, obj.isEditing());
        }
        [DataRow(null, true)]
        [DataRow(false, true)]
        [DataRow(true, false)]
        [TestMethod] public void isCreatingTest(bool? isEditing, bool excpected) {
            obj.existingObj = (isEditing is null)? null
                : isEditing == false ? new Country() : new Country(d);
            areEqual(excpected, obj.isCreating());
        }
        [TestMethod] public void initValuesTest() {
            object v = rndStr;
            ValidationContext c = new (new Country(d));
            c.MemberName = nameof(d.Code);
            obj.initValues(v, c); 
            areEqual(obj.id, v);
            areEqual(obj.currentObj, c.ObjectInstance);
            areEqual(obj.pi.Name, c.MemberName);
        }
        [DataRow(nameof(CountryData.Name))]
        [DataRow(nameof(CountryData.Code))]
        [DataRow(nameof(CountryData.Details))]
        [DataRow(nameof(CountryData.ValidFrom))]
        [DataRow(nameof(CountryData.ValidTo))]
        [TestMethod] public void getByPropertyTest(string propertName) { 
            obj.pi = typeof(Country).GetProperty(propertName);
            var o = new Country(d);
            obj.id = obj.pi.GetValue(o).ToString();
            obj.repo.Add(o);
            var x = obj.getByProperty();
            allPropertiesAreEqual(o.Data, x.Data);
        }
        [DataRow(null, true)]
        [DataRow(false, true)]
        [DataRow(true, false)]
        [TestMethod] public void validationResultTest(bool? isEditing, bool excpected) {
            isCreatingTest(isEditing, excpected);
            obj.pi = typeof(Country).GetProperty("Name");
            var r = obj.validationResult();
            if (excpected == true) areEqual(ValidationResult.Success, r);
            else areEqual($"The <{obj.pi.Name}> == <{obj.id}> is already registered", r.ErrorMessage);
        }
        [DataRow(null, true)]
        [DataRow(false, true)]
        [DataRow(true, false)]
        [TestMethod] public void isValidTest(bool? isEditing, bool excpected) {
            object v = d.Code;
            var o = new Country(d);
            ValidationContext c = new(o);
            c.MemberName = nameof(d.Code);
            if (isEditing == true) obj.repo.Add(o);
            var r = obj.isValid(v, c);
            if (excpected == true) areEqual(ValidationResult.Success, r);
            else areEqual($"The <{obj.pi.Name}> == <{obj.id}> is already registered", r.ErrorMessage);
        }
    }
}
