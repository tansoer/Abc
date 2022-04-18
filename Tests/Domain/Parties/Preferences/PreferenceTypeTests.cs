using System;
using System.Collections.Generic;
using Abc.Aids.Random;
using Abc.Data.Classifiers;
using Abc.Data.Parties;
using Abc.Domain.Classifiers;
using Abc.Domain.Common;
using Abc.Domain.Parties.Preferences;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Parties.Preferences {

    [TestClass]
    public class PreferenceTypeTests 
        : SealedTests<PreferenceType, Classifier<PreferenceType>> {

        protected override PreferenceType createObject()
            => new (GetRandom.ObjectOf<ClassifierData>());

        [TestMethod] public void OptionsTest() {
            var count = GetRandom.UInt8(10, 30);
            var r = GetRepo.Instance<IPreferenceOptionsRepo>();

            for (var i = 0; i < count; i++) {
                var d = GetRandom.ObjectOf<PreferenceOptionData>();
                if (i % 4 == 0) d.PreferenceTypeId = obj.Id;
                r.Add(new PreferenceOption(d));
            }

            var t = isReadOnly() as IReadOnlyList<PreferenceOption>;
            Assert.IsNotNull(t);
            Assert.AreEqual(Math.Ceiling(count / 4.0), t.Count);
        }

    }

}