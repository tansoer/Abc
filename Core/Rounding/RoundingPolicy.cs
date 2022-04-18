using System;
using System.Globalization;
using Abc.Aids.Values;

namespace Abc.Core.Rounding {

    public sealed class RoundingPolicy : IRoundingPolicy {

        private decimal floor(decimal d) => Math.Floor(d);

        private decimal ceiling(decimal d) => Math.Ceiling(d);

        private bool isPositive(decimal d) => d >= 0;

        internal static bool isPositive(double d) => d >= 0;

        internal static double floor(double d) => Math.Floor(d);

        internal static double ceiling(double d) => Math.Ceiling(d);

        internal static double coeficient(sbyte percision) => Math.Pow(10.0, percision);

        private byte digit(decimal d, in sbyte percision) {
            var coef = ConvertTo.Decimal(coeficient(percision));
            var s = (d * coef).ToString(CultureInfo.InvariantCulture);
            var found = false;
            byte r = 0;

            foreach (var v in s) {
                if (found) return (byte) (v - '0');
                if (v == '.') found = true;
                else r = (byte) (v - '0');
            }

            return r;
        }

        internal static byte digit(double d, sbyte percision) {
            var coef = coeficient(percision);
            var s = (d * coef).ToString(CultureInfo.InvariantCulture);
            var found = false;
            byte r = 0;

            foreach (var v in s) {
                if (found) return (byte) (v - '0');
                if (v == '.') found = true;
                else r = (byte) (v - '0');
            }

            return r;
        }

        internal static bool isRoundByStep(RoundingStrategy roundingStrategy) {
            return roundingStrategy == RoundingStrategy.RoundDownByStep ||
                   roundingStrategy == RoundingStrategy.RoundUpByStep;
        }

        public RoundingPolicy() {
            RoundingStrategy = RoundingStrategy.Round;
            NumberOfDigits = 2;
            RoundingDigit = 5;
        }

        public RoundingPolicy(RoundingStrategy roundingStrategy, double roundingStep) : this() {
            if (!isRoundByStep(roundingStrategy)) return;
            RoundingStrategy = roundingStrategy;
            RoundingStep = roundingStep;
        }

        public RoundingPolicy(RoundingStrategy roundingStrategy, sbyte numberOfDigits) : this() {
            if (isRoundByStep(roundingStrategy)) return;
            RoundingStrategy = roundingStrategy;
            NumberOfDigits = numberOfDigits;
        }

        public RoundingPolicy(RoundingStrategy roundingStrategy, sbyte numberOfDigits, byte roundingDigit) : this() {
            if (roundingStrategy != RoundingStrategy.Round) return;
            RoundingStrategy = roundingStrategy;
            NumberOfDigits = numberOfDigits;
            RoundingDigit = roundingDigit;
        }

        public RoundingStrategy RoundingStrategy { get; }
        public sbyte NumberOfDigits { get; }
        public double RoundingStep { get; }
        public byte RoundingDigit { get; }

        public double Round(double amount) {
            return RoundingStrategy switch
            {
                RoundingStrategy.RoundUp => roundUp(amount),
                RoundingStrategy.RoundDown => roundDown(amount),
                RoundingStrategy.RoundTowardsNegative => roundTowardsNegative(amount),
                RoundingStrategy.RoundTowardsPositive => roundTowardsPositive(amount),
                RoundingStrategy.RoundUpByStep => roundUpByStep(amount),
                RoundingStrategy.RoundDownByStep => roundDownByStep(amount),
                _ => round(amount)
            };
        }

        public decimal Round(decimal amount) {
            return RoundingStrategy switch
            {
                RoundingStrategy.RoundUp => roundUp(amount),
                RoundingStrategy.RoundDown => roundDown(amount),
                RoundingStrategy.RoundTowardsNegative => roundTowardsNegative(amount),
                RoundingStrategy.RoundTowardsPositive => roundTowardsPositive(amount),
                RoundingStrategy.RoundUpByStep => roundUpByStep(amount),
                RoundingStrategy.RoundDownByStep => roundDownByStep(amount),
                _ => round(amount)
            };
        }

        private decimal round(decimal d) {
            d = Math.Round(d, NumberOfDigits + 2);
            var rd = digit(d, NumberOfDigits);

            return rd < RoundingDigit ? roundDown(d) : roundUp(d);
        }

        private decimal roundDownByStep(decimal d) {
            d /= ConvertTo.Decimal(RoundingStep);
            d = isPositive(d) ? floor(d) : ceiling(d);

            return ConvertTo.Decimal(RoundingStep) * d;
        }

        private decimal roundUpByStep(decimal d) {
            d /= ConvertTo.Decimal(RoundingStep);
            d = isPositive(d) ? ceiling(d) : floor(d);

            return ConvertTo.Decimal(RoundingStep) * d;
        }
        private decimal roundTowardsPositive(decimal d) {
            d = Math.Round(d, NumberOfDigits + 2);

            return isPositive(d) ? roundUp(d) : roundDown(d);
        }
        private decimal roundTowardsNegative(decimal d) {
            d = Math.Round(d, NumberOfDigits + 2);

            return isPositive(d) ? roundDown(d) : roundUp(d);
        }
        private decimal roundDown(decimal d) {
            d = Math.Round(d, NumberOfDigits + 2);
            var coef = ConvertTo.Decimal(coeficient(NumberOfDigits));
            d *= coef;
            d = isPositive(d) ? floor(d) : ceiling(d);

            return d / coef;
        }
        private decimal roundUp(decimal d) {
            d = Math.Round(d, NumberOfDigits + 2);
            var coef = ConvertTo.Decimal(coeficient(NumberOfDigits));
            d *= coef;
            d = isPositive(d) ? ceiling(d) : floor(d);

            return d / coef;
        }

        internal double roundUp(double d) {
            d = Math.Round(d, NumberOfDigits + 2);
            var coef = coeficient(NumberOfDigits);
            d *= coef;
            d = isPositive(d) ? ceiling(d) : floor(d);

            return d / coef;
        }

        internal double roundDown(double d) {
            d = Math.Round(d, NumberOfDigits + 2);
            var coef = coeficient(NumberOfDigits);
            d *= coef;
            d = isPositive(d) ? floor(d) : ceiling(d);

            return d / coef;
        }

        internal double round(double d) {
            d = Math.Round(d, NumberOfDigits + 2);
            var rd = digit(d, NumberOfDigits);

            return rd < RoundingDigit ? roundDown(d) : roundUp(d);
        }

        internal double roundUpByStep(double d) {
            d /= RoundingStep;
            d = isPositive(d) ? ceiling(d) : floor(d);

            return RoundingStep * d;
        }

        internal double roundDownByStep(double d) {
            d /= RoundingStep;
            d = isPositive(d) ? floor(d) : ceiling(d);

            return RoundingStep * d;
        }

        internal double roundTowardsPositive(double d) {
            d = Math.Round(d, NumberOfDigits + 2);

            return isPositive(d) ? roundUp(d) : roundDown(d);
        }

        internal double roundTowardsNegative(double d) {
            d = Math.Round(d, NumberOfDigits + 2);

            return isPositive(d) ? roundDown(d) : roundUp(d);
        }

    }

}
