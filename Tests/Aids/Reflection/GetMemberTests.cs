using System;
using System.Linq.Expressions;
using Abc.Aids.Reflection;
using Abc.Core.Loinc.Response;
using Abc.Data.Currencies;
using Abc.Domain.Parties.Contacts;
using Abc.Facade.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Reflection {

    [TestClass] public class GetMemberTests : TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(GetMember);
        [TestMethod] public void NameTest() {
            areEqual("Data", GetMember.Name<Country>(o => o.Data));
            areEqual("Name", GetMember.Name<CountryData>(o => o.Name));
            areEqual("NameTest", GetMember.Name<GetMemberTests>(o => o.NameTest()));
            areEqual(string.Empty, GetMember.Name((Expression<Func<CountryData, object>>) null));
            areEqual(string.Empty, GetMember.Name((Expression<Action<CountryData>>) null));
        }
        [TestMethod] public void DisplayNameTest() {
            areEqual("Data", GetMember.DisplayName<Country>(o => o.Data));
            areEqual("Valid from", GetMember.DisplayName<MeasureView>(o => o.ValidFrom));
            areEqual("Name", GetMember.DisplayName<MeasureView>(o => o.Name));
            areEqual("Valid to", GetMember.DisplayName<MeasureView>(o => o.ValidTo));
            areEqual(string.Empty, GetMember.DisplayName((Expression<Func<MeasureView, object>>) null));
            areEqual(string.Empty, GetMember.DisplayName<MeasureView>((string) null));
            //Impossible to use for methods
            //Assert.AreEqual(string.Empty, GetMember.DisplayName<GetMemberTests>(o => o.NameTest()));
        }
        [TestMethod] public void IsRequiredTest() {
            isFalse( GetMember.IsRequired<MeasureView>(x => x.Formula));
            isTrue(GetMember.IsRequired<MeasureView>(x => x.Code));
            isFalse(GetMember.IsRequired((Expression<Func<MeasureView, object>>)null));
            isFalse(GetMember.IsRequired<MeasureView>((string)null));
        }
        [TestMethod] public void ColumnNameTest() {
            areEqual("Formula", GetMember.ColumnName<MeasureView>(x => x.Formula));
            areEqual("LOINC_NUM", GetMember.ColumnName<LoincResponse>(x => x.Id));
            areEqual(string.Empty, GetMember.DisplayName((Expression<Func<MeasureView, object>>)null));
            areEqual(string.Empty, GetMember.DisplayName<MeasureView>((string)null));
        }
    }
}


