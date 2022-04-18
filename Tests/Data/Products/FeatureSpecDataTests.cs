﻿using Abc.Data.Common;
using Abc.Data.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Products {
    [TestClass] public class FeatureSpecDataTests :SealedTests<FeatureSpecData, EntityBaseData> {
        [TestMethod] public void ValueTest() => isNullable<ValueData>();
    }
}