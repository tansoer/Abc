﻿using System.Globalization;

namespace Abc.Aids.Regions {

    public class UseCulture {

        public static CultureInfo Current => CultureInfo.CurrentCulture;

        public static CultureInfo English => new CultureInfo("en-GB");

        public static CultureInfo Invariant => CultureInfo.InvariantCulture;

    }

}